using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scenario/TransitionConditions/AllTaskCompleted")]
public class AllTasksCompletedCondition : TransitionCondition
{

    public override bool IsMet()
    {
        return ServiceLocator.Instance.TaskManager.AllRequiredCompleted();
    }
}
