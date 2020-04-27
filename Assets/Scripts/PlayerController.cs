using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : MonoBehaviour
{
    public GameObject eggPrefab;

    // Start is called before the first frame update
    void Start()
    {
        transform.position += new Vector3(Random.Range(-4, 4), -1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        var networkIdentity = GetComponent<Mirror.NetworkIdentity>();
        var rb = GetComponent<Rigidbody>();
        if (!networkIdentity.hasAuthority)
        {
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(new Vector3(0, 0, 50));
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(new Vector3(0, 0, -50));
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector3(-50, 0, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector3(50, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            GameObject newEgg = Instantiate(eggPrefab);
            NetworkServer.Spawn(newEgg);
            newEgg.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
