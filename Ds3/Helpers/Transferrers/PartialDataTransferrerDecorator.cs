using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ds3.Models;
using Ds3.Helpers.Jobs;
using System.IO;
using Ds3.Runtime;

namespace Ds3.Helpers.Transferrers
{
    internal class PartialDataTransferrerDecorator : ITransferrer
    {

        private readonly ITransferrer _transferrer;
        private readonly int _retries;

        internal PartialDataTransferrerDecorator(ITransferrer transferrer, int retries = 5) {
            this._transferrer = transferrer;
            this._retries = retries;
        }

        public void Transfer(
           IDs3Client client,
           string bucketName,
           string objectName,
           long blobOffset,
           Guid jobId,
           IEnumerable<Range> ranges,
           Stream stream) 
        {

            int currentTry = 0;
            var transferrer = this._transferrer;
            var _ranges = ranges;

            while (true)
            {
                try
                {
                    transferrer.Transfer(client, bucketName, objectName, blobOffset, jobId, _ranges, stream);
                    break;
                }
                catch (Ds3ContentLengthNotMatch exception)
                {

                    if (this._retries != -1 && currentTry >= this._retries)
                    {
                        throw new Ds3NoMoreRetriesException("Exhausted retries for retrieving data when partial data was received.", exception);
                    }

                    // Issue a partial get for the remainder of the request
                    // Seek back one byte to make sure that the connection did not fail part way through a byte
                    stream.Seek(-1, SeekOrigin.Current);

                    _ranges = JobsUtil.RetryRanges(_ranges, exception.BytesRead, exception.ContentLength);
                    transferrer = new PartialReadTransferrer();

                    currentTry++;
                }
            }
        }
    }
}
