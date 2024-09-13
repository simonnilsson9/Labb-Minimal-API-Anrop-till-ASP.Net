using Labb__Minimal_API___Anrop_till_ASP.Net.Models;
using Labb__Minimal_API___Anrop_till_ASP.Net.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web_Application.Models;
using Web_Application.Services;

namespace Web_Application.Controllers
{
	public class BookController : Controller
	{
		private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

		public async Task<IActionResult> Start()
		{
			return View();
		}

		public async Task<IActionResult> BooksByGenre(string genre)
		{
            if (string.IsNullOrWhiteSpace(genre))
            {
                ViewBag.ShowResults = false;
                return View(Enumerable.Empty<BookInfoDTO>()); // Return an empty list to avoid displaying any books
            }

            List<BookInfoDTO> list = new List<BookInfoDTO>();
            var response = await _bookService.GetBooksByGenre<ResponseDTO>(genre);

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<BookInfoDTO>>(Convert.ToString(response.Result));
                ViewBag.ShowResults = list.Any(); // Set this to true if there are results
            }
            else
            {
                ViewBag.ShowResults = false; // No results
            }
            ViewBag.genre = genre.ToUpper();
            return View(list);
        }
        public async Task<IActionResult> BooksByAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {                
                ViewBag.ShowResults = false;
                return View(Enumerable.Empty<BookInfoDTO>()); // Return an empty list to avoid displaying any books
            }

            List<BookInfoDTO> list = new List<BookInfoDTO>();
            var response = await _bookService.GetBooksByAuthor<ResponseDTO>(author);

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<BookInfoDTO>>(Convert.ToString(response.Result));
                ViewBag.ShowResults = list.Any(); // Set this to true if there are results
            }
            else
            {
                ViewBag.ShowResults = false; // No results
            }
			ViewBag.Authorname = author.ToUpper();
            return View(list);
        }


        public async Task<IActionResult> BookIndex()
		{
			List<BookInfoDTO> list = new List<BookInfoDTO>();
			var response = await _bookService.GetAllBooks<ResponseDTO>();

			if(response != null && response.IsSuccess)
			{
				list = JsonConvert.DeserializeObject<List<BookInfoDTO>>(Convert.ToString(response.Result));
            }
            return View(list);

        }

		public async Task<IActionResult> BookDetailsByTitle(string title)
		{
			BookInfoDTO bDTO = new BookInfoDTO();

			var response = await _bookService.GetBookByTitle<ResponseDTO>(title);

			if(response != null && response.IsSuccess)
			{
                BookInfoDTO model = JsonConvert.DeserializeObject<BookInfoDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            ViewBag.ErrorMessage = "The book was not found. Please try searching for a different title.";
            return View();
		}
		
		public async Task<IActionResult> BookDetails(int id)
		{
			BookInfoDTO bDTO = new BookInfoDTO();

			var response = await _bookService.GetBookById<ResponseDTO>(id);

			if(response != null && response.IsSuccess)
			{
				BookInfoDTO model = JsonConvert.DeserializeObject<BookInfoDTO>(Convert.ToString(response.Result));
				return View(model);
			}

			return View();
		}

		public async Task<IActionResult> CreateBook()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateBook(BookCreateDTO model)
		{
			if(ModelState.IsValid)
			{
				var response = await _bookService.CreateBookAsync<ResponseDTO>(model);

                if ((response != null && response.IsSuccess))
                {
					return RedirectToAction(nameof(BookIndex));
                }
            }

			return View(model);
		}

		public async Task<IActionResult> UpdateBook(int id)
		{
			var response = await _bookService.GetBookById<ResponseDTO>(id);

			if(response != null && response.IsSuccess)
			{
				BookInfoDTO model = JsonConvert.DeserializeObject<BookInfoDTO>(Convert.ToString(response.Result));
				return View(model);
			}

			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> UpdateBook(BookInfoDTO model)
		{
			if(ModelState.IsValid)
			{
				var response = await _bookService.UpdateBookAsync<ResponseDTO>(model);

				if(response != null && response.IsSuccess)
				{
					return RedirectToAction(nameof(BookIndex));
				}
			}

			return View(model);
		}

		public async Task<IActionResult> DeleteBook(int id)
		{
			var response = await _bookService.GetBookById<ResponseDTO>(id);

			if(response != null && response.IsSuccess)
			{
				BookInfoDTO model = JsonConvert.DeserializeObject<BookInfoDTO>(Convert.ToString(response.Result));
				return View(model);
			}

			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> DeleteBook(BookInfoDTO model)
		{
			if (ModelState.IsValid)
			{
				var response = await _bookService.DeleteBookAsync<ResponseDTO>(model.ID);

				if (response != null && response.IsSuccess)
				{
					return RedirectToAction(nameof(BookIndex));
				}
			}

			return View(model);
		}

	}
}
