using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Node")]
public class DialogueNode : ScriptableObject
{
    public string SpeakerName;
    [TextArea] public string Text;
    public List<DialogueChoice> Choices;
}

[Serializable]
public class DialogueChoice
{
    public string Text;
    public DialogueNode Next;
    public Scenario Scenario;
}
