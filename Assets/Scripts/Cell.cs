using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    public Entity Contained;

    private void Awake()
    {
        Ring.Spaces.Add(this);
    }
}
