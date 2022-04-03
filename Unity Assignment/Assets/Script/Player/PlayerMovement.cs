using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AudioClip shoutingClip;
    public float speedDampTime = 0.01f;
    public float sensitivtyX = 1.0f;

    private Animator anim;
    private HashIDs hash;

    private float elapsedTime = 0;
    private bool noBackMov = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetLayerWeight(1, 1f);
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
    }

    void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        bool sneak = Input.GetButton("Sneak");
        float turn = Input.GetAxis("Turn");

        Rotating(turn);
        MovementManagement(v, sneak);

        elapsedTime += Time.deltaTime;
        //Debug.Log("Elapsed time is : " + elapsedTime);
    }

    void Update()
    {
        //cache the shout input
        bool shout = Input.GetButtonDown("Attract");
        //set animator shouting parameter
        anim.SetBool(hash.shoutingBool, shout);

        AudioManagement(shout);
    }

    void MovementManagement(float vertical, bool sneaking)
    {
        anim.SetBool(hash.sneakingBool, sneaking);

        if (vertical > 0)
        {
            anim.SetFloat(hash.speedFloat, 1.5f, speedDampTime, Time.deltaTime);
            noBackMov = true;
        }
        if(vertical < 0)
        {
            if (noBackMov == true)
            {
                elapsedTime = 0;
                noBackMov = false;
            }
            anim.SetFloat(hash.speedFloat, -1.5f, speedDampTime, Time.deltaTime);
            anim.SetBool("Backwards", true);

            Rigidbody ourBody = this.GetComponent<Rigidbody>();
            float movement = Mathf.Lerp(0.0f, -0.025f, 0.7f);
            Vector3 moveBack = new Vector3(0.0f, 0.0f, movement);
            moveBack = ourBody.transform.TransformDirection(moveBack);
            ourBody.transform.position += moveBack;
        }
        if (vertical == 0)
        {
            anim.SetFloat(hash.speedFloat, 0.01f);
            anim.SetBool(hash.backwardsBool, false);
            noBackMov = true;
        }
    }

    void Rotating(float mouseXInput)
    {
        //access avatar rigidbody
        Rigidbody ourBody = this.GetComponent<Rigidbody>();

        //first check if we have rotation data that needs to be applied
        if (mouseXInput != 0)
        {
            //if so we use mouseX value to create a euler angle which proivides rotation in the Y axis which is then turned to a Quaternion
            Quaternion deltaRotation = Quaternion.Euler(0f, mouseXInput * sensitivtyX, 0f);

            //and then applied to turn avatar via the rigidbody
            ourBody.MoveRotation(ourBody.rotation * deltaRotation);
        }
    }

    void AudioManagement(bool shout)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            //and if footsteps are not already playing
            if (!GetComponent<AudioSource>().isPlaying)
            {
                //play footsteps
                GetComponent<AudioSource>().pitch = 0.27f;
                GetComponent<AudioSource>().Play();
            }
            else
            {
                //otherwise stop the footsteps
                GetComponent<AudioSource>().Stop();
            }
        }

        if (shout)
        {
            //3d sound to play shouting clip where we are
            AudioSource.PlayClipAtPoint(shoutingClip, transform.position);
        }
    }
}
