using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "Enemy Hook", menuName = "Enemy/Actions/Hook")]
public class EnemyHook : EnemyAction
{

    public int Damage;

    public override bool Check(Entity source)
    {
        var dist = Player.instance.transform.position - source.transform.position;
        if(dist.magnitude <= 1)
        {
            if((source as Enemy).actionPoints >= actionCost)
            return true;
        }
        return false;
    }

    public override void Do(Entity source)
    {
        source.GetComponentInChildren<Animator>().SetTrigger("Hook");
        (source as Enemy).ready = false;
        source.transform.DOPunchPosition((Player.instance.transform.position - source.transform.position) / 2, 0.7f, 0).OnComplete(() => (source as Enemy).ready = true);
        var dir = Player.instance.transform.position - source.transform.position;
        source.LookTo(dir.Unidirectional());
        Player.instance.TakeDamage(Damage);
        if (source is Enemy)
        {
            (source as Enemy).actionPoints -= actionCost;
        }
    }
}
