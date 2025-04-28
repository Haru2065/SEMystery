using UnityEngine;

public class TutorialScenarioManager : ScenarioManager
{
    [SerializeField]
    [Tooltip("�v���C���[�ړ��X�N���v�g")]
    private PlayerMove playerMove;

    /// <summary>
    /// �`���[�g���A���V�[���ŃV�i���I�e�L�X�g��\�����郁�\�b�h
    /// </summary>
    /// <param name="texts">JSON�ŕ\������V�i���I</param>
    /// <param name="Speaker">�N���b���Ă��邩</param>
    public override void ShowScenarioText(ScenarioText texts, string Speaker)
    {
        //�e�L�X�g���\������Ă���ԁA�ړ��ł��Ȃ��悤�ɂ���
        playerMove.IsMoving = true;

        //�V�i���I�̃e�L�X�g�Ɖ�b�L����UGUI��ݒ肷��
        ScenarioTextUGUI.text = texts.text;
        SpeakerTextUGUI.text = Speaker;

        //ID���擾���ĕ\��
        switch (texts.id)
        {
            case 1:
                break;

            case 2:

                //��b�L����UGUI��\��
                SpeakerTextUGUI.gameObject.SetActive(true);
                break;

            case 3:
                break;

            case 4:

                //��b�L����UGUI��\��
                SpeakerTextUGUI.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// �N���b�N���ꂽ��e�L�X�g�̕\����i�߂郁�\�b�h
    /// </summary>
    protected override void OnClickNextText()
    {
        //�x�[�X�̏������s��
        base.OnClickNextText();

        //�����Ō�܂Ńe�L�X�g��\�����ꂽ��e�L�X�g�E�B���h�E���\���ɂ��āA�L�����̑�����\�ɂ���
        if (currentIndex == scenarioList.Count)
        {
            TextWindow.SetActive(false);

            //�L�����̑�����\�ɂ���
            playerMove.IsMoving = false;
        }
            
    }
}

