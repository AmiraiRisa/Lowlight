using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Core/DayPool")]
public class DayPool : ScriptableObject
{
    public WorldState state;
    public DayLevel level;
    public string[] sceneNames;
}
