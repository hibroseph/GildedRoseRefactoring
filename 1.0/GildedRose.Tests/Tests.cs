using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using GildedRose;
using System.Linq;

namespace GildedRose.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Quality_Never_Negative()
        {
            IInventory inventory = GetSingleItemInventory(5, 2);

            AdvanceXDays(inventory, 15);

            foreach (Item item in inventory.GetAllItems())
            {
                Assert.AreEqual(0, item.Quality);
            }
        }

        [TestMethod]
        public void Quality_Degrades_Twice_As_Fast_After_Sell_By()
        {
            IInventory inventory = GetSingleItemInventory(1, 10);

            AdvanceXDays(inventory, 2);

            Assert.AreEqual(7, inventory.GetFirstItem().Quality);

        }

        [TestMethod]
        public void Quality_Increase_Age_Brie()
        {
            IInventory inventory = GetSingleItemInventory("Aged Brie", 10, 0);

            AdvanceXDays(inventory, 5);

            Assert.AreEqual(5, inventory.GetFirstItem().Quality);

            AdvanceXDays(inventory, 2);

            Assert.AreEqual(7, inventory.GetFirstItem().Quality);
        }

        [TestMethod]
        public void Quality_Increase_Age_Brie_Twice_As_Fast_After_Sell_By()
        {
            IInventory inventory = GetSingleItemInventory("Aged Brie", 5, 5);

            AdvanceXDays(inventory, 5);

            Assert.AreEqual(10, inventory.GetFirstItem().Quality);

            AdvanceXDays(inventory, 5);

            Assert.AreEqual(20, inventory.GetFirstItem().Quality);
        }

        [TestMethod]
        public void Quality_Never_Greater_Than_50()
        {
            IInventory inventory = GetSingleItemInventory("Aged Brie", 2, 35);

            AdvanceXDays(inventory, 50);

            Assert.AreEqual(50, inventory.GetFirstItem().Quality);
        }

        [TestMethod]
        public void Legendary_Item_Never_Sold_Never_Decrease_Quality()
        {
            IInventory inventory = GetSingleItemInventory("Sulfuras, Hand of Ragnaros", 0, 80);

            AdvanceXDays(inventory, 20);

            Assert.AreEqual(80, inventory.GetFirstItem().Quality);
            Assert.AreEqual(0, inventory.GetFirstItem().SellIn);

        }

        [TestMethod]
        public void Backstage_Pass_Quality()
        {
            IInventory inventory = GetSingleItemInventory("Backstage passes to a TAFKAL80ETC concert", 12, 4);

            AdvanceXDays(inventory, 2);

            Assert.AreEqual(6, inventory.GetFirstItem().Quality);

            AdvanceXDays(inventory, 5);

            Assert.AreEqual(16, inventory.GetFirstItem().Quality);

            AdvanceXDays(inventory, 3);

            Assert.AreEqual(25, inventory.GetFirstItem().Quality);

            // By this point there should be 2 days left
            Assert.AreEqual(2, inventory.GetFirstItem().SellIn);

            AdvanceXDays(inventory, 2);

            Assert.AreEqual(31, inventory.GetFirstItem().Quality);
            Assert.AreEqual(0, inventory.GetFirstItem().SellIn);

            AdvanceXDays(inventory, 1);

            Assert.AreEqual(0, inventory.GetFirstItem().Quality);

            AdvanceXDays(inventory, 10);

            Assert.AreEqual(0, inventory.GetFirstItem().Quality);
        }

        private IInventory GetSingleItemInventory(int sellIn, int quality)
        {
            return GetSingleItemInventory("Legos", sellIn, quality);
        }

        private IInventory GetSingleItemInventory(string name, int sellIn, int quality)
        {
            return new Inventory(new Item
            {
                Name = name,
                SellIn = sellIn,
                Quality = quality
            });
        }

        private void AdvanceXDays(IInventory inventory, int days)
        {
            for (int i = 0; i < days; i++)
            {
                inventory.UpdateQuality();
            }
        }


    }
}
