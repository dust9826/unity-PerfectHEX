using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnitState : BattleState
{
    int index = -1;

    public override void Enter()
    {
        base.Enter();
        StartCoroutine("ChangeCurrentUnit");
    }

    IEnumerator ChangeCurrentUnit()
    {
        index = (index + 1) % entities.Count;
        turn.Change(entities[index]);
        Debug.Log(entities[index]);
        cameraRig.follow = turn.actor.transform;
        SelectTile(turn.actor.pos);
        yield return null;
        if (turn.actor.isPlayer)
            owner.ChangeState<CommandSelectionState>();
        else
            owner.ChangeState<EnemySequenceState>();
    }
}
