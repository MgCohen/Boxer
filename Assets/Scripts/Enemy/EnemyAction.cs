using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : ScriptableObject
{

    public int actionCost;

    public bool loopable;

    public virtual bool Check(Entity source)
    {
        return false;
    }

    public virtual void Do(Entity source)
    {
    }
}
