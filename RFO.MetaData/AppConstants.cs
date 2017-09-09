
namespace RFO.MetaData
{
    /// <summary>
    /// Defines application constants
    /// </summary>
    public static class AppConstants
    {
        #region Commin Information

        /// <summary>
        /// The admin role
        /// </summary>
        public const string AdminRole = "Admin";

        /// <summary>
        /// The employee role
        /// </summary>
        public const string EmployeeRole = "Employee";

        /// <summary>
        /// The user role
        /// </summary>
        public const string UserRole = "User";

        /// <summary>
        /// Application display name.
        /// </summary>
        public const string AppName = "DEABAK";

        /// <summary>
        /// The maximum records per page
        /// </summary>
        public const int MaxRecordsPerPage = 99999;

        /// <summary>
        /// The number record per page
        /// </summary>
        public const int NumRecordPerPage = 24;

        #endregion

        #region Upload Images

        /// <summary>
        /// The image dir name
        /// </summary>
        public const string ImageDirName = "/Content/UploadImages";

        /// <summary>
        /// The no image file name
        /// </summary>
        public const string NoImageFileName = "noimages.jpg";

        /// <summary>
        /// The maximum image width
        /// </summary>
        public const int MaximumImageWidth = 500;

        /// <summary>
        /// The maximum image height
        /// </summary>
        public const int MaximumImageHeight = 500;

        #endregion

        #region WebAPI

        /// <summary>
        /// The web API address URL
        /// </summary>
        public const string WebAPIAddressUrl = "http://localhost:49236/";

        #endregion
    }
}