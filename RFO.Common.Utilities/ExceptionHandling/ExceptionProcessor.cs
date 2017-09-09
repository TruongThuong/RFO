
using System;
using System.ComponentModel.Composition;
using RFO.Common.Utilities.Exceptions;

namespace RFO.Common.Utilities.ExceptionHandling
{
    /// <summary>
    /// Exception handler
    /// </summary>
    [Export(typeof (IExceptionProcessor))]
    public class ExceptionProcessor : IExceptionProcessor
    {
        /// <summary>
        /// Gets the exception message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public string GetExceptionMessage(Exception exception)
        {
            // Get exception message
            var specifiedException = exception as AbstractException;
            var message = specifiedException != null
                ? this.GetExceptionMessage(specifiedException)
                : exception.Message;
            return message;
        }

        /// <summary>
        /// Gets the exception message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        private string GetExceptionMessage(AbstractException exception)
        {
            var message = string.Empty;

            // Handle database exception
            AbstractException specifiedException = exception as DatabaseException;
            if (specifiedException != null) // DatabaseException
            {
                message = this.GetExceptionMessage((DatabaseException) specifiedException);
            }
            else // Others
            {
                // Handle business exception
                specifiedException = exception as BusinessException;
                if (specifiedException != null) // BusinessException
                {
                    message = specifiedException.Message;
                }
                else // Others
                {
                    // Handle configuration exception
                    specifiedException = exception as ConfigurationException;
                    if (specifiedException != null) // ConfigurationException
                    {
                        message = this.GetExceptionMessage((ConfigurationException)specifiedException);
                    }
                }
            }

            return message;
        }


        /// <summary>
        /// Gets the exception message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        private string GetExceptionMessage(ConfigurationException exception)
        {
            var message = string.Empty;

            var exceptionErrorCode = (ConfigurationErrorCode) exception.ErrCode;

            switch (exceptionErrorCode)
            {
                case ConfigurationErrorCode.Load:
                    message = "Có lỗi sảy ra trong quá trình load thông tin cấu hình. Vui lòng kiểm tra lại.";
                    break;

                case ConfigurationErrorCode.Update:
                    message = "Có lỗi sảy ra trong quá trình cập nhật thông tin cấu hình. Vui lòng kiểm tra lại.";
                    break;
            }

            return message;
        }

        /// <summary>
        /// Gets the exception message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        private string GetExceptionMessage(DatabaseException exception)
        {
            var message = string.Empty;

            var exceptionErrorCode = (DatabaseErrorCode) exception.ErrCode;

            switch (exceptionErrorCode)
            {
                case DatabaseErrorCode.Delete:
                    message =
                        "Có lỗi sảy ra trong quá trình xóa dữ liệu. Hiện thời dữ liệu này vẫn đang được sử dụng trong các bảng khác của cơ sở dữ liệu.";
                    break;

                case DatabaseErrorCode.Insert:
                    message = "Có lỗi sảy ra trong quá trình thêm dữ liệu.";
                    break;

                case DatabaseErrorCode.Load:
                    message = "Có lỗi sảy ra trong quá trình load dữ liệu.";
                    break;

                case DatabaseErrorCode.Update:
                    message = "Có lỗi sảy ra trong quá trình cập nhật dữ liệu.";
                    break;

                case DatabaseErrorCode.UploadImage:
                    message = "Có lỗi sảy ra trong quá trình upload dữ liệu.";
                    break;
            }

            return message;
        }
    }
}