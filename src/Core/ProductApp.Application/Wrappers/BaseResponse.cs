using System;

namespace ProductApp.Application.Wrappers
{
    public class BaseResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Message { get; set; }
        public bool IsSuccess { get; set; } = true;
    }
}
