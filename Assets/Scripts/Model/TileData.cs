using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Tile Data", menuName = "Scriptable Object/Tile Data", order = int.MaxValue)]
public class TileData : ScriptableObject
{
    public GameObject tilePrefab;
    public Vector3 pivot;
    public Vector3 size;
}
