using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    [Tooltip("プレイヤーのTrasnformを指定")]
    private Transform playerTarget;

    
    [SerializeField]
    [Tooltip("カメラのオフセット")]
    private Vector3 offset;

    [SerializeField]
    [Tooltip("カメラの追尾速度")]
    private float cameraSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// プレイヤーの処理の後に処理を行うためにLateUpdateを使用
    /// </summary>
    void LateUpdate()
    {
        if(playerTarget == null)
        {
            Debug.LogWarning("Player Transform is not assigned!");
            return;
        }

        // プレイヤーの位置にオフセットを加えた目標位置を計算
        Vector3 desiredPosition = playerTarget.position + offset;

        // スムーズにカメラを移動
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed);

        // カメラの位置を更新
        transform.position = smoothedPosition;

        // プレイヤーを常に注視
        transform.LookAt(playerTarget);
    }
}
