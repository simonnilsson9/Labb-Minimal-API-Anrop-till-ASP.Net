﻿using AutoMapper.Internal;
using Labb__Minimal_API___Anrop_till_ASP.Net.Models;
using Newtonsoft.Json;
using System.Text;
using Web_Application.Models;

namespace Web_Application.Services
{
	public class BaseService : IBaseService
	{
		public ResponseDTO responseModel { get; set; }
		public IHttpClientFactory _httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            responseModel = new ResponseDTO();
        }
        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = _httpClient.CreateClient("LabbWebAPI");

                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.URL);
                client.DefaultRequestHeaders.Clear();

                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }
                HttpResponseMessage apiResponse = null;

                switch (apiRequest.ApiType)
                {
                    case StaticDetails.ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case StaticDetails.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case StaticDetails.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case StaticDetails.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        break;

                }

                apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResponseDTO = JsonConvert.DeserializeObject<T>(apiContent);

                return apiResponseDTO;
            }
            catch (Exception e)
            {
                var DTO = new ResponseDTO()
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string>()
                    {
                        Convert.ToString(e.Message)
                    },
                    IsSuccess = false
                };

                var result = JsonConvert.SerializeObject(DTO);
                var apiResponseDTO = JsonConvert.DeserializeObject<T>(result);

                return apiResponseDTO;
            }
        }

            public void Dispose()
		{
			GC.SuppressFinalize(true);
		}
	}
}
