using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// �J���[ID�̗񋓌^
/// </summary>
[Serializable]
public enum ColorID { Red, Green, Blue, Yellow, Purple, Gray, LightBlue, Brown, Pink }

/// <summary>
/// �J���[�p�X���[�h���}�l�[�W���\�ŊǗ�
/// </summary>
public class ColorPassWardManager : MonoBehaviour
{
    //�J���[�p�X���[�h�̐����̏��ԃ��X�g
    public List<ColorID> passWordSequence = new List<ColorID>
    {
        ColorID.Red, ColorID.Yellow,ColorID.Green,
        ColorID.Blue, ColorID.Purple, ColorID.LightBlue,
        ColorID.Gray,ColorID.Brown, ColorID.Pink
    };

    [Header("UI�Q��")]
    [SerializeField] private TextMeshProUGUI resultText;

    [SerializeField, Tooltip("�J���[�{�^��")] private Button[] colorButtons;

    [SerializeField]
    [Tooltip("���Z�b�g�{�^��")]
    private Button ResetButton;

    [SerializeField]
    [Tooltip("����{�^��")]
    private Button closeButton;

    [SerializeField]
    [Tooltip("�q���g�e�L�X�g")]
    private TextMeshProUGUI hintText;

    [SerializeField]
    [Tooltip("�e�L�X�g�E�B���h�E")]
    private GameObject textWindow;

    [SerializeField]
    [Tooltip("�p�X���[�h���͑S�̉��")]
    private GameObject colorPassWardWindow;

    

    //���ǂ��܂œ��͐����ł��Ă��邩
    private int currectIndex = 0;

    //������͂�����
    private int inputCount = 0;




    // Start is called before the first frame update
    void Start()
    {
        //�z�񕶒l��
        for(int i = 0; i < colorButtons.Length; i++)
        {
            ColorID id = colorButtons[i].GetComponent<ColorButton>().colorID;

            colorButtons[i].onClick.AddListener(() => OnColorButtonPressed(id));
        }

        //���Z�b�g�{�^���������ꂽ��{�^�������ƃe�L�X�g�����Z�b�g
        ResetButton.onClick.AddListener(() => Reset());

        //����{�^���������ꂽ��A�p�X���[�h���͉�ʂ����
        closeButton.onClick.AddListener(() => CloseColorPassWardWindow());

        //�q���g�l�e�L�X�g�E�B���h�E�����
        textWindow.SetActive(false);

        //�����\��������
        resultText.text = "";
    }

    /// <summary>
    /// /�J���[�p�X���[�h���������Ƃ��̏���
    /// </summary>
    /// <param name="pressedID">�J���[��ID</param>
    public void OnColorButtonPressed(ColorID pressedID)
    {
        //�J���[ID�ƃp�X���[�h�̓��͒l����v������A���͐����񐔂��J�E���g
        if (pressedID == passWordSequence[currectIndex])
        {
            //���͐����񐔂𑝂₷
            currectIndex++;

            Debug.Log(currectIndex);

            //���͐����񐔂������ɒB���������
            if(currectIndex >= passWordSequence.Count)
            {
                //��������
                unlock();
            }
            else
            {
                resultText.text = $"{pressedID}";
            }
        }
        //�ԈႤ�Ǝ��s
        else
        {
            //���s����
            Fail();

            //���͎��s�񐔂𑝂₷
            inputCount++;

            //���͎��s�񐔂��R��B������q���g���o��
            if(inputCount >= 3)
            {
                //�e�L�X�g�E�B���h�E��\��
                textWindow.SetActive(true);
                hintText.text = $"�T�����ă�����T����";

                //���Z�b�g�R�[���`�����J�n
                StartCoroutine(ResetText());
            }
        }
        
    }

    //�����������\�b�h
    private void unlock()
    {
        //�����ƕ\��
        resultText.text = "success!!";

        //���Z�b�g�R�[���`���J�n
        StartCoroutine(ResetText());
    }

    /// <summary>
    /// �������s���\�b�h
    /// </summary>
    private void Fail()
    {
        //���s��\������
        resultText.text = "failure";
    }

    /// <summary>
    /// ���Z�b�g���\�b�h
    /// </summary>
    private void Reset()
    {
        //�����񐔂����Z�b�g
        currectIndex = 0;

        //�o�̓E�B���h�E���\��
        resultText.text = "";
    }

    /// <summary>
    /// �J���[�p�X���[�h��ʂ���郁�\�b�h
    /// </summary>
    private void CloseColorPassWardWindow()
    {
        colorPassWardWindow.SetActive(false);
    }

    /// <summary>
    /// ���Z�b�g�̃R�[���`��
    /// </summary>
    /// <returns>2�t���[���҂�</returns>
    private IEnumerator ResetText()
    {
        //2�t���[���ナ�Z�b�g���\�b�h�J�n
        yield return new WaitForSeconds(2f);
        Reset();


        //���͎��s�񐔂��R�ɒB������e�L�X�g�E�B���h�E���\��
        if (inputCount >= 3)
        {
            textWindow.SetActive(false);

            //���͎��s�񐔂����Z�b�g
            inputCount = 0;
        }

        //1�t���[���҂�
        yield return null;
    }
}
