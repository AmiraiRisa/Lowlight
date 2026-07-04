using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Instance { get; private set; }

    [SerializeField] private DialoguePresenter _dialoguePresenter;
    [SerializeField] private InventoryComponent _inventoryComponent;
    [SerializeField] private ScenarioRunner _scenarioRunner;
    [SerializeField] private AudioSource _audioSystem;
    [SerializeField] private RoomsManager _roomsManager;
    [SerializeField] private TaskManager _taskManager;
    [SerializeField] private WorldStateManager _worldStateManager;


    public DialoguePresenter DialoguePresenter => _dialoguePresenter;
    public InventoryComponent InventoryComponent => _inventoryComponent;
    public ScenarioRunner ScenarioRunner => _scenarioRunner;
    public AudioSource AudioSystem => _audioSystem;
    public RoomsManager RoomsManager => _roomsManager;
    public TaskManager TaskManager => _taskManager;
    public WorldStateManager WorldStateManager => _worldStateManager;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }
}
