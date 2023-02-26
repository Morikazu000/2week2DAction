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

    [SerializeField,Range(0.001f,1)]
    private float _RayRange = 0.001f;
    private float _HalfScaleX = default;
    private float _HalfScaleY = default;
    private float _startRaydiff = 0.0001f;//Rayのスタートポジション調整
    private float _insideAjust = 0.1f;//Ray同士の処理が重複しないため

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
        //右上
        Debug.DrawRay(new Vector2(_ColliderRightPoint.x + _startRaydiff, _ColliderRightPoint.y - _insideAjust), Vector2.right * _RayRange, Color.red);
        //右下
        Debug.DrawRay(new Vector2(_ColliderRightPoint.x + _startRaydiff, _ColliderLeftPoint.y + _insideAjust), Vector2.right * _RayRange, Color.blue);

        // 衝突したものを取得するためのRay出す
        // 右上から出ているRay
        RaycastHit2D rightTopHitObject = Physics2D.Raycast(new Vector2(_ColliderRightPoint.x + _startRaydiff, _ColliderRightPoint.y - _insideAjust), Vector2.right, _RayRange);
        // 右下から出ているRay
        RaycastHit2D rightUnderHitObject = Physics2D.Raycast(new Vector2(_ColliderRightPoint.x + _startRaydiff, _ColliderLeftPoint.y + _insideAjust), Vector2.right, _RayRange);


        // 何も衝突していない時
        if (rightTopHitObject.collider == null && rightUnderHitObject.collider == null)
        {

            _isRightWallTouch = false;
            return;
        }

        // 右下から出ているRayがnullで、右上から出ているRayに当たっているTagがGroundだった時
        if (rightUnderHitObject.collider == null && rightTopHitObject.collider.CompareTag("Ground"))
        {
            // 当たった位置を計算
            float hitPoint = rightTopHitObject.transform.position.x - rightTopHitObject.transform.localScale.x / 2 - _HalfScaleX;

            //ポジション調整
            gameObject.transform.position = new Vector2(hitPoint, gameObject.transform.position.y);

            // 壁との接触判定をtrueにする
            _isRightWallTouch = true;
            return;
        }

        // 右下から出ているRayがnullで、右上から出ているRayに当たっているTagがGroundだった時
        if (rightTopHitObject.collider == null && rightUnderHitObject.collider.CompareTag("Ground"))
        {
            // 当たった位置を計算
            float hitPoint = rightUnderHitObject.transform.position.x - rightUnderHitObject.transform.localScale.x / 2 - _HalfScaleX;

            //ポジション調整
            gameObject.transform.position = new Vector2(hitPoint, gameObject.transform.position.y);

            // 壁との接触判定をtrueにする
            _isRightWallTouch = true;
            return;
        }

        //Rayに触れたオブジェクトのTagが("Ground")だったら
        if (rightTopHitObject.collider.CompareTag("Ground") && rightUnderHitObject.collider.CompareTag("Ground"))
        {
            // 当たった位置を計算
            float hitPoint = rightTopHitObject.transform.position.x - rightTopHitObject.transform.localScale.x / 2 - _HalfScaleX;

            //ポジション調整
            gameObject.transform.position = new Vector2(hitPoint, gameObject.transform.position.y);

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
        //左上
        Debug.DrawRay(new Vector2(_ColliderLeftPoint.x - _startRaydiff, _ColliderRightPoint.y - _insideAjust), Vector2.left * _RayRange, Color.red);
        //左下
        Debug.DrawRay(new Vector2(_ColliderLeftPoint.x - _startRaydiff, _ColliderLeftPoint.y + _insideAjust), Vector2.left * _RayRange, Color.blue);

        // 衝突したものを取得するためのRay出す
        // 右上から出ているRay
        RaycastHit2D leftTopHitObject = Physics2D.Raycast(new Vector2(_ColliderLeftPoint.x - _startRaydiff, _ColliderRightPoint.y - _insideAjust), Vector2.left, _RayRange);
        // 右下から出ているRay
        RaycastHit2D LeftUnderHitObject = Physics2D.Raycast(new Vector2(_ColliderLeftPoint.x - _startRaydiff, _ColliderLeftPoint.y + _insideAjust), Vector2.left, _RayRange);


        // 何も衝突していない時
        if (leftTopHitObject.collider == null && LeftUnderHitObject.collider == null)
        {

            _isLeftWallTouch = false;
            return;
        }

        // 左下から出ているRayがnullで、左上から出ているRayに当たっているTagがGroundだった時
        if (LeftUnderHitObject.collider == null && leftTopHitObject.collider.CompareTag("Ground"))
        {

            // 当たった位置を計算
            float hitPoint = leftTopHitObject.transform.position.x + leftTopHitObject.transform.localScale.x / 2 + _HalfScaleX;

            //ポジション調整
            gameObject.transform.position = new Vector2(hitPoint, gameObject.transform.position.y);

            // 壁との接触判定をtrueにする
            _isLeftWallTouch = true;
            return;
        }

        // 左上から出ているRayがnullで、左下から出ているRayに当たっているTagがGroundだった時
        if (leftTopHitObject.collider == null && LeftUnderHitObject.collider.CompareTag("Ground"))
        {
            // 当たった位置を計算
            float hitPoint = LeftUnderHitObject.transform.position.x + LeftUnderHitObject.transform.localScale.x / 2 + _HalfScaleX;

            //ポジション調整
            gameObject.transform.position = new Vector2(hitPoint, gameObject.transform.position.y);

            // 壁との接触判定をtrueにする
            _isLeftWallTouch = true;
            return;
        }

        //Rayに触れたオブジェクトのTagが("Ground")だったら
        if (leftTopHitObject.collider.CompareTag("Ground") && LeftUnderHitObject.collider.CompareTag("Ground"))
        {
            // 当たった位置を計算
            float hitPoint = leftTopHitObject.transform.position.x + leftTopHitObject.transform.localScale.x / 2 + _HalfScaleX;

            //ポジション調整
            gameObject.transform.position = new Vector2(hitPoint, gameObject.transform.position.y);

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
        //右下
        Debug.DrawRay(new Vector2(_ColliderRightPoint.x -_insideAjust, _ColliderLeftPoint.y - _startRaydiff), Vector2.down * _RayRange, Color.red);
        //左下
        Debug.DrawRay(new Vector2(_ColliderLeftPoint.x + _insideAjust, _ColliderLeftPoint.y - _startRaydiff), Vector2.down * _RayRange, Color.blue);

        // 衝突したものを取得するためのRay出す
        // 右上から出ているRay
        RaycastHit2D groundRightHitObject = Physics2D.Raycast(new Vector2(_ColliderRightPoint.x - _insideAjust, _ColliderLeftPoint.y - _startRaydiff), Vector2.down, _RayRange);
        // 右下から出ているRay
        RaycastHit2D groundLeftHitObject = Physics2D.Raycast(new Vector2(_ColliderLeftPoint.x + _insideAjust, _ColliderLeftPoint.y - _startRaydiff), Vector2.down, _RayRange);


        // 何も衝突していない時
        if (groundRightHitObject.collider == null && groundLeftHitObject.collider == null)
        {

            _isGroundTouch = false;
            return;
        }

        // 左下に向かって出ているRayがnullで、右下に向かって出ているRayに当たっているTagがGroundだった時
        if (groundLeftHitObject.collider == null && groundRightHitObject.collider.CompareTag("Ground"))
        {

            // 当たった位置を計算
            float hitPoint = groundRightHitObject.transform.position.y + groundRightHitObject.transform.localScale.y / 2 + _HalfScaleY;

            // 衝突したポイントにポジション調整
            gameObject.transform.position = new Vector2(gameObject.transform.position.x , hitPoint);

            // 壁との接触判定をtrueにする
            _isGroundTouch = true;
            return;
        }

        // 右下に向かって出ているRayがnullで、左下に向かって出ているRayに当たっているTagがGroundだった時
        if (groundRightHitObject.collider == null && groundLeftHitObject.collider.CompareTag("Ground"))
        {
            // 当たった位置を計算
            float hitPoint = groundLeftHitObject.transform.position.y + groundLeftHitObject.transform.localScale.y / 2 + _HalfScaleY;

            // 衝突したポイントにポジション調整
            gameObject.transform.position = new Vector2(gameObject.transform.position.x,hitPoint);

            // 壁との接触判定をtrueにする
            _isGroundTouch = true;
            return;
        }

        // 両方のRayに触れたオブジェクトのTagが("Ground")だったら
        if (groundRightHitObject.collider.CompareTag("Ground") && groundLeftHitObject.collider.CompareTag("Ground"))
        {
            // 当たった位置を計算
            float hitPoint = groundRightHitObject.transform.position.y + groundRightHitObject.transform.localScale.y / 2 + _HalfScaleY;

            // 衝突したポイントにポジション調整
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, hitPoint);

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
        //右下
        Debug.DrawRay(new Vector2(_ColliderRightPoint.x - _insideAjust, _ColliderRightPoint.y + _startRaydiff), Vector2.up * _RayRange, Color.red);
        //左下
        Debug.DrawRay(new Vector2(_ColliderLeftPoint.x + _insideAjust, _ColliderRightPoint.y + _startRaydiff), Vector2.up * _RayRange, Color.blue);

        // 衝突したものを取得するためのRay出す
        // 右上から出ているRay
        RaycastHit2D roofRightHitObject = Physics2D.Raycast(new Vector2(_ColliderRightPoint.x - _insideAjust, _ColliderRightPoint.y + _startRaydiff), Vector2.up, _RayRange);
        // 右下から出ているRay
        RaycastHit2D roofLeftHitObject = Physics2D.Raycast(new Vector2(_ColliderLeftPoint.x + _insideAjust, _ColliderRightPoint.y + _startRaydiff), Vector2.up, _RayRange);


        // 何も衝突していない時
        if (roofRightHitObject.collider == null && roofLeftHitObject.collider == null)
        {

            _isRoofTouch = false;
            return;
        }

        // 左上に向かって出ているRayがnullで、右上に向かって出ているRayに当たっているTagがGroundだった時
        if (roofLeftHitObject.collider == null && roofRightHitObject.collider.CompareTag("Ground"))
        {
            // 当たった位置を計算
            float hitPoint = roofRightHitObject.transform.position.y - roofRightHitObject.transform.localScale.y / 2 - _HalfScaleY;

            // ポジション調整
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, hitPoint);

            // 壁との接触判定をtrueにする
            _isRoofTouch = true;
            return;
        }

        //下からのRayがGroundに当たってて、上からのRayが何も衝突していなかったら
        if (roofRightHitObject.collider == null && roofLeftHitObject.collider.CompareTag("Ground"))
        {
            // 当たった位置を計算
            float hitPoint = roofLeftHitObject.transform.position.y - roofLeftHitObject.transform.localScale.y / 2 - _HalfScaleY;

            // ポジション調整
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, hitPoint);

            // 壁との接触判定をtrueにする
            _isRoofTouch = true;
            return;
        }

        //Rayに触れたオブジェクトのTagが("Ground")だったら
        if (roofRightHitObject.collider.CompareTag("Ground") && roofLeftHitObject.collider.CompareTag("Ground"))
        {
            // 当たった位置を計算
            float hitPoint = roofRightHitObject.transform.position.y - roofRightHitObject.transform.localScale.y / 2 - _HalfScaleY;

            // ポジション調整
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, hitPoint);


            // 壁との接触判定をtrueにする
            _isRoofTouch = true;
            return;
        }

    }
}
