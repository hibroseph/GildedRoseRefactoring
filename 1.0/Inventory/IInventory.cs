using GildedRose;
using System;
using System.Collections.Generic;
using System.Text;

namespace GildedRose
{
    public interface IInventory
    {
        Item GetFirstItem();
        List<Item> GetAllItems();
        void UpdateQuality();
    }
}
