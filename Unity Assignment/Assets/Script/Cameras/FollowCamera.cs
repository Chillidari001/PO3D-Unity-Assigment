using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject target;
    Vector3 offset;
    private float mouseX;
    private float mouseY;
    private float mouseZ;


    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        mouseZ = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetMouseButton(1))
        {
            offset = Quaternion.Euler(0, mouseX, 0) * offset;
        }
        
        float angleBetween = Vector3.Angle(Vector3.up, transform.forward);
        //Debug.Log("offset Angle: " + angleBetween);
        if (((angleBetween > 100.0f) && (mouseY < 0)) || ((angleBetween < 140.0f) && (mouseY > 0)))
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 LocalRight = target.transform.worldToLocalMatrix.MultiplyVector(transform.right);
                offset = Quaternion.AngleAxis(mouseY, LocalRight) * offset;
            }
        }

        float dist = Vector3.Distance(target.transform.position, transform.position);
        Debug.Log("dist: " + dist);
        if (mouseZ > 0)
        {
            offset = Vector3.Scale(offset, new Vector3(1.05f, 1.05f, 1.05f));
        }
        if (mouseZ < 0)
        {
            offset = Vector3.Scale(offset, new Vector3(0.95f, 0.95f, 0.95f));
        }

        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position + (rotation * offset);
        transform.LookAt(target.transform);
    }
}
