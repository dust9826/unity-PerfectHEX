using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSequenceState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine("Sequence");
    }

    IEnumerator Sequence()
    {
        Interaction i = board.GetInteraction(pos);

        // 상호작용할 위치를 보게 한다.
        Movement m = turn.actor.GetComponent<Movement>();
        Directions dir = DirectionsExtensions.GetDirection(turn.actor.pos, pos);
        yield return StartCoroutine(m.Turn(dir));

        // 상호작용을 한다.
        if(i.Info == InteractionInfo.Door)
        {
            board.RemoveInteraction(pos);
        }
        i.Interact();
        yield return new WaitForSeconds(0.2f);
        while(i.IsInteracting)
            yield return null;
        
        SelectTile(turn.actor.pos);
        Debug.Log("Interact End");
        owner.ChangeState<SelectUnitState>();
    }
}
 