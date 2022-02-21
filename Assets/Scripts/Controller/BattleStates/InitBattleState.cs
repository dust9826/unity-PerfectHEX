using UnityEngine;
using System.Collections;

public class InitBattleState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init());
    }
    IEnumerator Init()
    {
        board.SetBoard();

        entities.Add(board.GetPlayer());

        yield return null;

        owner.ChangeState<SelectUnitState>();
    }
    
}
