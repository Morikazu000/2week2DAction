using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : UseGravity
{

    [SerializeField, Header("�ړ����x")]
    private float _PlayerSpeed = default;

    [SerializeField, Header("�W�����v��")]
    private float _jumpStartPower = default;

    [SerializeField,Header("�W�����v������")]
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
    ///�ړ��p���\�b�h
    /// </summary>
    private void MoveSystem()
    {
        float _Horizontal = Input.GetAxisRaw("Horizontal");
        
        switch (_Horizontal)
        {
            case 0:
                return;
                
                //�E�Ɉړ��ł��邩�ǂ���
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

                //���Ɉړ��ł��邩�ǂ���
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
    /// �W�����v����p�̃��\�b�h
    /// </summary>
    private void JumpSystem()
    {
        //�|�W�V����y�����Z���Ă���
        this.transform.position = new Vector2(transform.position.x, transform.position.y + _nowJumpPower);

        _nowJumpPower -= _jumpSubtraction;
        
        if(_nowJumpPower <= 0)
        {
            _isJump = false;
            _nowJumpPower = _jumpStartPower;
        }
    }

    
}
