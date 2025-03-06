//using RestSharp;

//namespace EventManagerAPI.Services
//{
//    public class ChatGPTService
//    {
//        private readonly string _apiKey;

//        public ChatGPTService(string apiKey)
//        {
//            _apiKey = apiKey;
//        }

//        public async Task<string> SendMessageAsync(string message)
//        {
//            var client = new RestClient("https://api.openai.com/v1/chat/completions");
//            var request = new RestRequest();

//            // Set the method
//            request.Method = Method.Post; // Updated from Method.POST to Method.Post

//            // Set the headers
//            request.AddHeader("Authorization", $"Bearer {_apiKey}");
//            request.AddHeader("Content-Type", "application/json");

//            // Create the request body
//            var body = new
//            {
//                model = "gpt-3.5-turbo", // or use "gpt-4" if you have access
//                messages = new[]
//                {
//                new { role = "user", content = message }
//            }
//            };

//            request.AddJsonBody(body);

//            // Execute the request
//            var response = await client.ExecuteAsync(request);

//            // Check if the request was successful
//            if (response.IsSuccessful)
//            {
//                // Extract the response content
//                return response.Content;
//            }

//            // Handle error response
//            throw new Exception($"Error: {response.StatusCode}, Message: {response.Content}");

//            //return "Hello, I am a chatbot!";
//        }
//    }
//}
