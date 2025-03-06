namespace EventManagerAPI.DTO.Response
{
    public class ServiceResult
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }

        public static ServiceResult Success() => new ServiceResult { Succeeded = true };
        public static ServiceResult Failure(string message) => new ServiceResult { Succeeded = false, Message = message };
    }
}

