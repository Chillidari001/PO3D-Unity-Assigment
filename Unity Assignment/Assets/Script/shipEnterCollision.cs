using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class shipEnterCollision : MonoBehaviour
{
    [SerializeField] GameObject shipEnterCollider = null;
    public bool canEnter = false;
    [SerializeField] private TextMeshProUGUI canEnterText;

    private void Start()
    {
        canEnterText.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider shipEnterCollider)
    {
        canEnter = true;
        canEnterText.gameObject.SetActive(true);
    }
}