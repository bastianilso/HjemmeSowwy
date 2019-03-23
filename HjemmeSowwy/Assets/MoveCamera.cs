using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Start is called before the first frame update

    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public float speedKey = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKey(KeyCode.W))
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z+speedKey);
        }
      if (Input.GetKey(KeyCode.S))
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z-speedKey);
        }
      if (Input.GetKey(KeyCode.A))
        {
            this.transform.position = new Vector3(this.transform.position.x-speedKey, this.transform.position.y, this.transform.position.z);
        }
      if (Input.GetKey(KeyCode.D))
        {
            this.transform.position = new Vector3(this.transform.position.x+speedKey, this.transform.position.y, this.transform.position.z);
        }
      yaw += speedH * Input.GetAxis("Mouse X");
      pitch -= speedV * Input.GetAxis("Mouse Y");

      transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        // When mouse is moved to the left, rotate camera to the left
        // When mouse is moved to the right, rotate camera to the right
        // When mouse is moved up, rotate camera up
        // When mouse is moved down, rotate camera down
        // When mouse isn't moved, don't do anything 
    }
}
