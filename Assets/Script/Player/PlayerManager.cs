using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// �v���C���[�̃}�l�[�W���[
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("�v���C���[�̈ړ����x")]
    private float playerMoveSpeed;

    public float PlayerMoveSpeed
    {
        get => playerMoveSpeed;
    }

    //�v���C���[�̈ړ��p�x�N�g��
    private Vector3 moveDir;

    public Vector3 MoveDir
    {
        get => moveDir; set => moveDir = value;
    }
}
