using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{

    public float inputForce = 40f;
    public float jumpForce = 500f;
    public float distToGround;
    public float epsilon = 0.1f;
    public float minHeightScale = 0.2f;
    public float maxHeightScale = 20f;

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

        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    // FixedUpdate is called whenever physics is called (fixed timesteps)
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

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
        }

        if (Input.GetKey(KeyCode.R))
        {
            var curScale = transform.localScale;
            transform.localScale = new Vector3(
                curScale.x, Mathf.Min(maxHeightScale, curScale.y + 0.3f), curScale.z);
            distToGround = GetComponent<Collider>().bounds.extents.y;
        }
        else if (Input.GetKey(KeyCode.F))
        {
            var curScale = transform.localScale;
            transform.localScale = new Vector3(
                curScale.x, Mathf.Max(minHeightScale, curScale.y - 0.5f), curScale.z);
            distToGround = GetComponent<Collider>().bounds.extents.y;
        }

    }

    // [Command]
    // void CmdSpawnEgg()
    // {
    //     GameObject newEgg = Instantiate(eggPrefab);
    //     newEgg.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    //     NetworkServer.Spawn(newEgg);
    // }

}
