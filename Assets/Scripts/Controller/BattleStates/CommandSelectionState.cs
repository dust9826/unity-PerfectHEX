using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandSelectionState : BaseAbilityMenuState
{
    // 메뉴창이 취소가 되었을때 처리
    protected override void Cancel()
    {
        if (turn.hasUnitMoved && !turn.lockMove)
        {
            turn.UndoMove();
            //abilityMenuPanelController.SetLocked(0, false);
            SelectTile(turn.actor.pos);
        }
        else
        {
            owner.ChangeState<ExploreState>();
        }
    }

    // 버튼을 선택했을 때의 상태 변경
    protected override void Confirm()
    {
        switch (abilityMenuPanelController.selection)
        {
            case 0: // Move
                owner.ChangeState<MoveTargetState>();
                break;
            case 1: // Action
                owner.ChangeState<InteractionTargetState>();
                break;
            case 2: // Wait
                owner.ChangeState<SelectUnitState>();
                break;
        }
    }

    // 메뉴를 연다.
    protected override void LoadMenu()
    {
        // 메뉴의 옵션(분류) 세팅
        if (menuOptions == null)
        {
            menuTitle = "Commands";
            menuOptions = new List<string>(3);
            menuOptions.Add("Move");
            menuOptions.Add("Interaction");
            menuOptions.Add("Wait");
        }

        abilityMenuPanelController.Show(menuTitle, menuOptions);


        //// 해당 캐릭터가 이번 턴에서
        //// 이미 이동했는지 또는 공격했는지를 체크해서
        //// 버튼을 잠금상태로 만든다.
        //// 한번의 턴 == 1번 공격 and 1번 이동
        //abilityMenuPanelController.SetLocked(0, turn.hasUnitMoved);
        //abilityMenuPanelController.SetLocked(1, turn.hasUnitActed);
    }
}
