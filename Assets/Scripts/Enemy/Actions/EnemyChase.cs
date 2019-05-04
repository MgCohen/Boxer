using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "Enemy Chase", menuName = "Enemy/Actions/Chase")]
public class EnemyChase : EnemyAction
{
    public override bool Check(Entity source)
    {
        var dist = (Player.instance.transform.position - source.transform.position).magnitude;
        if(dist > 1)
        {
            if((source as Enemy).actionPoints >= actionCost)
            {
                return true;
            }
        }
        return false;
    }

    public override void Do(Entity source)
    {
        var dir = (Player.instance.transform.position - source.transform.position);
        var path = dir.Unidirectional();
        if (path == Vector3.zero)
        {
            path = dir.Directional();
            var chance = Random.Range(0f, 1f);
            if(chance > 0.5f)
            {
                path.x = 0;
            }
            else
            {
                path.y = 0;
            }
        }
        source.LookTo(path);
        var targetCell = Ring.CellAt(source.transform.position + path);
        (source as Enemy).ready = false;
        source.transform.DOMove(targetCell.transform.position, 0.15f).OnComplete(() => (source as Enemy).ready = true);
        source.currentCell.Contained = null;
        source.currentCell = targetCell;
        targetCell.Contained = source;
        (source as Enemy).actionPoints -= actionCost;
    }
}
