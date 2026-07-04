using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    [SerializeField] private DayPool[] _dayPools;
    [SerializeField] private DayPool _fixedDays;
    private int _fixedDayIndex = 0;
    private HashSet<string> _playedScenes = new();

    private void OnEnable()
    {
        ServiceLocator.Instance.WorldStateManager.OnDayResolved += LoadNext;
    }
    private void OnDisable()
    {
        ServiceLocator.Instance.WorldStateManager.OnDayResolved -= LoadNext;
    }
    public void LoadNext(DayResolution resolution)
    {
        if (_fixedDayIndex < _fixedDays.sceneNames.Length)
        {
            SceneManager.LoadScene(_fixedDays.sceneNames[_fixedDayIndex]);
            _fixedDayIndex++;
            return;
        }

        LoadFromPool(resolution.Winner, resolution.Level);
    }

    private void LoadFromPool(WorldState state, DayLevel level)
    {
        var pool = _dayPools.FirstOrDefault(p => p.state == state && p.level == level);
        if (pool == null) return;

        var available = pool.sceneNames
            .Where(s => !_playedScenes.Contains(s))
            .ToArray();

        if (available.Length == 0)
        {
            foreach (var scene in pool.sceneNames)
                _playedScenes.Remove(scene);

            available = pool.sceneNames;
        }

        var selected = available[UnityEngine.Random.Range(0, available.Length)];
        _playedScenes.Add(selected);
        SceneManager.LoadScene(selected);
    }
}
