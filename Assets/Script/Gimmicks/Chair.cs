//using JetBrains.Annotations;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

///// <summary>
///// �֎q�̃X�N���v�g
///// </summary>
//public class Chair : MonoBehaviour
//{
//    //�͂߂邩
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
//    /// �v���C���[���G�ꂽ��͂߂�悤�ɂ���
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
//    /// �v���C���[�����ꂽ��͂߂Ȃ��悤�ɂ���
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
