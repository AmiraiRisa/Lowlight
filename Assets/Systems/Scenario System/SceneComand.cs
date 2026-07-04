using System.Collections;
using UnityEngine;

public abstract class SceneCommand : ScriptableObject
{
    public abstract IEnumerator Execute();
}
