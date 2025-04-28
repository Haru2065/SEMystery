using UnityEngine;

public class TutorialScenarioManager : ScenarioManager
{
    [SerializeField]
    [Tooltip("プレイヤー移動スクリプト")]
    private PlayerMove playerMove;

    /// <summary>
    /// チュートリアルシーンでシナリオテキストを表示するメソッド
    /// </summary>
    /// <param name="texts">JSONで表示するシナリオ</param>
    /// <param name="Speaker">誰が話しているか</param>
    public override void ShowScenarioText(ScenarioText texts, string Speaker)
    {
        //テキストが表示されている間、移動できないようにする
        playerMove.IsMoving = true;

        //シナリオのテキストと会話キャラUGUIを設定する
        ScenarioTextUGUI.text = texts.text;
        SpeakerTextUGUI.text = Speaker;

        //IDを取得して表示
        switch (texts.id)
        {
            case 1:
                break;

            case 2:

                //会話キャラUGUIを表示
                SpeakerTextUGUI.gameObject.SetActive(true);
                break;

            case 3:
                break;

            case 4:

                //会話キャラUGUIを表示
                SpeakerTextUGUI.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// クリックされたらテキストの表示を進めるメソッド
    /// </summary>
    protected override void OnClickNextText()
    {
        //ベースの処理を行う
        base.OnClickNextText();

        //もし最後までテキストを表示されたらテキストウィンドウを非表示にして、キャラの操作を可能にする
        if (currentIndex == scenarioList.Count)
        {
            TextWindow.SetActive(false);

            //キャラの操作を可能にする
            playerMove.IsMoving = false;
        }
            
    }
}

