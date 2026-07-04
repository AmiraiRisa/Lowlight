using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scenario/Commands/ChangeRoom")]
public class ChangeRoomCommand : SceneCommand
{
    [SerializeField] private string _roomId;
    [SerializeField] private int _spawnPointIndex;
    public override IEnumerator Execute()
    {
        ServiceLocator.Instance.RoomsManager.TransitionToRoom(_roomId, _spawnPointIndex);
        yield return null;
    }
}
