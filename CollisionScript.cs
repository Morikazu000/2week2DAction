using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{

    private BoxCollider2D _Collider;
    private Transform _tr;

    [SerializeField, Header("�Փ˔���̋���"), Range(0.5f, 5f)]
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
        //�|�W�V�����擾
        _tr = gameObject.transform;

        //�R���C�_�[�擾
        _Collider = GetComponent<BoxCollider2D>();

        //�R���C�_�[�̔������擾
        _HalfScaleX = _Collider.transform.localScale.x / 2;
        _HalfScaleY = _Collider.transform.localScale.y / 2;

       
    }

    /// <summary>   
    /// �S�ẴR���W��������
    /// �A�b�v�f�[�g�ł������������ΏՓ˔������
    /// </summary>
    protected void AllCollisionCheck()
    {

        //�R���C�_�[�̉E��̃|�C���g�ݒ�
        _ColliderRightPoint = new Vector2(_tr.localPosition.x + _HalfScaleX, _tr.localPosition.y + _HalfScaleY);

        //�R���C�_�[�̍����̃|�C���g�ݒ�
        _ColliderLeftPoint = new Vector2(_tr.localPosition.x - _HalfScaleX, _tr.localPosition.y - _HalfScaleY);


        RightWallCollision();
        LeftWallCollision();

    }  

    /// <summary>
    /// �E�̕ǂ̏Փ˔���
    /// </summary>
    protected void RightWallCollision()
    {
        print("�ڐG����E");
        Debug.DrawRay(new Vector2(_ColliderRightPoint.x, transform.position.y), Vector2.right * _RayRange , Color.red);

        //�Փ˂������̂��擾���邽�߂�Ray�o��
        RaycastHit2D _HitObject = Physics2D.Raycast(new Vector2(_ColliderRightPoint.x, transform.position.y), Vector2.right, _RayRange);

        //Ray�ɐG�ꂽ�I�u�W�F�N�g��Tag��("Ground")��������
        if (_HitObject.collider.CompareTag("Ground"))
        {
            //�Փ˂����|�C���g�Ƀ|�W�V��������
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

            //�ǂƂ̐ڐG�����true�ɂ���
            _isRightWallTouch = true;
            return;
        }

        //�Փ˂��Ă��Ȃ�������
        else 
        {
            //�ڐG����false�ɂ���
            _isRightWallTouch = false;
        }
    }

    /// <summary>
    /// ���̕ǂ̐ڐG����
    /// </summary>
    protected void LeftWallCollision()
    {
        print("�ڐG���荶");
        Debug.DrawRay(new Vector2(_ColliderRightPoint.x, transform.position.y), Vector2.left * _RayRange, Color.red);

        //�Փ˂������̂��擾���邽�߂�Ray�o��
        RaycastHit2D _HitObject = Physics2D.Raycast(new Vector2(_ColliderLeftPoint.x, transform.position.y), Vector2.left, _RayRange);

        //Ray�ɐG�ꂽ�I�u�W�F�N�g��Tag��("Ground")��������
        if (_HitObject.collider.CompareTag("Ground"))
        {
            //�Փ˂����|�C���g�Ƀ|�W�V��������
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

            //�ǂƂ̐ڐG�����true�ɂ���
            _isLeftWallTouch = true;
            return;
        }

        else
        {
            _isLeftWallTouch = false;
        }

    }
}
