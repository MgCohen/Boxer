using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "Hook", menuName = "Actions/Hook")]
public class Hook : Action
{

    public int Damage;

    public override void Do(Entity Source, Entity Target)
    {
        base.Do(Source, Target);
        Source.GetComponentInChildren<Animator>().SetTrigger("Hook");
        Source.transform.DOPunchPosition((Target.transform.position - Source.transform.position) / 2, 0.7f, 0).OnComplete(() => Player.instance.CheckActions());
        if (Target is Enemy)
        {
            (Target as Enemy).TakeDamage(Damage);
        }
        if (Source is Player)
        {
            (Source as Player).currentPoints -= 1;
        }
    }
}
