
using System;
using System.IO;

namespace RFO.WebAPI.Models
{
    public class FileStatus
    {
        public string group { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int size { get; set; }
        public string progress { get; set; }
        public string url { get; set; }
        public string thumbnail_url { get; set; }
        public string delete_url { get; set; }
        public string delete_type { get; set; }
        public string error { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileStatus"/> class.
        /// </summary>
        public FileStatus()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileStatus"/> class.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        public FileStatus(FileInfo fileInfo)
        {
            this.SetValues(fileInfo.Name, (int) fileInfo.Length);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileStatus"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileLength">Length of the file.</param>
        public FileStatus(string fileName, int fileLength)
        {
            this.SetValues(fileName, fileLength);
        }

        /// <summary>
        /// Sets the values.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileLength">Length of the file.</param>
        private void SetValues(string fileName, int fileLength)
        {
            this.name = fileName;
            this.type = "image/png";
            this.size = fileLength;
            this.progress = "1.0";
        }
    }
}