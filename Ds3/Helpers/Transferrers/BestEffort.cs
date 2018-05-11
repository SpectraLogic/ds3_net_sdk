﻿/*
 * ******************************************************************************
 *   Copyright 2014-2016 Spectra Logic Corporation. All Rights Reserved.
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
using System.Diagnostics;
using System.IO;
using Ds3.Helpers.Jobs;
using Ds3.Models;
using Ds3.Runtime;

namespace Ds3.Helpers.Transferrers
{
    internal static class BestEffort
    {
        private static readonly TraceSwitch Log = new TraceSwitch("Ds3.Helpers.Transferrers", "set in config file");

        public static void ModifyForRetry(Stream stream, int retries, ref int currentTry, string objectName, long offset, Exception ex)
        {
            CanRetry(stream, retries, currentTry, objectName, offset, ex);

            currentTry++;

            if (Log.TraceVerbose) { Trace.TraceInformation("Reset stream position to offset = {0}.", offset); }
            stream.Position = offset;
        }

        public static void ModifyForRetry(Stream stream, int retries, ref int currentTry, string objectName, long offset, ref IEnumerable<Range> ranges, ref ITransferrer transferrer, Ds3ContentLengthNotMatch ex)
        {
            CanRetry(stream, retries, currentTry, objectName, offset, ex);

            // Issue a partial get for the remainder of the request
            // Seek back one byte to make sure that the connection did not fail part way through a byte
            stream.Seek(-1, SeekOrigin.Current);

            ranges = JobsUtil.RetryRanges(ranges, ex.BytesRead, ex.ContentLength);
            transferrer = new PartialReadTransferrer();

            currentTry++;
        }

        private static void CanRetry(Stream stream, int retries, int currentTry, string objectName, long offset, Exception ex)
        {
            if (retries != -1 && currentTry >= retries)
            {
                throw new Ds3NoMoreRetransmitException(
                    string.Format(Resources.NoMoreRetransmitException, retries, objectName, offset), currentTry, ex);
            }

            if (!stream.CanSeek) throw new Ds3NotSupportedStream(Resources.NotSupportedStream, ex);
        }
    }
}
