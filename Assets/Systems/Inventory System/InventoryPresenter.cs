using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPresenter : MonoBehaviour
{
    [SerializeField] private InventoryComponent _inventory;
    [SerializeField] private InventoryView _view;
    [SerializeField] private InputReader _input;

    private void OnEnable()
    {
        _input.OpenInventoryEvent += OpenInventory;
        _input.CloseInventoryEvent += CloseInventory;
        _input.InventoryNavigateEvent += OnNavigate;
        _inventory.OnItemAdded += OnInventoryChanged;
    }

    private void OnDisable()
    {
        _input.OpenInventoryEvent -= OpenInventory;
        _input.CloseInventoryEvent -= CloseInventory;
        _input.InventoryNavigateEvent -= OnNavigate;
        _inventory.OnItemAdded -= OnInventoryChanged;
    }

    private void OpenInventory()
    {
        _input.SetMode(InputMode.Inventory);
        _view.Show();
        _view.BuildSlots(_inventory.Slots);
        UpdateDescription();
    }

    private void CloseInventory()
    {
        _input.SetMode(InputMode.Gameplay);
        _view.Hide();
    }

    private void OnNavigate(Vector2 direction)
    {
        _view.Navigate(direction);
        UpdateDescription();
    }

    private void OnInventoryChanged(InventorySlot slot)
    {
        _view.BuildSlots(_inventory.Slots);
        UpdateDescription();
    }

    private void UpdateDescription()
    {
        var slots = _inventory.Slots;
        if (slots.Count == 0) return;
        _view.SetDescription(slots[_view.SelectedIndex].Item.itemDescription);
    }
}
