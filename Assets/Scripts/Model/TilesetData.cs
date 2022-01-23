using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tileset Data", menuName = "Scriptable Object/Tileset Data", order = int.MaxValue)]
public class TilesetData : ScriptableObject
{
    public List<TileData> tileDatas;
}
