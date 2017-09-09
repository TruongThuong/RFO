using RFO.AspNet.Utilities.MembershipService;
using RFO.Common.Utilities.Exceptions;
using RFO.Common.Utilities.Logging;
using RFO.MetaData;
using RFO.Model.DummyDataGenerator;
using RFO.Model.DummyDataGenerator.Constant;
using RFO.Model.DummyDataGenerator.Seed;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;

namespace RFO.Model.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<RFODbContext>
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(Configuration).Name);

        /// <summary>
        /// The _dummy data path
        /// </summary>
        private readonly string dummyDataPath = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", string.Empty) + "\\DummyDataGenerator\\";

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Seed(RFODbContext context)
        {
            Logger.Debug("Seed...");

            this.InitDummyData(dummyDataPath);
            this.ConnectDB();
            this.SeedDummyData(context);

            Logger.Debug("Seed...DONE");
        }

        /// <summary>
        /// Connects the database.
        /// </summary>
        private void ConnectDB()
        {
            Logger.Debug("ConnectDB...");

            try
            {
                // Add this code line to fix following bug:
                // System.InvalidOperationException: The model backing the 'DBEntities' 
                // context has changed since the database was created. 
                // Consider using Code First Migrations to update the database 
                // (http://go.microsoft.com/fwlink/?LinkId=238269).
                Database.SetInitializer<RFODbContext>(null);
                SimpleMembershipInitializer<RFODbContext>.Initialize();
                SimpleMembershipInitializer<RFODbContext>.CreateRoles(
                    new string[]
                    {
                        AppConstants.AdminRole,
                        AppConstants.EmployeeRole,
                        AppConstants.UserRole,
                    });
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.ToString());
            }

            Logger.Debug("ConnectDB...DONE");
        }

        /// <summary>
        /// Seeds the dummy data.
        /// </summary>
        private void SeedDummyData(RFODbContext context)
        {
            Logger.Debug("SeedDummyData...");

            try
            {
                SeedCommonInfo.Instance.Seed();
                SeedUser.Instance.Seed(context);
                SeedMenu.Instance.Seed();
                SeedProduct.Instance.Seed();
                SeedProductImage.Instance.Seed();
                SeedOrderState.Instance.Seed();
                SeedPromotion.Instance.Seed();
                SeedTable.Instance.Seed();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Failed to SeedDummyData: {0}", ex);
            }

            Logger.Debug("SeedDummyData...DONE");
        }

        private void InitDummyData(string dummyDataPath)
        {
            Logger.Debug("InitDummyData...");

            try
            {
                var baseDir = dummyDataPath;

                DummyDataProvider.Instance.DummyDataGeneratorPath = baseDir;

                // EmployeeName
                DummyDataProvider.Instance.ReadSimpleData(DummyDataType.EMPLOYEE_NAME, Path.Combine(baseDir, "DummyData/Common/EmployeeName.txt"));

                // Address
                DummyDataProvider.Instance.ReadSimpleData(DummyDataType.ADDRESS, Path.Combine(baseDir, "DummyData/Common/Address.txt"));

                // Email
                DummyDataProvider.Instance.ReadSimpleData(DummyDataType.EMAIL, Path.Combine(baseDir, "DummyData/Common/Email.txt"));

                // Number
                DummyDataProvider.Instance.ReadSimpleData(DummyDataType.NUMBER, Path.Combine(baseDir, "DummyData/Common/Number.txt"));

                // Phone
                DummyDataProvider.Instance.ReadSimpleData(DummyDataType.PHONE, Path.Combine(baseDir, "DummyData/Common/Phone.txt"));

                // ShortDescription
                DummyDataProvider.Instance.ReadSimpleData(DummyDataType.BRIEF_DESCRIPTION, Path.Combine(baseDir, "DummyData/Common/BriefDescription.txt"));

                // Title
                DummyDataProvider.Instance.ReadSimpleData(DummyDataType.TITLE, Path.Combine(baseDir, "DummyData/Common/Title.txt"));

                // Website
                DummyDataProvider.Instance.ReadSimpleData(DummyDataType.WEBSITE, Path.Combine(baseDir, "DummyData/Common/Website.txt"));

                // SimpleData
                DummyDataProvider.Instance.ReadSimpleData(DummyDataType.SIMPLE_DATA, Path.Combine(baseDir, "DummyData/Common/SimpleData.txt"));

                // Position 
                DummyDataProvider.Instance.ReadSimpleData(DummyDataType.POSITION, Path.Combine(baseDir, "DummyData/Common/Position.txt"));

                // Result 
                DummyDataProvider.Instance.ReadSimpleData(DummyDataType.RESULT, Path.Combine(baseDir, "DummyData/Common/Result.txt"));

                // Place 
                DummyDataProvider.Instance.ReadSimpleData(DummyDataType.PLACE, Path.Combine(baseDir, "DummyData/Common/Place.txt"));

                // Language
                DummyDataProvider.Instance.ReadSimpleData(DummyDataType.LANGUAGE, Path.Combine(baseDir, "DummyData/Common/Language.txt"));

                // Education
                DummyDataProvider.Instance.ReadSimpleData(DummyDataType.EDUCATION, Path.Combine(baseDir, "DummyData/Common/Education.txt"));

                // Menu
                DummyDataProvider.Instance.ReadSimpleData(DummyDataType.MENU, Path.Combine(baseDir, "DummyData/Common/FoodCategoryName.txt"));

                // Food
                DummyDataProvider.Instance.ReadSimpleData(DummyDataType.FOOD, Path.Combine(baseDir, "DummyData/Common/FoodName.txt"));

                // Images
                DummyDataProvider.Instance.ReadImages(DummyDataType.IMAGE, Path.Combine(baseDir, "DummyData/Images"));

                // HtmlContent
                DummyDataProvider.Instance.ReadHtmlData(DummyDataType.HTML, Path.Combine(baseDir, "DummyData/HtmlContent"));
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Failed to InitDummyData: {0}", ex);
            }

            Logger.Debug("InitDummyData...DONE");
        }
    }
}
