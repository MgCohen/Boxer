using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring: MonoBehaviour
{
    public static List<Cell> Spaces = new List<Cell>();

    public static Cell CellAt(Vector3 Pos)
    {
        Cell selected = null;
        foreach(var cell in Spaces)
        {
            if(cell.transform.position == Pos)
            {
                selected = cell;
            }
        }
        return selected;
    }

    private void Start()
    {
        //develop boardpos
    }
}
