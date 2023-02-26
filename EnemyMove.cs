using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : UseGravity
{
    [SerializeField,Header("ˆÚ“®‘¬“x"),Range(1f,5)]
    private float _moveSpeed = 1;

    [SerializeField, Header("ˆÚ“®”ÍˆÍ")]
    private float _moveRange = 5;
    
    private Vector3 tr;

    private bool _isMoveLeft = false;
    private bool _isMoveRight = true;
    void Start()
    {
        tr = GetComponent<Transform>().position;
    }

    void Update()
    {
        AllCollisionCheck();

        EnemyMoveSystem();
        print(tr.x + _moveRange);
    }
    private void FixedUpdate()
    {
        GravitySystem();

    }
    private void EnemyMoveSystem()
    {
        if(transform.position.x < tr.x - _moveRange)
        {
            _isMoveLeft = false;
            _isMoveRight = true;
            
        }
        else if(transform.position.x > tr.x  + _moveRange)
        {
            _isMoveLeft = true;
            _isMoveRight = false;
        }

        if (_isMoveRight == true)
        {
            transform.Translate(Vector2.right * _moveSpeed * Time.deltaTime);
        }
        if (_isMoveLeft == true)
        {
            transform.Translate(Vector2.left * _moveSpeed * Time.deltaTime);
        }

    }
}
