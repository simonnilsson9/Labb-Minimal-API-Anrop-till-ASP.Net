using AutoMapper;
using Labb__Minimal_API___Anrop_till_ASP.Net.Models;
using Labb__Minimal_API___Anrop_till_ASP.Net.Models.DTO;
using Labb__Minimal_API___Anrop_till_ASP.Net.Repository;

namespace Labb__Minimal_API___Anrop_till_ASP.Net.EndPoints
{
	public static class ApiEndpoints
	{

		public static void ConfigurationEndPoints(this WebApplication app)
		{
			app.MapGet("/api/books", GetAllBooks).WithName("GetBooks").Produces<APIResponse>();

            app.MapGet("/api/books/{author}", GetBooksByAuthor).WithName("GetBooksByAuthor").Produces<APIResponse>();

            app.MapGet("/api/books/genre/{genre}", GetBooksByGenre).WithName("GetBooksByGenre").Produces<APIResponse>();

            app.MapGet("/api/book/{id:int}", GetBookById).WithName("GetBook").Produces<APIResponse>();

            app.MapGet("/api/book/{title}", GetBookByTitle).WithName("GetBookByTitle").Produces<APIResponse>();

            app.MapPost("/api/book", CreateBook).WithName("CreateBook").Accepts<BookCreateDTO>("application/json").Produces(201).Produces(400);

			app.MapPut("/api/book", UpdateBook).WithName("UpdateBook").Accepts<BookInfoDTO>("application/json").Produces<BookInfoDTO>(200).Produces(400);

			app.MapDelete("/api/book/{id:int}", DeleteBook).WithName("DeleteBook");
		}

		private async static Task<IResult> GetAllBooks(IBookRepository bookRepository)
		{
			APIResponse response = new APIResponse();

			response.Result = await bookRepository.GetAllBooksAsync();
			response.IsSuccess = true;
			response.StatusCode = System.Net.HttpStatusCode.OK;

			return Results.Ok(response);
		}

		private async static Task<IResult> GetBookById(IBookRepository bookRepository, int id)
		{
			APIResponse response = new APIResponse();
			
			var book = await bookRepository.GetBookAsyncById(id);
			if(book == null)
			{
				response.IsSuccess = false;
				response.StatusCode = System.Net.HttpStatusCode.NotFound;
				response.ErrorMessages.Add("No book was found. Invalid ID.");
				return Results.NotFound(response);
			}

			response.Result = book;
			response.IsSuccess = true;
			response.StatusCode= System.Net.HttpStatusCode.OK;

			return Results.Ok(response);
		}

		private async static Task<IResult> GetBookByTitle(IBookRepository bookRepository, string title)
		{
			APIResponse response = new APIResponse();

			var book = await bookRepository.GetBookAsyncByTitle(title);
			if(book == null)
			{
				response.IsSuccess = false;
				response.StatusCode = System.Net.HttpStatusCode.NotFound;
				response.ErrorMessages.Add("No book with that title was found. Try again.");
				return Results.NotFound(response);
			}

			response.Result = book;
			response.IsSuccess = true;
			response.StatusCode = System.Net.HttpStatusCode.OK;

			return Results.Ok(response);
		}

        private async static Task<IResult> GetBooksByAuthor(IBookRepository bookRepository, string author)
        {
            APIResponse response = new APIResponse();
            var books = await bookRepository.GetBooksByAuthorAsync(author);

            if (books == null || !books.Any())
            {
                response.IsSuccess = false;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.ErrorMessages.Add("No books were found with that author.");
                return Results.NotFound(response);
            }

            response.Result = books;
            response.IsSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.OK;

            return Results.Ok(response);
        }

		private async static Task<IResult> GetBooksByGenre(IBookRepository bookRepository, string genre)
		{
			APIResponse response = new APIResponse();
            var books = await bookRepository.GetBooksByGenre(genre);

            if (books == null || !books.Any())
            {
                response.IsSuccess = false;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.ErrorMessages.Add("No books were found in that genre");
                return Results.NotFound(response);
            }

            response.Result = books;
            response.IsSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.OK;

            return Results.Ok(response);
        }

        private async static Task<IResult> CreateBook(IBookRepository bookRepository, IMapper mapper, BookCreateDTO bookCreateDTO)
		{
			APIResponse response = new() {IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

			if(bookRepository.GetBookAsyncByTitle(bookCreateDTO.Title).GetAwaiter().GetResult() != null)
			{
				response.ErrorMessages.Add("Book already exists!");
				return Results.BadRequest(response);
			}

			Book book = mapper.Map<Book>(bookCreateDTO);
			await bookRepository.CreateBookAsync(book);
			await bookRepository.SaveAsync();
			BookInfoDTO bookInfoDTO = mapper.Map<BookInfoDTO>(book);

			response.Result = bookInfoDTO;
			response.IsSuccess = true;
			response.StatusCode = System.Net.HttpStatusCode.OK;

			return Results.Ok(response);
		}

		private async static Task<IResult> UpdateBook(IBookRepository bookRepository, IMapper mapper, BookInfoDTO bookUpdateDTO)
		{
			APIResponse response = new() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

			await bookRepository.UpdateBookAsync(mapper.Map<Book>(bookUpdateDTO));
			await bookRepository.SaveAsync();

			response.Result = mapper.Map<BookInfoDTO>(await bookRepository.GetBookAsyncById(bookUpdateDTO.ID));
			response.IsSuccess = true;
			response.StatusCode = System.Net.HttpStatusCode.OK;

			return Results.Ok(response);
		}

		private async static Task<IResult> DeleteBook(IBookRepository bookRepository, int id)
		{
			APIResponse response = new() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

			Book bookToDelete = await bookRepository.GetBookAsyncById(id);

			if(bookToDelete != null)
			{
				await bookRepository.DeleteBookAsync(bookToDelete);
				await bookRepository.SaveAsync();

				response.IsSuccess = true;
				response.StatusCode = System.Net.HttpStatusCode.NoContent;

				return Results.Ok(response);
			}
			else
			{
				response.ErrorMessages.Add("No book was found. Invalid ID.");
				return Results.BadRequest(response);
			}
		}
	}
}
