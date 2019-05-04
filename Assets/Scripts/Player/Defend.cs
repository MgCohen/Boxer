using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Defend", menuName = "Actions/Defend")]
public class Defend : Action
{
    public int Defense;

    public override void Do(Entity source, Entity Target)
    {
        base.Do(source, Target);
        if(source is Player)
        {
            source.GetComponentInChildren<Animator>().SetTrigger(animationName);
            (source as Player).BonusDef += Defense;
        }
    }
}
