using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public abstract class BaseAbilityMenuState : BattleState
{
    protected string menuTitle;
    // 해당 메뉴의 버튼 분류
    protected List<string> menuOptions;


    public override void Enter()
    {
        base.Enter();
        SelectTile(turn.actor.pos);

        // 메뉴판을 호출한다.
        LoadMenu();
    }
    public override void Exit()
    {
        base.Exit();
        // 상태가 해제되면 메뉴판을 숨긴다.
        abilityMenuPanelController.Hide();
    }
    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        // InputManager 에서 입력된 마우스의 번호가 e 에 들어온다.
        if (e.info == 0) Confirm();
        else Cancel();
    }
    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        // 좌우 또는 상하로 메뉴판에서 선택된
        // 버튼을 변경할 수 있다.
        // InputManager 에서 상하좌우 입력에 따라
        // -1~1 값을 e로 전달
        if (e.info.x > 0 || e.info.y < 0)
            abilityMenuPanelController.Next();
        else
            abilityMenuPanelController.Previous();
    }
    protected abstract void LoadMenu();
    protected abstract void Confirm();
    protected abstract void Cancel();
}