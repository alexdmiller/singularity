using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position += new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), 0);
    }

    // Update is called once per frame
    void Update()
    {
        var networkIdentity = GetComponent<Mirror.NetworkIdentity>();
        if (!networkIdentity.hasAuthority)
        {
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
        }
    }
}
