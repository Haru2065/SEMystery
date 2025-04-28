using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


/// <summary>
/// Json��Addressables�Ń��[�h����X�N���v�g
/// </summary>
public class JsonLoder : MonoBehaviour
{
    [SerializeField]
    [Tooltip("���[�h������Json�t�@�C���̃A�h���X�L�[�����")]
    private string addressKey;

    /// <summary>
    /// Json�����[�h���A�R�[���o�b�N�ɓn���R�[���`��
    /// </summary>
    /// <param name="onSuccess"></param>
    /// <returns>���[�h����������܂ő҂�</returns>
    public IEnumerator LoadJsonText(Action<string> onSuccess)
    {
        //�w�肳�ꂽ�A�h���X�L�[����TextAsset��񓯊��Ń��[�h����
        AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(addressKey);

        //�ǂݍ��݂���������܂ł܂�
        yield return handle;

        //�ǂݍ��݂������������`�F�b�N
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            //�ǂݍ���TextAsset�̒��g���擾����
            string json = handle.Result.text;

            //�擾����TextAsset�̓��e���R���\�[���Ŋm�F
            Debug.Log(json);

            //�����҂�
            yield return null;

            //������\��
            Debug.Log("����!");

            //�������̃R�[���o�b�N�iJsonUtility�ł̃p�[�X��\���������Ăяo���B
            onSuccess?.Invoke(json);
        }
        else
        {
            Debug.Log($"Json�̃��[�h���s:{addressKey}");
        }

        //�ǂݍ��񂾃��\�[�X���������
        Addressables.Release(handle);
    }
}
