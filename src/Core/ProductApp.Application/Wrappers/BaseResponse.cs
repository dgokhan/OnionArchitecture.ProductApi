using System;
using System.Collections;

namespace ProductApp.Application.Wrappers
{
    public class BaseResponse
    {
        public string Message { get; }
        public ResponseType ResponseType { get; }

        public BaseResponse(string message)
        {
            Message = message;
        }

        public BaseResponse(ResponseType responseType, string errorMessage)
        {
            ResponseType = responseType;
            Message = errorMessage;
        }
    }
    public enum ResponseType
    {
        OK = 1,
        EmptyValue,
        ExceptionError,
        SqlException,
        CreateError,
        ReadError,
        UpdateError,
        DeleteError,
        Error
    }
    public interface IResult<out T>
    {
        bool Succeeded { get; }
        T Value { get; }
        BaseResponse Info { get; }
    }
    public class Result<T> : IResult<T>
    {
        public Result(bool succeeded, T value, BaseResponse info)
        {
            Succeeded = succeeded;
            Value = value;
            Info = info;
        }

        public Result(bool succeeded, BaseResponse info)
        {
            Succeeded = succeeded;
            Info = info;
        }

        public Result(bool succeeded, T value)
        {
            Succeeded = succeeded;
            Value = value;
        }

        public bool Succeeded { get; }
        public T Value { get; }
        public BaseResponse Info { get; } = new BaseResponse("");
    }
    public static class Result
    {
        public static IResult<T> Func<T>()
        {
            return new Result<T>(true, new BaseResponse(ResponseType.OK, "No errors detected"));
        }

        public static IResult<T> Func<T>(T resValue)
        {
            try
            {
                ICollection collection = resValue as ICollection;
                if (resValue is not null && collection.Count is not 0)
                {
                    return new Result<T>(true, resValue, new BaseResponse(ResponseType.OK, "No errors detected"));
                }
                else
                    return new Result<T>(false, resValue, new BaseResponse(ResponseType.EmptyValue, "Null value detected!"));
            }
            catch (Exception ex)
            {
                return new Result<T>(false, resValue, new BaseResponse(ResponseType.ExceptionError, ex.Message));
            } 
        } 
    }
}
