using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float zMove;
    float zRot,yRot,xRot;
    Rigidbody rb;


    [SerializeField] float rotationSpeed;
    [SerializeField] float speed;
    void Start()
    {
        zMove = Input.GetAxis("Vertical");
        rb =this.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        yRot = Input.GetAxis("Mouse Y");
        xRot=Input.GetAxis("Mouse X");
        zMove = Input.GetAxis("Vertical");
        zRot=Input.GetAxis("Horizontal");
        Debug.Log(zMove);
    }

    private void FixedUpdate()
    {

        rb.velocity = transform.forward * speed * zMove;
        float rotMulti = 360 * Time.deltaTime * rotationSpeed;
        Vector3 rotation=new Vector3 (-yRot, xRot, zRot);
        transform.Rotate(rotation*rotMulti);
    }

}
