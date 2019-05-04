using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Standard Condition", menuName = "Enemy/Condition/Standard")]
public class StandardCondition : BehaviourCondition
{
    public override bool Check(Entity source)
    {
        return false;
    }
}
