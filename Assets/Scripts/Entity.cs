using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Cell currentCell = null;
    private void Start()
    {
        currentCell = Ring.CellAt(transform.position);
        currentCell.Contained = this;
    }
    public void LookTo(Vector3 dir)
    {
        if (dir.x > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (dir.x < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
