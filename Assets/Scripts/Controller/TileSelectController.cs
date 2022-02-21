using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelectController : MonoBehaviour
{
    [SerializeField]
    Transform tileSelectionP;
    [SerializeField]
    List<Color> selectionColor;
    [SerializeField]
    GameObject selectionPrefab;

    List<GameObject> tileSelections;

    private void Awake()
    {
        tileSelections = new List<GameObject>();
    }

    private void Start()
    {
        tileSelectionP.transform.position = new Vector3(0.5f, 0.5f, 0.5f);
    }

    public void AddSelectionTile(Point pos, TileLayer layer)
    {
        GameObject newSelection = Instantiate(selectionPrefab);
        newSelection.transform.parent = tileSelectionP;
        newSelection.transform.localPosition = new Vector3(pos.x, 0, pos.y);
        tileSelections.Add(newSelection);
        SetSelectionColor(newSelection, layer);
    }

    public void OnMove(object sender, InfoEventArgs<Point> e)
    {
    }

    public void OnFire(object sender, InfoEventArgs<int> e)
    {

    }

    public void RemoveSelectionColor()
    {
        foreach(var tile in tileSelections)
        {
            Destroy(tile);
        }
        tileSelections.Clear();
    }

    void SetSelectionColor(GameObject tileSelection, TileLayer layer)
    {
        Renderer tileRender = tileSelection.transform.GetChild(0).GetComponent<Renderer>();
        tileRender.material.color = selectionColor[(int)layer];
    }
}
