using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : UseGravity
{

    [SerializeField, Header("移動速度")]
    private float _PlayerSpeed = default;

    [SerializeField, Header("ジャンプ力")]
    private float _jumpStartPower = default;

    [SerializeField,Header("ジャンプ減少量")]
    private float _jumpSubtraction = default;

    private float _nowJumpPower = default;
    private bool _isJump = false;


    void Update()
    {
        AllCollisionCheck();
        MoveSystem();

        if (Input.GetKeyDown(KeyCode.Space) && _isJump == false)
        {

            _nowJumpPower = _jumpStartPower;
            _isJump = true;

        }

        if(_isRoofTouch == true)
        {
            _nowJumpPower = 0;
        }
    }

    private void FixedUpdate()
    {
        GravitySystem();


        if (_isJump == true)
        {
            JumpSystem();
        }
                
        if (Input.GetKey(KeyCode.W) && _isRoofTouch == false)
        {
            transform.Translate(Vector2.up * _PlayerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) && _isGroundTouch == false)
        {
            transform.Translate(Vector2.down * _PlayerSpeed * Time.deltaTime);
        }
    }

    ///<summary>
    ///移動用メソッド
    /// </summary>
    private void MoveSystem()
    {
        float _Horizontal = Input.GetAxisRaw("Horizontal");
        
        switch (_Horizontal)
        {
            case 0:
                return;
                
                //右に移動できるかどうか
            case 1:
                if (_isRightWallTouch == false)
                {
                    transform.Translate(Vector2.right * _PlayerSpeed * Time.deltaTime);
                    break;
                }
                else
                {
                    return;
                }

                //左に移動できるかどうか
            case -1:
                if (_isLeftWallTouch == false)
                {
                    transform.Translate(Vector2.left * _PlayerSpeed * Time.deltaTime);
                    break;

                }
                else
                {
                    return;
                }

        }



    }

    /// <summary>
    /// ジャンプする用のメソッド
    /// </summary>
    private void JumpSystem()
    {
        //ポジションyを加算していく
        this.transform.position = new Vector2(transform.position.x, transform.position.y + _nowJumpPower);

        _nowJumpPower -= _jumpSubtraction;
        
        if(_nowJumpPower <= 0)
        {
            _isJump = false;
            _nowJumpPower = _jumpStartPower;
        }
    }

    
}
