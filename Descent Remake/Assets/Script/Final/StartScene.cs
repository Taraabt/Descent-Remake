using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Star : MonoBehaviour
{
   [SerializeField]Canvas canvas;
    private void OnCollisionEnter(Collision collision)
    {
        canvas.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}
