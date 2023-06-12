using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace csharp
{
    [TestFixture]
    public class GildedRoseShould
    {
        private const string AgedBrie = "Aged Brie";
        private Item _sulfuras = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 };
        private GildedRose _gildedRose;

        [SetUp]
        public void SetUp()
        {
            _gildedRose = new GildedRose();
        }

        [Test]
        public void ReduceTheSellInValueWhenEndDay()
        {
            const int initialSellIn = 1;
            var randomItem = GivenAnItemWith("Random Item", initialSellIn, 0);
            IList<Item> items = GivenAListOfItemsWith(randomItem);
            GivenAGildedRoseWithItems(items);

            WhenEndDay();

            ThenAssertSellInValue(initialSellIn - 1, randomItem);
        }

        [Test]
        public void ReduceTheQualityByOneWhenEndDayAndSellInIsGreaterThanZero()
        {
            const int sellIn = 1;
            const int initialQuality = 1;
            var randomItem = GivenAnItemWith("Random Item", sellIn, initialQuality);
            IList<Item> items = GivenAListOfItemsWith(randomItem);
            GivenAGildedRoseWithItems(items);

            WhenEndDay();

            ThenAssertQualityValue(initialQuality - 1, randomItem);
        }

        [Test]
        public void DontReduceTheQualityBelowZeroWhenEndDay()
        {
            const int sellIn = 0;
            const int initialQuality = 0;
            var randomItem = GivenAnItemWith("Random Item", sellIn, initialQuality);
            IList<Item> items = GivenAListOfItemsWith(randomItem);
            GivenAGildedRoseWithItems(items);

            WhenEndDay();

            ThenAssertQualityIsGreaterOrEqualThanZero(randomItem);
        }

        [Test]
        public void ReduceTheQualityByTwoWhenEndDayAndSellInIsBelowOrEqualToZeroAndInitialQualityIsGreaterThanTwo()
        {
            const int sellIn = 0;
            const int initialQuality = 4;
            var randomItem = GivenAnItemWith("Random Item", sellIn, initialQuality);
            IList<Item> items = GivenAListOfItemsWith(randomItem);
            GivenAGildedRoseWithItems(items);

            WhenEndDay();

            ThenAssertQualityValue(initialQuality - 2, randomItem);
        }

        [Test]
        public void IncreaseTheQualityOfAgedBrieByOneWhenEndDayAndSellInIsGreaterThanZero()
        {
            const int sellIn = 10;
            const int initialQuality = 0;
            var randomItem = GivenAnItemWith(AgedBrie, sellIn, initialQuality);
            IList<Item> items = GivenAListOfItemsWith(randomItem);
            GivenAGildedRoseWithItems(items);

            WhenEndDay();

            ThenAssertQualityValue(initialQuality + 1, randomItem);
        } 
        
        [Test]
        public void IncreaseTheQualityOfAgedBrieByTwoWhenEndDayAndSellInIsLesserOrZero()
        {
            const int sellIn = 0;
            const int initialQuality = 0;
            var randomItem = GivenAnItemWith(AgedBrie, sellIn, initialQuality);
            IList<Item> items = GivenAListOfItemsWith(randomItem);
            GivenAGildedRoseWithItems(items);

            WhenEndDay();

            ThenAssertQualityValue(initialQuality + 2, randomItem);
        }

        [TestCase(51)]
        [TestCase(200)]
        public void NeverIncreaseTheQualityOfAnItemAbove50WhenEndDay(int amountOfDaysHappening)
        {
            const int sellIn = 0;
            const int initialQuality = 0;
            var randomItem = GivenAnItemWith(AgedBrie, sellIn, initialQuality);
            IList<Item> items = GivenAListOfItemsWith(randomItem);
            GivenAGildedRoseWithItems(items);

            for (var i = 0; i < amountOfDaysHappening; i++)
                WhenEndDay();

            ThenAssertQualityValue(initialQuality + 50, randomItem);
        }

        [TestCase(10)]
        [TestCase(100)]
        public void NeverDecreaseSulfurasQuality(int amountOfDaysHappening)
        {
            var sulfurasInitialQuality = _sulfuras.Quality;
            IList<Item> items = GivenAListOfItemsWith(_sulfuras);
            GivenAGildedRoseWithItems(items);

            for (var i = 0; i < amountOfDaysHappening; i++)
                WhenEndDay();

            ThenAssertQualityValue(sulfurasInitialQuality, _sulfuras);
        }
        
        [TestCase(10)]
        [TestCase(100)]
        public void NeverDecreaseSulfurasSellIn(int amountOfDaysHappening)
        {
            var sulfurasInitialSellIn = _sulfuras.SellIn;
            IList<Item> items = GivenAListOfItemsWith(_sulfuras);
            GivenAGildedRoseWithItems(items);

            for (var i = 0; i < amountOfDaysHappening; i++)
                WhenEndDay();

            ThenAssertSellInValue(sulfurasInitialSellIn, _sulfuras);
        }
        
        private static List<Item> GivenAListOfItemsWith(params Item[] item) =>
                item.ToList();

        private static Item GivenAnItemWith(string name, int sellIn, int quality) =>
                new Item { Name = name, SellIn = sellIn, Quality = quality };

        private void GivenAGildedRoseWithItems(IList<Item> items) =>
                _gildedRose.SetItems(items);

        private void WhenEndDay() =>
                _gildedRose.EndDay();

        private static void ThenAssertSellInValue(int expectedSellIn, Item item) =>
                Assert.AreEqual(expectedSellIn, item.SellIn);

        private static void ThenAssertQualityIsGreaterOrEqualThanZero(Item item) =>
                Assert.GreaterOrEqual(item.Quality, 0);

        private static void ThenAssertQualityValue(int expectedQuality, Item item) =>
                Assert.AreEqual(expectedQuality, item.Quality);
    }
}