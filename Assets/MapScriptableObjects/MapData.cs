using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MapData", menuName ="Game/MapData")]
public class MapData : ScriptableObject
{
    public int width;
    public int height;
    public int[] cells;
}
