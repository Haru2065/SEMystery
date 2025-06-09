using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// タイルマップを塗りつぶし、消すツール
/// </summary>
[ExecuteInEditMode]  //実行しなくても使える
public class TilePainter : MonoBehaviour
{
    //塗るためのタイルマップ
    [SerializeField,Tooltip("塗るためのタイルマップ")]
    private Tilemap tilemap;

    [SerializeField,Tooltip("素材タイル")]
    private TileBase paintTile;

    [SerializeField,Tooltip("塗り始める位置")]
    private Vector2Int startPosition = new Vector2Int(0, 0);


    [SerializeField, Tooltip("横")]
    private int width;

    [SerializeField, Tooltip("縦")]
    private int height;

    [SerializeField,Tooltip("手動トリガー用チェックで塗る")]
    private bool paint = false; 

    [SerializeField,Tooltip("チェックで消す")] 
    private bool erase = false;

    void Update()
    {
        if (!Application.isPlaying)
        {
            if (paint)
            {
                FillArea(startPosition, width, height);
                paint = false;
            }

            if (erase)
            {
                EraseArea(startPosition, width, height);
                erase = false;
            }
        }
    }


    void FillArea(Vector2Int start, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int pos = new Vector3Int(start.x + x, start.y + y, 0);
                tilemap.SetTile(pos, paintTile);
            }
        }
    }

    /// <summary>範囲のタイルを消す</summary>
    void EraseArea(Vector2Int start, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int pos = new Vector3Int(start.x + x, start.y + y, 0);
                tilemap.SetTile(pos, null); // nullをセットすると消える
            }
        }
    }
}
