using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*-------------------------------------------------------------

�@�@�@�@�@�@�@�@�@�@�@2023/02/13
�@�@�@�@�@�@�@�@�@�@�@�X�R�a�ƒ�
                    �@�Փ˔���
                    
-------------------------------------------------------------*/


public class CollisionScript : MonoBehaviour
{

    private BoxCollider2D _Collider;
    private Transform _tr;

    [SerializeField,Range(0.001f,1)]
    private float _RayRange = 0.001f;
    private float _HalfScaleX = default;
    private float _HalfScaleY = default;
    private float _startRaydiff = 0.0001f;//Ray�̃X�^�[�g�|�W�V��������
    private float _insideAjust = 0.1f;//Ray���m�̏������d�����Ȃ�����

    private Vector2 _ColliderRightPoint = default;
    private Vector2 _ColliderLeftPoint = default;

    protected bool _isRightWallTouch = false;
    protected bool _isLeftWallTouch = false;
    protected bool _isGroundTouch = false;
    protected bool _isRoofTouch = false;


    private void Start()
    {
        // �|�W�V�����擾
        _tr = gameObject.transform;

        // �R���C�_�[�擾
        _Collider = GetComponent<BoxCollider2D>();

        // �R���C�_�[�̔������擾
        _HalfScaleX = _Collider.transform.localScale.x / 2;
        _HalfScaleY = _Collider.transform.localScale.y / 2;


    }

    /// <summary>   
    /// �S�ẴR���W��������
    /// �A�b�v�f�[�g�ł������������ΏՓ˔������
    /// </summary>
    protected void AllCollisionCheck()
    {

        // �R���C�_�[�̉E��̃|�C���g�ݒ�
        _ColliderRightPoint = new Vector2(_tr.localPosition.x + _HalfScaleX, _tr.localPosition.y + _HalfScaleY);

        // �R���C�_�[�̍����̃|�C���g�ݒ�
        _ColliderLeftPoint = new Vector2(_tr.localPosition.x - _HalfScaleX, _tr.localPosition.y - _HalfScaleY);

        RightWallCollision();
        LeftWallCollision();
        GroundCollision();
        RoofCollision();
    }

    /// <summary>
    /// �E�̕ǂ̏Փ˔���
    /// </summary>
    protected void RightWallCollision()
    {
        //�E��
        Debug.DrawRay(new Vector2(_ColliderRightPoint.x + _startRaydiff, _ColliderRightPoint.y - _insideAjust), Vector2.right * _RayRange, Color.red);
        //�E��
        Debug.DrawRay(new Vector2(_ColliderRightPoint.x + _startRaydiff, _ColliderLeftPoint.y + _insideAjust), Vector2.right * _RayRange, Color.blue);

        // �Փ˂������̂��擾���邽�߂�Ray�o��
        // �E�ォ��o�Ă���Ray
        RaycastHit2D rightTopHitObject = Physics2D.Raycast(new Vector2(_ColliderRightPoint.x + _startRaydiff, _ColliderRightPoint.y - _insideAjust), Vector2.right, _RayRange);
        // �E������o�Ă���Ray
        RaycastHit2D rightUnderHitObject = Physics2D.Raycast(new Vector2(_ColliderRightPoint.x + _startRaydiff, _ColliderLeftPoint.y + _insideAjust), Vector2.right, _RayRange);


        // �����Փ˂��Ă��Ȃ���
        if (rightTopHitObject.collider == null && rightUnderHitObject.collider == null)
        {

            _isRightWallTouch = false;
            return;
        }

        // �E������o�Ă���Ray��null�ŁA�E�ォ��o�Ă���Ray�ɓ������Ă���Tag��Ground��������
        if (rightUnderHitObject.collider == null && rightTopHitObject.collider.CompareTag("Ground"))
        {
            // ���������ʒu���v�Z
            float hitPoint = rightTopHitObject.transform.position.x - rightTopHitObject.transform.localScale.x / 2 - _HalfScaleX;

            //�|�W�V��������
            gameObject.transform.position = new Vector2(hitPoint, gameObject.transform.position.y);

            // �ǂƂ̐ڐG�����true�ɂ���
            _isRightWallTouch = true;
            return;
        }

        // �E������o�Ă���Ray��null�ŁA�E�ォ��o�Ă���Ray�ɓ������Ă���Tag��Ground��������
        if (rightTopHitObject.collider == null && rightUnderHitObject.collider.CompareTag("Ground"))
        {
            // ���������ʒu���v�Z
            float hitPoint = rightUnderHitObject.transform.position.x - rightUnderHitObject.transform.localScale.x / 2 - _HalfScaleX;

            //�|�W�V��������
            gameObject.transform.position = new Vector2(hitPoint, gameObject.transform.position.y);

            // �ǂƂ̐ڐG�����true�ɂ���
            _isRightWallTouch = true;
            return;
        }

        //Ray�ɐG�ꂽ�I�u�W�F�N�g��Tag��("Ground")��������
        if (rightTopHitObject.collider.CompareTag("Ground") && rightUnderHitObject.collider.CompareTag("Ground"))
        {
            // ���������ʒu���v�Z
            float hitPoint = rightTopHitObject.transform.position.x - rightTopHitObject.transform.localScale.x / 2 - _HalfScaleX;

            //�|�W�V��������
            gameObject.transform.position = new Vector2(hitPoint, gameObject.transform.position.y);

            // �ǂƂ̐ڐG�����true�ɂ���
            _isRightWallTouch = true;
            return;
        }

    }

