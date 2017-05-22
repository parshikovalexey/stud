using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stud.webapi.Models
{
    public enum ErrorCodes
    {
         InvalidItemId = 1,
         ItemsNotFound = 2,
         InvalidItemModel = 3,
         SaveItemError = 4,
         ItemAlreadyExists = 5,
         UpdateItemError = 6
     }
    public class ErrorResponse
    {
         public ErrorCodes ErrorCode { get; set; }
         public string Message { get; set; }
 
         public ErrorResponse()
         {

         }
 
         public ErrorResponse(ErrorCodes ErrorCode, string Message)
         {
            this.ErrorCode = ErrorCode;
            this.Message = Message;
         }
    }
}