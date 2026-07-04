using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using GameCore.UserInput;

public class InputReader : MonoBehaviour
{
    //Gameplay
    public event Action<Vector2> MoveEvent;
    public event Action<bool> SprintEvent;
    public event Action InteractEvent;
    public event Action OpenInventoryEvent;

    //Dialogue
    public event Action NextPhraseEvent;
    public event Action<int> DialogueNavigateEvent;

    //Inventory 
    public event Action<Vector2> InventoryNavigateEvent;
    public event Action CloseInventoryEvent;



    private UserInput _input;


    private void Awake()
    {
        _input = new UserInput();
    }

    private void OnEnable()
    {
        RegisterGameplay();
        RegisterDialogue();
        RegisterInventory();

        SetMode(InputMode.Gameplay);
    }

    private void OnDisable()
    {
        UnregisterGameplay();
        UnregisterDialogue();
        UnregisterInventory();
    }

    public void SetMode(InputMode mode)
    {
        _input.Gameplay.Disable();
        _input.Dialogue.Disable();
        _input.Inventory.Disable();

        switch (mode)
        {
            case InputMode.Gameplay:
                _input.Gameplay.Enable();
                break;
            case InputMode.Dialogue:
                _input.Dialogue.Enable();
                break;
            case InputMode.Inventory:
                _input.Inventory.Enable();
                break;
        }
    }

    private void RegisterGameplay()
    {
        _input.Gameplay.Move.performed += OnMove;
        _input.Gameplay.Move.canceled += OnMove;

        _input.Gameplay.Sprint.performed += OnSprint;
        _input.Gameplay.Sprint.canceled += OnSprint;

        _input.Gameplay.Interact.performed += OnInteract;

        _input.Gameplay.OpenInventory.performed += OnOpenInventory;
    }

    private void UnregisterGameplay()
    {
        _input.Gameplay.Move.performed -= OnMove;
        _input.Gameplay.Move.canceled -= OnMove;

        _input.Gameplay.Sprint.performed -= OnSprint;
        _input.Gameplay.Sprint.canceled -= OnSprint;

        _input.Gameplay.Interact.performed -= OnInteract;

        _input.Gameplay.OpenInventory.performed -= OnOpenInventory;
    }

    private void RegisterDialogue()
    {
        _input.Dialogue.Next.performed += OnDialogueNext;
        _input.Dialogue.Navigate.performed += OnNavigate;
    }

    private void UnregisterDialogue()
    {
        _input.Dialogue.Next.performed -= OnDialogueNext;
        _input.Dialogue.Navigate.performed -= OnNavigate;
    }

    private void RegisterInventory()
    {
        _input.Inventory.Navigate.performed += OnInventoryNavigate;
        _input.Inventory.Close.performed += OnCloseInventory;
    }

    private void UnregisterInventory()
    {
        _input.Inventory.Navigate.performed -= OnInventoryNavigate;
        _input.Inventory.Close.performed -= OnCloseInventory;
    }


    private void OnMove(InputAction.CallbackContext ctx)
    {
        MoveEvent?.Invoke(ctx.ReadValue<Vector2>());
    }

    private void OnSprint(InputAction.CallbackContext ctx)
    {
        SprintEvent?.Invoke(ctx.phase == InputActionPhase.Performed);
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        InteractEvent?.Invoke();
    }


    private void OnDialogueNext(InputAction.CallbackContext ctx)
    {
        NextPhraseEvent?.Invoke();
    }
    private void OnOpenInventory(InputAction.CallbackContext ctx)
    {
        OpenInventoryEvent?.Invoke();
    }

    private void OnCloseInventory(InputAction.CallbackContext ctx)
    {
        CloseInventoryEvent?.Invoke();
    }

    private void OnInventoryNavigate(InputAction.CallbackContext ctx)
    {
        InventoryNavigateEvent?.Invoke(ctx.ReadValue<Vector2>());
    }

    private void OnNavigate(InputAction.CallbackContext ctx)
    {
        DialogueNavigateEvent?.Invoke((int)ctx.ReadValue<float>());
    }
}
