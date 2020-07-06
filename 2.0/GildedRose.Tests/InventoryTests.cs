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

        private Item GetNewItem()
        {
            return new Item() { Name = "Cool Item", Quality = 5, SellIn = 20 };
        }
    }
}
