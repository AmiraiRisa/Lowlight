using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioRunner : MonoBehaviour
{
    public IEnumerator Run(Scenario scenario)
    {
        foreach (var command in scenario.commands)
            yield return command.Execute();
    }
}
