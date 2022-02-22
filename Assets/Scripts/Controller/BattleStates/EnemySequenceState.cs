using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySequenceState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine("Sequence");
    }

    IEnumerator Sequence()
    {
        Movement m = turn.actor.GetComponent<Movement>();
        Directions direction = (Directions)(((int)turn.actor.dir + 1) % 4);

        if(board.CanMove(dir2P[(int)direction] + pos))
        {
            board.MoveEntity(pos, dir2P[(int)direction] + pos);
            
            SelectTile(dir2P[(int)direction] + pos);

            yield return StartCoroutine(m.Traverse(new List<Directions>() { direction }));
            
            turn.actor.Place(pos);
        }

        yield return null;

        owner.ChangeState<SelectUnitState>();
    }

    bool CanMove(Point point, Point diff)
    {
        Point t = point + diff;
        if (t.x < 0 || t.y < 0 || t.x >= board.Width || t.y >= board.Height)
            return false;
        return true;
    }

    List<Point> dir2P = new List<Point>() {
        new Point(0, 1), new Point(1, 0),
        new Point(0, -1), new Point(-1, 0) };
}
