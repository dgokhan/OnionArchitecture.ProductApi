﻿using System;

namespace ProductApp.Application.Wrappers
{
    public class ServiceResponse<T> : BaseResponse
    {
        public T Value { get; set; }

        public ServiceResponse(T value)
        {
            this.Value = value;
        }
    }
}
