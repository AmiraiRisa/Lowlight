using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePresenter : MonoBehaviour
{
    [SerializeField] private DialogueSystem _system;
    [SerializeField] private DialogueView _view;
    [SerializeField] private InputReader _input;

    public bool IsRunning => _system.IsRunning;

    public void StartDialogue(DialogueGraph graph)
    {
        _input.SetMode(InputMode.Dialogue);
        
        _system.OnNodeChanged += _view.UpdateView;

        _system.OnDialogueEnded += OnDialogueEnded;
        _input.NextPhraseEvent += OnNext;
        _input.DialogueNavigateEvent += OnNavigate;
        _view.OnTypingComplete += OnTypingComplete;

        _system.StartDialogue(graph);
        _view.Show();
    }

    private void OnNext()
    {
        if (_view.IsTyping)
        {
            _view.CompleteText();
            return;
        }
        if (_view.SelectedIndex > -1)
        {
            StartCoroutine(_system.Choose(_view.SelectedIndex));
            return;
        }

        _system.Next();
    }

    private void OnNavigate(int direction)
    {
        _view.Navigate(direction);
    }

    private void OnDialogueEnded()
    {
        _system.OnNodeChanged -= _view.UpdateView;
        _system.OnDialogueEnded -= OnDialogueEnded;
        _input.NextPhraseEvent -= OnNext;
        _input.DialogueNavigateEvent -= OnNavigate;
        _view.OnTypingComplete -= OnTypingComplete;

        _view.Hide();
        _input.SetMode(InputMode.Gameplay);
    }

    private void OnTypingComplete()
    {
        _view.ShowAnswers(_system.CurrentNode);
    }
}
