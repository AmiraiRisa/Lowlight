using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scenario/DaylyTasks/Task")]
public class TaskData : ScriptableObject
{
    public string taskId;
    public string displayText;
    public bool isOptional;
}
