using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Star : MonoBehaviour
{

    public int scene;
    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(scene);
    }
}
