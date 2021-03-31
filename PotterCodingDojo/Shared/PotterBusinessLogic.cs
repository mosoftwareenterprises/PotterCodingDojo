using System.Collections.Generic;
using System.Threading.Tasks;

namespace PotterCodingDojo.Shared
{
    public class PotterBusinessLogic
    {
        public async Task<decimal> CalculateBestPrice( List<BookInBasket> bookInBaskets )
        {
            if (bookInBaskets.Count == 1)
            {
                return 8.0M;
            }
            else if (bookInBaskets.Count == 2)
            {
                return 8.0M*2.0M*0.95M;
            }
            else if (bookInBaskets.Count == 3)
            {
                return 8.0M * 3.0M * 0.90M;
            }
            else if (bookInBaskets.Count == 4)
            {
                return 8.0M * 4.0M * 0.80M;
            }
            else if (bookInBaskets.Count == 5)
            {
                return 8.0M * 5.0M * 0.75M;
            }
            return decimal.MaxValue;
        }
    }
}
