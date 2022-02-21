using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : StateMachine
{
    public CameraRig cameraRig;

    public BoardController board;
    
    public Transform tileSelectionIndicator;

    public Point pos;

    public TileSelectController tileSelectController;
    public AbilityMenuPanelController abilityMenuPanelController;
    public Turn turn = new Turn();
    public List<Entity> entities = new List<Entity>();
    public Dictionary<Point, List<Directions>> floorTiles = new Dictionary<Point, List<Directions>>();

    private void Start()
    {
        ChangeState<InitBattleState>();
    }
}
