using System;
using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;


/// <summary>
/// CSV�t�@�C����Addressables�Ń��[�h����X�N���v�g
/// </summary>
public class CSVLoader : MonoBehaviour
{
    //CSVLoder�̃C���X�^���X
    private static CSVLoader instance;

    /// <summary>
    /// �C���X�^���X�̃Q�b�^�[�Z�b�^�[
    /// </summary>
    public static CSVLoader Instance
    {
        get => instance;
        set => instance = value;
    }

    [SerializeField]
    [Tooltip("���[�h������CSV�̃A�h���X�L�[")]
    private string addressKey;

    /// <summary>
    /// �C���X�^���X��
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// CSV�����[�h����R�[���`��
    /// </summary>
    /// <param name="OnSuccess">���[�h������������</param>
    /// <returns></returns>
    public IEnumerator LoadCSVText(Action<string> onsuccess)
    {
        //�w�肳�ꂽ�A�h���X�L�[�iCSV�t�@�C���j��񓯊��Ń��[�h����
        AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(addressKey);

        //�ǂݍ��݂���������܂ő҂�
        yield return handle;

        //�ǂݍ��݂������������`�F�b�N
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            //�ǂݍ��񂾃e�L�X�g�A�Z�b�g�̒��g���擾
            string csvdata = handle.Result.text;

            //�擾�����e�L�X�g�A�Z�b�g��\��
            Debug.Log(csvdata);

            //�����҂�
            yield return null;

            Debug.Log("����");
        }
        else
        {
            Debug.Log($"CSV�̃��[�h�̎��s:{addressKey}");
        }

        //�ǂݍ��񂾃��\�[�X���������
        Addressables.Release(handle);
    }
}
