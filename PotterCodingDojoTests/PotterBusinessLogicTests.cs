using Microsoft.VisualStudio.TestTools.UnitTesting;
using PotterCodingDojo.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PotterCodingDojoTests
{
    [TestClass]
    public class PotterBusinessLogicTests
    {
        /*
        One copy of any of the five books costs 8 EUR. 
        If, however, you buy two different books from the series, you get a 5% discount on those two books.
        If you buy 3 different books, you get a 10% discount.
        With 4 different books, you get a 20% discount. 
        If you go the whole hog, and buy all 5, you get a huge 25% discount.

        Note that if you buy, say, four books, of which 3 are different titles, you get a 10% discount on the 3 that form part of a set, but the fourth book still costs 8 EUR.*/
        [TestMethod]
        public async Task CalculateBestPrice_OneBook_FullPrice()
        {
            //Arrange
            var systemUnderTest = CreateSystemUnderTest();
            List<BookInBasket> testData = new List<BookInBasket> { new BookInBasket { Id = 1, Title = "Philosophers Stone" } };

            //Act
            decimal bestPriceCalculated = await systemUnderTest.CalculateBestPrice( testData );

            //Assert
            Assert.AreEqual( 8.0M, bestPriceCalculated, "Incorrect price calculated for a single book" );

        }

        [TestMethod]
        public async Task CalculateBestPrice_TwoDifferentBooks_5PercentDiscount()
        {
            //Arrange
            var systemUnderTest = CreateSystemUnderTest();
            List<BookInBasket> testData = new List<BookInBasket> {
                new BookInBasket { Id = 1, Title = "Philosophers Stone" },
                new BookInBasket { Id = 2, Title = "Chamber of secrets" }
            };

            //Act
            decimal bestPriceCalculated = await systemUnderTest.CalculateBestPrice( testData );

            //Assert
            Assert.AreEqual( 15.20M, bestPriceCalculated, "Incorrect price calculated for a single book" );

        }

        [TestMethod]
        public async Task CalculateBestPrice_ThreeDifferentBooks_TenPercentDiscount()
        {
            //Arrange
            var systemUnderTest = CreateSystemUnderTest();
            List<BookInBasket> testData = new List<BookInBasket> {
                new BookInBasket { Id = 1, Title = "Philosophers Stone" },
                new BookInBasket { Id = 2, Title = "Chamber of secrets" },
                new BookInBasket { Id = 3, Title = "Prisoner of Azkaban" }
            };

            //Act
            decimal bestPriceCalculated = await systemUnderTest.CalculateBestPrice( testData );

            //Assert
            Assert.AreEqual( 21.60M, bestPriceCalculated, "Incorrect price calculated for a single book" );

        }

        [TestMethod]
        public async Task CalculateBestPrice_FourDifferentBooks_TwentyPercentDiscount()
        {
            //Arrange
            var systemUnderTest = CreateSystemUnderTest();
            List<BookInBasket> testData = new List<BookInBasket> {
                new BookInBasket { Id = 1, Title = "Philosophers Stone" },
                new BookInBasket { Id = 2, Title = "Chamber of secrets" },
                new BookInBasket { Id = 3, Title = "Prisoner of Azkaban" },
                new BookInBasket { Id = 4, Title = "Goblet of Fire" }
            };

            //Act
            decimal bestPriceCalculated = await systemUnderTest.CalculateBestPrice( testData );

            //Assert
            Assert.AreEqual( 25.60M, bestPriceCalculated, "Incorrect price calculated for a single book" );

        }

        [TestMethod]
        public async Task CalculateBestPrice_FiveDifferentBooks_TwentyFivePercentDiscount()
        {
            //Arrange
            var systemUnderTest = CreateSystemUnderTest();
            List<BookInBasket> testData = new List<BookInBasket> {
                new BookInBasket { Id = 1, Title = "Philosophers Stone" },
                new BookInBasket { Id = 2, Title = "Chamber of secrets" },
                new BookInBasket { Id = 3, Title = "Prisoner of Azkaban" },
                new BookInBasket { Id = 4, Title = "Goblet of Fire" },
                new BookInBasket { Id = 5, Title = "Order of the Phoenix" }
            };

            //Act
            decimal bestPriceCalculated = await systemUnderTest.CalculateBestPrice( testData );

            //Assert
            Assert.AreEqual( 30.0M, bestPriceCalculated, "Incorrect price calculated for a single book" );

        }

        private PotterBusinessLogic CreateSystemUnderTest()
        {
            var systemUnderTest = new PotterBusinessLogic();
            return systemUnderTest;
        }
    }
}