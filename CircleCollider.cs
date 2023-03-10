using UnityEngine;

public class CircleCollider : MonoBehaviour
{
    [SerializeField]
    private Transform _PlayerCharacter; // プレイヤーのTransform

    private float _colliderSize;
     
    private float _vectorX = default; // プレイヤーとの距離
    private float _vectorY = default; // プレイヤーとの距離
    private float _distance = default; // プレイヤーと自オブジェクトの距離
    private float _squareRoot = default; // 平方根を解くための変数

    private bool _collision = default;
    private bool _isRightWallTouch = false;
    private bool _isLeftWallTouch = false;
    private bool _isGroundTouch = false;
    private bool _isRoofTouch = false;


    private void Start()
    {


        // 半径 + 半径
        _colliderSize = (_PlayerCharacter.lossyScale.x / 2) + (this.transform.lossyScale.x / 2);


    }

    private void FixedUpdate()
    {

        // プレイヤーと自オブジェクトの距離の中心
        _vectorX = _PlayerCharacter.position.x - this.gameObject.transform.position.x;
        _vectorY = _PlayerCharacter.position.y - this.gameObject.transform.position.y;

        // ピタゴラスの定理
        _distance = _vectorX * _vectorX + _vectorY * _vectorY;

        // 平方根を解く
        _squareRoot = Mathf.Sqrt(_distance);

        // Playerと自オブジェクトの距離がColliderSizeより小さくなったら
        if (_squareRoot <= _colliderSize)
        {
            VectorDirection();
        }
        else
        {
            _isRightWallTouch = false;
            _isLeftWallTouch= false;
            _isRoofTouch= false;
            _isGroundTouch= false;
        }
    }

    /// <summary>
    /// 自オブジェクトから見たPlayerのオブジェクトの方向
    /// </summary>
    /// <returns>プレイヤーの接触している場所の判定</returns>
    private bool VectorDirection()
    {
        // アークタンジェントでラジアンを算出
        float rad = Mathf.Atan2(_vectorY, _vectorX);

        // ラジアンを角度に変換
        float playerAngle = rad * Mathf.Rad2Deg;

        print(playerAngle);
        // 360°を８等分
        float circleAngle = 45;

        // プレイヤーの接触した地点が -45°より大きく、45°より小さい場合プレイヤーの左に接触
        if (playerAngle > -circleAngle && playerAngle < circleAngle)
        {
            _isLeftWallTouch = true;
            _collision = _isLeftWallTouch;
        }

        // プレイヤーに接触した地点が 135°より大きく、180°より小さい場合または、
        // プレイヤーに接触した地点が -135°より大きく、-180 °より小さい場合プレイヤーの右に接触 
        else if (playerAngle > circleAngle * 3 && playerAngle < circleAngle * 4
            || playerAngle  < -circleAngle * 4 && playerAngle > -circleAngle *31)
        {
            _isRightWallTouch = true;
            _collision = _isRightWallTouch;
        }

        // プレイヤーに接触した地点が45°より大きく、135°より小さい場合プレイヤーの下に接触
        else if (playerAngle > circleAngle && playerAngle < circleAngle * 3)
        {
            _isGroundTouch = true;
            _collision = _isGroundTouch;
        }

        // プレイヤーに接触した地点が -45°より大きく、-135°より小さい場合プレイヤーの上に接触
        else if(playerAngle < -circleAngle && playerAngle > -circleAngle * 4)
        {
            _isRoofTouch = true;
            _collision = _isRoofTouch;
        }

        return _collision;

    }

}
