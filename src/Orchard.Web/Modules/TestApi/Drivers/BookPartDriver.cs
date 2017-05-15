using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using TestApi.Models;

namespace TestApi.Drivers
{
    public class BookPartDriver : ContentPartDriver<BookPart>
    {
        protected override string Prefix { get { return "Book"; } }

        protected override DriverResult Editor(BookPart part, dynamic shapeHelper) {
            return ContentShape("Parts_Book_Edit", () => shapeHelper
                .EditorTemplate(TemplateName: "Parts/Book", Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(BookPart part, IUpdateModel updater, dynamic shapeHelper) {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }

        protected override DriverResult Display(BookPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_Book", () => shapeHelper.Parts_Book(
                Name: part.Name,
                Author: part.Author,
                Year: part.Year,
                Id: part.Id
            ));
        }
    }
}