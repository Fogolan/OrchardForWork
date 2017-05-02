using Orchard.ContentManagement;

namespace TestApi.Models
{
    public class BookPart : ContentPart<BookPartRecord>
    {
        public string Name
        {
            get { return Record.Name; }
            set { Record.Name = value; }
        }

        public string Author
        {
            get { return Record.Author; }
            set { Record.Author = value; }
        }

        public int Year
        {
            get { return Record.Year; }
            set { Record.Year = value; }
        }
    }
}