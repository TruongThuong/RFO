
namespace RFO.Common.Utilities.Logging
{
    /// <summary>
    /// This class is used by client applications to request ILogger instances.
    /// </summary>
    public sealed class LoggerManager
    {
        /// <summary>
        /// Retrieves or creates a named  Logger.
        /// </summary>
        /// <param name="loggerName">The name of  Logger to retrieve</param>
        /// <returns>The  Logger with the name specified</returns>
        public static ILogger GetLogger(string loggerName)
        {
            return new Logger(loggerName);
        }

        /// <summary>
        /// Retrieves or creates a named  Logger.
        /// </summary>
        /// <param name="loggerName">The name of  Logger to retrieve</param>
        /// <param name="configFile">The configuration file of  Logger to retrieve</param>
        /// <returns>The  Logger with the name and configuration file specified</returns>
        public static ILogger GetLogger(string loggerName, string configFile)
        {
            return new Logger(loggerName, configFile);
        }

        /// <summary>
        /// Set configuration for  Loggers.
        /// </summary>
        /// <param name="xmlConfigFile">XML Configuration file</param>
        public static void SetConfigFile(string xmlConfigFile)
        {
            Logger.InitConfig(xmlConfigFile);
        }
    }
}