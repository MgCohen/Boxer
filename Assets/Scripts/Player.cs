using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : Entity
{
    public static Player instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
        TouchSystem.Swipe.AddListener(Move);
        TouchSystem.Doubletouch.AddListener(Action);
    }
    private void OnDisable()
    {
        TouchSystem.Swipe.RemoveListener(Move);
        TouchSystem.Doubletouch.RemoveListener(Action);
    }

    public bool input = true;
    public int Hp;
    public int ActionPoints;
    public int currentPoints;
    public int MoveSpeed = 1;
    public int BonusMove;
    public int BonusDef;
    public int BonusDmg;

    public Action attack;
    public Action defend;
    public Action engage;
    public Action special;

    public void PassTurn()
    {
        input = false;
        currentPoints = ActionPoints;
        Manager.instance.PassTurn();
        TouchSystem.SetEnabled(false);
    }

    public void StartTurn()
    {
        TurnReset();
        input = true;
        TouchSystem.SetEnabled(true);
    }

    public void TurnReset()
    {
        BonusMove = 0;
        BonusDef = 0;
        BonusDmg = 0;
    }

    public void Move()
    {
        var dir2 = TouchSystem.swipeDirection.Unidirectional();
        var dir = new Vector3(dir2.x, dir2.y, 0);
        var currentSpeed = MoveSpeed + BonusMove;
        var targetCell = Ring.CellAt(transform.position + (dir * currentSpeed));
        while (!targetCell && currentSpeed > 0)
        {
            currentSpeed -= 1;
            targetCell = Ring.CellAt(transform.position + (dir * currentSpeed));
        }
        if (targetCell)
        {
            var nextCell = Ring.CellAt(targetCell.transform.position + dir);
            if (nextCell)
            {
                LookTo(dir);
            }
            else
            {
                LookTo(dir * -1);
            }
        }
        Move(targetCell);
    }

    public void Move(Vector3 pos)
    {
        var targetCell = Ring.CellAt(pos);
        if (targetCell)
        {
            Move(targetCell);
        }
    }

    public void Move(Cell targetCell)
    {
        if (targetCell)
        {
            var target = targetCell.Contained;
            if (!target)
            {
                transform.DOMove(targetCell.transform.position, 0.15f).OnComplete(() => { currentPoints -= 1; CheckActions(); });
                currentCell.Contained = null;
                currentCell = targetCell;
                targetCell.Contained = this;
            }
            else
            {
                if (target is Enemy)
                {
                    engage.Do(this, target);
                }
            }
        }
    }

    public void Action()
    {
        var pos = TouchSystem.worldTouchPos.Round();
        var cell = Ring.CellAt(pos);
        if (!cell) return;
        var ent = cell.Contained;
        var dir = (cell.transform.position - transform.position).Directional();
        LookTo(dir);
        if (ent)
        {
            if (ent is Player)
            {
                defend.Do(this, null);
            }
            else
            {
                attack.Do(this, ent);
            }
        }
    }

    public void CheckActions()
    {
        if(currentPoints<= 0)
        {
            PassTurn();
        }
    }

    public void TakeDamage(int damage)
    {
        damage -= BonusDef;
        Hp -= damage;
        //set take damage anim
    }


}
