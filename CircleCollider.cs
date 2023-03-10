using UnityEngine;

public class CircleCollider : MonoBehaviour
{
    [SerializeField]
    private Transform _PlayerCharacter; // �v���C���[��Transform

    private float _colliderSize;
     
    private float _vectorX = default; // �v���C���[�Ƃ̋���
    private float _vectorY = default; // �v���C���[�Ƃ̋���
    private float _distance = default; // �v���C���[�Ǝ��I�u�W�F�N�g�̋���
    private float _squareRoot = default; // ���������������߂̕ϐ�

    private bool _collision = default;
    private bool _isRightWallTouch = false;
    private bool _isLeftWallTouch = false;
    private bool _isGroundTouch = false;
    private bool _isRoofTouch = false;


    private void Start()
    {


        // ���a + ���a
        _colliderSize = (_PlayerCharacter.lossyScale.x / 2) + (this.transform.lossyScale.x / 2);


    }

    private void FixedUpdate()
    {

        // �v���C���[�Ǝ��I�u�W�F�N�g�̋����̒��S
        _vectorX = _PlayerCharacter.position.x - this.gameObject.transform.position.x;
        _vectorY = _PlayerCharacter.position.y - this.gameObject.transform.position.y;

        // �s�^�S���X�̒藝
        _distance = _vectorX * _vectorX + _vectorY * _vectorY;

        // ������������
        _squareRoot = Mathf.Sqrt(_distance);

        // Player�Ǝ��I�u�W�F�N�g�̋�����ColliderSize��菬�����Ȃ�����
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
    /// ���I�u�W�F�N�g���猩��Player�̃I�u�W�F�N�g�̕���
    /// </summary>
    /// <returns>�v���C���[�̐ڐG���Ă���ꏊ�̔���</returns>
    private bool VectorDirection()
    {
        // �A�[�N�^���W�F���g�Ń��W�A�����Z�o
        float rad = Mathf.Atan2(_vectorY, _vectorX);

        // ���W�A�����p�x�ɕϊ�
        float playerAngle = rad * Mathf.Rad2Deg;

        print(playerAngle);
        // 360�����W����
        float circleAngle = 45;

        // �v���C���[�̐ڐG�����n�_�� -45�����傫���A45����菬�����ꍇ�v���C���[�̍��ɐڐG
        if (playerAngle > -circleAngle && playerAngle < circleAngle)
        {
            _isLeftWallTouch = true;
            _collision = _isLeftWallTouch;
        }

        // �v���C���[�ɐڐG�����n�_�� 135�����傫���A180����菬�����ꍇ�܂��́A
        // �v���C���[�ɐڐG�����n�_�� -135�����傫���A-180 ����菬�����ꍇ�v���C���[�̉E�ɐڐG 
        else if (playerAngle > circleAngle * 3 && playerAngle < circleAngle * 4
            || playerAngle  < -circleAngle * 4 && playerAngle > -circleAngle *31)
        {
            _isRightWallTouch = true;
            _collision = _isRightWallTouch;
        }

        // �v���C���[�ɐڐG�����n�_��45�����傫���A135����菬�����ꍇ�v���C���[�̉��ɐڐG
        else if (playerAngle > circleAngle && playerAngle < circleAngle * 3)
        {
            _isGroundTouch = true;
            _collision = _isGroundTouch;
        }

        // �v���C���[�ɐڐG�����n�_�� -45�����傫���A-135����菬�����ꍇ�v���C���[�̏�ɐڐG
        else if(playerAngle < -circleAngle && playerAngle > -circleAngle * 4)
        {
            _isRoofTouch = true;
            _collision = _isRoofTouch;
        }

        return _collision;

    }

}
