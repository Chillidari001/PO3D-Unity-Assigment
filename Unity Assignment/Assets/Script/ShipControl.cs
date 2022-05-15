using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    public float forwardSpeed = 8f, strafeSpeed = 2f, hoverSpeed = 5.5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 1f, strafeAcceleration = 2f, hoverAcceleration = 2f;

    //public Transform ship;
    
    public Transform blade1;
    public Transform blade2;
    private float bladeSpeed = 500;

    [SerializeField]public Transform arm;
    [Header("Claw Input")]
    [SerializeField] KeyCode clawUp = KeyCode.Alpha2;
    [SerializeField] KeyCode clawDown = KeyCode.Alpha1;
    private float clawSpeed = 50;


    public Transform gear;
    private float gearSpeed = 100;

    public bool isGrounded;
    public float groundCheck;
    public LayerMask groundMask;

    public float lookRateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    public ShipInAndOut controlScript;
    
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;

        Cursor.lockState = CursorLockMode.Confined;

        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(transform.position + new Vector3(0, -1.6f, 0), groundCheck, groundMask);

        if (!isGrounded)
        {
            blade1.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * bladeSpeed);
            blade2.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * bladeSpeed);

            gear.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * gearSpeed);

            if (GetComponent<Animator>())
            {
                anim.SetBool("inAir", true);
            }
        }
        else
        {
            if (GetComponent<Animator>())
            {
                anim.SetBool("inAir", false);
            }
        }


        if (Input.GetKey(KeyCode.Space))
        {
            blade1.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * bladeSpeed);
            blade2.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * bladeSpeed);
        }
        if(Input.GetKey(KeyCode.C))
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

        clawMovement();
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

    void keyboardRotate()
    {
        Rigidbody ourBody = this.GetComponentInParent<Rigidbody>();
        if (activeStrafeSpeed != 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(0f, activeStrafeSpeed * Time.deltaTime * 1.1f, 0f);

            ourBody.MoveRotation(ourBody.rotation * deltaRotation);
        }
    }

    void clawMovement()
    {
        if (Input.GetKey(clawUp))
        {
            arm.Rotate(new Vector3(0, 0, 5) * Time.deltaTime * clawSpeed);
        }
        if (Input.GetKey(clawDown))
        {
            arm.Rotate(new Vector3(0, 0, -5) * Time.deltaTime * clawSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (controlScript.mouseControl)
        {
            GetComponent<Animator>().enabled = false;
            mouseMovement();
        }
        if (!controlScript.mouseControl)
        {
            GetComponent<Animator>().enabled = true;
            regularMovement();
            keyboardRotate();
        }
    }
}
