using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{

    private BoxCollider2D _Collider;
    private Transform _tr;

    [SerializeField, Header("衝突判定の距離"), Range(0.5f, 5f)]
    private float _RayRange = 0.5f;

    private float _HalfScaleX = default;
    private float _HalfScaleY = default;
    private Vector2 _ColliderRightPoint = default;
    private Vector2 _ColliderLeftPoint = default;

    protected bool _isRightWallTouch = false;
    protected bool _isLeftWallTouch = false;
    protected bool _isGroundTouch = false;
    protected bool _isRoofTouch = false;


    private void Start()
    {
        //ポジション取得
        _tr = gameObject.transform;

        //コライダー取得
        _Collider = GetComponent<BoxCollider2D>();

        //コライダーの半分を取得
        _HalfScaleX = _Collider.transform.localScale.x / 2;
        _HalfScaleY = _Collider.transform.localScale.y / 2;

       
    }

    /// <summary>   
    /// 全てのコリジョン判定
    /// アップデートでこれを処理すれば衝突判定取れる
    /// </summary>
    protected void AllCollisionCheck()
    {

        //コライダーの右上のポイント設定
        _ColliderRightPoint = new Vector2(_tr.localPosition.x + _HalfScaleX, _tr.localPosition.y + _HalfScaleY);

        //コライダーの左下のポイント設定
        _ColliderLeftPoint = new Vector2(_tr.localPosition.x - _HalfScaleX, _tr.localPosition.y - _HalfScaleY);


        RightWallCollision();
        LeftWallCollision();

    }  

    /// <summary>
    /// 右の壁の衝突判定
    /// </summary>
    protected void RightWallCollision()
    {
        print("接触判定右");
        Debug.DrawRay(new Vector2(_ColliderRightPoint.x, transform.position.y), Vector2.right * _RayRange , Color.red);

        //衝突したものを取得するためのRay出す
        RaycastHit2D _HitObject = Physics2D.Raycast(new Vector2(_ColliderRightPoint.x, transform.position.y), Vector2.right, _RayRange);

        //Rayに触れたオブジェクトのTagが("Ground")だったら
        if (_HitObject.collider.CompareTag("Ground"))
        {
            //衝突したポイントにポジション調整
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

            //壁との接触判定をtrueにする
            _isRightWallTouch = true;
            return;
        }

        //衝突していなかったら
        else 
        {
            //接触判定falseにする
            _isRightWallTouch = false;
        }
    }

    /// <summary>
    /// 左の壁の接触判定
    /// </summary>
    protected void LeftWallCollision()
    {
        print("接触判定左");
        Debug.DrawRay(new Vector2(_ColliderRightPoint.x, transform.position.y), Vector2.left * _RayRange, Color.red);

        //衝突したものを取得するためのRay出す
        RaycastHit2D _HitObject = Physics2D.Raycast(new Vector2(_ColliderLeftPoint.x, transform.position.y), Vector2.left, _RayRange);

        //Rayに触れたオブジェクトのTagが("Ground")だったら
        if (_HitObject.collider.CompareTag("Ground"))
        {
            //衝突したポイントにポジション調整
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

            //壁との接触判定をtrueにする
            _isLeftWallTouch = true;
            return;
        }

        else
        {
            _isLeftWallTouch = false;
        }

    }
}
