using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class InteractionComponent : MonoBehaviour
{
    [SerializeField] private InputReader input;

    private IInteractable _currentInteractable;

    private void OnEnable()
    {
        input.InteractEvent += TryInteract;
    }

    private void OnDisable()
    {
        input.InteractEvent -= TryInteract;
    }

    private void TryInteract()
    {
        StartCoroutine(_currentInteractable.Interact());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            _currentInteractable = interactable;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable interactable)
            && interactable == _currentInteractable)
        {
            _currentInteractable = null;
        }
    }
}
