using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Button : NetworkBehaviour
{
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        var networkIdentity = GetComponent<Mirror.NetworkIdentity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            if (pressingButton)
            {
                door.transform.Translate(new Vector3(0, 0.1f, 0));
            }
            else
            {
                if (door.transform.position.y >= 0)
                {
                    door.transform.Translate(new Vector3(0, -0.1f, 0));
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // var playerController = other.gameObject.GetComponent<PlayerController>();

        if (isServer)
        {
            Debug.Log("Enter");
            pressingButton = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // var playerController = other.gameObject.GetComponent<PlayerController>();

        if (isServer)
        {
            Debug.Log("Exit");
            pressingButton = false;
        }
    }

    public bool pressingButton;

    //[Command]
    //void CmdOnButtonEnter()
    //{
    //    pressingButton = true;
    //}

    //[Command]
    //void CmdOnButtonExit()
    //{
    //    pressingButton = false;
    //}
}
