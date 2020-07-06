using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryService.ClientService;
using InventoryService.Common;
using System.Collections.Generic;

namespace GildedRose.InventoryTests
{
    [TestClass]
    public class InventoryTests
    {
        private Inventory _inventory;

        [TestInitialize]
        public void SetUp()
        {
            _inventory = new Inventory(new List<Item>());
        }

        [TestMethod]
        public void TestSizeOf1()
        {
            _inventory.AddItem(GetNewItem());

            Assert.AreEqual(1, _inventory.Size());
        }

        [TestMethod]
        public void TestSizeOf2()
        {
            _inventory.AddItem(GetNewItem());
            _inventory.AddItem(GetNewItem());

            Assert.AreEqual(2, _inventory.Size());
        }

        [TestMethod]
        public void TestSizeOf10()
        {
            for (int i = 0; i < 10; i++)
            {
                _inventory.AddItem(GetNewItem());
            }

            Assert.AreEqual(10, _inventory.Size());
        }

        [TestMethod]
        public void TestQualityNeverNegative()
        {
            int id = _inventory.AddItem(GetNewItem(2, 2));

            UpdateQualityXNumberOfDays(5);

            Assert.AreEqual(0, _inventory.GetItem(id).Quality);
        }

        [TestMethod]
        public void TestQualityDegradesTwiceAsFastAfterSellByDate()
        {
            int id = _inventory.AddItem(GetNewItem(10, 2));

            UpdateQualityXNumberOfDays(2);

            Assert.AreEqual(8, _inventory.GetItem(id).Quality);

            UpdateQualityXNumberOfDays(2);

            Assert.AreEqual(4, _inventory.GetItem(id).Quality);

            UpdateQualityXNumberOfDays(1);

            Assert.AreEqual(2, _inventory.GetItem(id).Quality);
        }

        [TestMethod]
        public void TestAgedBrieIncreaseInQualityOlderItGets()
        {
            int id = _inventory.AddItem(GetAgedBrieItem(10, 2));

            UpdateQualityXNumberOfDays(2);

            Assert.AreEqual(12, _inventory.GetItem(id).Quality);
        }

        [TestMethod]
        public void TestItemQualityNeverExceeds50_AgedBrie()
        {
            int id = _inventory.AddItem(GetAgedBrieItem(48, 10));

            UpdateQualityXNumberOfDays(10);

            Assert.AreEqual(50, _inventory.GetItem(id).Quality);
        }

        [TestMethod]
        public void TestLegendaryItemNeverDecreasesInQuality()
        {
            int id = _inventory.AddItem(GetLegendaryItem(40, 5));

            UpdateQualityXNumberOfDays(20);

            Item legendaryItem = _inventory.GetItem(id);
            Assert.AreEqual(40, legendaryItem.Quality);
            Assert.AreEqual(5, legendaryItem.SellIn);
        }

        [TestMethod]
        public void TestBackstagePassesQuality()
        {
            int id = _inventory.AddItem(GetBackstagePass(10, 10));

            UpdateQualityXNumberOfDays(3);

            Assert.AreEqual(16, _inventory.GetItem(id).Quality);

            UpdateQualityXNumberOfDays(2);

            Assert.AreEqual(5, _inventory.GetItem(id).SellIn);
            Assert.AreEqual(20, _inventory.GetItem(id).Quality);

            UpdateQualityXNumberOfDays(5);

            Assert.AreEqual(0, _inventory.GetItem(id).SellIn);
            Assert.AreEqual(35, _inventory.GetItem(id).Quality);

            UpdateQualityXNumberOfDays(1);

            Assert.AreEqual(-1, _inventory.GetItem(id).SellIn);
            Assert.AreEqual(0, _inventory.GetItem(id).Quality);

            UpdateQualityXNumberOfDays(20);

            Assert.AreEqual(-21, _inventory.GetItem(id).SellIn);
            Assert.AreEqual(0, _inventory.GetItem(id).Quality);
        }

        private Item GetNewItem()
        {
            return new Item() { Name = "Laptop", Quality = 5, SellIn = 20 };
        }

        private Item GetNewItem(string name, int quality, int sellIn)
        {
            return new Item() { Name = name, Quality = quality, SellIn = sellIn };
        }

        private Item GetNewItem(int quality, int sellIn)
        {
            return new Item() { Name = "World's Best Mom Hat", Quality = quality, SellIn = sellIn };
        }

        private Item GetAgedBrieItem(int quality, int sellIn)
        {
            return new Item() { Name = "Aged Brie", Quality = quality, SellIn = sellIn };
        }

        private Item GetBackstagePass(int quality, int sellIn)
        {
            return new Item() { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = quality, SellIn = sellIn };
        }

        private Item GetLegendaryItem(int quality, int sellin)
        {
            return new Item() { Name = "Sulfuras, Hand of Ragnaros", Quality = quality, SellIn = sellin };
        }


        private void UpdateQualityXNumberOfDays(int days)
        {
            for (int i = 0; i < days; i++)
            {
                _inventory.UpdateQuality();
            }
        }
    }
}
