using System;
using TodoManager.Models.Enum;

namespace TodoManager.Utility
{
    public class BusinessException : Exception
    {
        public string Code { get; set; }
        public BusinessExceptionLevelEnum Level { get; set; }

        public BusinessException(BusinessExceptionLevelEnum level)
        {
            this.Level = level;
        }

        public BusinessException(BusinessExceptionLevelEnum level, string message)
            : base(message)
        {
            this.Level = level;
        }

        public BusinessException(BusinessExceptionLevelEnum level, string code, string message)
            : base(message)
        {
            this.Level = level;
            this.Code = code;
        }

        public BusinessException(string code, string message)
            : base(message)
        {
            this.Level = BusinessExceptionLevelEnum.Service;
            this.Code = code;
        }

        public BusinessException(BusinessExceptionLevelEnum level, string code, string message, Exception ex)
            : base(message, ex)
        {
            this.Level = level;
            this.Code = code;
        }

        public BusinessException(BusinessExceptionLevelEnum level, Exception ex)
            : base("", ex)
        {
            this.Level = level;
        }
    }
}
