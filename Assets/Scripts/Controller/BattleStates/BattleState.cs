using UnityEngine;
using System.Collections.Generic;

public class BattleState : State
{
    protected BattleController owner;

    public CameraRig cameraRig { get { return owner.cameraRig; } }
    public BoardController board { get { return owner.board; } }
    public Transform tileSelectionIndicator { get { return owner.tileSelectionIndicator; } }
    public Point pos { get { return owner.pos; } set { owner.pos = value; } }

    public TileSelectController tileSelectController { get { return owner.tileSelectController; } }
    public AbilityMenuPanelController abilityMenuPanelController { get { return owner.abilityMenuPanelController; } }
    public Turn turn { get { return owner.turn; } }
    public List<Entity> entities { get { return owner.entities; } }

    public Dictionary<Point, List<Directions>> floorTiles { get { return owner.floorTiles; } }

    protected virtual void Awake()
    {
        owner = GetComponent<BattleController>();
    }

    protected override void AddListeners()
    {
        InputController.moveEvent += OnMove;
        InputController.fireEvent += OnFire;
    }

    protected override void RemoveListeners()
    {
        InputController.moveEvent -= OnMove;
        InputController.fireEvent -= OnFire;
    }

    protected virtual void OnMove(object sender, InfoEventArgs<Point> e)
    {

    }

    protected virtual void OnFire(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void SelectTile(Point p)
    {
        if (pos == p)
            return;
        pos = p;
        tileSelectionIndicator.localPosition = new Vector3(pos.x, 0, pos.y) + new Vector3(0.5f, 0.5f, 0.5f);
    }
}
