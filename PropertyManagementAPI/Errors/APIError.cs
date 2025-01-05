using System.Text.Json;

namespace PropertyManagementAPI.Errors
{
    public class APIError
    {
        public APIError(int ErrorCode, string ErrorMessage, string ErrorDetails=null)
        {
            this.ErrorCode = ErrorCode;
            this.ErrorMessage = ErrorMessage;
            this.ErrorDetails = ErrorDetails;
        }

        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}
