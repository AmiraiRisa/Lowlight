using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private GameObject _ui;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private int columns = 4; // должно совпадать с Constraint Count в Grid Layout


    private List<InventorySlotView> _slots = new();
    private int _selectedIndex = -1;

    public event Action<int> OnNavigate;

    private void OnEnable()
    {
        Hide();
    }

    public void Show() => _ui.SetActive(true);
    public void Hide() => _ui.SetActive(false);

    public void BuildSlots(List<InventorySlot> slots)
    {
        foreach (var slot in _slots)
            Destroy(slot.gameObject);
        _slots.Clear();

        foreach (var slot in slots)
        {
            var obj = Instantiate(_slotPrefab, _container);
            var view = obj.GetComponent<InventorySlotView>();
            view.SetData(slot);
            _slots.Add(view);
        }

        if (_slots.Count > 0)
        {
            _selectedIndex = 0;
            UpdateSelection();
        }
    }

    public void Navigate(Vector2 direction)
    {
        if (_slots.Count == 0) return;

        int newIndex = _selectedIndex;

        if (direction.x > 0) newIndex++;
        else if (direction.x < 0) newIndex--;
        else if (direction.y < 0) newIndex += columns;
        else if (direction.y > 0) newIndex -= columns;

        _selectedIndex = Mathf.Clamp(newIndex, 0, _slots.Count - 1);
        UpdateSelection();
    }

    public void SetDescription(string text)
    {
        _description.text = text;
    }

    public int SelectedIndex => _selectedIndex;

    private void UpdateSelection()
    {
        for (int i = 0; i < _slots.Count; i++)
            _slots[i].SetSelected(i == _selectedIndex);
    }
}
