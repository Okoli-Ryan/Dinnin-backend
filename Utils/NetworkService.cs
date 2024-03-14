using Newtonsoft.Json;

namespace OrderUp_API.Utils {
    public class NetworkService {

        private readonly HttpClient client;
        public NetworkService() {
            HttpClient client = new();

            this.client = client;
        }

        public NetworkService(Dictionary<String, string> headers) {

            HttpClient client = new();

            foreach (var header in headers) {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            this.client = client;
        }

        public async Task<DefaultResponse<ResponseBodyType>> Post<RequestBodyType, ResponseBodyType>(string url, RequestBodyType body, Dictionary<string, string>? headers) {

            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
            request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            if (headers is not null) {

                foreach (var header in headers) {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            try {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();


                var responseBodyString = await response.Content.ReadAsStringAsync();

                if (String.IsNullOrEmpty(responseBodyString)) {
                    return new DefaultSuccessResponse<ResponseBodyType>(default);
                }


                return new DefaultResponse<ResponseBodyType>() {
                    ResponseData = JsonConvert.DeserializeObject<ResponseBodyType>(responseBodyString),
                    ResponseCode = ResponseCodes.SUCCESS,
                    ResponseMessage = ResponseMessages.SUCCESS
                };

            }
            catch (Exception ex) {

                Debug.WriteLine(ex);

                return new DefaultErrorResponse<ResponseBodyType>() {
                    ResponseData = default,
                    ResponseCode = ResponseCodes.FAILURE,
                    ResponseMessage = ResponseMessages.FAILURE
                };

            }
        }
    }
}
