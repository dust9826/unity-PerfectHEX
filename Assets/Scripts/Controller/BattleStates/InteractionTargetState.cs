using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTargetState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log(turn.actor.pos);
        SelectTile(turn.actor.pos);

        Dictionary<Point, TileLayer> tiles = board.GetTilesInRange(pos, 1);
        foreach (var tile in tiles)
        {
            tileSelectController.AddSelectionTile(tile.Key, tile.Value);
        }
    }

    public override void Exit()
    {
        base.Exit();
        tileSelectController.RemoveSelectionColor();
    }

    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        Point t = cameraRig.GetRotatePoint(e.info);
        if (!CanMove(pos, t))
            return;
        t = pos + t;
        Debug.LogFormat("{0}, {1} : {2} Layer", t.x, t.y, board.board[t.x, t.y]);
        SelectTile(t);
    }

    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        if (e.info == 0)
        {
            if (!board.CanInteract(pos))
                return;
            Debug.Log("Interacting");
            owner.ChangeState<InteractionSequenceState>();
        }
        else
        {
            Cancel();
        }
    }

    Point Vector3toPoint(Vector3 pos)
    {
        return new Point((int)(pos.x - 0.5f), (int)(pos.z - 0.5f));
    }

    bool CanMove(Point point, Point diff)
    {
        Point t = point + diff;
        if (t.x < 0 || t.y < 0 || t.x >= board.Width || t.y >= board.Height)
            return false;
        return true;
    }

    void Cancel()
    {
        owner.ChangeState<CommandSelectionState>();
    }
}
