using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{

    public float inputForce = 40f;

    public GameObject eggPrefab;

    // Start is called before the first frame update
    void Start()
    {
        transform.position += new Vector3(Random.Range(-4, 4), -1, 0);

        if (!isLocalPlayer)
        {
            var rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var networkIdentity = GetComponent<Mirror.NetworkIdentity>();
        var rb = GetComponent<Rigidbody>();
        if (!networkIdentity.hasAuthority)
        {
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(new Vector3(0, 0, inputForce));
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(new Vector3(0, 0, -inputForce));
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector3(-inputForce, 0, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector3(inputForce, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdSpawnEgg();
        }
    }

    [Command]
    void CmdSpawnEgg()
    {
        GameObject newEgg = Instantiate(eggPrefab);
        newEgg.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        NetworkServer.Spawn(newEgg);
    }

}
