/*
 * ******************************************************************************
 *   Copyright 2014 Spectra Logic Corporation. All Rights Reserved.
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

namespace Ds3.Models
{
    public class DeleteDs3ObjectError
    {
        public string Key { get; private set; }
        public string Code { get; private set; }
        public string Message { get; private set; }

        public DeleteDs3ObjectError(string key, string code, string message)
        {
            this.Key = key;
            this.Code = code;
            this.Message = message;
        }
    }
}
