using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scenario/Scenario")]
public class Scenario : ScriptableObject
{
    [SerializeField] public List<SceneCommand> commands;
}
