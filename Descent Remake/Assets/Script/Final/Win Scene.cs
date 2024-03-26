using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;

    private void OnCollisionEnter(Collision collision)
    {
        Camera.main.cullingMask = layerMask;
    }
}
