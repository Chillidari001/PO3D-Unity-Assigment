using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] KeyCode enterExitKey = KeyCode.P;

    /*public Camera triggeredCam;
    public Camera liveCam;
    public Camera followCam;*/
    public Camera shipCam;
    public Camera playerCam;

    private void Start()
    {
        shipCam.enabled = false;
        playerCam.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(enterExitKey))
        {
            shipCam.enabled = !shipCam.enabled;
            playerCam.enabled = !playerCam.enabled;
        }
    }
}