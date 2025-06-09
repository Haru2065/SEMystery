using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// �^�C���}�b�v��h��Ԃ��A�����c�[��
/// </summary>
[ExecuteInEditMode]  //���s���Ȃ��Ă��g����
public class TilePainter : MonoBehaviour
{
    //�h�邽�߂̃^�C���}�b�v
    [SerializeField,Tooltip("�h�邽�߂̃^�C���}�b�v")]
    private Tilemap tilemap;

    [SerializeField,Tooltip("�f�ރ^�C��")]
    private TileBase paintTile;

    [SerializeField,Tooltip("�h��n�߂�ʒu")]
    private Vector2Int startPosition = new Vector2Int(0, 0);


    [SerializeField, Tooltip("��")]
    private int width;

    [SerializeField, Tooltip("�c")]
    private int height;

    [SerializeField,Tooltip("�蓮�g���K�[�p�`�F�b�N�œh��")]
    private bool paint = false; 

    [SerializeField,Tooltip("�`�F�b�N�ŏ���")] 
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

    /// <summary>�͈͂̃^�C��������</summary>
    void EraseArea(Vector2Int start, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int pos = new Vector3Int(start.x + x, start.y + y, 0);
                tilemap.SetTile(pos, null); // null���Z�b�g����Ə�����
            }
        }
    }
}
