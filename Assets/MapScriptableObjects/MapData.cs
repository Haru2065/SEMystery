using UnityEngine;

/// <summary>
/// スクリプタブルオブジェクトを作成するスクリプト
/// </summary>
[CreateAssetMenu(fileName ="MapData", menuName ="Map/MapData")]
public class MapData : ScriptableObject
{
    //横の幅
    public int width;

    //縦の幅
    public int height;

    //セルの配列（壁床データ）
    public int[] cells;
}
