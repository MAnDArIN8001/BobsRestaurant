using System.Collections.Generic;
using System.Linq;
using Comunication.Pickable;
using UnityEditor.Searcher;
using UnityEngine;

namespace Bob.Comunication.Items
{
    public class ItemsStorage<T>
    {
        private int _maxCapacity;
        
        private List<T> _itemList;

        public bool IsFull => _itemList.Count == _maxCapacity;

        public IReadOnlyList<T> ItemsList => _itemList;

        public ItemsStorage(int capacity, int maxCapacity)
        {
            _maxCapacity = maxCapacity;
            
            _itemList = new List<T>(capacity);
        }

        public bool TryAddItem(T newItem)
        {
            if (_itemList.Contains(newItem))
            {
                Debug.LogWarning($"The item storage {this} is already contains the object {newItem}");

                return false;
            }

            if (_itemList.Count+1 > _maxCapacity)
            {
                Debug.LogWarning($"The item storage {this} is full");

                return false;
            }
            
            _itemList.Add(newItem);

            return true;
        }

        public bool TryRemoveItem(T item)
        {
            if (!_itemList.Contains(item))
            {
                Debug.Log($"The items storage doesnt contains any item {item}");

                return false;
            }

            _itemList.Remove(item);

            return true;
        }

        public bool TryTakeLast(out T item)
        {
            if (_itemList.Count == 0)
            {
                Debug.LogWarning("Theres no items in storage");

                item = default;
                
                return false;
            }

            item = _itemList.Last();

            return true;
        }
    }
}