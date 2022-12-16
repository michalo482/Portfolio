using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed;
    private PlayerController controller;
    private SpawnManager counter;
    private new Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {        
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
        counter = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        
        if (!controller.gameOver)
        {
            speed = 10 + (float)(counter.spawnCount);
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            if (controller.dash)
            {
                rigidbody.AddForce(Vector3.left * 40, ForceMode.VelocityChange);
                //transform.Translate(Vector3.left * Time.deltaTime * (speed * 2));               
            }
            else
            {
                rigidbody.AddForce(-rigidbody.velocity * 0.2f);
            }   
        }
        
        if (transform.position.x < - 15 && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);            
        }
    }


}
