using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scenario/TransitionConditions/TaskCompleted")]
public class TaskCompletedCondition : TransitionCondition
{
    [SerializeField] private string _taskId;

    public override bool IsMet() =>
        ServiceLocator.Instance.TaskManager.IsTaskCompleted(_taskId);
}
