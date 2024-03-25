using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    [SerializeField]Transform[] toDisable;
    [SerializeField]LayerMask layerMask;

    [SerializeField]Transform toEnable;
    [SerializeField]Animator finalcut;
    private void OnCollisionEnter(Collision collision)
    {
        //finalcut = collision.gameObject.GetComponent<Animator>();
        finalcut.enabled = true;
        PlayerGuns pGuns= collision.gameObject.GetComponent<PlayerGuns>();
        PlayerMovement pMove = collision.gameObject.GetComponent<PlayerMovement>();

        pGuns.enabled = false;
        pMove.enabled = false;
        foreach (Transform t in toDisable)
        {
            t.gameObject.SetActive(false);
        }
        toEnable.gameObject.SetActive(true);
        Camera.main.gameObject.SetActive(true);
        Camera.main.transform.position=toEnable.transform.position;
        Camera.main.transform.rotation = toEnable.transform.rotation;
        Camera.main.cullingMask =layerMask;

    }


}
