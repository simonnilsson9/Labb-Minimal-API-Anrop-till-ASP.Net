using Labb__Minimal_API___Anrop_till_ASP.Net.Models.DTO;

namespace Web_Application.Services
{
	public class BookService : BaseService, IBookService
	{
		private readonly IHttpClientFactory _httpClientFactory;

        public BookService(IHttpClientFactory httpClient) : base(httpClient) 
        {
            _httpClientFactory = httpClient;
        }
        public async Task<T> CreateBookAsync<T>(BookCreateDTO bookDTO)
		{
			return await SendAsync<T>(new Models.APIRequest
			{
				ApiType = StaticDetails.ApiType.POST,
				Data = bookDTO,
				URL = StaticDetails.BookApiBase + "/api/book",
				AccessToken = ""
			});
		}

		public async Task<T> DeleteBookAsync<T>(int id)
		{
			return await SendAsync<T>(new Models.APIRequest
			{
				ApiType = StaticDetails.ApiType.DELETE,
				Data = id,
				URL = StaticDetails.BookApiBase + "/api/book/" + id,
				AccessToken = ""
			});
		}

		public async Task<T> GetAllBooks<T>()
		{
			return await SendAsync<T>(new Models.APIRequest()
			{
				ApiType = StaticDetails.ApiType.GET,
				URL = StaticDetails.BookApiBase + "/api/books",
				AccessToken = ""
			});
		}

		public async Task<T> GetBookById<T>(int id)
		{
			return await SendAsync<T>(new Models.APIRequest
			{
				ApiType = StaticDetails.ApiType.GET,
				URL = StaticDetails.BookApiBase + "/api/book/" + id,
				AccessToken = ""
			});
		}

        public async Task<T> GetBookByTitle<T>(string title)
        {
			return await SendAsync<T>(new Models.APIRequest
			{
				ApiType = StaticDetails.ApiType.GET,
				URL = StaticDetails.BookApiBase + "/api/book/" + title,
				AccessToken = ""
			});
        }

        public async Task<T> GetBooksByAuthor<T>(string author)
        {
            return await SendAsync<T>(new Models.APIRequest()
            {
                ApiType = StaticDetails.ApiType.GET,
                URL = StaticDetails.BookApiBase + "/api/books/" + author,
                AccessToken = ""
            });
        }

        public async Task<T> GetBooksByGenre<T>(string genre)
        {
            return await SendAsync<T>(new Models.APIRequest()
            {
                ApiType = StaticDetails.ApiType.GET,
                URL = StaticDetails.BookApiBase + "/api/books/genre/" + genre,
                AccessToken = ""
            });
        }

        public async Task<T> UpdateBookAsync<T>(BookInfoDTO bookDTO)
		{
			return await SendAsync<T>(new Models.APIRequest
			{
				ApiType = StaticDetails.ApiType.PUT,
				Data = bookDTO,
				URL = StaticDetails.BookApiBase + "/api/book",
				AccessToken = ""
			});
		}
	}
}
