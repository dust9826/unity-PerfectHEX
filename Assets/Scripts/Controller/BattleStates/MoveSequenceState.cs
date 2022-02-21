using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSequenceState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine("Sequence");
    }

    IEnumerator Sequence()
    {
        //turn.actor.Place(pos);
        Movement m = turn.actor.GetComponent<Movement>();

        yield return StartCoroutine(m.Traverse(floorTiles[pos]));

        turn.actor.Place(pos);

        owner.ChangeState<SelectUnitState>();
    }
}