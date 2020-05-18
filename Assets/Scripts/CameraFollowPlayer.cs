using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Rendering.PostProcessing;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;

    public float distance = 20f;
    public float height = 12f;
    public float dampening = 0.3f;

    // public PostProcessVolume postFX;


    // Start is called before the first frame update
    void Start()
    {
        // Unity debug logs *every frame* if we don't have an audio listener set.
        // However, we want ours set to the player's object, which isn't created until
        // they connect to the network. To work around this, we put a default audio
        // listener on the camera (which is created at game start) but turn the volume
        // to 0. When the player spawns (see update method below), it will have an audio
        // listener on it, so we disable this audio listener and turn the volume back
        // on.
        AudioListener.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // ensure we have our own player before continuing.
        if (player == null)
        {
            var players = GameObject.FindGameObjectsWithTag("Player");
            foreach (var p in players)
            {
                var netId = p.GetComponent<Mirror.NetworkIdentity>();
                if (netId == null)
                {
                    continue;
                }
                if (netId.hasAuthority)
                {
                    player = p.transform;
                    break;
                }
            }
            // We wanted to get a player but failed to do so. Don't update camera.
            if (player == null)
            {
                return;
            }

            // At this point, we wanted to get a player, and were successful! We can run
            // "startup" stuff here.
            GetComponent<AudioListener>().enabled = false;
            AudioListener.volume = 1;
        }

        // var goal = player.position +  (player.forward.normalized * dirScale);
        var goal = player.position + player.TransformDirection(new Vector3(0f, height, -distance));
        transform.position = Vector3.Lerp(transform.position, goal, dampening * Time.deltaTime);
        transform.LookAt(player);

        // postFX section.
        // if (postFX == null)
        // {
        //     return;
        // }
        // Depth of field
        // var dof = postFX.profile.GetSetting<DepthOfField>();
        // if (dof == null)
        // {
        //     return;
        // }
        // // Debug.Log("Overriding focus distance");
        // dof.focusDistance.Override(Vector3.Distance(transform.position, player.position));
    }
}
