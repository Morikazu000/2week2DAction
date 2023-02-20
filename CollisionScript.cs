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

    [SerializeField, Header("�Փ˔���̋���"), Range(0.001f, 5f)]
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
        Debug.DrawRay(new Vector2(_ColliderRightPoint.x, transform.position.y), Vector2.right * _RayRange, Color.red);

        // �Փ˂������̂��擾���邽�߂�Ray�o��
        RaycastHit2D hitObject = Physics2D.Raycast(new Vector2(_ColliderRightPoint.x, transform.position.y), Vector2.right, _RayRange);
       
        // �����Փ˂��Ă��Ȃ��A�������͏Փ˂������̂�tag��Untagged��������
        if (hitObject.collider == null || hitObject.collider.CompareTag("Untagged"))
        {
            _isRightWallTouch = false;
            return;
        }

        // Ray�ɐG�ꂽ�I�u�W�F�N�g��Tag��("Ground")��������
        else if (hitObject.collider.CompareTag("Ground"))
        {

            // �Փ˂����|�C���g�Ƀ|�W�V�������� (�Փ˂����I�u�W�F�N�g�̃|�W�V�����@-�@�Փ˂����I�u�W�F�N�g��x���̑傫��, �������g��y��)
            gameObject.transform.position = new Vector2(hitObject.transform.position.x - hitObject.transform.localScale.x, gameObject.transform.position.y);

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
        Debug.DrawRay(new Vector2(_ColliderLeftPoint.x, transform.position.y), Vector2.left * _RayRange, Color.red);

        // �Փ˂������̂��擾���邽�߂�Ray�o��
        RaycastHit2D hitObject = Physics2D.Raycast(new Vector2(_ColliderLeftPoint.x, transform.position.y), Vector2.left, _RayRange);

        // �����Փ˂��Ă��Ȃ��A�������͏Փ˂������̂�tag��Untagged��������
        if (hitObject.collider == null || hitObject.collider.CompareTag ("Untagged"))
        {
            _isLeftWallTouch = false;
            return;
        }

        // Ray�ɐG�ꂽ�I�u�W�F�N�g��Tag��("Ground")��������
        else if (hitObject.collider.CompareTag("Ground"))
        {
            // �Փ˂����|�C���g�Ƀ|�W�V�������� (�Փ˂����I�u�W�F�N�g�̃|�W�V�����@+�@�Փ˂����I�u�W�F�N�g��x���̑傫��, �������g��y��)
            gameObject.transform.position = new Vector2(hitObject.transform.position.x + hitObject.transform.localScale.x, gameObject.transform.position.y);

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
        Debug.DrawRay(new Vector2(transform.position.x, _ColliderLeftPoint.y), Vector2.down * _RayRange, Color.red);

        // �Փ˂������̂��擾���邽�߂�Ray�o��
        RaycastHit2D hitObject = Physics2D.Raycast(new Vector2(transform.position.x, _ColliderLeftPoint.y), Vector2.down, _RayRange);


        // �����Փ˂��Ă��Ȃ��A�������͏Փ˂������̂�tag��Untagged��������
        if (hitObject.collider == null || hitObject.collider.CompareTag("Untagged"))
        {
            _isGroundTouch = false;
            return;
        }

        // Ray�ɐG�ꂽ�I�u�W�F�N�g��Tag��("Ground")��������
        else if (hitObject.collider.CompareTag("Ground"))
        {
            // �Փ˂����|�C���g�Ƀ|�W�V��������
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, hitObject.transform.position.y + hitObject.transform.localScale.y);

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
        Debug.DrawRay(new Vector2(transform.position.x, _ColliderRightPoint.y), Vector2.up * _RayRange, Color.red);

        // �Փ˂������̂��擾���邽�߂�Ray�o��
        RaycastHit2D hitObject = Physics2D.Raycast(new Vector2(transform.position.x, _ColliderRightPoint.y), Vector2.up, _RayRange);


        // �����Փ˂��Ă��Ȃ��A�������͏Փ˂������̂�tag��Untagged��������
        if (hitObject.collider == null || hitObject.collider.CompareTag("Untagged"))
        {
            print("�����Փ˂��ĂȂ�");
            _isRoofTouch = false;
            return;
        }

        // Ray�ɐG�ꂽ�I�u�W�F�N�g��Tag��("Ground")�ŁA�V��ɐڐG���Ă��Ȃ��������
        else if (hitObject.collider.CompareTag("Ground") && _isRoofTouch == false)
        {
            print("�Փ˂��Ă��");
            // �Փ˂����|�C���g�Ƀ|�W�V��������
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, hitObject.transform.position.y - hitObject.transform.localScale.y);

            // �ǂƂ̐ڐG�����true�ɂ���
            _isRoofTouch = true;
            return;
        }

    }

}
