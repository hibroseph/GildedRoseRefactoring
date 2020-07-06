using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRose
{
    public class Inventory : IInventory
    {
        private List<Item> _items;

        public Inventory(List<Item> items)
        {
            _items = items;
        }

        public Inventory(Item item)
        {
            _items = new List<Item> { item };
        }

        public Item GetFirstItem()
        {
            return _items.First();
        }
        public List<Item> GetAllItems()
        {
            if (_items == null)
            {
                return new List<Item>();
            }
            else
            {
                return _items;
            }
        }

        public void UpdateQuality()
        {
            AdjustQuality();

            IncreaseAge();
        }

        private void AdjustQuality()
        {
            foreach (Item item in _items)
            {
                // An item never has a quality over 50 or a negative quality
                if (item.Quality >= 50 || item.Quality < 0)
                {
                    return;
                }

                if (item.Name.Contains("Aged Brie"))
                {
                    AdjustAgedBrieQuality(item);
                }
                else if (item.Name.Contains("Backstage passes"))
                {
                    AdjustBackstagePassQuality(item);
                }
                else if (IsLegendaryItem(item))
                {
                    return;
                }
                else
                {
                    AdjustNormalItemQuality(item);
                }

            }
        }

        private void AdjustAgedBrieQuality(Item item)
        {
            if (item.SellIn > 0 || item.Quality == 49)
            {
                item.Quality++;
            }
            else
            {
                item.Quality += 2;
            }
        }

        private void AdjustBackstagePassQuality(Item item)
        {
            switch (item.SellIn)
            {
                case int i when i > 10:
                    item.Quality++;
                    break;
                case int i when i > 5 && i <= 10:
                    item.Quality += 2;
                    break;
                case int i when i > 0 && i <= 5:
                    item.Quality += 3;
                    break;
                case int i when i <= 0:
                    item.Quality = 0;
                    break;
                default:
                    throw new NotImplementedException();

            }
        }

        private void AdjustNormalItemQuality(Item item)
        {
            if (item.SellIn > 0 && item.Quality > 0)
            {
                item.Quality--;
            }
            else if (item.Quality >= 2)
            {
                item.Quality -= 2;
            }
        }

        private void IncreaseAge()
        {
            foreach (Item item in _items)
            {
                if (!IsLegendaryItem(item))
                {
                    item.SellIn--;
                }
            }
        }

        private bool IsLegendaryItem(Item item)
        {
            return item.Name.Contains("Sulfuras");
        }
    }
}
