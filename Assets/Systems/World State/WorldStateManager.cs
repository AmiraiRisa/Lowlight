using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum WorldState { Acceptance, Anxiety, Aggression, Avoidance }
public enum DayLevel { None, Light, Medium, Heavy }

public class WorldStateManager : MonoBehaviour
{
    [SerializeField] private int _acceptanceStartValue;
    [SerializeField] private int _lightThreshold;
    [SerializeField] private int _mediumThreshold;
    [SerializeField] private int _heavyThreshold;

    private Dictionary<WorldState, int> _values;

    private readonly WorldState[] Destructive =
        { WorldState.Anxiety, WorldState.Aggression, WorldState.Avoidance };

    public event Action<DayResolution> OnDayResolved;

    private void Awake()
    {
        _values = new Dictionary<WorldState, int>
        {
            { WorldState.Acceptance, _acceptanceStartValue },
            { WorldState.Anxiety, 0 },
            { WorldState.Aggression, 0 },
            { WorldState.Avoidance, 0 }
        };
    }

    public void ChangeStateValue(WorldState state, int value)
    {
        _values[state] += value;
        if (Destructive.Contains(state))
        {
            _values[WorldState.Acceptance] = Mathf.Max(0, _values[WorldState.Acceptance] - 1);
        }
    }

    public void ResolveDay()
    {
        var winner = DetermineWinner();
        var level = GetLevel(_values[winner]);

        var lines = _values.Select(line => $"{line.Key}: {line.Value}");
        Debug.Log($"[WORLD_STATE_SYSTEM] Day was resolved\n" +
                  string.Join("\n", lines) + "\n" +
                  $"Winner: {winner} [{level}]");

        OnDayResolved?.Invoke(new DayResolution(winner, level));
    }

    private WorldState DetermineWinner()
    {
        var max = _values.Values.Max();
        var candidates = _values.Where(kv => kv.Value == max).Select(kv => kv.Key).ToList();

        if (candidates.Count == 1) return candidates[0];

        var destructiveCandidates = candidates.Where(s => Destructive.Contains(s)).ToList();

        switch (destructiveCandidates.Count)
        {
            case 0: 
                break;
            case 1: 
                return destructiveCandidates[0];
            case 2:
                if (destructiveCandidates.Contains(WorldState.Avoidance))
                {
                    if (destructiveCandidates.Contains(WorldState.Anxiety))
                        return WorldState.Anxiety;
                    return WorldState.Avoidance;
                }
                return WorldState.Aggression;
            case 3: 
                return destructiveCandidates[UnityEngine.Random.Range(0, 3)];
        }
        
        return WorldState.Acceptance;
    }

    private DayLevel GetLevel(int value)
    {
        if (value >= _heavyThreshold) return DayLevel.Heavy;
        if (value >= _mediumThreshold) return DayLevel.Medium;
        if (value >= _lightThreshold) return DayLevel.Light;
        return DayLevel.None;
    }
}


public class DayResolution
{
    public WorldState Winner { get; }
    public DayLevel Level { get; }

    public DayResolution(WorldState winner, DayLevel level)
    {
        Winner = winner;
        Level = level;
    }
}