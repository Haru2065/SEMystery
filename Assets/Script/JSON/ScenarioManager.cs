using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// Json�t�@�C����id����text���
/// </summary>
[Serializable]
public class ScenarioText
{
    public int id;
    public string Speaker;
    public string text;
}

/// <summary>
/// Addressables�Ń��[�h����Json��C#�ɕϊ�������ɕ\������X�N���v�g
/// </summary>
public abstract class ScenarioManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Json���[�h����X�N���v�g")]
    private JsonLoder loder;

    [SerializeField]
    [Tooltip("�ǂݍ��݂���Json�����")]
    private string ScenarioJsonKey;

    [SerializeField]
    [Tooltip("�e�L�X�g�E�B���h�E")]
    protected GameObject TextWindow;

    [SerializeField]
    [Tooltip("Json�t�@�C����\������UGUI")]
    protected TextMeshProUGUI ScenarioTextUGUI;

    [SerializeField]
    [Tooltip("���ꂪ�b���Ă��邩��\������UGUI")]
    protected TextMeshProUGUI SpeakerTextUGUI;

    //�ϊ������e�L�X�g�����Ԃɕ\�����郊�X�g
    protected List<ScenarioText> scenarioList;

    //�\���C���f�b�N�X
    protected int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        //Json�ǂݍ��݃X�N���v�g���烍�[�h�J�n
        //�󂯎����Json���p�[�X
        StartCoroutine(loder.LoadJsonText(json =>
        {
            //Json�S�̂�Dictionary�ɕϊ�
            var dict = JsonConvert.DeserializeObject<Dictionary<string, List<ScenarioText>>>(json);

            //json�L�[������Ε\�����X�g�ɑ��
            if (dict.TryGetValue(ScenarioJsonKey, out scenarioList))
            {
                currentIndex = 0;

                //�e�L�X�g�E�B���h�E��\��
                TextWindow.SetActive(true);

                //Json�ŏ����ꂽ�V�i���I��\��
                ShowScenarioText(scenarioList[currentIndex], scenarioList[currentIndex].Speaker);
            }
            else
            {
                Debug.LogWarning($"�w�肵���L�[��������܂���ł����B{ScenarioJsonKey}");
            }

            //�ŏ��͒N���b���Ă��邩��\������UGUI���\���ɂ���
            SpeakerTextUGUI.gameObject.SetActive(false);
        }));
    }

    /// <summary>
    /// �e�L�X�g�N���b�N����
    /// </summary>
    protected void Update()
    {
        //�}�E�X���N���b�N���ꂽ��e�L�X�g�̕\����i�߂�
        if (Input.GetMouseButtonDown(0))
        {
            OnClickNextText();
        }
    }

    /// <summary>
    /// �N���b�N���ꂽ��e�L�X�g�̕\����i�߂郁�\�b�h
    /// </summary>
    protected virtual void OnClickNextText()
    {
        //�\���C���f�b�N�X�����ɐi�߂�
        currentIndex++;

        //�\���C���f�b�N�X���Ō�܂ŕ\������܂ŕ\��
        if (currentIndex < scenarioList.Count)
        {
            //�\���C���f�b�N�X�̐��ɓ����Ă���Json�e�L�X�g��\��
            ShowScenarioText(scenarioList[currentIndex], scenarioList[currentIndex].Speaker);
        }
    }

    /// <summary>
    /// �ϊ�����Json�V�i���I��\��
    /// </summary>
    public abstract void ShowScenarioText(ScenarioText texts, string Speaker);
}
