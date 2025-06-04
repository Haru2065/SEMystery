using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// プレイヤーの操作スクリプト
/// </summary>
public class PlayerMove : PlayerManager
{

    [SerializeField]
    [Tooltip("グリッド移動サイズ")]
    public float gridSize;

    //[SerializeField]
    [SerializeField]
    [Tooltip("椅子の掴むスクリプト")]
    private PlayerGrabber grabber;

    //移動できるか
    private bool isMoving;

    /// <summary>
    /// 移動できるかのゲッターセッター
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

            // 入力取得（WASD/矢印/ゲームパッド）
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(moveX) > 0) moveY = 0;

            Vector2 inputDir = new Vector2(moveX, moveY);

            if (inputDir != Vector2.zero)
            {
                TryMove(inputDir);
            }
    }

    private void TryMove(Vector2 dir)
    {
        Vector3 playerTargetPos = transform.position + (Vector3)(dir * gridSize);

        if(grabber.IsHolding && grabber.chair != null)
        {
            Vector3 chairTargetPos = grabber.chair.position + (Vector3)(dir * gridSize);

            if(MapHitManager.Instance.IsWall(chairTargetPos)) return;

           
        }

        if (MapHitManager.Instance.IsWall(playerTargetPos)) return;

        StartCoroutine(Move(transform, playerTargetPos));
    }

    //プレイヤーのみ移動させるコールチン
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