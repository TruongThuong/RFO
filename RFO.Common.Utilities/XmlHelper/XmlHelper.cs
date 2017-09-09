// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlHelper.cs" company="TNL">
//   Copyright (c) TNL, 2016
//   All rights are reserved. Reproduction or transmission in whole or in part, in
//   any form or by any means, electronic, mechanical or otherwise, is prohibited
//   without the prior written consent of the copyright owner.
// </copyright>
// <summary>
//   Defines the XmlHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using RFO.Common.Utilities.Logging;

namespace RFO.Common.Utilities.XMLHelper
{
    /// <summary>
    /// XML helper class for serialize/deserialize
    /// </summary>
    /// <typeparam name="T">Instance of class will be serialized</typeparam>
    public class XmlHelper<T> where T : class
    {
        #region Fields

        /// <summary>
        /// Logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof (XmlHelper<T>).Name);

        #endregion

        #region Implementation of IXMLHelper

        /// <summary>
        /// Convert to string in xml format
        /// </summary>
        /// <param name="obj">Object to convert</param>
        /// <returns></returns>
        public static string ToString(T obj)
        {
            var sw = new StringWriter();
            try
            {
                var xmlSrlz = new XmlSerializer(typeof (T));
                xmlSrlz.Serialize(sw, obj);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ToString - Exception: {0}", ex.ToString());
            }
            return sw.ToString();
        }

        /// <summary>
        /// Create XML file from an object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool ToFile(T obj, string filePath)
        {
            var result = true;
            try
            {
                SaveToFile(obj, filePath);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ToFile - Exception: {0}", ex.ToString());
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Serialize object and save to XML file
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="filePath"></param>
        public static void SaveToFile(T obj, string filePath)
        {
            try
            {
                var xmlSrlz = new XmlSerializer(typeof (T));
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    xmlSrlz.Serialize(fs, obj);
                    fs.Flush();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("SaveToFile - Exception: {0}", ex.ToString());
            }
        }

        /// <summary>
        /// Load object from xml format string
        /// </summary>
        /// <param name="xmlString">xml string</param>
        /// <returns></returns>
        public static T Load(string xmlString)
        {
            var obj = default (T);
            try
            {
                var xmlSrlz = new XmlSerializer(typeof (T));
                var sw = new StringReader(xmlString);
                obj = (T) xmlSrlz.Deserialize(sw);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Load - Exception: {0}", ex.ToString());
            }
            return obj;
        }

        /// <summary>
        /// Load object from file
        /// </summary>
        /// <param name="filename">file path</param>
        /// <returns></returns>
        public static T LoadFromFile(string filename)
        {
            var obj = default(T);
            try
            {
                var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                obj = LoadFromStream(fs);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("LoadFromFile - Exception: {0}", ex.ToString());
            }
            return obj;
        }

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="md5String"></param>
        /// <returns></returns>
        public static T LoadFromFile(string path, out string md5String)
        {
            var obj = default(T);
            md5String = string.Empty;
            try
            {
                var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                obj = LoadFromStream(fileStream, out md5String);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("LoadFromFile - Exception: {0}", ex.ToString());
            }
            return obj;
        }

        /// <summary>
        /// Validate xml file and deserialize to object
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="xsdPaths"></param>
        /// <returns></returns>
        public static T LoadFromFile(string xmlPath, params string[] xsdPaths)
        {
            var obj = default(T);
            try
            {
                string md5String;
                obj = LoadFromFile(xmlPath, out md5String, xsdPaths);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("LoadFromFile - Exception: {0}", ex.ToString());
            }
            return obj;
        }

        /// <summary>
        /// Validate xml file and deserialize to object
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="md5String"></param>
        /// <param name="xsdPaths"></param>
        /// <returns></returns>
        public static T LoadFromFile(string xmlPath, out string md5String, params string[] xsdPaths)
        {
            var errs = ValidateXml(xmlPath, xsdPaths);
            if (errs != null && errs.Count > 0)
            {
                throw new ValidationException(string.Format("XMLPath=[{0}] is invalid", xmlPath), errs.ToArray());
            }
            return LoadFromFile(xmlPath, out md5String);
        }

        /// <summary>
        /// Load object from a stream
        /// </summary>
        /// <returns></returns>
        public static T LoadFromStream(Stream str)
        {
            using (str)
            {
                var s = new XmlSerializer(typeof (T));
                var xmlTextReader = new XmlTextReader(str);
                return (T) s.Deserialize(xmlTextReader);
            }
        }

        /// <summary>
        /// Load object from a stream
        /// </summary>
        /// <param name="str"></param>
        /// <param name="md5String"></param>
        /// <returns></returns>
        public static T LoadFromStream(Stream str, out string md5String)
        {
            using (str)
            {
                var s = new XmlSerializer(typeof (T));
                var xmlTextReader = new XmlTextReader(str);
                var instance = (T) s.Deserialize(xmlTextReader);
                using (var md5 = new MD5CryptoServiceProvider())
                {
                    str.Seek(0, SeekOrigin.Begin);
                    md5String = BitConverter.ToString(md5.ComputeHash(str));
                }
                return instance;
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Validate xml file by many separated schema files
        /// </summary>
        /// <param name="xmlPath">The xml file path</param>
        /// <param name="xsdPaths">Xsd paths</param>
        /// <exception cref="IOException">Throw IOEception when xml file  does not exists</exception>
        /// <returns></returns>
        /// <remarks>The validation process will ignore the missing schema</remarks>
        private static IList<string> ValidateXml(string xmlPath, params string[] xsdPaths)
        {
            var funcName = "ValidateXml";
            if (xsdPaths == null)
            {
                Logger.WarnFormat("{0} - Cannot validate xml because it does't have xsd", funcName);
                return null;
            }

            // Check schema files exist
            var xsdSet = new XmlSchemaSet();
            foreach (string xsdPath in xsdPaths)
            {
                if (File.Exists(xsdPath))
                {
                    xsdSet.Add(null, xsdPath);
                }
                else
                {
                    var err = string.Format("{0} - Exception: Error occurs while validating [{0}] due to " +
                                            "Schema [{1}] does not exist", xmlPath, xsdPath);
                    Logger.Error(err);
                    throw new Exception(err);
                }
            }

            var xmlSetting = new XmlReaderSettings()
            {
                ValidationType = ValidationType.Schema,
                IgnoreComments = true,
                Schemas = xsdSet
            };

            var xmlErrors = new List<string>();
            ValidationEventHandler handler = (sender, e) =>
            {
                if (e.Severity == XmlSeverityType.Error)
                {
                    xmlErrors.Add(e.Message);
                    Logger.Error(e.Message, e.Exception);
                }
            };
            xmlSetting.ValidationEventHandler += handler;

            using (var validateReader = XmlReader.Create(xmlPath, xmlSetting))
            {
                while (validateReader.Read())
                {
                    // Looping until enable to read
                }
                validateReader.Close();
            }

            xmlSetting.ValidationEventHandler -= handler;

            return xmlErrors;
        }

        #endregion
    }

    /// <summary>
    /// The helper class for XML validation
    /// </summary>
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException(string message, params string[] errors)
            : base(message)
        {
            Errors = errors;
        }

        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Errors = (string[]) info.GetValue("Errors", typeof (string[]));
        }

        public static string[] Errors { get; set; }

        /// <summary>
        /// The get object data.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Errors", Errors, typeof (string[]));
        }
    }
}