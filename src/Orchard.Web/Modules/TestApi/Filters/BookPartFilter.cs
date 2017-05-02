using Orchard.Localization;
using Orchard.Projections.Descriptors.Filter;
using TestApi.Models;
using IFilterProvider = Orchard.Projections.Services.IFilterProvider;

namespace TestApi.Filters
{
    public class BookPartFilter : IFilterProvider
    {
        public Localizer T { get; set; }

        public BookPartFilter()
        {
            T = NullLocalizer.Instance;
        }

        public void Describe(DescribeFilterContext describe)
        {
            describe.For(
                    "Content",
                    T("Content"),
                    T("Content"))

                .Element(
                    "BookParts",
                    T("Book Parts"),
                    T("Book parts"),
                    ApplyFilter,
                    DisplayFilter
                );
        }

        private void ApplyFilter(FilterContext context)
        {
            context.Query = context.Query.Join(x => x.ContentPartRecord(typeof(BookPartRecord)));
        }

        private LocalizedString DisplayFilter(FilterContext context)
        {
            return T("Content with BookPart");
        }
    }
}