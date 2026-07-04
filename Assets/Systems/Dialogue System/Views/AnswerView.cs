using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _answer;
    [SerializeField] private GameObject _marker;

    public void ChooseMarker(bool value)
    {
        _marker.SetActive(value);
    }

    public void SetText(string text)
    {
        _answer.text = text;
    }
}
