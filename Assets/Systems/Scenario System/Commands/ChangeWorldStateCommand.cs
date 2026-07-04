using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scenario/Commands/ChangeWorldState")]
public class ChangeWorldStateCommand : SceneCommand
{
    [SerializeField] private WorldState _state;
    [SerializeField] private int _value;

    public override IEnumerator Execute()
    {
        ServiceLocator.Instance.WorldStateManager.ChangeStateValue(_state, _value);
        yield return null;
    }
}
