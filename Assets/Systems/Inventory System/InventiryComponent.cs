using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryComponent : MonoBehaviour
{
    [SerializeField] public List<InventorySlot> Slots = new();

    public event Action<InventorySlot> OnItemAdded;
    public event Action<int> OnItemRemoved;

    public void AddItem(ItemData item)
    {
        var existing = Slots.Find(s => s.Item == item);

        if (existing != null)
        {
            existing.AddCount();
            OnItemAdded?.Invoke(existing);
            return;
        }

        var slot = new InventorySlot(item);
        Slots.Add(slot);
        OnItemAdded?.Invoke(slot);
    }
  
    private void RemoveItem(int index)
    {
        var slot = Slots[index];

        slot.RemoveCount();

        if (slot.Count <= 0)
            Slots.Remove(slot);

        OnItemRemoved?.Invoke(index);
    }

    public string GetDescription(int index)
    {
        return Slots[index].Item.itemDescription;
    }

    public bool HasItem(ItemData item) =>
        Slots.Exists(s => s.Item == item);
}


[Serializable]
public class InventorySlot
{
    public ItemData Item;
    public int Count { get; private set; }

    public InventorySlot(ItemData item)
    {
        Item = item;
        Count = 1;
    }

    public void AddCount() => Count++;
    public void RemoveCount() => Count--;
}