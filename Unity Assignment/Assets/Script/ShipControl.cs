using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    public float forwardSpeed = 11f, strafeSpeed = 7f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 1f, strafeAcceleration = 2f, hoverAcceleration = 2f;

    //public Transform ship;
    
    public Transform blade1;
    public Transform blade2;
    private float bladeSpeed = 500;

    public bool isGrounded;
    public float groundCheck;
    public LayerMask groundMask;

    public float lookRateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    public ShipInAndOut controlScript;

    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;

        Cursor.lockState = CursorLockMode.Confined;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(transform.position + new Vector3(0, -1.6f, 0), groundCheck, groundMask);

        if (!isGrounded)
        {
            blade1.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * bladeSpeed);
            blade2.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * bladeSpeed);
        }


        if (Input.GetKey(KeyCode.Space))
        {
            blade1.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * bladeSpeed);
            blade2.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * bladeSpeed);
        }
        if(Input.GetKey(KeyCode.LeftControl))
        {
            blade1.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * bladeSpeed);
            blade2.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * bladeSpeed);
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            {
                blade1.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * bladeSpeed);
                blade2.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * bladeSpeed);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            blade1.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * bladeSpeed);
            blade2.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * bladeSpeed);
        }

    }

    void mouseMovement()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);
        transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, 0f, Space.Self);
        
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);

        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
        transform.position += transform.up * activeHoverSpeed * Time.deltaTime;
    }

    void regularMovement()
    {
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);

        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
        transform.position += transform.up * activeHoverSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (controlScript.mouseControl)
        {
            mouseMovement();
        }
        if (!controlScript.mouseControl)
        {
            regularMovement();
        }
    }
}
