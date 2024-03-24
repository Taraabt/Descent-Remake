using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PlayerMovement : MonoBehaviour 
{
    float zMove;
    float zRot, yRot, xRot;
    Rigidbody rb;
    Vector3 xyz;
    Vector3 tRot;

    float oldX;
    float oldY;
    public bool hasKey=false;


    [SerializeField] GameObject[] ui;
    [SerializeField] float rotationSpeed;
    [SerializeField] float mouseSensitivity;
    [SerializeField] float speed;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        PlayerHp.OnDeath += Death;

        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {

        float y = Input.GetAxis("Mouse Y");
        float x = Input.GetAxis("Mouse X");

        xRot = yRot = 0;

        if (x != oldX)
        {
            xRot = x*mouseSensitivity;
        }

        if (y != oldY)
        {
            yRot = y* mouseSensitivity;
        }

        zRot = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime * -1;
        xyz = new Vector3(-yRot, xRot, zRot);
        tRot = transform.right + transform.up + transform.forward;
        zMove = Input.GetAxis("Vertical");

        oldX = x;
        oldY = y;

        float rearInput=Input.GetAxisRaw("Rear");
        if (Input.GetButtonDown("Rear"))
        {
            if(rearInput > 0)
            {
                ui[0].SetActive(false);
                ui[2].SetActive(true);
            }
            else if(rearInput <0)
            {
                ui[0].SetActive(false);
                ui[1].SetActive(true);
            }
        }else if (Input.GetButtonUp("Rear"))
        {
            ui[1].SetActive(false);
            ui[2].SetActive(false);
            ui[0].SetActive(true);
        }


    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed * zMove;
        transform.Rotate(xyz);
    }

    void Death()
    {
        PlayerHp.OnDeath -= Death;
        this.enabled = false;
    }

}
