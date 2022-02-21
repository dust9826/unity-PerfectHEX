using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ExploreState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log(turn.actor.pos);
        SelectTile(turn.actor.pos);

        Dictionary<Point, TileLayer> tiles = board.GetTilesInRange(pos, 4);

        foreach (var tile in tiles)
        {
            tileSelectController.AddSelectionTile(tile.Key, board.board[tile.Key.x, tile.Key.y]);
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
        SelectTile(t + pos);
    }

    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        if (e.info == 0)
            owner.ChangeState<CommandSelectionState>();
    }

    bool CanMove(Point point, Point diff)
    {
        Point t = point + diff;
        if (t.x < 0 || t.y < 0 || t.x >= board.Width || t.y >= board.Height)
            return false;
        return true;
    }
}
