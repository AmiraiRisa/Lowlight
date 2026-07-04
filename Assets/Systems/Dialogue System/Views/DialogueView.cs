using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueView : MonoBehaviour
{
    [SerializeField] private TMP_Text _speaker;
    [SerializeField] private TMP_Text _phrase;
    [SerializeField] private List<AnswerView> _answers;
    [SerializeField] private GameObject _answerPrefab;

    [SerializeField] private GameObject _ui;
    [SerializeField] private GameObject _container;

    private Coroutine _typingCoroutine;
    public int SelectedIndex;
    private string _text;

    public event Action OnTypingComplete;

    public bool IsTyping { get; private set; }

    private void Awake()
    {
        Hide();
    }

    public void Show()
    {
        _ui.SetActive(true);
    }

    public void Hide()
    {
        _ui.SetActive(false);
    }

    public void UpdateView(DialogueNode node)
    {
        foreach (var answer in _answers)
            Destroy(answer.gameObject);
        _answers.Clear();

        _speaker.text = node.SpeakerName;
        _text = node.Text;

        _typingCoroutine = StartCoroutine(TypeText(_text));
    }

    public void Navigate(int direction)
    {
        if (_answers.Count == 0) return;
        SelectedIndex = Mathf.Clamp(SelectedIndex + direction, 0, _answers.Count - 1);
        UpdateSelection();
    }

    public void ShowAnswers(DialogueNode node)
    {
        if (node.Choices.Count == 0 || node.Choices[0].Text == string.Empty)
        {
            SelectedIndex = -1;
            return;
        }

        SelectedIndex = 0;
        foreach (var choice in node.Choices)
        {
            var obj = Instantiate(_answerPrefab, _container.transform);
            var view = obj.GetComponent<AnswerView>();
            view.SetText(choice.Text);
            _answers.Add(view);
        }

        UpdateSelection();
    }

    private void UpdateSelection()
    {
        for (int i = 0; i < _answers.Count; i++)
            _answers[i].ChooseMarker(i == SelectedIndex);
    }

    public void CompleteText()
    {
        StopCoroutine(_typingCoroutine);
        IsTyping = false;
        _phrase.text = _text;
        OnTypingComplete?.Invoke();
    }

    private IEnumerator TypeText(string text)
    {
        IsTyping = true;

        _phrase.text = string.Empty;
        foreach (char c in text)
        {
            _phrase.text += c;
            yield return new WaitForSeconds(0.02f);
        }

        IsTyping = false;
        OnTypingComplete?.Invoke();
    }

}
