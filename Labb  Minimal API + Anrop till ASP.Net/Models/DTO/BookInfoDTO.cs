
namespace Labb__Minimal_API___Anrop_till_ASP.Net.Models.DTO

{
	public class BookInfoDTO
	{
		public int ID { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public int PublicationYear { get; set; }
		public string Genre { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
    }
}
