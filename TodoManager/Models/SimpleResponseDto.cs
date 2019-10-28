using System;
using TodoManager.Models.Enum;
using TodoManager.Utility;

namespace TodoManager.Models.Dto
{
    public class SimpleResponseDto<T>
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public long ServerTime { get; set; }
        public T Result { get; set; }

        private SimpleResponseDto()
        {
        }

        private static SimpleResponseDto<T> NewInstance()
        {
            return new SimpleResponseDto<T>();
        }

        public static SimpleResponseDto<T> OK(T result)
        {
            SimpleResponseDto<T> dto = NewInstance();
            dto.Code = "0000";
            dto.Message = "OK";
            dto.ServerTime = DateTime.Now.Ticks;
            dto.Result = result;

            return dto;
        }

        public static SimpleResponseDto<T> Failed(string code, string message, string requestKey)
        {
            SimpleResponseDto<T> dto = NewInstance();
            dto.Code = code;
            dto.Message = message;
            dto.ServerTime = DateTime.Now.Ticks;

            return dto;
        }

        public static SimpleResponseDto<T> Failed(string code, string message)
        {
            return Failed(code, message, null);
        }

        public static SimpleResponseDto<T> Failed(ResponseCodeEnum responseCodeEnum)
        {
            SimpleResponseDto<T> dto = NewInstance();
            dto.Code = ((int)responseCodeEnum).ToString();
            dto.Message = responseCodeEnum.GetMessage();
            dto.ServerTime = DateTime.Now.Ticks;

            return dto;
        }

        public static SimpleResponseDto<T> BusinessException(BusinessException be)
        {
            SimpleResponseDto<T> dto = NewInstance();
            dto.Code = be.Code;
            dto.Message = be.Message;
            dto.ServerTime = DateTime.Now.Ticks;

            return dto;
        }

        public static SimpleResponseDto<T> SystemException(Exception e)
        {
            ResponseCodeEnum responseCodeEnum = ResponseCodeEnum.ResponseCode_500;
            SimpleResponseDto<T> dto = NewInstance();
            dto.Code = ((int)responseCodeEnum).ToString();
            dto.Message = responseCodeEnum.GetMessage();
            dto.ServerTime = DateTime.Now.Ticks;

            return dto;
        }

        public static SimpleResponseDto<T> Error()
        {
            ResponseCodeEnum responseCodeEnum = ResponseCodeEnum.ResponseCode_500;
            SimpleResponseDto<T> dto = NewInstance();
            dto.Code = ((int)responseCodeEnum).ToString();
            dto.Message = responseCodeEnum.GetMessage();
            dto.ServerTime = DateTime.Now.Ticks;

            return dto;
        }
    }
}
