using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private DayData _dayData;
    private HashSet<string> _completedTasks = new();

    public event Action<string> OnTaskCompleted;
    public event Action OnAllRequiredCompleted;

    public void CompleteTask(string taskId)
    {
        if (_completedTasks.Contains(taskId)) return;
        if (_dayData.tasks.All(t => t.taskId != taskId)) return;

        _completedTasks.Add(taskId);
        OnTaskCompleted?.Invoke(taskId);

        if (AllRequiredCompleted())
            OnAllRequiredCompleted?.Invoke();
    }

    public bool IsTaskCompleted(string taskId) =>
        _completedTasks.Contains(taskId);

    public bool AllRequiredCompleted() =>
        _dayData.tasks
            .Where(t => !t.isOptional)
            .All(t => _completedTasks.Contains(t.taskId));
}
