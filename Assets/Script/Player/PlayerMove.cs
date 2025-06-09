using System.Collections;
using UnityEngine;

/// <summary>
/// �v���C���[�̑���X�N���v�g
/// </summary>
public class PlayerMove : PlayerManager
{

    [SerializeField]
    [Tooltip("�O���b�h�ړ��T�C�Y")]
    public float gridSize;

    [SerializeField]
    [Tooltip("�֎q�̒͂ރX�N���v�g")]
    private PlayerGrabber grabber;

    [SerializeField]
    [Tooltip("�����蔻��X�N���v�g")]
    private MapHitManager hitManager;

    //�ړ��ł��邩
    private bool isMoving;

    /// <summary>
    /// �ړ��ł��邩�̃Q�b�^�[�Z�b�^�[
    /// </summary>
    public bool IsMoving
    {
        get => isMoving;
        set => isMoving = value;
    }

    private void Start()
    {
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMoving) return;

            Vector2 direction = Vector2.zero;

            // ���͎擾�iWASD/���/�Q�[���p�b�h�j
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(moveX) > 0) moveY = 0;

            Vector2 inputDir = new Vector2(moveX, moveY);

            if (inputDir != Vector2.zero)
            {
                TryMove(inputDir);
            }
    }

    /// <summary>
    /// �ǂ��Ȃ���Ԃŕ����邩�̃`�F�b�N���郁�\�b�h
    /// </summary>
    /// <param name="dir"></param>
    private void TryMove(Vector2 dir)
    {
        //�v���C���[�̓����蔻����ʒu����ݒ�
        Vector3 playerTargetPos = transform.position + (Vector3)(dir * gridSize);

        //�֎q��͂�ł���ꍇ�֎q���ǂƂ̓����蔻������
        if(grabber.IsHolding && grabber.chair != null)
        {
            //�֎q�̓����蔻����֎q�̈ʒu����ݒ�
            Vector3 chairTargetPos = grabber.chair.position + (Vector3)(dir * gridSize);

            //�����ǂ�����Γ����Ȃ��悤�ɂ���
            if(hitManager.IsWall(chairTargetPos)) return;
        }

        //�����ǂ�����΃v���C���[�͓����Ȃ��悤�ɂ���
        if (hitManager.IsWall(playerTargetPos)) return;

        //�v���C���[�̈ړ��R�[���`�����J�n
        StartCoroutine(Move(transform, playerTargetPos));
    }

    //�v���C���[�݈̂ړ�������R�[���`��
    IEnumerator Move(Transform  mover,Vector3 target)
    {
        isMoving = true;

        Vector3 start = mover.position;

        float elapsed = 0f;
        float duration = gridSize / PlayerMoveSpeed;

        while (elapsed < duration)
        {
           mover.position = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mover.position = target;
        isMoving = false;
    }
}