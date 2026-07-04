using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scenario/Commands/CompleteTask")]
public class CompleteTaskCommand : SceneCommand
{
    [SerializeField] private string _taskId;

    public override IEnumerator Execute()
    {
        ServiceLocator.Instance.TaskManager.CompleteTask(_taskId);
        yield return null;
    }
}
