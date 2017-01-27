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

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Ds3.Runtime;

namespace Ds3.Helpers.TransferStrategies
{
    internal static class ExceptionExtensions
    {
        private static readonly IList<Type> RecoverableExceptions = new List<Type>
        {
            typeof(Ds3ContentLengthNotMatch),
            typeof(WebException),
            typeof(IOException)
        };

        public static bool IsRecoverableException(this Exception e)
        {
            return RecoverableExceptions.Contains(e.GetType());
        }

        public static bool IsUnrecoverableException(this Exception e) {
            return !IsRecoverableException(e);
        }
    }
}
