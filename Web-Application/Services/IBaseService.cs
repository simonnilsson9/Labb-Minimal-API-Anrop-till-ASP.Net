using Web_Application.Models;

namespace Web_Application.Services
{
	public interface IBaseService : IDisposable
	{
		ResponseDTO responseModel { get; set; }
		Task<T> SendAsync<T>(APIRequest request);
	}
}
