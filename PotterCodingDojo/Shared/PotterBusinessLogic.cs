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
                //We have more than 1 book in the basket
                int totalNumberOfBooks = bookInBaskets.Count();
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

                //So we have a dictionary of the only items on order.
                if (bookCountersTemp.Count == 5)
                {
                    //We have a complete set, so lets find it.


                    //How many unique complete sets of books do we have?
                    int totalUniqueSetsOfFive = Math.Min( bookCountersTemp[1], Math.Min( bookCountersTemp[2], Math.Min( bookCountersTemp[3], Math.Min( bookCountersTemp[4], bookCountersTemp[5] ) ) ) );
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
                Dictionary<int, int> bookCountersTemp2 = new Dictionary<int, int>();
                foreach (var item in bookCountersTemp.Where( t => t.Value > 0 ))
                {
                    bookCountersTemp2.Add( item.Key, item.Value );
                }


                //Deal with 4 items now


                if (bookCountersTemp2.Count == 4)
                {
                    int totalUniqueSetsOfFour = Math.Min( bookCountersTemp2.ElementAt( 0 ).Value, Math.Min( bookCountersTemp2.ElementAt( 1 ).Value, Math.Min( bookCountersTemp2.ElementAt( 2 ).Value, bookCountersTemp2.ElementAt( 3 ).Value ) ) );

                    currentPrice += totalUniqueSetsOfFour * (4.0M * 8.0M * 0.80M);
                    bookCounters.Clear();
                    foreach (var item in bookCountersTemp2.Where( t => t.Value > 0 ))
                    {
                        bookCounters.Add( item.Key, item.Value - totalUniqueSetsOfFour );
                    }
                    totalNumberOfBooks -= (totalUniqueSetsOfFour * 4);
                }

                bookCountersTemp.Clear();
                bookCountersTemp2.Clear();
                //Populate the next list
                foreach (var item in bookCounters.Where( t => t.Value > 0 ))
                {
                    bookCountersTemp2.Add( item.Key, item.Value );
                }


                if (bookCountersTemp2.Count == 3)
                {
                    int totalUniqueSetsOfThree = Math.Min( bookCountersTemp2.ElementAt( 0 ).Value, Math.Min( bookCountersTemp2.ElementAt( 1 ).Value, bookCountersTemp2.ElementAt( 2 ).Value ) );

                    currentPrice += totalUniqueSetsOfThree * (3.0M * 8.0M * 0.90M);
                    bookCounters.Clear();
                    foreach (var item in bookCountersTemp2.Where( t => t.Value > 0 ))
                    {
                        bookCounters.Add( item.Key, item.Value - totalUniqueSetsOfThree );
                    }
                    totalNumberOfBooks -= (totalUniqueSetsOfThree * 3);
                }

                bookCountersTemp.Clear();
                bookCountersTemp2.Clear();
                //Populate the next list
                foreach (var item in bookCounters.Where( t => t.Value > 0 ))
                {
                    bookCountersTemp2.Add( item.Key, item.Value );
                }
                if (bookCountersTemp2.Count == 2)
                {
                    int totalUniqueSetsOfTwo = Math.Min( bookCountersTemp2.ElementAt( 0 ).Value, bookCountersTemp2.ElementAt( 1 ).Value );

                    currentPrice += totalUniqueSetsOfTwo * (2.0M * 8.0M * 0.95M);
                    bookCounters.Clear();
                    foreach (var item in bookCountersTemp2.Where( t => t.Value > 0 ))
                    {
                        bookCounters.Add( item.Key, item.Value - totalUniqueSetsOfTwo );
                    }
                    totalNumberOfBooks -= (totalUniqueSetsOfTwo * 2);
                }


                //Now what is left?
                currentPrice += (totalNumberOfBooks * 8.0M);

                return currentPrice;
            }
            return decimal.MaxValue;
        }
    }
}
