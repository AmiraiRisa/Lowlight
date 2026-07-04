using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlotView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private Outline _outline;

    private void Awake()
    {
        _outline.enabled = false;
    }

    public void SetData(InventorySlot slot)
    {
        _image.sprite = slot.Item.itemImage;
        _itemName.text = slot.Item.itemName;
    }

    public void SetSelected(bool selected)
    {
        _outline.enabled = selected;
    }
}
