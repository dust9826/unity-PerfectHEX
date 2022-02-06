using UnityEngine;
using UnityEditor;

using System.IO;
using System.Collections.Generic;

public class NewBoardCreator : MonoBehaviour
{
    [SerializeField] TilesetData tileset;
    [SerializeField] GameObject tileViewPrefab;

    [SerializeField] Transform visibleTileArea;
    [SerializeField] Transform dataTileArea;

    [SerializeField] int width = 10;
    [SerializeField] int height = 10;
    [SerializeField] int depth = 10;

    [SerializeField] Point3 pos;

    [SerializeField] LevelData levelData;

    [SerializeField] int curTileId;

    int[,,] tiles = new int[10, 10, 10];

    Dictionary<Point, Tile> tileObjects = new Dictionary<Point, Tile>();

    List<VisibleTileData> tileDatas = new List<VisibleTileData>();

    List<int> tileLayer = new List<int>();

    int rotate = 0;
    public int idx = 0;

    Marker marker
    {
        get
        {
            if(_marker == null || _marker.IsTileNull())
            {
                GameObject newMarker = GameObject.Find("TilePreView");
                if(newMarker != null)
                {
                    DestroyImmediate(newMarker);
                }
                _marker = new Marker(tileset, curTileId);
            }
            return _marker;
        }
    }
    Marker _marker;

    public Point3 Pos
    {
        get { return pos; }
        set
        {
            pos = value;
            UpdateMarker();
        }
    }

    public void SetTile()
    {
        TileData tileData = tileset.tileDatas[curTileId];
        GameObject instance = Instantiate(tileData.tilePrefab) as GameObject;
        instance.transform.parent = visibleTileArea;
        instance.transform.position = new Vector3(pos.x, pos.y, pos.z);
        instance.transform.position += tileData.pivot;
        instance.transform.position -= new Vector3(0.5f, 0, 0.5f);

        tileDatas.Add(new VisibleTileData(pos, rotate, curTileId));

        SetRect(pos, tileData.size, idx++);
    }

    public void SetRect(Point3 pos, Vector3 size, int _idx)
    {
        for (int x = pos.x; x < pos.x + size.x; x++)
        {
            for (int y = pos.y; y < pos.y + size.y; y++)
            {
                for (int z = pos.z; z < pos.z + size.z; z++) 
                {
                    tiles[x,y,z] = _idx;
                }
            }
        }
    }

    /// <summary>
    /// 일부분의 타일의 모습을 재설정한다.
    /// </summary>
    public void UpdateTiles(Point3 pos, Vector3 size)
    {
        for (int x = pos.x - 1; x < pos.x + size.x + 1; x++)
        {
            for (int y = pos.y - 1; y < pos.y + size.y + 1; y++)
            {
                for (int z = pos.z - 1; z < pos.z + size.z + 1; z++)
                {
                    // 범위 밖이라면 continue
                    if (x < 0 || y < 0 || z < 0 || x > width || y > height || z > depth)
                        continue;
                    // 표시할 필요가 없다면 continue
                    if (tiles[x, y, z] == 0)
                        continue;
                    if(CheckTile(new Point3(x,y,z)))
                    {

                    }
                }
            }
        }
    }

    /// <summary>
    /// 전체적인 타일의 모습을 재설정한다.
    /// </summary>
    public void UpdateTiles()
    {

    }
    
    /// <summary>6방향을 담은 필드</summary>
    Point3[] dirs = { new Point3(1, 0, 0), new Point3(-1, 0, 0), 
                    new Point3(0, 1, 0), new Point3(0, -1, 0),
                    new Point3(0, 0, 1), new Point3(0, 0, -1) };
    /// <summary>
    /// 주변 6방향에 빈칸이 있는지 확인하는 메소드
    /// </summary>
    /// <returns>표시해야하면 true</returns>
    public bool CheckTile(Point3 pos)
    {
        foreach(Point3 dir in dirs)
        {
            Point3 t = dir + pos;
            if (tiles[t.x, t.y, t.z] == 0)
                return true;
        }
        return false;
    }

    public void UpdateMarker()
    {
        marker.SetTileData(tileset, curTileId);
        marker.SetPosition(Pos);
    }

    public void ResetLevel()
    {
        _marker = null;
    }

    public void OnGUI()
    {
        
    }
}

public class Marker
{
    TileData tileData;
    int tileId;
    GameObject tile;

    public Marker(TilesetData data, int id)
    {
        tileData = data.tileDatas[id];
        tileId = id;
        tile = GameObject.Instantiate(tileData.tilePrefab) as GameObject;
        tile.name = "TilePreView";
    }

    public void SetTileData(TilesetData data, int id)
    {
        if (id == tileId)
            return;
        tileData = data.tileDatas[id];
        tileId = id;
        GameObject.DestroyImmediate(tile);
        tile = GameObject.Instantiate(tileData.tilePrefab) as GameObject;
        tile.name = "TilePreView";
    }

    public void SetPosition(Point3 pos)
    {
        tile.transform.position = new Vector3(pos.x, pos.y, pos.z);
        tile.transform.position += tileData.pivot;
        tile.transform.position -= new Vector3(0.5f, 0, 0.5f);
    }

    public void SetRotation(int degree)
    {

    }

    public bool IsTileNull()
    {
        return tile == null;
    }
}