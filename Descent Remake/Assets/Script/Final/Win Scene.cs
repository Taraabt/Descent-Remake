using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour
{
    [SerializeField] Transform toDisable;


    private void OnCollisionEnter(Collision collision)
    {
        toDisable.gameObject.SetActive(true);
        SceneManager.LoadScene(3);
    }
}
