using System.Security.AccessControl;
using static Web_Application.Services.StaticDetails;

namespace Web_Application.Models
{
	public class APIRequest
	{
		public ApiType ApiType { get; set; }
		public string URL { get; set; }

		public object Data { get; set; }
		public string AccessToken { get; set; }
	}
}

