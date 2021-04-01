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
                return CalculateCostForMultipleBooks( bookInBaskets );
            }
        }

        private decimal CalculateCostForMultipleBooks( List<BookInBasket> bookInBaskets )
        {
            int totalNumberOfBooks = bookInBaskets.Count();
            int totalUniqueSetsOfOne = 0;
            int totalUniqueSetsOfFive = 0;
            int totalUniqueSetsOfFour = 0;
            int totalUniqueSetsOfThree = 0;
            int totalUniqueSetsOfTwo = 0;

            Dictionary<int, int> countsOfEachBookType = new Dictionary<int, int>
            {
                { 1, bookInBaskets.Count( t => t.Id == 1 ) },
                { 2, bookInBaskets.Count( t => t.Id == 2 ) },
                { 3, bookInBaskets.Count( t => t.Id == 3 ) },
                { 4, bookInBaskets.Count( t => t.Id == 4 ) },
                { 5, bookInBaskets.Count( t => t.Id == 5 ) }
            };//Id, Count of books

            countsOfEachBookType = RemoveEmptyCounters( countsOfEachBookType );
            DealWithUniqueSetsOfFive( ref totalNumberOfBooks, ref totalUniqueSetsOfFive, ref countsOfEachBookType );

            countsOfEachBookType = RemoveEmptyCounters( countsOfEachBookType );
            DealWithUniqueSetsOfFour( ref totalNumberOfBooks, ref totalUniqueSetsOfFour, ref countsOfEachBookType );

            countsOfEachBookType = RemoveEmptyCounters( countsOfEachBookType );
            DealWithUniqueSetsOfThree( ref totalNumberOfBooks, ref totalUniqueSetsOfThree, ref countsOfEachBookType );

            countsOfEachBookType = RemoveEmptyCounters( countsOfEachBookType );
            DealWithUniqueSetsOfTwo( ref totalNumberOfBooks, ref totalUniqueSetsOfTwo, ref countsOfEachBookType );

            totalUniqueSetsOfOne = totalNumberOfBooks;

            return CalculateTotal( totalUniqueSetsOfOne, totalUniqueSetsOfTwo, totalUniqueSetsOfThree, totalUniqueSetsOfFour, totalUniqueSetsOfFive );
        }

        private void DealWithUniqueSetsOfTwo( ref int totalNumberOfBooks, ref int totalUniqueSetsOfTwo, ref Dictionary<int, int> bookCounters )
        {
            if (bookCounters.Count == 2)
            {
                totalUniqueSetsOfTwo = Math.Min( bookCounters.ElementAt( 0 ).Value, bookCounters.ElementAt( 1 ).Value );
                bookCounters = ReduceCounters( bookCounters, totalUniqueSetsOfTwo );
                totalNumberOfBooks -= (totalUniqueSetsOfTwo * 2);
            }
        }

        private void DealWithUniqueSetsOfThree( ref int totalNumberOfBooks, ref int totalUniqueSetsOfThree, ref Dictionary<int, int> bookCounters )
        {
            if (bookCounters.Count == 3)
            {
                totalUniqueSetsOfThree = Math.Min( bookCounters.ElementAt( 0 ).Value, Math.Min( bookCounters.ElementAt( 1 ).Value, bookCounters.ElementAt( 2 ).Value ) );
                bookCounters = ReduceCounters( bookCounters, totalUniqueSetsOfThree );
                totalNumberOfBooks -= (totalUniqueSetsOfThree * 3);
            }
        }

        private void DealWithUniqueSetsOfFour( ref int totalNumberOfBooks, ref int totalUniqueSetsOfFour, ref Dictionary<int, int> bookCounters )
        {
            if (bookCounters.Count == 4)
            {
                totalUniqueSetsOfFour = Math.Min( bookCounters.ElementAt( 0 ).Value, Math.Min( bookCounters.ElementAt( 1 ).Value, Math.Min( bookCounters.ElementAt( 2 ).Value, bookCounters.ElementAt( 3 ).Value ) ) );
                bookCounters = ReduceCounters( bookCounters, totalUniqueSetsOfFour );
                totalNumberOfBooks -= (totalUniqueSetsOfFour * 4);
            }
        }

        private void DealWithUniqueSetsOfFive( ref int totalNumberOfBooks, ref int totalUniqueSetsOfFive, ref Dictionary<int, int> bookCounters )
        {
            if (bookCounters.Count == 5)
            {
                totalUniqueSetsOfFive = Math.Min( bookCounters.ElementAt( 0 ).Value, Math.Min( bookCounters.ElementAt( 1 ).Value, Math.Min( bookCounters.ElementAt( 2 ).Value, Math.Min( bookCounters.ElementAt( 3 ).Value, bookCounters.ElementAt( 4 ).Value ) ) ) );
                bookCounters = ReduceCounters( bookCounters, totalUniqueSetsOfFive );
                totalNumberOfBooks -= (totalUniqueSetsOfFive * 5);
            }
        }

        private decimal CalculateTotal( int totalUniqueSetsOfOne, int totalUniqueSetsOfTwo, int totalUniqueSetsOfThree, int totalUniqueSetsOfFour, int totalUniqueSetsOfFive )
        {
            const decimal singleBookPrice = 8.0M;
            const decimal discountFor5BooksOfSet = 0.75M;
            const decimal discountFor4BooksOfSet = 0.80M;
            const decimal discountFor3BooksOfSet = 0.90M;
            const decimal discountFor2BooksOfSet = 0.95M;
            const decimal discountFor1BookOfSet = 1.0M;
            decimal totalPrice =
                (totalUniqueSetsOfFive * (5.0M * singleBookPrice * discountFor5BooksOfSet)) +
                (totalUniqueSetsOfFour * (4.0M * singleBookPrice * discountFor4BooksOfSet)) +
                (totalUniqueSetsOfThree * (3.0M * singleBookPrice * discountFor3BooksOfSet)) +
                (totalUniqueSetsOfTwo * (2.0M * singleBookPrice * discountFor2BooksOfSet)) +
                (totalUniqueSetsOfOne * (1.0M * singleBookPrice * discountFor1BookOfSet));


            //We need to optimise here as its cheaper to buy 2 sets of 4 rather than a 5 and a 3 together!
            if (totalUniqueSetsOfFive >= 1 && totalUniqueSetsOfThree >= 1)
            {
                totalPrice =
                ((totalUniqueSetsOfFive - 1) * (5.0M * singleBookPrice * discountFor5BooksOfSet)) +
                ((totalUniqueSetsOfFour + 2) * (4.0M * singleBookPrice * discountFor4BooksOfSet)) +
                ((totalUniqueSetsOfThree - 1) * (3.0M * singleBookPrice * discountFor3BooksOfSet)) +
                (totalUniqueSetsOfTwo * (2.0M * singleBookPrice * discountFor2BooksOfSet)) +
                (totalUniqueSetsOfOne * (1.0M * singleBookPrice * discountFor1BookOfSet));
            }


            return totalPrice;
        }

        private Dictionary<int, int> ReduceCounters( Dictionary<int, int> listToRemoveItemsFrom, int numberOfItemsToRemove )
        {
            Dictionary<int, int> reducedList = new Dictionary<int, int>();
            foreach (var item in listToRemoveItemsFrom)
            {
                reducedList.Add( item.Key, item.Value - numberOfItemsToRemove );
            }
            return reducedList;
        }

        private Dictionary<int, int> RemoveEmptyCounters( Dictionary<int, int> listContainingItemsToRemove )
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
