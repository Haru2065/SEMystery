using System;
using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;


/// <summary>
/// CSVファイルをAddressablesでロードするスクリプト
/// </summary>
public class CSVLoader : MonoBehaviour
{
    //CSVLoderのインスタンス
    private static CSVLoader instance;

    /// <summary>
    /// インスタンスのゲッターセッター
    /// </summary>
    public static CSVLoader Instance
    {
        get => instance;
        set => instance = value;
    }

    [SerializeField]
    [Tooltip("ロードしたいCSVのアドレスキー")]
    private string addressKey;

    /// <summary>
    /// インスタンス化
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
    /// CSVをロードするコールチン
    /// </summary>
    /// <param name="OnSuccess">ロードが成功したか</param>
    /// <returns></returns>
    public IEnumerator LoadCSVText(Action<string> onsuccess)
    {
        //指定されたアドレスキー（CSVファイル）を非同期でロードする
        AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(addressKey);

        //読み込みが完了するまで待つ
        yield return handle;

        //読み込みが成功したかチェック
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            //読み込んだテキストアセットの中身を取得
            string csvdata = handle.Result.text;

            //取得したテキストアセットを表示
            Debug.Log(csvdata);

            //少し待つ
            yield return null;

            Debug.Log("成功");
        }
        else
        {
            Debug.Log($"CSVのロードの失敗:{addressKey}");
        }

        //読み込んだリソースを解放する
        Addressables.Release(handle);
    }
}
