using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scenario/Commands/EndDay")]
public class EndDayCommand : SceneCommand
{
    public override IEnumerator Execute()
    {
        ServiceLocator.Instance.WorldStateManager.ResolveDay();
        yield return null;
    }
}
