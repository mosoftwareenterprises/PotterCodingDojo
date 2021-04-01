using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotterCodingDojo.Shared
{
    public class PotterBusinessLogic
    {
        public async Task<decimal> CalculateBestPrice( List<BookInBasket> bookInBaskets )
        {
            if (bookInBaskets.Count < 1)
            {
                return 0.0M;
            }
            else if (bookInBaskets.Count == 1)
            {
                return 8.0M;
            }
            else
            {
                //The trick is that you have to write some code that is intelligent enough to notice that two sets of four books is cheaper than a set of five and a set of three.
                //We need to find out how many sets of each type we have first, and THEN do the calculation


                //We have more than 1 book in the basket
                int totalNumberOfBooks = bookInBaskets.Count();
                int totalNonDiscountedBooks = 0;
                int totalUniqueSetsOfFive = 0;
                int totalUniqueSetsOfFour = 0;
                int totalUniqueSetsOfThree = 0;
                int totalUniqueSetsOfTwo = 0;

                Dictionary<int, int> bookCounters = new Dictionary<int, int>();//Id, Count of books
                bookCounters.Add( 1, bookInBaskets.Count( t => t.Id == 1 ) );
                bookCounters.Add( 2, bookInBaskets.Count( t => t.Id == 2 ) );
                bookCounters.Add( 3, bookInBaskets.Count( t => t.Id == 3 ) );
                bookCounters.Add( 4, bookInBaskets.Count( t => t.Id == 4 ) );
                bookCounters.Add( 5, bookInBaskets.Count( t => t.Id == 5 ) );

                //Remove all empty items
                bookCounters = RemoveEmptyCounters( bookCounters );

                //So we have a dictionary of the only items on order.
                if (bookCounters.Count == 5)
                {
                    //How many unique complete sets of books do we have?
                    totalUniqueSetsOfFive = Math.Min( bookCounters[1], Math.Min( bookCounters[2], Math.Min( bookCounters[3], Math.Min( bookCounters[4], bookCounters[5] ) ) ) );

                    bookCounters = ReduceCounters( bookCounters, totalUniqueSetsOfFive );

                    //So we have potential incomplete sets now
                    totalNumberOfBooks -= (totalUniqueSetsOfFive * 5);
                }

                bookCounters = RemoveEmptyCounters( bookCounters );


                if (bookCounters.Count == 4)
                {
                    totalUniqueSetsOfFour = Math.Min( bookCounters.ElementAt( 0 ).Value, Math.Min( bookCounters.ElementAt( 1 ).Value, Math.Min( bookCounters.ElementAt( 2 ).Value, bookCounters.ElementAt( 3 ).Value ) ) );
                    bookCounters = ReduceCounters( bookCounters, totalUniqueSetsOfFour );
                    totalNumberOfBooks -= (totalUniqueSetsOfFour * 4);
                }

                bookCounters = RemoveEmptyCounters( bookCounters );

                if (bookCounters.Count == 3)
                {
                    totalUniqueSetsOfThree = Math.Min( bookCounters.ElementAt( 0 ).Value, Math.Min( bookCounters.ElementAt( 1 ).Value, bookCounters.ElementAt( 2 ).Value ) );
                    bookCounters = ReduceCounters( bookCounters, totalUniqueSetsOfThree );
                    totalNumberOfBooks -= (totalUniqueSetsOfThree * 3);
                }

                bookCounters = RemoveEmptyCounters( bookCounters );

                if (bookCounters.Count == 2)
                {
                    totalUniqueSetsOfTwo = Math.Min( bookCounters.ElementAt( 0 ).Value, bookCounters.ElementAt( 1 ).Value );
                    bookCounters = ReduceCounters( bookCounters, totalUniqueSetsOfTwo );
                    totalNumberOfBooks -= (totalUniqueSetsOfTwo * 2);
                }


                //Now what is left?
                totalNonDiscountedBooks = totalNumberOfBooks;


                decimal totalPrice =
                    (totalUniqueSetsOfFive * (5.0M * 8.0M * 0.75M)) +
                    (totalUniqueSetsOfFour * (4.0M * 8.0M * 0.80M)) +
                    (totalUniqueSetsOfThree * (3.0M * 8.0M * 0.90M)) +
                    (totalUniqueSetsOfTwo * (2.0M * 8.0M * 0.95M)) +
                    (totalNonDiscountedBooks * 8.0M);


                if (totalUniqueSetsOfFive >= 1 && totalUniqueSetsOfThree >= 1)
                {
                    totalPrice =
                    ((totalUniqueSetsOfFive - 1) * (5.0M * 8.0M * 0.75M)) +
                    ((totalUniqueSetsOfFour + 2) * (4.0M * 8.0M * 0.80M)) +
                    ((totalUniqueSetsOfThree - 1) * (3.0M * 8.0M * 0.90M)) +
                    (totalUniqueSetsOfTwo * (2.0M * 8.0M * 0.95M)) +
                    (totalNonDiscountedBooks * 8.0M);
                }


                return totalPrice;
            }

            return decimal.MaxValue;
        }

        private Dictionary<int, int> ReduceCounters( Dictionary<int, int> bookCounters, int totalUniqueSetsOfFive )
        {
            Dictionary<int, int> reducedList = new Dictionary<int, int>();
            foreach (var item in bookCounters)
            {
                reducedList.Add( item.Key, item.Value - totalUniqueSetsOfFive );
            }
            return reducedList;
        }

        private static Dictionary<int, int> RemoveEmptyCounters( Dictionary<int, int> listContainingItemsToRemove )
        {
            Dictionary<int, int> sanitisedList = new Dictionary<int, int>();
            foreach (var item in listContainingItemsToRemove.Where( t => t.Value > 0 ))
            {
                sanitisedList.Add( item.Key, item.Value );
            }
            return sanitisedList;
        }
    }
}
