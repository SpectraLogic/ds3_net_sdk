/*
 * ******************************************************************************
 *   Copyright 2016 Spectra Logic Corporation. All Rights Reserved.
 *   Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 *   this file except in compliance with the License. A copy of the License is located at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 *   or in the "license" file accompanying this file.
 *   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *   CONDITIONS OF ANY KIND, either express or implied. See the License for the
 *   specific language governing permissions and limitations under the License.
 * ****************************************************************************
 */
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Ds3;
using Ds3.Calls;
using Ds3.Helpers;
using Ds3.Helpers.Streams;

namespace Ds3Examples
{
    class GetAvailableChunksExample
    {
        static void Main(string[] args)
        {
            // Create the IDs3Client instance
            IDs3Client client = Ds3Builder.FromEnv().Build();

            IDs3ClientHelpers helpers = new Ds3ClientHelpers(client);

            string bucket = "bucket-name";
            string directory = "TestData";

            // Create a bucket if it does not already exist.
            helpers.EnsureBucketExists(bucket);

            // Generate the list of files to put for the job
            var objectList = FileHelpers.ListObjectsForDirectory(directory);

            // This is used later to create the stream for the object, this
            // can be replaced with custom code to create the file stream
            var streamBuilder = FileHelpers.BuildFilePutter(directory);

            // Create the bulk put job
            var putBulkResponse = client.PutBulkJobSpectraS3(
                new PutBulkJobSpectraS3Request(bucket, objectList));

            // Get the jobId from the response
            Guid jobId = putBulkResponse.ResponsePayload.JobId;

            // Create a set of chunk ids to know what has been processed and what as yet to be processed
            var chunkIds = from chunk in putBulkResponse.ResponsePayload.Objects
                           select chunk.ChunkId;
            var chunkSet = new HashSet<Guid>(chunkIds);

            // Continue processing until all the chunks have been sent.
            while (chunkSet.Count > 0)
            {
                // Get the set of chunks that are currently available for processing
                var chunkResponse = client.GetJobChunksReadyForClientProcessingSpectraS3(
                    new GetJobChunksReadyForClientProcessingSpectraS3Request(jobId)
                        .WithPreferredNumberOfChunks(10)); // This can be changed to any number
                                                           // but 10 is a good default and you
                                                           // are not guaranteed to get this many         

                chunkResponse.Match((ts, response) =>
                {
                    // If this matcher is called this means that we can safely process chunks without
                    // fear of the PutObject call failing due to cache unavailable conditions

                    // It is also safe to process all the chunks in parallel as well, or to process
                    // each chunk sequentially, while sending each object in parallel

                    foreach (var chunk in response.Objects)
                    {
                        chunkSet.Remove(chunk.ChunkId);

                        // this next step can be done in parallel
                        foreach (var obj in chunk.ObjectsList)
                        {
                            // Create the stream and seek to the correct position for that
                            // blob offset, and then wrap in a ObjectRequestStream to 
                            // limit the amount of data transferred to the length of the 
                            // blob being processed.
                            var stream = streamBuilder.Invoke(obj.Name);
                            stream.Seek(obj.Offset, System.IO.SeekOrigin.Begin);
                            var wrappedStream = new ObjectRequestStream(stream, obj.Length);

                            // Put the blob
                            client.PutObject(
                                new PutObjectRequest(bucket, obj.Name, wrappedStream)
                                    .WithJob(jobId)
                                    .WithOffset(obj.Offset));
                        }
                    }
                },
                Thread.Sleep);
            }
        }
    }
}
