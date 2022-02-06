using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : ScriptableObject
{
    public int[,,] tiles;
    public int[] tileLayer;
    public TilesetData tileset;
    public List<VisibleTileData> tileDatas;
}

public class VisibleTileData
{
    Point3 pos;
    int yRotate;
    int id;

    public VisibleTileData(Point3 _pos, int _yRotate, int _id)
    {
        pos = _pos;
        yRotate = _yRotate;
        id = _id;
    }
}