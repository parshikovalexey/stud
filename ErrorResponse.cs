using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApplication2.Models
{
    public enum ErrorCodes
    {
        InvalidWeightId = 1,
        WeightNotFound = 2,
        InvalidWeightModel = 3,
        SaveWeightError = 4,
        InvalidUserInfoModel = 5,
        InvalidUserID = 6,
        ChangePasswordError = 7,
        InvalidLikesModel = 8
    }


    public class ErrorResponse
    {
        public ErrorCodes ErrorCode { get; set; }
        public string Message { get; set; }
        public int Index { get; set; }

        public ErrorResponse()
        {
        }

        public ErrorResponse(ErrorCodes ErrorCode, string Message, int IndexOfError) // добавил конструктор
        {
            this.ErrorCode = ErrorCode;
            this.Message = Message;
        }

        public ErrorResponse(ErrorCodes ErrorCode, string Message)
        {
            this.ErrorCode = ErrorCode;
            this.Message = Message;
        }
    }
}