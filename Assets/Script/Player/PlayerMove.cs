using System.Collections;
using UnityEngine;

/// <summary>
/// プレイヤーの操作スクリプト
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    [Tooltip("プレイヤーの移動速度")]
    private float PlayerMoveSpeed;

    [SerializeField]
    [Tooltip("グリッド移動サイズ")]
    public float gridSize;

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

    // Update is called once per frame
    void Update()
    {
        if (isMoving == false)
        {
            Vector2 direction = Vector2.zero;

            // 入力取得（WASD/矢印/ゲームパッド）
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(moveX) > 0) moveY = 0;

            Vector2 moveDir = new Vector2(moveX, moveY);

            if (moveDir != Vector2.zero)
            {
                StartCoroutine(Move(moveDir));
            }

        }

        IEnumerator Move(Vector2 direction)
        {
            isMoving = true;

            Vector3 targetPos = transform.position;

            Vector3 endPos = targetPos + (Vector3)(direction.normalized * gridSize);

            float elapsed = 0f;
            float duration = gridSize / PlayerMoveSpeed;

            while (elapsed < duration)
            {
                transform.position = Vector3.Lerp(targetPos, endPos, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = endPos;
            isMoving = false;
        }


    }
}