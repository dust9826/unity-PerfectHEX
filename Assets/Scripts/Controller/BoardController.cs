using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 수정 필요
// board의 값만 수정하고 tileLayerData의 값은 수정하지 않는다.
public class BoardController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> tileLayers;

    [SerializeField]
    int width;
    [SerializeField]
    int height;

    [SerializeField]
    Vector2 offset;

    [SerializeField]
    public bool isLoad = false;

    public TileLayer[,] board;
    public List<int[,]> tileLayerData;

    List<Interaction> interactionsData;
    List<Entity> entitiesData;
    Entity player;

    public int Width { get { return width; } }
    public int Height { get { return height; } }

    #region MonoBehaviour CallBacks

    private void Awake()
    {

    }

    private void Start()
    {

    }

    #endregion

    public Dictionary<Point, TileLayer> GetTilesInRange(Point pos, int range)
    {
        Dictionary<Point, TileLayer> ret = new Dictionary<Point, TileLayer>();

        for (int x = -range; x <= range; x++) 
        {
            for (int y = -range; y <= range; y++) 
            {
                int _x = x + pos.x;
                int _y = y + pos.y;
                if (_x < 0 || _x >= width || _y < 0 || _y >= height)
                    continue;
                if (Mathf.Abs(x) + Mathf.Abs(y) > range)
                    continue;
                ret.Add(new Point(_x, _y), board[_x, _y]);
            }
        }

        return ret;
    }

    [ContextMenu("SetBoard")]
    public void SetBoard()
    {
        InitBoard();
        SetLayerInfo(TileLayer.Ground);
        SetLayerInfo(TileLayer.Floor);
        SetLayerInfo(TileLayer.Wall);
        SetLayerInfo(TileLayer.Interactive);
        SetLayerInfo(TileLayer.Entity);
        SetLayerInfo(TileLayer.Light);
        isLoad = true;
    }

    public Entity GetPlayer()
    {
        if(player == null)
        {
            foreach(var entity in entitiesData)
            {
                if(entity.name == "Player")
                {
                    player = entity;
                }
            }
        }
        return player;
    }

    // 수정 필요
    // board의 값만 수정하고 tileLayerData의 값은 수정하지 않는다.
    // 위에도 적어둠
    public void AddEntity(Point pos)
    {
        board[pos.x, pos.y] = TileLayer.Entity;

    }

    public void RemoveEntity(Point pos)
    {
        board[pos.x, pos.y] = TileLayer.Floor;
    }

    public void RemoveInteraction(Point pos)
    {
        int idx = tileLayerData[(int)TileLayer.Interactive][pos.x, pos.y];
        for (int x = 0; x < width; x++) 
        {
            for (int y = 0; y < height; y++) 
            {
                if (tileLayerData[(int)TileLayer.Interactive][x, y] == idx)
                {
                    tileLayerData[(int)TileLayer.Interactive][x, y] = 0;
                    board[x, y] = TileLayer.Floor;
                }
            }
        }
    }

    public bool CanMove(Point pos)
    {
        if (pos.x < 0 || pos.y < 0 || pos.x >= width || pos.y >= height)
            return false;
        return board[pos.x, pos.y] == TileLayer.Floor;
    }

    public bool CanInteract(Point pos)
    {
        if (pos.x < 0 || pos.y < 0 || pos.x >= width || pos.y >= height)
            return false;
        return board[pos.x, pos.y] == TileLayer.Interactive;
    }

    public Interaction GetInteraction(Point pos)
    {
        int idx = tileLayerData[(int)TileLayer.Interactive][pos.x, pos.y];
        return interactionsData[idx-1];
    }

    void InitBoard()
    {
        board = new TileLayer[width, height];

        tileLayerData = new List<int[,]>();
        entitiesData = new List<Entity>();
        interactionsData = new List<Interaction>();
        for (int i = 0; i < (int)TileLayer.Size; i++)
        {
            tileLayerData.Add(new int[width, height]);
        }
    }

    void SetLayerInfo(TileLayer layer)
    {
        GameObject tileLayer = tileLayers[(int)layer];
        int[,] data = tileLayerData[(int)layer];
        for (int i = 0; i < tileLayer.transform.childCount; i++)
        {
            Transform tileTransform = tileLayer.transform.GetChild(i);
            TileData tileData = tileTransform.GetComponent<TileData>();
            Vector2 origin = new Vector2(tileTransform.position.x, tileTransform.position.z) - offset;
            Vector2 size = tileData.size;
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    Vector2 pos = GetRotatePos(new Vector2(x, y), tileTransform.eulerAngles.y);
                    int _x = (int)pos.x + (int)origin.x;
                    int _y = (int)pos.y + (int)origin.y;

                    if (tileData.nonColiders.Contains(new Point(x, y)))
                        continue;

                    data[_x, _y] = i + 1;
                    board[_x, _y] = layer;
                    //Debug.LogFormat("{0}, {1} : {2}th of {3} layer", _x, _y, i + 1, layer);
                }
            }

            if (layer == TileLayer.Interactive)
            {
                interactionsData.Add(tileTransform.GetComponent<Interaction>());
            }

            if (layer == TileLayer.Entity)
            {
                entitiesData.Add(tileTransform.GetComponent<Entity>());
            }
        }
    }

    Vector2 GetRotatePos(Vector2 pos, float rotate)
    {
        Vector2 ret = Vector2.zero;
        switch((int)rotate)
        {
            case 0:
                ret = new Vector2(pos.x, pos.y);
                break;
            case 90:
                ret = new Vector2(pos.y, -pos.x);
                break;
            case 180:
                ret = new Vector2(-pos.x, -pos.y);
                break;
            case 270:
                ret = new Vector2(-pos.y, pos.x);
                break;
        }
        return ret;
    }
}