    /// <summary>
    /// ���̕ǂ̏Փ˔���
    /// </summary>
    protected void LeftWallCollision()
    {
        //����
        Debug.DrawRay(new Vector2(_ColliderLeftPoint.x - _startRaydiff, _ColliderRightPoint.y - _insideAjust), Vector2.left * _RayRange, Color.red);
        //����
        Debug.DrawRay(new Vector2(_ColliderLeftPoint.x - _startRaydiff, _ColliderLeftPoint.y + _insideAjust), Vector2.left * _RayRange, Color.blue);

        // �Փ˂������̂��擾���邽�߂�Ray�o��
        // �E�ォ��o�Ă���Ray
        RaycastHit2D leftTopHitObject = Physics2D.Raycast(new Vector2(_ColliderLeftPoint.x - _startRaydiff, _ColliderRightPoint.y - _insideAjust), Vector2.left, _RayRange);
        // �E������o�Ă���Ray
        RaycastHit2D LeftUnderHitObject = Physics2D.Raycast(new Vector2(_ColliderLeftPoint.x - _startRaydiff, _ColliderLeftPoint.y + _insideAjust), Vector2.left, _RayRange);


        // �����Փ˂��Ă��Ȃ���
        if (leftTopHitObject.collider == null && LeftUnderHitObject.collider == null)
        {

            _isLeftWallTouch = false;
            return;
        }

        // ��������o�Ă���Ray��null�ŁA���ォ��o�Ă���Ray�ɓ������Ă���Tag��Ground��������
        if (LeftUnderHitObject.collider == null && leftTopHitObject.collider.CompareTag("Ground"))
        {

            // ���������ʒu���v�Z
            float hitPoint = leftTopHitObject.transform.position.x + leftTopHitObject.transform.localScale.x / 2 + _HalfScaleX;

            //�|�W�V��������
            gameObject.transform.position = new Vector2(hitPoint, gameObject.transform.position.y);

            // �ǂƂ̐ڐG�����true�ɂ���
            _isLeftWallTouch = true;
            return;
        }

        // ���ォ��o�Ă���Ray��null�ŁA��������o�Ă���Ray�ɓ������Ă���Tag��Ground��������
        if (leftTopHitObject.collider == null && LeftUnderHitObject.collider.CompareTag("Ground"))
        {
            // ���������ʒu���v�Z
            float hitPoint = LeftUnderHitObject.transform.position.x + LeftUnderHitObject.transform.localScale.x / 2 + _HalfScaleX;

            //�|�W�V��������
            gameObject.transform.position = new Vector2(hitPoint, gameObject.transform.position.y);

            // �ǂƂ̐ڐG�����true�ɂ���
            _isLeftWallTouch = true;
            return;
        }

        //Ray�ɐG�ꂽ�I�u�W�F�N�g��Tag��("Ground")��������
        if (leftTopHitObject.collider.CompareTag("Ground") && LeftUnderHitObject.collider.CompareTag("Ground"))
        {
            // ���������ʒu���v�Z
            float hitPoint = leftTopHitObject.transform.position.x + leftTopHitObject.transform.localScale.x / 2 + _HalfScaleX;

            //�|�W�V��������
            gameObject.transform.position = new Vector2(hitPoint, gameObject.transform.position.y);

            // �ǂƂ̐ڐG�����true�ɂ���
            _isLeftWallTouch = true;
            return;
        }

    }

