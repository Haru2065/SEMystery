using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
/// �}�b�v�̓����蔻����Ǘ�����}�l�[�W���[(�ǂȂǁj
/// </summary>
public class MapHitManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("�ǂ̃^�C���}�b�v")]
    private List<Tilemap> HitTileMap;

    /// <summary>
    /// �w�肵�����[���h���W���ǂ��ǂ����𔻒肵�܂��B
    /// </summary>
    /// <param name="targetWorldPos">���肷�郏�[���h���W</param>
    /// <returns>�ǂł���� true�A����ȊO�� false</returns>
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
