using UnityEngine;
using UnityEditor;

using System.IO;
using System.Collections.Generic;

public class BoardCreator : MonoBehaviour
{
    [SerializeField] TilesetData tileSets;
    [SerializeField] GameObject tileViewPrefab;

    [SerializeField] GameObject tileSelectionIndicatorPrefab;

    [SerializeField] Transform visibleTileArea;
    [SerializeField] Transform dataTileArea;

    [SerializeField] int width = 10;
    [SerializeField] int depth = 10;
    [SerializeField] int height = 8;

    [SerializeField] Point pos;
    [SerializeField] int curHeight;

    [SerializeField] LevelData levelData;

    [SerializeField] int curTileData;

    // 타일맵의 실질적인 모습
    Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();
    /// <summary>
    /// 타일맵의 그래픽적인 모습을 나타낸다.
    /// </summary>
    Dictionary<Vector3, TileData> tileDatas = new Dictionary<Vector3, TileData>();

    Transform marker
    {
        get
        {
            if (_marker == null)
            {
                GameObject instance = Instantiate(tileSelectionIndicatorPrefab) as GameObject;
                _marker = instance.transform;
            }
            return _marker;
        }
    }
    Transform _marker;

    public void SetTile()
    {
        TileData tileData = tileSets.tileDatas[curTileData];
        GameObject instance = Instantiate(tileData.tilePrefab) as GameObject;
        instance.transform.parent = visibleTileArea;
        instance.transform.position = new Vector3(pos.x, curHeight, pos.y);
        instance.transform.position += tileData.pivot;
        instance.transform.position -= new Vector3(0.5f, 0, 0.5f);
        tileDatas.Add(new Vector3(pos.x, curHeight, pos.y), tileData);

        Rect rect = new Rect(pos.x, pos.y, tileData.size.x, tileData.size.z);
        SetRect(rect, (int)(tileData.size.y * 4) + curHeight * 4);
    }
    
    public void Grow()
    {
        GrowSingle(pos);
    }

    public void Shrink()
    {
        ShrinkSingle(pos);
    }

    public void GrowArea()
    {
        Rect r = RandomRect();
        GrowRect(r);
    }

    public void ShrinkArea()
    {
        Rect r = RandomRect();
        ShrinkRect(r);
    }

    public void UpdateMarker()
    {
        Tile t = tiles.ContainsKey(pos) ? tiles[pos] : null;
        marker.localPosition = t != null ? t.center : new Vector3(pos.x, 0, pos.y);
    }

    public void Clear()
    {
        for (int i = visibleTileArea.childCount - 1; i >= 0; --i)
            DestroyImmediate(visibleTileArea.GetChild(i).gameObject);
        tileDatas.Clear();
        for (int i = dataTileArea.childCount - 1; i >= 0; --i)
            DestroyImmediate(dataTileArea.GetChild(i).gameObject);
        tiles.Clear();
    }

    public void Save()
    {
        string filePath = Application.dataPath + "/Resources/Levels";

        if (!Directory.Exists(filePath))
            CreateSaveDirectory();

        LevelData board = ScriptableObject.CreateInstance<LevelData>();
        board.tiles = new List<Vector3>(tiles.Count);

        foreach (Tile t in tiles.Values)
            board.tiles.Add(new Vector3(t.pos.x, t.height, t.pos.y));

        board.tileset = tileSets;

        foreach (var t in tileDatas)
            board.tileDatas.Add(new Vector4(t.Key.x, t.Key.y, t.Key.z, 1));

        string fileName = string.Format("Assets/Resources/Levels/{1}.asset", filePath, name);
        AssetDatabase.CreateAsset(board, fileName);
    }

    public void Load()
    {
        Clear();
        if (levelData == null)
            return;

        foreach (Vector3 v in levelData.tiles)
        {
            Tile t = Create();
            t.Load(v);
            tiles.Add(t.pos, t);
        }
    }

    Rect RandomRect()
    {
        int x = UnityEngine.Random.Range(0, width);
        int y = UnityEngine.Random.Range(0, depth);
        int w = UnityEngine.Random.Range(0, width - x + 1);
        int h = UnityEngine.Random.Range(0, depth - y + 1);
        return new Rect(x, y, w, h);
    }

    void SetRect(Rect rect, int h)
    {
        for (int y = (int)rect.yMin; y < (int)rect.yMax; y++)
        {
            for (int x = (int)rect.xMin; x < (int)rect.xMax; x++)
            {
                Point p = new Point(x, y);
                SetSingle(p, h);
            }
        }
    }

    void GrowRect(Rect rect)
    {
        for (int y = (int)rect.yMin; y < (int)rect.yMax; y++)
        {
            for (int x = (int)rect.xMin; x < (int)rect.xMax; x++)
            {
                Point p = new Point(x, y);
                GrowSingle(p);
            }
        }
    }

    void ShrinkRect(Rect rect)
    {
        for (int y = (int)rect.yMin; y < (int)rect.yMax; y++)
        {
            for (int x = (int)rect.xMin; x < (int)rect.xMax; x++)
            {
                Point p = new Point(x, y);
                ShrinkSingle(p);
            }
        }
    }

    Tile Create()
    {
        GameObject instance = Instantiate(tileViewPrefab) as GameObject;
        instance.transform.parent = dataTileArea;
        return instance.GetComponent<Tile>();
    }

    Tile GetOrCreate(Point p)
    {
        if (tiles.ContainsKey(p))
            return tiles[p];

        Tile t = Create();

        t.Load(p, 0);
        tiles.Add(p, t);

        return t;
    }

    void SetSingle(Point p, int h)
    {
        Tile t = GetOrCreate(p);
        if (h <= height)
            t.SetHeight(h);
    }

    void GrowSingle(Point p)
    {
        Tile t = GetOrCreate(p);
        if (t.height < height)
            t.Grow();
    }

    void ShrinkSingle(Point p)
    {
        if (!tiles.ContainsKey(p))
            return;

        Tile t = tiles[p];
        t.Shrink();

        if (t.height <= 0)
        {
            tiles.Remove(p);
            DestroyImmediate(t.gameObject);
        }
    }

    void CreateSaveDirectory()
    {
        string filePath = Application.dataPath + "/Resources";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets", "Resources");

        filePath += "/Levels";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets/Resources", "Levels");
        AssetDatabase.Refresh();
    }
}
