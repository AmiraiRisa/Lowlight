using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour
{
    [SerializeField] private InputReader input;
    public Rigidbody2D Rigidbody { get; private set; }


    [SerializeField] private float defaultSpeed;
    [SerializeField] private float addedSpeed;

    private Vector2 _moveInput;
    private float _speed;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        _speed = defaultSpeed;
    }

    private void OnEnable()
    {
        input.MoveEvent += HandleMove;
        input.SprintEvent += SprintMove;
    }

    private void OnDisable()
    {
        input.MoveEvent -= HandleMove;
        input.SprintEvent -= SprintMove;
    }

    private void Update()
    {
        Rigidbody.velocity = _moveInput * _speed;
    }

    private void HandleMove(Vector2 input)
    {
        _moveInput = input;
    }

    private void SprintMove(bool phase)
    {
        if (phase)
        {
            _speed = defaultSpeed + addedSpeed;
        }
        else
        {
            _speed = defaultSpeed;
        }
    }
}
