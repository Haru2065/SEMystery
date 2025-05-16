using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// Jsonファイルのid情報とtext情報
/// </summary>
[Serializable]
public class ScenarioText
{
    public int id;
    public string Speaker;
    public string text;
}

/// <summary>
/// AddressablesでロードしたJsonをC#に変換した後に表示するスクリプト
/// </summary>
public abstract class ScenarioManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Jsonロードするスクリプト")]
    private JsonLoder loder;

    [SerializeField]
    [Tooltip("読み込みたいJsonを入力")]
    private string ScenarioJsonKey;

    [SerializeField]
    [Tooltip("テキストウィンドウ")]
    protected GameObject TextWindow;

    [SerializeField]
    [Tooltip("Jsonファイルを表示するUGUI")]
    protected TextMeshProUGUI ScenarioTextUGUI;

    [SerializeField]
    [Tooltip("だれが話しているかを表示するUGUI")]
    protected TextMeshProUGUI SpeakerTextUGUI;

    //変換したテキストを順番に表示するリスト
    protected List<ScenarioText> scenarioList;

    //表示インデックス
    protected int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        //Json読み込みスクリプトからロード開始
        //受け取ったJsonをパース
        StartCoroutine(loder.LoadJsonText(json =>
        {
            //Json全体をDictionaryに変換
            var dict = JsonConvert.DeserializeObject<Dictionary<string, List<ScenarioText>>>(json);

            //jsonキーがあれば表示リストに代入
            if (dict.TryGetValue(ScenarioJsonKey, out scenarioList))
            {
                currentIndex = 0;

                //テキストウィンドウを表示
                TextWindow.SetActive(true);

                //Jsonで書かれたシナリオを表示
                ShowScenarioText(scenarioList[currentIndex], scenarioList[currentIndex].Speaker);
            }
            else
            {
                Debug.LogWarning($"指定したキーが見つかりませんでした。{ScenarioJsonKey}");
            }

            //最初は誰が話しているかを表示するUGUIを非表示にする
            SpeakerTextUGUI.gameObject.SetActive(false);
        }));
    }

    /// <summary>
    /// テキストクリック操作
    /// </summary>
    protected void Update()
    {
        //マウス左クリックされたらテキストの表示を進める
        if (Input.GetMouseButtonDown(0))
        {
            OnClickNextText();
        }
    }

    /// <summary>
    /// クリックされたらテキストの表示を進めるメソッド
    /// </summary>
    protected virtual void OnClickNextText()
    {
        //表示インデックスを次に進める
        currentIndex++;

        //表示インデックスが最後まで表示するまで表示
        if (currentIndex < scenarioList.Count)
        {
            //表示インデックスの数に入っているJsonテキストを表示
            ShowScenarioText(scenarioList[currentIndex], scenarioList[currentIndex].Speaker);
        }
    }

    /// <summary>
    /// 変換したJsonシナリオを表示
    /// </summary>
    public abstract void ShowScenarioText(ScenarioText texts, string Speaker);
}
