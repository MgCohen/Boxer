using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : ScriptableObject
{

    public string animationName;
    public int actionCost;

    public virtual void Do(Entity source, Entity Target)
    {

    }
}
