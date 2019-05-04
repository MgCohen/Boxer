using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourCondition : ScriptableObject
{
    public virtual bool Check(Entity source)
    {
        return true;
    }
}
