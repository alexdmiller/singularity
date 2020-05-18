using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ColorChooser : NetworkBehaviour
{
    public Color option1;
    public Color option2;

    public override void OnStartClient()
    {

        var color = option1;
        var playersFound = GameObject.FindGameObjectsWithTag("Player").Length;
        if (playersFound > 1)
        {
            color = option2;
        }
        GetComponentInChildren<Renderer>().material.color = color;
    }
}
