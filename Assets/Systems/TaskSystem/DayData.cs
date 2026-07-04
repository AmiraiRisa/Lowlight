using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scenario/DaylyTasks/Day")]
public class DayData : ScriptableObject
{
    public string dayId;
    public TaskData[] tasks;
}
