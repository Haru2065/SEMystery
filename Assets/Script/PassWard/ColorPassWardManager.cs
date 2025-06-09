using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// カラーIDの列挙型
/// </summary>
[Serializable]
public enum ColorID { Red, Green, Blue, Yellow, Purple, Gray, LightBlue, Brown, Pink }

/// <summary>
/// カラーパスワードをマネージャ―で管理
/// </summary>
public class ColorPassWardManager : MonoBehaviour
{
    //カラーパスワードの正解の順番リスト
    public List<ColorID> passWordSequence = new List<ColorID>
    {
        ColorID.Red, ColorID.Yellow,ColorID.Green,
        ColorID.Blue, ColorID.Purple, ColorID.LightBlue,
        ColorID.Gray,ColorID.Brown, ColorID.Pink
    };

    [Header("UI参照")]
    [SerializeField] private TextMeshProUGUI resultText;

    [SerializeField, Tooltip("カラーボタン")] private Button[] colorButtons;

    [SerializeField]
    [Tooltip("リセットボタン")]
    private Button ResetButton;

    [SerializeField]
    [Tooltip("閉じるボタン")]
    private Button closeButton;

    [SerializeField]
    [Tooltip("ヒントテキスト")]
    private TextMeshProUGUI hintText;

    [SerializeField]
    [Tooltip("テキストウィンドウ")]
    private GameObject textWindow;

    [SerializeField]
    [Tooltip("パスワード入力全体画面")]
    private GameObject colorPassWardWindow;

    

    //今どこまで入力成功できているか
    private int currectIndex = 0;

    //何回入力したか
    private int inputCount = 0;




    // Start is called before the first frame update
    void Start()
    {
        //配列文値を
        for(int i = 0; i < colorButtons.Length; i++)
        {
            ColorID id = colorButtons[i].GetComponent<ColorButton>().colorID;

            colorButtons[i].onClick.AddListener(() => OnColorButtonPressed(id));
        }

        //リセットボタンが押されたらボタン履歴とテキストをリセット
        ResetButton.onClick.AddListener(() => Reset());

        //閉じるボタンが押されたら、パスワード入力画面を閉じる
        closeButton.onClick.AddListener(() => CloseColorPassWardWindow());

        //ヒント様テキストウィンドウを閉じる
        textWindow.SetActive(false);

        //初期表示初期化
        resultText.text = "";
    }

    /// <summary>
    /// /カラーパスワードを押したときの処理
    /// </summary>
    /// <param name="pressedID">カラーのID</param>
    public void OnColorButtonPressed(ColorID pressedID)
    {
        //カラーIDとパスワードの入力値が一致したら、入力成功回数をカウント
        if (pressedID == passWordSequence[currectIndex])
        {
            //入力成功回数を増やす
            currectIndex++;

            Debug.Log(currectIndex);

            //入力成功回数が正解に達したら解除
            if(currectIndex >= passWordSequence.Count)
            {
                //解除成功
                unlock();
            }
            else
            {
                resultText.text = $"{pressedID}";
            }
        }
        //間違うと失敗
        else
        {
            //失敗処理
            Fail();

            //入力失敗回数を増やす
            inputCount++;

            //入力失敗回数が３回達したらヒントを出す
            if(inputCount >= 3)
            {
                //テキストウィンドウを表示
                textWindow.SetActive(true);
                hintText.text = $"探索してメモを探そう";

                //リセットコールチンを開始
                StartCoroutine(ResetText());
            }
        }
        
    }

    //解除成功メソッド
    private void unlock()
    {
        //成功と表示
        resultText.text = "success!!";

        //リセットコールチン開始
        StartCoroutine(ResetText());
    }

    /// <summary>
    /// 解除失敗メソッド
    /// </summary>
    private void Fail()
    {
        //失敗を表示する
        resultText.text = "failure";
    }

    /// <summary>
    /// リセットメソッド
    /// </summary>
    private void Reset()
    {
        //成功回数をリセット
        currectIndex = 0;

        //出力ウィンドウを非表示
        resultText.text = "";
    }

    /// <summary>
    /// カラーパスワード画面を閉じるメソッド
    /// </summary>
    private void CloseColorPassWardWindow()
    {
        colorPassWardWindow.SetActive(false);
    }

    /// <summary>
    /// リセットのコールチン
    /// </summary>
    /// <returns>2フレーム待つ</returns>
    private IEnumerator ResetText()
    {
        //2フレーム後リセットメソッド開始
        yield return new WaitForSeconds(2f);
        Reset();


        //入力失敗回数が３に達したらテキストウィンドウを非表示
        if (inputCount >= 3)
        {
            textWindow.SetActive(false);

            //入力失敗回数をリセット
            inputCount = 0;
        }

        //1フレーム待つ
        yield return null;
    }
}
