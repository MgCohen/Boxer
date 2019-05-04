using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public int Hp;
    [HideInInspector]public int actionPoints;
    [SerializeField]private int maxPoints;
    public bool ready = true;
    public bool done = false;

    private void OnEnable()
    {
        Manager.instance.enemies.Add(this);
        actionPoints = maxPoints;
    }
    private void OnDisable()
    {
        Manager.instance.enemies.Remove(this);
    }

    public List<EnemyBehaviour> behaviours;

    public void TakeDamage(int value)
    {
        Hp -= value;
        if(Hp <= 0)
        {
            Die();
        }
        //damage anim
    }

    public void Die()
    {
        currentCell.Contained = null;
        Destroy(gameObject);
    }

    public void Activate()
    {
        done = false;
        actionPoints = maxPoints;
        EnemyBehaviour selected = null;
        foreach(var behav in behaviours)
        {
            if (behav.condition.Check(this))
            {
                selected = behav;
                break;
            }
        }
        if (!selected)
        {
            selected = behaviours[0];
        }
        selected.Activate(this);
    }

}
