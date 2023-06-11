using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace csharp
{
    [TestFixture]
    public class GildedRoseShould
    {
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
        
        private static void ThenAssertQualityValue(int expectedQuality, Item item) => 
                Assert.AreEqual(expectedQuality, item.Quality);
    }
}