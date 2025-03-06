namespace EventManagerAPI.DTO.Response
{
	public class ApiResponse
	{
		public int Code { get; set; }
		public string Message { get; set; }
		public object Data { get; set; }

		public bool IsSuccess { get; set; }
	}
}
