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
    //ロードしたいアドレスキー
    private string addressKey;

    //保存パス
    private string savePath;

    //生成されているか
    private bool isCreating = false;

    /// <summary>
    /// windowを作るメソッド
    /// </summary>
    /// ツールの中のCSV to MapDataを表示する
    [MenuItem("Tools/CSV to MapData")]
    public static void ShowWindow()
    {
        GetWindow<CSVToMapDataWindow>("CSV→MapData生成");
    }        


    /// <summary>
    /// ウィンドウ内に表示するUIを設定するメソッド
    /// </summary>
    private void OnGUI()
    {
        GUILayout.Label("CSV → MapData自動生成", EditorStyles.boldLabel);

        //文字を表示する

        //ロードしたいアドレスキーを入力
        addressKey = EditorGUILayout.TextField("loadしたいCSVのアドレスキーを入力",addressKey);　//入力できるようにする

        //保存したい場所の表示（入力も可）
        //savePath = EditorGUILayout.TextField("保存したい場所を入力", savePath);

        //ボタンを押して生成中でなければ生成開始
        if(GUILayout.Button("Create ScriptableObject") && !isCreating)
        {

            // 保存パスをユーザーに選ばせる
            string path = EditorUtility.SaveFilePanelInProject(
                "ScriptableObjectを保存",
                "NewMapData",      // 初期ファイル名
                "asset",           // 拡張子
                "ScriptableObjectの保存先を選んでください"
            );

            // キャンセルされた場合は処理しない
            if (string.IsNullOrEmpty(path))
            {
                return; 
            }

            savePath = path;

            //生成フラグをtrue
            isCreating = true;
            
            //CSVをロードしてScriptableObjectを生成するコールチンをエディター内で開始
            EditorCoroutineUtility.StartCoroutineOwnerless(LoadAndCreate());
        }
    }

    /// <summary>
    /// CSVをロードし、ScriptableObjectを生成するコールチンを
    /// </summary>
    /// <returns>ロードが完了するまで待つ</returns>
    private IEnumerator LoadAndCreate()
    {
        ///指定されたアドレスキー（CSVファイル）を非同期でロードする
        AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(addressKey);
        
        //完了するまで待つ
        yield return handle;

        //読み込み成功すればコンソールビューにエラーを表示させ、コールチンを止める
        if(handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("CSVロードに失敗しました。");

            //生成フラグをfalse
            isCreating = false;

            //コールチン停止
            yield break;
        }
        //読み込み成功したらCSVのデータを表示する
        else
        {
            //読み込んだCSVのデータを取得
            string csvData = handle.Result.text;

            Debug.Log("読み込み成功!!");

            //読み込んだCSVを表示
            Debug.Log($"CSVdata:{csvData}");
        }

        //読み込んだCSVテキストを取得し1行ごとに分割

        //Trim():前後の空白改行を取り除く
        //Split(\n):テキストを行単位に分割 
        string[] lines = handle.Result.text.Trim().Split('\n');
        
        //縦方向のサイズ
        int height = lines.Length;

        //横方向のサイズ　
        //各セルを取り出す　CSVの各行のテキスト配列
        int width = lines[0].Split(',').Length;

        //マップの数値全データを格納するための一次元配列を作成
        int[] cells = new int [width * height];

        //各行をカンマで分割し、数字に変換して cells に詰め込む
        for (int y = 0; y < height; y++)
        {
            //csvの各行をカンマで区切り、列のデータに分ける
            string[] cols = lines[y].Trim().Split(",");

            //横方向の列を繰り返し処理を行う
            for (int x = 0; x < width; x++)
            {
                //各列の値を格納した配列です。
                if (int.TryParse(cols[x], out int val))
                {
                    //２次元配列を１限配列に変換
                    cells[y * width + x] = val;
                }
            }
        }

        //新しい MapData ScriptableObject を作る。
        MapData mapdata = ScriptableObject.CreateInstance<MapData>();

        //読み込んだデータを格納
        mapdata.width = width;
        mapdata.height = height;
        mapdata.cells = cells;

       

        //アセットを作成する
        AssetDatabase.CreateAsset(mapdata, savePath);

        //デバイスのディスクにアセットを保存
        AssetDatabase.SaveAssets();

        Debug.Log("生成成功:" + savePath);

        //読み込んだリソースを解放
        Addressables.Release(handle);

        //生成が完了したからfalse
        isCreating = false;

    }
}
