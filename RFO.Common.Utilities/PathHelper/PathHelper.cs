
using System;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading;

namespace RFO.Common.Utilities.PathHelper
{
    /// <summary>
    /// Utilities to handle path manipulation
    /// </summary>
    public static class PathHelper
    {
        #region Implementation of IPathHelper

        /// <summary>
        /// Get absolute path by passing a relative path to application folder
        /// </summary>
        /// <param name="relativePath">The relative path to application folder</param>
        /// <returns>The absolute path</returns>
        public static string GetAppPath(string relativePath)
        {
            var basedir = AppDomain.CurrentDomain.BaseDirectory;
            var absolutePath = Path.Combine(basedir, relativePath);
            return Path.GetFullPath(absolutePath);
        }

        public static string GetRelativePath(string path)
        {
            var toPath = GetAppPath(string.Empty);
            return RelativePathTo(toPath, path);
        }

        /// <summary>
        /// Ensure create folder
        /// </summary>
        /// <param name="path">The path to create</param>
        public static void EnsureCreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            while (!Directory.Exists(path))
            {
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// Creates a relative path from one file
        /// or folder to another.
        /// </summary>
        /// <param name="fromDirectory">
        /// Contains the directory that defines the
        /// start of the relative path.
        /// </param>
        /// <param name="toPath">
        /// Contains the path that defines the
        /// endpoint of the relative path.
        /// </param>
        /// <returns>
        /// The relative path from the start
        /// directory to the end path.
        /// </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string RelativePathTo(string fromDirectory, string toPath)
        {
            string newPath;

            if (fromDirectory != null && toPath != null)
            {
                var isRooted = Path.IsPathRooted(fromDirectory) && Path.IsPathRooted(toPath);

                if (isRooted)
                {
                    var isDifferentRoot = string.Compare(Path.GetPathRoot(fromDirectory), Path.GetPathRoot(toPath),
                        StringComparison.OrdinalIgnoreCase) != 0;

                    if (isDifferentRoot)
                    {
                        return toPath;
                    }
                }

                // Get relative path if toPath exist in frompath's parent directory
                var directoryInfo =
                    Directory.GetParent(Path.GetFullPath(fromDirectory.TrimEnd(Path.DirectorySeparatorChar)));
                if (directoryInfo != null)
                {
                    var parentOfFromDir = directoryInfo.FullName;

                    if (!toPath.ToLower().Contains(parentOfFromDir.ToLower()))
                    {
                        return toPath;
                    }
                }
                //end

                var relativePath = new StringCollection();
                var fromDirectories = fromDirectory.Split(Path.DirectorySeparatorChar);

                var toDirectories = toPath.Split(Path.DirectorySeparatorChar);

                var length = Math.Min(fromDirectories.Length, toDirectories.Length);

                var lastCommonRoot = -1;

                // find common root
                for (var x = 0; x < length; x++)
                {
                    if (String.Compare(fromDirectories[x], toDirectories[x], StringComparison.OrdinalIgnoreCase) != 0)
                        break;

                    lastCommonRoot = x;
                }
                if (lastCommonRoot == -1)
                    return toPath;

                // add relative folders in from path
                for (var x = lastCommonRoot + 1; x < fromDirectories.Length; x++)
                    if (fromDirectories[x].Length > 0)
                        relativePath.Add("..");

                // add to folders to path
                for (var x = lastCommonRoot + 1; x < toDirectories.Length; x++)
                    relativePath.Add(toDirectories[x]);

                // create relative path
                var relativeParts = new string[relativePath.Count];
                relativePath.CopyTo(relativeParts, 0);

                newPath = string.Join(Path.DirectorySeparatorChar.ToString(), relativeParts);
            }
            else if (fromDirectory == null)
            {
                throw new ArgumentNullException("fromDirectory");
            }
            else
            {
                throw new ArgumentNullException("toPath");
            }

            return newPath;
        }

        #endregion
    }
}