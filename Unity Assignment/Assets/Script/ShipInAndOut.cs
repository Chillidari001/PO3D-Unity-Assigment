using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInAndOut : MonoBehaviour
{
    public Camera shipCam;
    public Camera playerCam;

    [Header("Player")]
    [SerializeField] GameObject player = null;

    [Space, Header("Ship")]
    [SerializeField] GameObject ship = null;
    [SerializeField] GameObject physicalShip = null;
    [SerializeField] public bool mouseControl;

    [Header("Input")]
    [SerializeField] KeyCode enterExitKey = KeyCode.P;
    [SerializeField] KeyCode controlChoiceKey = KeyCode.Tab;
    // Start is called before the first frame update
    void Start()
    {
        ship.GetComponent<ShipControl>().enabled = false;
        shipCam.enabled = false;
        playerCam.enabled = true;
        mouseControl = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(enterExitKey))
        {
            ExitShip();
            shipCam.enabled = !shipCam.enabled;
            playerCam.enabled = !playerCam.enabled;
        }
        if (Input.GetKeyDown(controlChoiceKey))
        {
            mouseControl = !mouseControl;
        }

        if (!ship.GetComponent<ShipControl>().enabled)
        {
            physicalShip.GetComponent<Rigidbody>().useGravity = true;
        }
        if (ship.GetComponent<ShipControl>().enabled)
        {
            physicalShip.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    void ExitShip()
    {
        player.SetActive(!player.activeSelf);
        //ship.SetActive(!ship.activeSelf);
        ship.GetComponent<ShipControl>().enabled = !ship.GetComponent<ShipControl>().enabled;
        player.GetComponent<PlayerMovement>().enabled = !player.GetComponent<PlayerMovement>().enabled;

        player.transform.position = ship.transform.position + ship.transform.TransformDirection(Vector3.left)*6;
    }
}
