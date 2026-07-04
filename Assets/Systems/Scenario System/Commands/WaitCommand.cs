using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scenario/Commands/Wait")]
public class WaitCommand : SceneCommand
{
    public float seconds;
    public override IEnumerator Execute()
    {
        yield return new WaitForSeconds(seconds);
    }
}
