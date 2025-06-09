//using JetBrains.Annotations;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

///// <summary>
///// 椅子のスクリプト
///// </summary>
//public class Chair : MonoBehaviour
//{
//    //掴めるか
//    private bool canGrabbed;

//    public bool CanGrabbed
//    {
//        get => canGrabbed; 
//        set => canGrabbed = value;
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        canGrabbed = false;
//    }

//    /// <summary>
//    /// プレイヤーが触れたら掴めるようにする
//    /// </summary>
//    /// <param name="other"></param>
//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            canGrabbed = true;
//        }
//    }

//    /// <summary>
//    /// プレイヤーが離れたら掴めないようにする
//    /// </summary>
//    /// <param name="other"></param>
//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            canGrabbed = false;
//        }
//    }
//}
