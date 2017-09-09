
using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Net.Mail;
using RFO.Common.Utilities.Logging;

namespace RFO.Common.Utilities.MailHelper
{
    /// <summary>
    /// Loads emails for specified account and sending new email
    /// </summary>
    public static class MailHelper
    {
        #region Fields

        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof (MailHelper).Name);

        #endregion

        #region Implementation of IMailHelper

        /// <summary>
        /// The method for sending email
        /// </summary>
        /// <param name="mailServer">The mail server</param>
        /// <param name="subject">The subject of email</param>
        /// <param name="content">The content of email</param>
        /// <param name="to">The destination address will be received email</param>
        /// <param name="from">The source address</param>
        /// <param name="password">The password of source address</param>
        /// <param name="supportSSL">Support SLL or not</param>
        public static bool SendMail(string mailServer, string subject, string content,
                             string to, string from, string password, bool supportSSL = false)
        {
            var funcName = "SendMail";
            Logger.DebugFormat("{0} <-- Start", funcName);
            Logger.DebugFormat("{0} - mailServer=[{1}], from=[{2}], to=[{3}]", 
                new object[] { mailServer, mailServer, from, to });
            
            var result = true;
            try
            {
                var mailclient = new SmtpClient
                {
                    Host = mailServer,
                    EnableSsl = supportSSL,
                    UseDefaultCredentials = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(from, password)
                };

                var mm = new MailMessage(from, to)
                {
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = content,
                    Priority = MailPriority.High
                };

                mailclient.Send(mm);
            }
            catch (Exception ex)
            {
                result = false;
                Logger.ErrorFormat("{0} - Exception: [{1}]", funcName, ex.ToString());
            }

            Logger.DebugFormat("{0} - Result=[{1}]", funcName, result);
            Logger.DebugFormat("{0} --> End", funcName);

            return result;
        }

        #endregion
    }
}