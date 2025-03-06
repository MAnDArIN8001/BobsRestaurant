using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bob.Comunication.Items
{
    public class ItemsStorage<T> : IDisposable
    {
        public event Action<T> OnReceiveItem;
        public event Action<T> OnDropItem;
        
        private int _maxCapacity;
        
        private readonly List<T> _itemList;

        public bool IsFull => _itemList.Count == _maxCapacity;

        public IReadOnlyList<T> ItemsList => _itemList;

        public ItemsStorage(int maxCapacity)
        {
            _maxCapacity = maxCapacity;
            
            _itemList = new List<T>(maxCapacity);
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
            
            OnReceiveItem?.Invoke(newItem);

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

            OnDropItem?.Invoke(item);
            
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

        public bool Contains(T item) => _itemList.Contains(item);

        public void Dispose()
        {
            _itemList.Clear();
        }
    }
}