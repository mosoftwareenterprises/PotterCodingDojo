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
                decimal currentPrice = 0M;
                Dictionary<int, int> bookCounters = new Dictionary<int, int>();//Id, Count of books
                bookCounters.Add( 1, bookInBaskets.Count( t => t.Id == 1 ) );
                bookCounters.Add( 2, bookInBaskets.Count( t => t.Id == 2 ) );
                bookCounters.Add( 3, bookInBaskets.Count( t => t.Id == 3 ) );
                bookCounters.Add( 4, bookInBaskets.Count( t => t.Id == 4 ) );
                bookCounters.Add( 5, bookInBaskets.Count( t => t.Id == 5 ) );

                //Remove all empty items
                Dictionary<int, int> bookCountersTemp = new Dictionary<int, int>();
                foreach (var item in bookCounters.Where( t => t.Value > 0 ))
                {
                    bookCountersTemp.Add( item.Key, item.Value );
                }
                bookCounters.Clear();

                //So we have a dictionary of the only items on order.
                if (bookCountersTemp.Count == 5)
                {
                    //We have a complete set, so lets find it.


                    //How many unique complete sets of books do we have?
                    totalUniqueSetsOfFive = Math.Min( bookCountersTemp[1], Math.Min( bookCountersTemp[2], Math.Min( bookCountersTemp[3], Math.Min( bookCountersTemp[4], bookCountersTemp[5] ) ) ) );
                    //So we know the price is one complete set of 5x8x0.75
                    currentPrice = totalUniqueSetsOfFive * (5.0M * 8.0M * 0.75M);
                    bookCountersTemp[1] -= totalUniqueSetsOfFive;
                    bookCountersTemp[2] -= totalUniqueSetsOfFive;
                    bookCountersTemp[3] -= totalUniqueSetsOfFive;
                    bookCountersTemp[4] -= totalUniqueSetsOfFive;
                    bookCountersTemp[5] -= totalUniqueSetsOfFive;

                    //So we have potential incomplete sets now
                    totalNumberOfBooks -= (totalUniqueSetsOfFive * 5);
                }



                //Remove all empty items
                foreach (var item in bookCountersTemp.Where( t => t.Value > 0 ))
                {
                    bookCounters.Add( item.Key, item.Value );
                }


                //Deal with 4 items now


                if (bookCounters.Count == 4)
                {
                    totalUniqueSetsOfFour = Math.Min( bookCounters.ElementAt( 0 ).Value, Math.Min( bookCounters.ElementAt( 1 ).Value, Math.Min( bookCounters.ElementAt( 2 ).Value, bookCounters.ElementAt( 3 ).Value ) ) );

                    currentPrice += totalUniqueSetsOfFour * (4.0M * 8.0M * 0.80M);
                    bookCounters.Clear();
                    foreach (var item in bookCounters.Where( t => t.Value > 0 ))
                    {
                        bookCounters.Add( item.Key, item.Value - totalUniqueSetsOfFour );
                    }
                    totalNumberOfBooks -= (totalUniqueSetsOfFour * 4);
                }

                bookCountersTemp.Clear();
                //Populate the next list
                foreach (var item in bookCounters.Where( t => t.Value > 0 ))
                {
                    bookCountersTemp.Add( item.Key, item.Value );
                }


                if (bookCountersTemp.Count == 3)
                {
                    totalUniqueSetsOfThree = Math.Min( bookCountersTemp.ElementAt( 0 ).Value, Math.Min( bookCountersTemp.ElementAt( 1 ).Value, bookCountersTemp.ElementAt( 2 ).Value ) );

                    currentPrice += totalUniqueSetsOfThree * (3.0M * 8.0M * 0.90M);
                    bookCounters.Clear();
                    foreach (var item in bookCountersTemp.Where( t => t.Value > 0 ))
                    {
                        bookCounters.Add( item.Key, item.Value - totalUniqueSetsOfThree );
                    }
                    totalNumberOfBooks -= (totalUniqueSetsOfThree * 3);
                }

                bookCountersTemp.Clear();
                //Populate the next list
                foreach (var item in bookCounters.Where( t => t.Value > 0 ))
                {
                    bookCountersTemp.Add( item.Key, item.Value );
                }
                if (bookCountersTemp.Count == 2)
                {
                    totalUniqueSetsOfTwo = Math.Min( bookCountersTemp.ElementAt( 0 ).Value, bookCountersTemp.ElementAt( 1 ).Value );

                    currentPrice += totalUniqueSetsOfTwo * (2.0M * 8.0M * 0.95M);
                    bookCounters.Clear();
                    foreach (var item in bookCountersTemp.Where( t => t.Value > 0 ))
                    {
                        bookCounters.Add( item.Key, item.Value - totalUniqueSetsOfTwo );
                    }
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


                if ( totalUniqueSetsOfFive>=1 && totalUniqueSetsOfThree>=1)
                {
                    totalPrice =
                    ((totalUniqueSetsOfFive-1) * (5.0M * 8.0M * 0.75M)) +
                    ((totalUniqueSetsOfFour+2) * (4.0M * 8.0M * 0.80M)) +
                    ((totalUniqueSetsOfThree -1)* (3.0M * 8.0M * 0.90M)) +
                    (totalUniqueSetsOfTwo * (2.0M * 8.0M * 0.95M)) +
                    (totalNonDiscountedBooks * 8.0M);
                }




                return totalPrice;
            }

            return decimal.MaxValue;
        }
    }
}
