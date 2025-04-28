using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    [Tooltip("�v���C���[��Trasnform���w��")]
    private Transform playerTarget;

    
    [SerializeField]
    [Tooltip("�J�����̃I�t�Z�b�g")]
    private Vector3 offset;

    [SerializeField]
    [Tooltip("�J�����̒ǔ����x")]
    private float cameraSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// �v���C���[�̏����̌�ɏ������s�����߂�LateUpdate���g�p
    /// </summary>
    void LateUpdate()
    {
        if(playerTarget == null)
        {
            Debug.LogWarning("Player Transform is not assigned!");
            return;
        }

        // �v���C���[�̈ʒu�ɃI�t�Z�b�g���������ڕW�ʒu���v�Z
        Vector3 desiredPosition = playerTarget.position + offset;

        // �X���[�Y�ɃJ�������ړ�
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed);

        // �J�����̈ʒu���X�V
        transform.position = smoothedPosition;

        // �v���C���[����ɒ���
        transform.LookAt(playerTarget);
    }
}
