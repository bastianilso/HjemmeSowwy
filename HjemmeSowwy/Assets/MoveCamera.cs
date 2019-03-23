using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MoveCamera : MonoBehaviour
{
    // Start is called before the first frame update

    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public float speedKey = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private CharacterController controller;
    void Start()
    {
      controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
      Vector3 forward = transform.TransformDirection(Vector3.forward);
      float curSpeed = speedKey * Input.GetAxis("Vertical");
      controller.SimpleMove(forward * curSpeed); 

      yaw += speedH * Input.GetAxis("Mouse X");
      pitch -= speedV * Input.GetAxis("Mouse Y");

      transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
      
    }
}
