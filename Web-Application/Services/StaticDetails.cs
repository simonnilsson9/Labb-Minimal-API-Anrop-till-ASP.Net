namespace Web_Application.Services
{
	public class StaticDetails
	{
        public static string BookApiBase { get; set; }

		public enum ApiType
		{
			GET, POST, PUT, DELETE
		}
    }
}
