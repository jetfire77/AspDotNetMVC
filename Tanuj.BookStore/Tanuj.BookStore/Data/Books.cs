namespace Tanuj.BookStore.Data
{

    // this class because we are using code first apporach 




    public class Books
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string Category { get; set; }

        public string Language { get; set; }

        public int TotalPages { get; set; }
    }
}
