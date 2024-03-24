using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

   public void StartGame(){
        SceneManager.LoadScene(1);
   }
    public void QuitGame()
    {
        Application.Quit();
    }
    //public void OnPointerEnter(Collision collision)
    //{
    //    Color color = new Color(1f, 144 / 255f, 0f);
    //    TMP_Text text = collision.gameObject.GetComponent<TMP_Text>();
    //    Debug.Log("yes");
    //    text.color = color;
    //}
    //public void OnPointerExit(Collision collision)
    //{
    //    Color color = new Color(1f, 1f, 1f);
    //    TMP_Text text = collision.gameObject.GetComponent<TMP_Text>();
    //    Debug.Log("no");
    //    text.color = color;
    //}

}
