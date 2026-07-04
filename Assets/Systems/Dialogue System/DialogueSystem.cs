using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public event Action<DialogueNode> OnNodeChanged;
    public event Action OnDialogueEnded;

    public bool IsRunning { get; private set; }

    public DialogueNode CurrentNode { get; private set; }

    public void StartDialogue(DialogueGraph graph)
    {
        IsRunning = true;
        GetNode(graph.StartNode);
    }

    public void Next()
    {
        if (CurrentNode.Choices.Count == 0)
        {
            EndDialogue();
            return;
        }
        if (CurrentNode.Choices.Count == 1)
        {
            GetNode(CurrentNode.Choices[0].Next);
            return;
        }
    }

    public IEnumerator Choose(int index)
    {
        var choice = CurrentNode.Choices[index];

        if (choice.Scenario != null)
        {
            yield return ServiceLocator.Instance.ScenarioRunner.Run(choice.Scenario);
        }
        GetNode(choice.Next);
    }

    private void GetNode(DialogueNode node)
    {
        CurrentNode = node;
        OnNodeChanged?.Invoke(node);
    }

    private void EndDialogue()
    {
        IsRunning = false;
        OnDialogueEnded?.Invoke();
    }
}
