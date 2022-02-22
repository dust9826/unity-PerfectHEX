using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUnitState : BattleState
{
    public override void Enter()
    {
        base.Enter();

        StartCoroutine("AddEntities");
    }

    IEnumerator AddEntities()
    {
        Dictionary<Point, TileLayer> tiles = board.GetTilesInRange(pos, 4);
        foreach (var tile in tiles)
        {
            if (tile.Value != TileLayer.Entity)
                continue;
            Entity newEntity = board.GetEntity(tile.Key);
            if (entities.Contains(newEntity))
                continue;
            entities.Add(newEntity);
        }

        yield return null;

        owner.ChangeState<SelectUnitState>();
    }
}
