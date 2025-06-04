using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// プレイヤーのマネージャー
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [SerializeField, Tooltip("プレイヤーの移動速度")]
    private float playerMoveSpeed;

    public float PlayerMoveSpeed
    {
        get => playerMoveSpeed;
    }

   

    //プレイヤーの移動用ベクトル
    private Vector3 moveDir;

    public Vector3 MoveDir
    {
        get => moveDir; set => moveDir = value;
    }

}
