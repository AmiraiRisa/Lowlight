using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scenario/Commands/GetItem")]
public class GetItemCommand : SceneCommand
{
    [SerializeField] private ItemData _item;

    public override IEnumerator Execute()
    {
        ServiceLocator.Instance.InventoryComponent.AddItem(_item);
        yield return null;
    }
}