    /// <summary>
    /// �n�ʂƂ̏Փ˔���
    /// </summary>
    protected void GroundCollision()
    {
        //�E��
        Debug.DrawRay(new Vector2(_ColliderRightPoint.x -_insideAjust, _ColliderLeftPoint.y - _startRaydiff), Vector2.down * _RayRange, Color.red);
        //����
        Debug.DrawRay(new Vector2(_ColliderLeftPoint.x + _insideAjust, _ColliderLeftPoint.y - _startRaydiff), Vector2.down * _RayRange, Color.blue);

        // �Փ˂������̂��擾���邽�߂�Ray�o��
        // �E�ォ��o�Ă���Ray
        RaycastHit2D groundRightHitObject = Physics2D.Raycast(new Vector2(_ColliderRightPoint.x - _insideAjust, _ColliderLeftPoint.y - _startRaydiff), Vector2.down, _RayRange);
        // �E������o�Ă���Ray
        RaycastHit2D groundLeftHitObject = Physics2D.Raycast(new Vector2(_ColliderLeftPoint.x + _insideAjust, _ColliderLeftPoint.y - _startRaydiff), Vector2.down, _RayRange);


        // �����Փ˂��Ă��Ȃ���
        if (groundRightHitObject.collider == null && groundLeftHitObject.collider == null)
        {

            _isGroundTouch = false;
            return;
        }

        // �����Ɍ������ďo�Ă���Ray��null�ŁA�E���Ɍ������ďo�Ă���Ray�ɓ������Ă���Tag��Ground��������
        if (groundLeftHitObject.collider == null && groundRightHitObject.collider.CompareTag("Ground"))
        {

            // ���������ʒu���v�Z
            float hitPoint = groundRightHitObject.transform.position.y + groundRightHitObject.transform.localScale.y / 2 + _HalfScaleY;

            // �Փ˂����|�C���g�Ƀ|�W�V��������
            gameObject.transform.position = new Vector2(gameObject.transform.position.x , hitPoint);

            // �ǂƂ̐ڐG�����true�ɂ���
            _isGroundTouch = true;
            return;
        }

        // �E���Ɍ������ďo�Ă���Ray��null�ŁA�����Ɍ������ďo�Ă���Ray�ɓ������Ă���Tag��Ground��������
        if (groundRightHitObject.collider == null && groundLeftHitObject.collider.CompareTag("Ground"))
        {
            // ���������ʒu���v�Z
            float hitPoint = groundLeftHitObject.transform.position.y + groundLeftHitObject.transform.localScale.y / 2 + _HalfScaleY;

            // �Փ˂����|�C���g�Ƀ|�W�V��������
            gameObject.transform.position = new Vector2(gameObject.transform.position.x,hitPoint);

            // �ǂƂ̐ڐG�����true�ɂ���
            _isGroundTouch = true;
            return;
        }

        // ������Ray�ɐG�ꂽ�I�u�W�F�N�g��Tag��("Ground")��������
        if (groundRightHitObject.collider.CompareTag("Ground") && groundLeftHitObject.collider.CompareTag("Ground"))
        {
            // ���������ʒu���v�Z
            float hitPoint = groundRightHitObject.transform.position.y + groundRightHitObject.transform.localScale.y / 2 + _HalfScaleY;

            // �Փ˂����|�C���g�Ƀ|�W�V��������
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, hitPoint);

            // �ǂƂ̐ڐG�����true�ɂ���
            _isGroundTouch = true;
            return;
        }
    }

    /// <summary>
    /// �V��Ƃ̏Փ˔���
    /// </summary>
    protected void RoofCollision()
    {
        //�E��
        Debug.DrawRay(new Vector2(_ColliderRightPoint.x - _insideAjust, _ColliderRightPoint.y + _startRaydiff), Vector2.up * _RayRange, Color.red);
        //����
        Debug.DrawRay(new Vector2(_ColliderLeftPoint.x + _insideAjust, _ColliderRightPoint.y + _startRaydiff), Vector2.up * _RayRange, Color.blue);

        // �Փ˂������̂��擾���邽�߂�Ray�o��
        // �E�ォ��o�Ă���Ray
        RaycastHit2D roofRightHitObject = Physics2D.Raycast(new Vector2(_ColliderRightPoint.x - _insideAjust, _ColliderRightPoint.y + _startRaydiff), Vector2.up, _RayRange);
        // �E������o�Ă���Ray
        RaycastHit2D roofLeftHitObject = Physics2D.Raycast(new Vector2(_ColliderLeftPoint.x + _insideAjust, _ColliderRightPoint.y + _startRaydiff), Vector2.up, _RayRange);


        // �����Փ˂��Ă��Ȃ���
        if (roofRightHitObject.collider == null && roofLeftHitObject.collider == null)
        {

            _isRoofTouch = false;
            return;
        }

        // ����Ɍ������ďo�Ă���Ray��null�ŁA�E��Ɍ������ďo�Ă���Ray�ɓ������Ă���Tag��Ground��������
        if (roofLeftHitObject.collider == null && roofRightHitObject.collider.CompareTag("Ground"))
        {
            // ���������ʒu���v�Z
            float hitPoint = roofRightHitObject.transform.position.y - roofRightHitObject.transform.localScale.y / 2 - _HalfScaleY;

            // �|�W�V��������
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, hitPoint);

            // �ǂƂ̐ڐG�����true�ɂ���
            _isRoofTouch = true;
            return;
        }

        //�������Ray��Ground�ɓ������ĂāA�ォ���Ray�������Փ˂��Ă��Ȃ�������
        if (roofRightHitObject.collider == null && roofLeftHitObject.collider.CompareTag("Ground"))
        {
            // ���������ʒu���v�Z
            float hitPoint = roofLeftHitObject.transform.position.y - roofLeftHitObject.transform.localScale.y / 2 - _HalfScaleY;

            // �|�W�V��������
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, hitPoint);

            // �ǂƂ̐ڐG�����true�ɂ���
            _isRoofTouch = true;
            return;
        }

        //Ray�ɐG�ꂽ�I�u�W�F�N�g��Tag��("Ground")��������
        if (roofRightHitObject.collider.CompareTag("Ground") && roofLeftHitObject.collider.CompareTag("Ground"))
        {
            // ���������ʒu���v�Z
            float hitPoint = roofRightHitObject.transform.position.y - roofRightHitObject.transform.localScale.y / 2 - _HalfScaleY;

            // �|�W�V��������
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, hitPoint);


            // �ǂƂ̐ڐG�����true�ɂ���
            _isRoofTouch = true;
            return;
        }

    }
}
