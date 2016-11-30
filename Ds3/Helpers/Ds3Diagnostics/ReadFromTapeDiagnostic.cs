/*
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

using System.Linq;
using Ds3.Calls;
using Ds3.Models;

namespace Ds3.Helpers.Ds3Diagnostics
{
    internal class ReadFromTapeDiagnostic : Ds3DiagnosticCheck<BlobStoreTaskInformation>
    {
        private const string TaskName = "ReadChunkFromTapeTask";

        public override Ds3DiagnosticResults<BlobStoreTaskInformation> Get(Ds3DiagnosticClient ds3DiagnosticClient)
        {
            return Get(ds3DiagnosticClient, Func);
        }

        private static Ds3DiagnosticResult<BlobStoreTaskInformation> Func(IDs3Client client)
        {
            var response =
                client.GetDataPlannerBlobStoreTasksSpectraS3(new GetDataPlannerBlobStoreTasksSpectraS3Request());
            var tasks =
                response.ResponsePayload.Tasks.Where(
                    t => t.Name.Equals(TaskName) && t.State == BlobStoreTaskState.IN_PROGRESS);

            return tasks.Any()
                ? new Ds3DiagnosticResult<BlobStoreTaskInformation>(Ds3DiagnosticsCode.ReadingFromTape,
                    string.Format(DiagnosticsMessages.ReadingFromTape, tasks.Count()), tasks)
                : new Ds3DiagnosticResult<BlobStoreTaskInformation>(Ds3DiagnosticsCode.Ok, null, null);
        }
    }
}