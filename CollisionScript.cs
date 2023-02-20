using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*-------------------------------------------------------------

　　　　　　　　　　　2023/02/13
　　　　　　　　　　　森山和哉著
                    　衝突判定
                    
-------------------------------------------------------------*/


public class CollisionScript : MonoBehaviour
{

    private BoxCollider2D _Collider;
    private Transform _tr;

    [SerializeField, Header("衝突判定の距離"), Range(0.001f, 5f)]
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
        // ポジション取得
        _tr = gameObject.transform;

        // コライダー取得
        _Collider = GetComponent<BoxCollider2D>();

        // コライダーの半分を取得
        _HalfScaleX = _Collider.transform.localScale.x / 2;
        _HalfScaleY = _Collider.transform.localScale.y / 2;

       
    }

    /// <summary>   
    /// 全てのコリジョン判定
    /// アップデートでこれを処理すれば衝突判定取れる
    /// </summary>
    protected void AllCollisionCheck()
    {

        // コライダーの右上のポイント設定
        _ColliderRightPoint = new Vector2(_tr.localPosition.x + _HalfScaleX, _tr.localPosition.y + _HalfScaleY);

        // コライダーの左下のポイント設定
        _ColliderLeftPoint = new Vector2(_tr.localPosition.x - _HalfScaleX, _tr.localPosition.y - _HalfScaleY);

        RightWallCollision();
        LeftWallCollision();
        GroundCollision();
        RoofCollision();
    }

    /// <summary>
    /// 右の壁の衝突判定
    /// </summary>
    protected void RightWallCollision()
    {
        Debug.DrawRay(new Vector2(_ColliderRightPoint.x, transform.position.y), Vector2.right * _RayRange, Color.red);

        // 衝突したものを取得するためのRay出す
        RaycastHit2D hitObject = Physics2D.Raycast(new Vector2(_ColliderRightPoint.x, transform.position.y), Vector2.right, _RayRange);
       
        // 何も衝突していない、もしくは衝突したもののtagがUntaggedだった時
        if (hitObject.collider == null || hitObject.collider.CompareTag("Untagged"))
        {
            _isRightWallTouch = false;
            return;
        }

        // Rayに触れたオブジェクトのTagが("Ground")だったら
        else if (hitObject.collider.CompareTag("Ground"))
        {

            // 衝突したポイントにポジション調整 (衝突したオブジェクトのポジション　-　衝突したオブジェクトのx軸の大きさ, 自分自身のy軸)
            gameObject.transform.position = new Vector2(hitObject.transform.position.x - hitObject.transform.localScale.x, gameObject.transform.position.y);

            // 壁との接触判定をtrueにする
            _isRightWallTouch = true;
            return;
        }

    }

    /// <summary>
    /// 左の壁の衝突判定
    /// </summary>
    protected void LeftWallCollision()
    {
        Debug.DrawRay(new Vector2(_ColliderLeftPoint.x, transform.position.y), Vector2.left * _RayRange, Color.red);

        // 衝突したものを取得するためのRay出す
        RaycastHit2D hitObject = Physics2D.Raycast(new Vector2(_ColliderLeftPoint.x, transform.position.y), Vector2.left, _RayRange);

        // 何も衝突していない、もしくは衝突したもののtagがUntaggedだった時
        if (hitObject.collider == null || hitObject.collider.CompareTag ("Untagged"))
        {
            _isLeftWallTouch = false;
            return;
        }

        // Rayに触れたオブジェクトのTagが("Ground")だったら
        else if (hitObject.collider.CompareTag("Ground"))
        {
            // 衝突したポイントにポジション調整 (衝突したオブジェクトのポジション　+　衝突したオブジェクトのx軸の大きさ, 自分自身のy軸)
            gameObject.transform.position = new Vector2(hitObject.transform.position.x + hitObject.transform.localScale.x, gameObject.transform.position.y);

            // 壁との接触判定をtrueにする
            _isLeftWallTouch = true;
            return;
        }

    }

    /// <summary>
    /// 地面との衝突判定
    /// </summary>
    protected void GroundCollision()
    {
        Debug.DrawRay(new Vector2(transform.position.x, _ColliderLeftPoint.y), Vector2.down * _RayRange, Color.red);

        // 衝突したものを取得するためのRay出す
        RaycastHit2D hitObject = Physics2D.Raycast(new Vector2(transform.position.x, _ColliderLeftPoint.y), Vector2.down, _RayRange);


        // 何も衝突していない、もしくは衝突したもののtagがUntaggedだった時
        if (hitObject.collider == null || hitObject.collider.CompareTag("Untagged"))
        {
            _isGroundTouch = false;
            return;
        }

        // Rayに触れたオブジェクトのTagが("Ground")だったら
        else if (hitObject.collider.CompareTag("Ground"))
        {
            // 衝突したポイントにポジション調整
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, hitObject.transform.position.y + hitObject.transform.localScale.y);

            // 壁との接触判定をtrueにする
            _isGroundTouch = true;
            return;
        }
    }

    /// <summary>
    /// 天井との衝突判定
    /// </summary>
    protected void RoofCollision()
    {
        Debug.DrawRay(new Vector2(transform.position.x, _ColliderRightPoint.y), Vector2.up * _RayRange, Color.red);

        // 衝突したものを取得するためのRay出す
        RaycastHit2D hitObject = Physics2D.Raycast(new Vector2(transform.position.x, _ColliderRightPoint.y), Vector2.up, _RayRange);


        // 何も衝突していない、もしくは衝突したもののtagがUntaggedだった時
        if (hitObject.collider == null || hitObject.collider.CompareTag("Untagged"))
        {
            print("何も衝突してない");
            _isRoofTouch = false;
            return;
        }

        // Rayに触れたオブジェクトのTagが("Ground")で、天井に接触していなかったらら
        else if (hitObject.collider.CompareTag("Ground") && _isRoofTouch == false)
        {
            print("衝突してるよ");
            // 衝突したポイントにポジション調整
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, hitObject.transform.position.y - hitObject.transform.localScale.y);

            // 壁との接触判定をtrueにする
            _isRoofTouch = true;
            return;
        }

    }

}
