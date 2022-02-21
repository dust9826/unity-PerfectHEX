using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Turn
{
    // 이번 턴에 움직이는 대상
    public Entity actor;

    // 이동, 공격 진행 여부
    public bool hasUnitMoved;
    public bool hasUnitActed;

    // 최종적으로 이동을 완료했는지 체크하는 변수
    public bool lockMove;

    public bool isMine;

    // 시작 타일
    Point startPos;
    // 시작 방향
    Directions startDir;

    public void Change(Entity current)
    {
        // 행동하는 캐릭터 변경.
        actor = current;

        // 공격, 이동을 아직 안했음 으로 변경
        hasUnitMoved = false;
        hasUnitActed = false;
        lockMove = false;

        // 시작타일, 시작방향 설정
        startPos = actor.pos;
        startDir = actor.dir;
    }

    // 이동이 취소됨
    public void UndoMove()
    {
        hasUnitMoved = false;
        actor.Place(startPos);
        actor.dir = startDir;
        actor.Match();
    }
}
