using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewInputUpAndDown : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public float speed = 2.0f;
    void Start()
    {
        playerRb= GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveUpAndDown()
    {
        playerRb.AddForce(focalPoint.transform.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
    }
}
