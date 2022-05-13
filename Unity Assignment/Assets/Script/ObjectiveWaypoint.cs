using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveWaypoint : MonoBehaviour
{
    public Image img;
    public Transform target;

    public Camera playerCam;
    public Camera shipCam;
    Camera cam;

    public Text meters;

    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;
        
        if (playerCam.enabled)
        {
            cam = playerCam;
        }
        if(shipCam.enabled)
        {
            cam = shipCam;
        }

        Vector2 pos = cam.WorldToScreenPoint(target.position + offset);

        if (Vector3.Dot((target.position - cam.transform.position), cam.transform.forward) < 0)
        {
            //if -1 target is behind player
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }

            else
            {
                pos.x = minX;
            }
        }
        
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
        meters.text = ((int)Vector3.Distance(target.position, cam.transform.position)).ToString() + "m";
    }
}
