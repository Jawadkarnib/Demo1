using System.Globalization;
using Lab1._2.Abstraction;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1._2.Queries.GetDate
{
    public class GetDateHandler : IQueryHandler<GetDate, string>
    {
        public async Task<string> Handle(GetDate request, CancellationToken cancellationToken)
        {
            try
            {
                CultureInfo culture = new CultureInfo(request.language);
                string formattedDate = DateTime.Now.ToString("F", culture);
                return formattedDate;
            }
            catch (Exception ex)
            {
                throw new CustomApplicationException($"Error occurred in GetDateHandler: {ex.Message}", ex, 500);
            }
        }
    }
}