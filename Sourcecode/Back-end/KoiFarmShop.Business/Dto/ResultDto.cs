using Microsoft.Identity.Client;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KoiFarmShop.Business.Dto
{
    public class ResultDto
    {
        public bool IsSuccess { get; set; }
        public int Code { get; set; }
        public object? Data { get; set; }
        public string? Message { get; set; }

        public void success()
        {
            IsSuccess = true;
            Code = 400;
            Data = null;
            Message = "Success";
        }
        public void success(object data)
        {
            IsSuccess = true;
            Code = 400;
            Data = data;
            Message = "Success";
        }

        public void success(object data, string message) 
        {
            IsSuccess = true;
            Code = 400;
            Data = data;
            Message = message;
        }

        public void error(int code, string message)
        {
            IsSuccess = false;
            Code = code;
            Data = null;
            Message = message;
        }
        
    }

    
}
