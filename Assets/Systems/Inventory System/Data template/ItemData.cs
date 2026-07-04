using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] public string itemName;
    [TextArea] public string itemDescription;
    [SerializeField] public Sprite itemImage;

}
