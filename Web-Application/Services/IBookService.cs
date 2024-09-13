using Labb__Minimal_API___Anrop_till_ASP.Net.Models.DTO;

namespace Web_Application.Services
{
	public interface IBookService
	{
		Task<T> GetAllBooks<T>();
		Task<T> GetBookById<T>(int id);
		Task<T> GetBooksByAuthor<T>(string author);
		Task<T> GetBooksByGenre<T>(string genre);
		Task<T> CreateBookAsync<T>(BookCreateDTO bookDTO);
		Task<T> UpdateBookAsync<T>(BookInfoDTO bookDTO);
		Task<T> DeleteBookAsync<T>(int id);
		Task<T> GetBookByTitle<T>(string title);
	}
}
