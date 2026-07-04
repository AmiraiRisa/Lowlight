using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class PerspectiveScaler : MonoBehaviour
{
    [SerializeField] private float _minY = -5f;
    [SerializeField] private float _maxY = 0;
    [SerializeField] private float _minScale = 1f;
    [SerializeField] private float _maxScale = 1.3f;

    private void FixedUpdate()
    {
        float t = Mathf.InverseLerp(_maxY, _minY, transform.position.y);
        float scale = Mathf.Lerp(_minScale, _maxScale, t);
        transform.localScale = Vector3.one * scale;
    }
}
