using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public List<Enemy> enemies = new List<Enemy>();

    public void PassTurn()
    {
        StartCoroutine(Activations());
    }

    IEnumerator Activations()
    {
        yield return new WaitForSeconds(0.35f);
        foreach(var enm in enemies)
        {
            enm.Activate();
            while (!enm.done)
            {
                yield return null;
            }
        }
        Player.instance.StartTurn();
    }
}
