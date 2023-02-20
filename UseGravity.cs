using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*-------------------------------------------------------------

　　　　　　　　　　　2023/02/17
　　　　　　　　　　　森山和哉著
                    　重力処理
                    
-------------------------------------------------------------*/

public class UseGravity : CollisionScript
{
   
    [SerializeField, Header("重力加速度")]
    private float _gravityAcceleration = 9.8f;

    [SerializeField, Header("加速最大値")]
    private float _maxSpeed = 20f;

    private float _fallTime = default;

    /// <summary>
    /// 重力処理
    /// </summary>
    protected void GravitySystem()
    {
        //地面と接触していたら
        if(_isGroundTouch == true)
        {
            print("あああ");
            _fallTime = 0;
            return;
        }
        
        //地面と接触していなかったら
        //落下時間
        _fallTime += Time.deltaTime;

        //落下時間に重力をかける
        float fallSpeed = _gravityAcceleration * _fallTime;

        //スピードに制限を付ける
        if(fallSpeed <= -_maxSpeed)
        {
            fallSpeed = _maxSpeed;
        }

        //落下する速度
        float nowSpeed = -fallSpeed * Time.deltaTime;

        this.transform.Translate(new Vector3(0f, nowSpeed, 0f));

    }
}
