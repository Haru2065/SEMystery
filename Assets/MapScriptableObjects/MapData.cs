using UnityEngine;

/// <summary>
/// �X�N���v�^�u���I�u�W�F�N�g���쐬����X�N���v�g
/// </summary>
[CreateAssetMenu(fileName ="MapData", menuName ="Map/MapData")]
public class MapData : ScriptableObject
{
    //���̕�
    public int width;

    //�c�̕�
    public int height;

    //�Z���̔z��i�Ǐ��f�[�^�j
    public int[] cells;
}
