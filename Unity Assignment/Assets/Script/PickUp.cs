using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject collisionCube;
    public GameObject objective;
    public GameObject attachPoint;
    public GameObject wayPoint;
    private Collider collider;
    private bool isPicked = false;

    // Start is called before the first frame update
    void Start()
    {
        collider = objective.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPicked)
        {
            objective.transform.position = attachPoint.transform.position;
            wayPoint.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == collider)
        {
            isPicked = true;
        }
    }
}
