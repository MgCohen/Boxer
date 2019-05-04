using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour", menuName = "Enemy/Behaviour")]
public class EnemyBehaviour : ScriptableObject
{

    public BehaviourCondition condition;
    public List<EnemyAction> actions;
    public bool loopable;

    public void Activate(Entity source)
    {
        source.StartCoroutine(Activation(source));
    }

    IEnumerator Activation(Entity source, bool isLoop = false)
    {
        var points = (source as Enemy).actionPoints;
        for (int i = 0; i < actions.Count; i++)
        {
            if (actions[i].Check(source))
            {
                actions[i].Do(source);
                if (actions[i].loopable)
                {
                    i--;
                }
            }
            while(!(source as Enemy).ready)
            {
                yield return null;
            }

        }
        var newPoints = (source as Enemy).actionPoints;
        if (points != newPoints)
        {
            isLoop = false;
        }
        if (loopable && !isLoop)
        {
            source.StartCoroutine(Activation(source, true));
        }
        else
        {
            (source as Enemy).done = true;
        }
    }
}
