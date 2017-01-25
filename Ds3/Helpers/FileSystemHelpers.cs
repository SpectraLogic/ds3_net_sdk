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
using System.Linq;
using System.Runtime.InteropServices;
using Ds3.Models;

namespace Ds3.Helpers
{
    public static class FileSystemHelpers
    {
        /// <summary>
        /// Determines if the objects to read using a GET job can be saved on the given path
        /// </summary>
        /// <param name="path">Directory or UNC path of the volume to
        /// be checked (can be a network drive)</param>
        /// <param name="objectsToRead"></param>
        /// <returns><see cref="DriveResult"/></returns>
        public static DriveResult Check(string path, IEnumerable<Ds3Object> objectsToRead)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                {
                    var e = new ArgumentNullException(nameof(path));
                    return new DriveResult(DriveStatus.Error, e.Message, e);
                }

                if (objectsToRead == null || !objectsToRead.Any())
                {
                    var e = new ArgumentNullException(nameof(objectsToRead));
                    return new DriveResult(DriveStatus.Error, e.Message, e);
                }

                if (!Directory.Exists(path))
                {
                    return new DriveResult(DriveStatus.DirectoryNotExists, $"{path} does not exists.");
                }

                try
                {
                    if (!HavePermissions(path))
                    {
                        return new DriveResult(DriveStatus.NoPermission, $"User does not have permissions to access {path}.");
                    }
                }
                catch (Exception e)
                {
                    return new DriveResult(DriveStatus.Error, e.Message, e);
                }

                try
                {
                    var freeBytes = DriveFreeBytes(path);
                    var jobSize = objectsToRead.Where(o => o.Size.HasValue).Sum(o => o.Size.Value);

                    if (freeBytes < jobSize)
                    {
                        return new DriveResult(DriveStatus.NotEnoughSpace, $"Missing {jobSize - freeBytes} bytes.");
                    }
                }
                catch (Exception e)
                {
                    return new DriveResult(DriveStatus.Error, e.Message, e);
                }
            }
            catch (Exception e)
            {
                return new DriveResult(DriveStatus.Unknown, e.Message, e);
            }

            return new DriveResult(DriveStatus.Ok);
        }

        private static bool HavePermissions(string path)
        {
            try
            {
                using (File.Create(Path.Combine(path, Path.GetRandomFileName()), 1, FileOptions.DeleteOnClose))
                {
                    //do nothing
                }
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

        private static long DriveFreeBytes(string path)
        {
            if (!path.EndsWith("\\"))
            {
                path += '\\';
            }

            var driveInfo  = new DriveInfo(Path.GetPathRoot(path));

            return driveInfo.AvailableFreeSpace;
        }
    }

    public class DriveResult
    {
        public DriveResult(DriveStatus status, string errorMessage = null, Exception exception = null)
        {
            Status = status;
            ErrorMessage = errorMessage;
            Exception = exception;
        }

        public DriveStatus Status { get; }
        public string ErrorMessage { get; }
        public Exception Exception { get; }
    }

    public enum DriveStatus
    {
        Ok,
        NoPermission,
        NotEnoughSpace,
        Error,
        DirectoryNotExists,
        Unknown
    }
}