using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Jab", menuName = "Actions/Jab")]
public class Jab : Action
{
    public int Damage;

    public int Range;

    public override void Do(Entity source, Entity Target)
    {
        base.Do(source, Target);
        var dist = (source.transform.position - Target.transform.position).magnitude;
        if(dist >= Range + 1)
        {
            return;
        }
        source.GetComponentInChildren<Animator>().SetTrigger(animationName);
        if(source is Player)
        {
            (source as Player).currentPoints -= actionCost;
        }
        if(Target is Enemy)
        {
            (Target as Enemy).TakeDamage(Damage);
        }
        //Player.instance.CheckActions();
    }
}
