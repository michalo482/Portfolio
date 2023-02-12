using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateCamera : MonoBehaviour
{

    public float rotationSpeed;
    private Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * movement.x * rotationSpeed * Time.deltaTime);
    }

   public void OnMove(InputValue value)
    {
        movement = value.Get<Vector3>();
    }
}
