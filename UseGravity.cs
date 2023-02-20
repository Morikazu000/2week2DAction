using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*-------------------------------------------------------------

�@�@�@�@�@�@�@�@�@�@�@2023/02/17
�@�@�@�@�@�@�@�@�@�@�@�X�R�a�ƒ�
                    �@�d�͏���
                    
-------------------------------------------------------------*/

public class UseGravity : CollisionScript
{
   
    [SerializeField, Header("�d�͉����x")]
    private float _gravityAcceleration = 9.8f;

    [SerializeField, Header("�����ő�l")]
    private float _maxSpeed = 20f;

    private float _fallTime = default;

    /// <summary>
    /// �d�͏���
    /// </summary>
    protected void GravitySystem()
    {
        //�n�ʂƐڐG���Ă�����
        if(_isGroundTouch == true)
        {
            print("������");
            _fallTime = 0;
            return;
        }
        
        //�n�ʂƐڐG���Ă��Ȃ�������
        //��������
        _fallTime += Time.deltaTime;

        //�������Ԃɏd�͂�������
        float fallSpeed = _gravityAcceleration * _fallTime;

        //�X�s�[�h�ɐ�����t����
        if(fallSpeed <= -_maxSpeed)
        {
            fallSpeed = _maxSpeed;
        }

        //�������鑬�x
        float nowSpeed = -fallSpeed * Time.deltaTime;

        this.transform.Translate(new Vector3(0f, nowSpeed, 0f));

    }
}
