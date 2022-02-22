using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTargetState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log(turn.actor.pos);
        SelectTile(turn.actor.pos);

        //Dictionary<Point, TileLayer> tiles = board.GetTilesInRange(pos, 4);
        floorTiles.Clear();
        GetFloorTilesInRange(pos, 4, floorTiles);
        foreach (var tile in floorTiles)
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
        t = pos + t;
        Debug.LogFormat("{0}, {1} : {2} Layer", t.x, t.y, board.board[t.x, t.y]);
        SelectTile(t);
    }

    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        if(e.info == 0)
        {
            if (!floorTiles.ContainsKey(pos))
                return;
            if (turn.actor.pos == pos)
                return;
            board.MoveEntity(turn.actor.pos, pos);

            owner.ChangeState<MoveSequenceState>();
        }
        else
        {
            Cancel();
        }
    }

    List<Point> dir = new List<Point>() {
        new Point(0, 1), new Point(1, 0),
        new Point(0, -1), new Point(-1, 0) };

    void GetFloorTilesInRange(Point originPos, int lenght, Dictionary<Point, List<Directions>> tiles)
    {
        var tilemap = board.board;

        Queue<Pair<Point, List<Directions>>> q = new Queue<Pair<Point, List<Directions>>>();

        q.Enqueue(new Pair<Point, List<Directions>>(originPos, new List<Directions>()));
        tiles[originPos] = new List<Directions>();

        int c = 0;

        while (q.Count != 0 && c < 100)
        {
            c++;
            Pair<Point, List<Directions>> cur = q.Dequeue();

            if (cur.Second.Count == lenght)
                continue;
            for(int i=0;i<4;i++)
            {
                Point next = cur.First + dir[i];
                if (!CanMove(cur.First, dir[i]))
                    continue;
                if (tilemap[next.x, next.y] != TileLayer.Floor)
                    continue;
                if (tiles.ContainsKey(next))
                    continue;
                List<Directions> path = new List<Directions>();
                path.AddRange(cur.Second);
                path.Add((Directions)i);
                q.Enqueue(new Pair<Point, List<Directions>>(next, path));
                tiles[next] = path;
            }
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
