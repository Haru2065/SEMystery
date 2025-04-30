using System.IO;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections;
using UnityEditor;
using UnityEngine;
using Unity.EditorCoroutines.Editor;
using System.Reflection;
public class CSVToMapDataWindow : EditorWindow
{
    //���[�h�������A�h���X�L�[
    private string addressKey;

    //�ۑ��p�X
    private string savePath;

    //��������Ă��邩
    private bool isCreating = false;

    /// <summary>
    /// window����郁�\�b�h
    /// </summary>
    /// �c�[���̒���CSV to MapData��\������
    [MenuItem("Tools/CSV to MapData")]
    public static void ShowWindow()
    {
        GetWindow<CSVToMapDataWindow>("CSV��MapData����");
    }        


    /// <summary>
    /// �E�B���h�E���ɕ\������UI��ݒ肷�郁�\�b�h
    /// </summary>
    private void OnGUI()
    {
        GUILayout.Label("CSV �� MapData��������", EditorStyles.boldLabel);

        //������\������

        //���[�h�������A�h���X�L�[�����
        addressKey = EditorGUILayout.TextField("load������CSV�̃A�h���X�L�[�����",addressKey);�@//���͂ł���悤�ɂ���

        //�ۑ��������ꏊ�̕\���i���͂��j
        //savePath = EditorGUILayout.TextField("�ۑ��������ꏊ�����", savePath);

        //�{�^���������Đ������łȂ���ΐ����J�n
        if(GUILayout.Button("Create ScriptableObject") && !isCreating)
        {

            // �ۑ��p�X�����[�U�[�ɑI�΂���
            string path = EditorUtility.SaveFilePanelInProject(
                "ScriptableObject��ۑ�",
                "NewMapData",      // �����t�@�C����
                "asset",           // �g���q
                "ScriptableObject�̕ۑ����I��ł�������"
            );

            // �L�����Z�����ꂽ�ꍇ�͏������Ȃ�
            if (string.IsNullOrEmpty(path))
            {
                return; 
            }

            savePath = path;

            //�����t���O��true
            isCreating = true;
            
            //CSV�����[�h����ScriptableObject�𐶐�����R�[���`�����G�f�B�^�[���ŊJ�n
            EditorCoroutineUtility.StartCoroutineOwnerless(LoadAndCreate());
        }
    }

    /// <summary>
    /// CSV�����[�h���AScriptableObject�𐶐�����R�[���`����
    /// </summary>
    /// <returns>���[�h����������܂ő҂�</returns>
    private IEnumerator LoadAndCreate()
    {
        ///�w�肳�ꂽ�A�h���X�L�[�iCSV�t�@�C���j��񓯊��Ń��[�h����
        AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(addressKey);
        
        //��������܂ő҂�
        yield return handle;

        //�ǂݍ��ݐ�������΃R���\�[���r���[�ɃG���[��\�������A�R�[���`�����~�߂�
        if(handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("CSV���[�h�Ɏ��s���܂����B");

            //�����t���O��false
            isCreating = false;

            //�R�[���`����~
            yield break;
        }
        //�ǂݍ��ݐ���������CSV�̃f�[�^��\������
        else
        {
            //�ǂݍ���CSV�̃f�[�^���擾
            string csvData = handle.Result.text;

            Debug.Log("�ǂݍ��ݐ���!!");

            //�ǂݍ���CSV��\��
            Debug.Log($"CSVdata:{csvData}");
        }

        //�ǂݍ���CSV�e�L�X�g���擾��1�s���Ƃɕ���

        //Trim():�O��̋󔒉��s����菜��
        //Split(\n):�e�L�X�g���s�P�ʂɕ��� 
        string[] lines = handle.Result.text.Trim().Split('\n');
        
        //�c�����̃T�C�Y
        int height = lines.Length;

        //�������̃T�C�Y�@
        //�e�Z�������o���@CSV�̊e�s�̃e�L�X�g�z��
        int width = lines[0].Split(',').Length;

        //�}�b�v�̐��l�S�f�[�^���i�[���邽�߂̈ꎟ���z����쐬
        int[] cells = new int [width * height];

        //�e�s���J���}�ŕ������A�����ɕϊ����� cells �ɋl�ߍ���
        for (int y = 0; y < height; y++)
        {
            //csv�̊e�s���J���}�ŋ�؂�A��̃f�[�^�ɕ�����
            string[] cols = lines[y].Trim().Split(",");

            //�������̗���J��Ԃ��������s��
            for (int x = 0; x < width; x++)
            {
                //�e��̒l���i�[�����z��ł��B
                if (int.TryParse(cols[x], out int val))
                {
                    //�Q�����z����P���z��ɕϊ�
                    cells[y * width + x] = val;
                }
            }
        }

        //�V���� MapData ScriptableObject �����B
        MapData mapdata = ScriptableObject.CreateInstance<MapData>();

        //�ǂݍ��񂾃f�[�^���i�[
        mapdata.width = width;
        mapdata.height = height;
        mapdata.cells = cells;

       

        //�A�Z�b�g���쐬����
        AssetDatabase.CreateAsset(mapdata, savePath);

        //�f�o�C�X�̃f�B�X�N�ɃA�Z�b�g��ۑ�
        AssetDatabase.SaveAssets();

        Debug.Log("��������:" + savePath);

        //�ǂݍ��񂾃��\�[�X�����
        Addressables.Release(handle);

        //������������������false
        isCreating = false;

    }
}
