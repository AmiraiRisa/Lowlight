using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject tooltip;
    [SerializeField] private Collider2D _interactionTrigger;
    [SerializeField] private ScenarioRunner _scenarioRunner;

    [SerializeField] private ScenarioEntry[] _scenarios;
    private int _currentIndex = 0;


    private void Awake()
    {
        tooltip.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (tooltip != null)
                tooltip.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (tooltip != null)
                tooltip.SetActive(false);
        }
    }

    public IEnumerator Interact()
    {
        bool shouldAdvance = _scenarios[_currentIndex].Condition == null || _scenarios[_currentIndex].Condition.IsMet();
        if (shouldAdvance && _currentIndex < _scenarios.Length - 1)
        {
            _currentIndex++;
        }
            

        yield return _scenarioRunner.Run(_scenarios[_currentIndex].Scenario);

        if (_scenarios[_currentIndex].DisableAfter)
        {
            _interactionTrigger.enabled = false;
            yield break;
        }
    }
}

[Serializable]
public class ScenarioEntry
{
    public Scenario Scenario;
    public TransitionCondition Condition;
    public bool DisableAfter;
}

