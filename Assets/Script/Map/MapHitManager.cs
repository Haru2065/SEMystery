using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
/// マップの当たり判定を管理するマネージャー(壁など）
/// </summary>
public class MapHitManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("壁のタイルマップ")]
    private List<Tilemap> HitTileMap;

    /// <summary>
    /// 指定したワールド座標が壁かどうかを判定します。
    /// </summary>
    /// <param name="targetWorldPos">判定するワールド座標</param>
    /// <returns>壁であれば true、それ以外は false</returns>
    public bool IsWall(Vector3 targetWorldPos)
    {
        Vector3Int cellPos = Vector3Int.FloorToInt(targetWorldPos);

        foreach (var tilemap in HitTileMap)
        {
            if (tilemap.HasTile(cellPos))
            {
                return true;
            }
        }

        return false;
    }
}
