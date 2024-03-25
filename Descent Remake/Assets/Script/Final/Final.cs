using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{

    [SerializeField]Animator finalcut;
    private void OnCollisionEnter(Collision collision)
    {
        finalcut = collision.gameObject.GetComponent<Animator>();
        finalcut.gameObject.SetActive(true);
    }


}
