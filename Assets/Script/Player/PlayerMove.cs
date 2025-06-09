using System.Collections;
using UnityEngine;

/// <summary>
/// プレイヤーの操作スクリプト
/// </summary>
public class PlayerMove : PlayerManager
{

    [SerializeField]
    [Tooltip("グリッド移動サイズ")]
    public float gridSize;

    [SerializeField]
    [Tooltip("椅子の掴むスクリプト")]
    private PlayerGrabber grabber;

    [SerializeField]
    [Tooltip("当たり判定スクリプト")]
    private MapHitManager hitManager;

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

    /// <summary>
    /// 壁がない状態で歩けるかのチェックするメソッド
    /// </summary>
    /// <param name="dir"></param>
    private void TryMove(Vector2 dir)
    {
        //プレイヤーの当たり判定を位置から設定
        Vector3 playerTargetPos = transform.position + (Vector3)(dir * gridSize);

        //椅子を掴んでいる場合椅子も壁との当たり判定を取る
        if(grabber.IsHolding && grabber.chair != null)
        {
            //椅子の当たり判定を椅子の位置から設定
            Vector3 chairTargetPos = grabber.chair.position + (Vector3)(dir * gridSize);

            //もし壁があれば動けないようにする
            if(hitManager.IsWall(chairTargetPos)) return;
        }

        //もし壁があればプレイヤーは動けないようにする
        if (hitManager.IsWall(playerTargetPos)) return;

        //プレイヤーの移動コールチンを開始
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