using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scenario/Commands/PlaySound")]
public class PlaySoundCommand : SceneCommand
{
    public AudioClip clip;
    [SerializeField] private bool isSingle;

    public override IEnumerator Execute()
    {
        ServiceLocator.Instance.AudioSystem.PlayOneShot(clip);
        if (isSingle) 
        { 
            yield return new WaitForSeconds(clip.length); 
        }
        else 
        { 
            yield return null; 
        }

    }
}
