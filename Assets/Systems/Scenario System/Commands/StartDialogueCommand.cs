using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scenario/Commands/StartDialogue")]
public class ShowDialogueCommand : SceneCommand
{
    [SerializeField] private DialogueGraph _graph;

    public override IEnumerator Execute()
    {
        ServiceLocator.Instance.DialoguePresenter.StartDialogue(_graph);
        yield return new WaitUntil(() => !ServiceLocator.Instance.DialoguePresenter.IsRunning);
    }
}
