using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApplication2.Models
{
    public enum ErrorCodes {
        BadWeightId = 1,
        WeightNotFound = 2
    }


    public class ErrorResponse {
        public ErrorCodes ErrorCode { get; set; }
        public string Message { get; set; }

        public ErrorResponse() {
        }

        public ErrorResponse(ErrorCodes ErrorCode, string Message) {
            this.ErrorCode = ErrorCode;
            this.Message = Message;
        }
    }
}