using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class RoomsManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private CinemachineConfiner2D _confiner;
    [SerializeField] private Image _fade;
    [SerializeField] private Transform _player;
    [SerializeField] private RoomData[] _rooms;

    [SerializeField] private float _fadeDuration = 0.5f;
    [SerializeField] private float _pauseDuration = 0.1f;

    public void TransitionToRoom(string roomId, int spawnPointIndex)
    {
        StartCoroutine(DoTransition(FindRoom(roomId), spawnPointIndex));
    }

    public RoomData FindRoom(string roomId)
    {
        return Array.Find(_rooms, r => r.roomId == roomId);
    }

    private IEnumerator DoTransition(RoomData room, int spawnPointIndex)
    {
        yield return StartCoroutine(Fade(0f, 1f));

        Vector3 deltaPosition = room.spawnPoints[spawnPointIndex].position - _player.position;

        _player.position = room.spawnPoints[spawnPointIndex].position;

        _virtualCamera.OnTargetObjectWarped(_player, deltaPosition);
        _confiner.m_BoundingShape2D = room.bounds;
        _confiner.InvalidateCache();

        yield return new WaitForSeconds(_pauseDuration);

        yield return StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float from, float to)
    {
        float elapsed = 0f;
        while (elapsed < _fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, elapsed / _fadeDuration);
            _fade.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
        _fade.color = new Color(0f, 0f, 0f, to);
    }
}

[Serializable]
public class RoomData
{
    public string roomId;
    public CompositeCollider2D bounds;
    public Transform[] spawnPoints;
}