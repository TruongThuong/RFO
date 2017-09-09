using RFO.Common.Utilities.Logging;
using RFO.Model.DummyDataGenerator.Constant;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RFO.Model.DummyDataGenerator
{
    /// <summary>
    /// Data provider
    /// </summary>
    public class DummyDataProvider
    {
        #region Fields

        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(DummyDataProvider).Name);

        /// <summary>
        /// Definition a SeedBanner instance
        /// </summary>
        private static DummyDataProvider instance;

        /// <summary>
        /// The synchronize lock
        /// </summary>
        private static readonly object singletonLocker = new object();

        /// <summary>
        /// The dummy data storage
        /// </summary>
        private readonly Dictionary<DummyDataType, List<string>> dummyDataStorage = new Dictionary<DummyDataType, List<string>>();

        /// <summary>
        /// The seed random
        /// </summary>
        private readonly Random seedRandom = new Random(Environment.TickCount);

        #endregion

        /// <summary>
        /// Gets or sets the dummy data path.
        /// </summary>
        /// <value>
        /// The dummy data path.
        /// </value>
        public string DummyDataGeneratorPath { get; set; }

        /// <summary>
        /// Get instance of SeedBanner 
        /// </summary>
        /// <returns>SeedBanner instance</returns>
        public static DummyDataProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (singletonLocker)
                    {
                        if (instance == null)
                        {
                            instance = new DummyDataProvider();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Reads the simple data.
        /// </summary>
        /// <param name="dummyDataType">Type of the dummy data.</param>
        /// <param name="filePath">The file path.</param>
        public void ReadSimpleData(DummyDataType dummyDataType, string filePath)
        {
            Logger.Debug($"ReadSimpleData - dummyDataType=[{dummyDataType}], filePath=[{filePath}]...");

            try
            {
                // Read text file
                var lstData = new List<string>();

                // Read the file and display it line by line.
                if (File.Exists(filePath))
                {
                    using (var file = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = file.ReadLine()) != null)
                        {
                            if (!string.IsNullOrEmpty(line))
                            {
                                lstData.Add(line);
                            }
                        }
                    }
                }

                dummyDataStorage.Add(dummyDataType, lstData);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Failed to execute ReadSimpleData: {0}", ex);
            }

            Logger.Debug("ReadSimpleData...DONE");
        }

        /// <summary>
        /// Reads the HTML data.
        /// </summary>
        /// <param name="dummyDataType">Type of the dummy data.</param>
        /// <param name="dirHtml">The dir HTML.</param>
        public void ReadHtmlData(DummyDataType dummyDataType, string dirHtml)
        {
            Logger.Debug($"ReadHtmlData - dummyDataType=[{dummyDataType}], dirHtml=[{dirHtml}]...");

            try
            {
                var files = Directory.GetFiles(dirHtml);
                var lstData = files.Select(File.ReadAllText).ToList();
                dummyDataStorage.Add(dummyDataType, lstData);
            }
            catch(Exception ex)
            {
                Logger.ErrorFormat("Failed to execute ReadHtmlData: {0}", ex);
            }

            Logger.Debug("ReadHtmlData...DONE");
        }

        /// <summary>
        /// Reads the images.
        /// </summary>
        /// <param name="dummyDataType">Type of the dummy data.</param>
        /// <param name="dirImages">The dir HTML.</param>
        public void ReadImages(DummyDataType dummyDataType, string dirImages)
        {
            Logger.Debug($"ReadImages - dummyDataType=[{dummyDataType}], dirImages=[{dirImages}]...");

            try
            {
                var files = Directory.GetFiles(dirImages);
                var lstData = files.Select(file => new FileInfo(file)).Select(fileInfo => fileInfo.Name).ToList();
                dummyDataStorage.Add(dummyDataType, lstData);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Failed to execute ReadImages: {0}", ex);
            }

            Logger.Debug("ReadImages...DONE");
        }

        /// <summary>
        /// Gets the generated data.
        /// </summary>
        /// <param name="dummyDataType">Type of the dummy data.</param>
        /// <param name="isRandom">if set to <c>true</c> [is random].</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public string GetGeneratedData(DummyDataType dummyDataType, bool isRandom = true, int index = 0)
        {
            var lstData = dummyDataStorage[dummyDataType];
            var data = isRandom ? lstData[GetRandomNumber(lstData.Count)] : lstData[index];
            return this.ToUpperFirstCharacter(data);
        }

        /// <summary>
        /// Gets the random number.
        /// </summary>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public int GetRandomNumber(int max)
        {
            int seed;
            lock (singletonLocker)
            {
                seed = seedRandom.Next(max);
            }
            return seed;
        }

        /// <summary>
        /// Gets the random number.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public int GetRandomNumber(int min, int max)
        {
            int seed;
            lock (singletonLocker)
            {
                seed = seedRandom.Next(min, max);
            }
            return seed;
        }

        /// <summary>
        /// Gets the random number larger than zero.
        /// </summary>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public int GetRandomNumberLargerThanZero(int max)
        {
            int seed;
            lock (singletonLocker)
            {
                seed = seedRandom.Next(1, max);
            }
            return seed;
        }

        /// <summary>
        /// Gets the random number larger than1.
        /// </summary>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public int GetRandomNumberLargerThan1(int max)
        {
            int seed;
            lock (singletonLocker)
            {
                seed = seedRandom.Next(2, max);
            }
            return seed;
        }

        /// <summary>
        /// Gets the number item.
        /// </summary>
        /// <param name="dummyDataType">Type of the dummy data.</param>
        /// <returns></returns>
        public int GetNumItem(DummyDataType dummyDataType)
        {
            var itemsVI = dummyDataStorage[dummyDataType];
            return itemsVI.Count;
        }

        /// <summary>
        /// Gets the random day.
        /// </summary>
        /// <param name="startYear">The start year.</param>
        /// <returns></returns>
        public DateTime GetRandomDay(int startYear)
        {
            DateTime start = new DateTime(startYear, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(seedRandom.Next(range));
        }

        /// <summary>
        /// Gets the random day.
        /// </summary>
        /// <param name="startYear">The start year.</param>
        /// <param name="startMonth">The start month.</param>
        /// <returns></returns>
        public DateTime GetRandomDay(int startYear, int startMonth)
        {
            DateTime start = new DateTime(startYear, startMonth, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(seedRandom.Next(range));
        }

        /// <summary>
        /// Gets the random day.
        /// </summary>
        /// <param name="startYear">The start year.</param>
        /// <param name="upperRangeDate">The upper range date.</param>
        /// <returns></returns>
        public DateTime GetRandomDay(int startYear, DateTime upperRangeDate)
        {
            DateTime start = new DateTime(startYear, 1, 1);
            int range = (upperRangeDate - start).Days;
            return start.AddDays(seedRandom.Next(range));
        }

        /// <summary>
        /// To the upper first character.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        private string ToUpperFirstCharacter(string s)
        {
            return string.IsNullOrEmpty(s) ? s : s[0].ToString().ToUpper() + s.Substring(1);
        }
    }

    /// <summary>
    /// Generator extension
    /// </summary>
    public static class GeneratorExtention
    {
        /// <summary>
        /// Gets the random item.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static int GetRandomItem(this List<int> items)
        {
            return items[DummyDataProvider.Instance.GetRandomNumber(items.Count)];
        }

        /// <summary>
        /// Gets the random item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static T GetRandomItem<T>(this List<T> items) where T : class
        {
            return items[DummyDataProvider.Instance.GetRandomNumber(items.Count)];
        }

        /// <summary>
        /// Gets the random item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static T GetRandomItem<T>(this BlockingCollection<T> items) where T : class
        {
            return items.ElementAt(DummyDataProvider.Instance.GetRandomNumber(items.Count()));
        }
    }
}