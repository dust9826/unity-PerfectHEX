using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreatorController : MonoBehaviour
{
    [SerializeField]
    private NewBoardCreator boardCreator;

    private Camera mainC;

    RepeaterKey up = new RepeaterKey(KeyCode.LeftBracket);
    RepeaterKey down = new RepeaterKey(KeyCode.RightBracket);
    RepeaterKey set = new RepeaterKey(KeyCode.F);

    void Start()
    {
        AddListeners();
        boardCreator.UpdateMarker();
        mainC = Camera.main;
    }

    void Update()
    {
        if (up.Update() == 1)
            boardCreator.Pos += new Point3(0, 1, 0);
        if (down.Update() == 1)
            boardCreator.Pos += new Point3(0, -1, 0);
        if (set.Update() == 1)
            boardCreator.SetTile();

    }
    void AddListeners()
    {
        InputController.moveEvent += OnMove;
    }

    void OnMove(object sender, InfoEventArgs<Point> e)
    {
        boardCreator.Pos = boardCreator.Pos + e.info;
        mainC.transform.position += new Vector3(e.info.x, 0, e.info.y);
    }
}
