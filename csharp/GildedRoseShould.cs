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

            ThenAssertSellInValueDecreased(initialSellIn, randomItem);
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

            ThenAssertQualityValueDecreased(initialQuality, randomItem);
        }

        private static List<Item> GivenAListOfItemsWith(params Item[] item) =>
                item.ToList();

        private static Item GivenAnItemWith(string name, int sellIn, int quality) =>
                new Item { Name = name, SellIn = sellIn, Quality = quality };

        private void GivenAGildedRoseWithItems(IList<Item> items) =>
                _gildedRose.SetItems(items);

        private void WhenEndDay() =>
                _gildedRose.EndDay();

        private static void ThenAssertSellInValueDecreased(int initialSellIn, Item item) => 
                Assert.AreEqual(initialSellIn - 1, item.SellIn);
        
        private static void ThenAssertQualityValueDecreased(int initialQuality, Item item) => 
                Assert.AreEqual(initialQuality - 1, item.Quality);
    }
}