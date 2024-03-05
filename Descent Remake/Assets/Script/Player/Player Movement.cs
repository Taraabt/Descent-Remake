using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float zMove;
    float zRot, yRot, xRot;
    Rigidbody rb;


    [SerializeField] float rotationSpeed;
    [SerializeField] float mouseSensitivity;
    [SerializeField] float speed;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        yRot += Input.GetAxis("Mouse Y");
        xRot += Input.GetAxis("Mouse X");

        zMove = Input.GetAxis("Vertical");
        zRot += Input.GetAxis("Horizontal");        
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed * zMove;
        float z = zRot * 360 * rotationSpeed* Time.deltaTime;
        
        Vector3 rotation = new Vector3(-yRot * mouseSensitivity, xRot * mouseSensitivity, z);

        transform.localRotation = Quaternion.Euler(rotation);
    }

}
