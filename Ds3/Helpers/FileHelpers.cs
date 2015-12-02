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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ds3.Helpers.Streams;
using Ds3.Models;

namespace Ds3.Helpers
{
    public static class FileHelpers
    {
        /// <summary>
        ///     Creates a function that can open a file stream for writing.
        ///     Backwards-compatible signature before prefix was added
        ///     Creates all directories needed to save the file.
        /// </summary>
        /// <param name="root">The root directory within which to save objects.</param>
        /// <returns></returns>
        public static Func<string, Stream> BuildFileGetter(string root)
        {
            return BuildFileGetter(root, string.Empty);
        }

        /// <summary>
        ///     Creates a function that can open a file stream for writing.
        ///     For use with IJob.Transfer(getter).
        ///     Creates all directories needed to save the file.
        /// </summary>
        /// <param name="root">The root directory within which to save objects.</param>
        /// <param name="prefix">A string to prepend to the filename.</param>
        /// <returns></returns>
        public static Func<string, Stream> BuildFileGetter(string root, string prefix)
        {
            return key =>
            {
                var fullPath = Path.Combine(root, ConvertKeyToPath(key));
                var fixedPath = PrependPrefix(fullPath, prefix);
                EnsureDirectoryForFile(fixedPath);
                return new DisposableStream(File.OpenWrite(fixedPath));
            };
        }

        private static void EnsureDirectoryForFile(string path)
        {
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        ///     Creates a function that can open a file stream for reading.
        ///     For use with IJob.Transfer(putter).
        ///     Backwards-compatible signature before prefix was added
        /// </summary>
        /// <param name="root">The root directory within which to read objects.</param>
        /// <returns></returns>
        public static Func<string, Stream> BuildFilePutter(string root)
        {
            return BuildFilePutter(root, string.Empty);
        }

        /// <summary>
        ///     Creates a function that can open a file stream for reading.
        ///     For use with IJob.Transfer(putter).
        /// </summary>
        /// <param name="root">The root directory within which to read objects.</param>
        /// <param name="prefix">A string to prepend to the object name.</param>
        /// <returns></returns>
        public static Func<string, Stream> BuildFilePutter(string root, string prefix)
        {
            return
                key =>
                    new DisposableStream(File.OpenRead(Path.Combine(root, RemovePrefix(ConvertKeyToPath(key), prefix))));
        }

        /// <summary>
        ///     Returns a list of object key, size pairs for a directory root (recursive).
        ///     For use with IDs3ClientHelpers.StartWriteJob(bucket, objectsToWrite)
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IEnumerable<Ds3Object> ListObjectsForDirectory(string root)
        {
            return ListObjectsForDirectory(root, string.Empty);
        }

        public static IEnumerable<Ds3Object> ListObjectsForDirectory(string root, string prefix)
        {
            // remove trailing slash (it works but spoil the count)
            if (root.EndsWith("\\") || root.EndsWith("/"))
            {
                root = root.Substring(0, root.Length - 1);
            }
            var rootDirectory = new DirectoryInfo(root);
            var rootSize = rootDirectory.FullName.Length + 1;
            return rootDirectory
                .EnumerateFiles("*", SearchOption.AllDirectories)
                .Select(file => new Ds3Object(
                    PrependPrefix(ConvertPathToKey(file.FullName.Substring(rootSize)), prefix),
                    file.Length
                    ));
        }

        private static string ConvertKeyToPath(string key)
        {
            return key.Replace('/', Path.DirectorySeparatorChar);
        }

        private static string ConvertPathToKey(string path)
        {
            return path.Replace(Path.DirectorySeparatorChar, '/');
        }

        /// <summary>
        ///     Add a prefix string to the beginning of a filename
        ///     For use with ListObjectsForDirectory
        /// </summary>
        /// <param name="path">Full path too file</param>
        /// <param name="prefix">String to prepend to filename</param>
        /// <returns>full path with prefix prepended</returns>
        private static string PrependPrefix(string path, string prefix)
        {
            var fileName = Path.GetFileName(path);
            var fixedPath = path.Substring(0, path.Length - fileName.Length) + prefix + fileName;
            return fixedPath;
        }

        /// <summary>
        ///     Remove the prefix string from a filename to find the actual file
        ///     For use with BuildFilePutter
        /// </summary>
        /// <param name="path">Full path too file</param>
        /// <param name="prefix">String to prepend to filename</param>
        /// <returns>full path with prefix prepended</returns>
        private static string RemovePrefix(string path, string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                return path;
            }
            var fileName = Path.GetFileName(path);
            var fixedName = fileName.Replace(prefix, string.Empty);
            var fixedPath = path.Substring(0, path.Length - fileName.Length) + fixedName;
            return fixedPath;
        }
    }
}