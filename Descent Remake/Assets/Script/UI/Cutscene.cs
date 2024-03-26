using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Cutscene : MonoBehaviour
{

    [SerializeField]VideoPlayer clip;
    [SerializeField] GameObject startGame;
    bool esc, skip, back,invio;
    AsyncOperation preload;


    private void Start()
    {
        ScoreManager.score = 0;
        preload = SceneManager.LoadSceneAsync(2);
        clip.loopPointReached += FinishClip;
        preload.allowSceneActivation = false;
    }
    private void Update()
    {
        invio = Input.GetKeyDown(KeyCode.Return);
        esc =Input.GetKeyDown(KeyCode.Escape);
        skip = Input.GetKeyDown(KeyCode.RightArrow);
        back = Input.GetKeyDown(KeyCode.LeftArrow);

        if (invio&&startGame.activeSelf){
            StartGame();
        }
        if (esc){
            if (startGame.activeSelf)
            {
                clip.Play();
                startGame.SetActive(false);
            }
            else
            {
                clip.Pause();
                startGame.SetActive(true);
            }
        }
        if (skip)
        {
            clip.frame += 60;
        }else if (back)
        {
            clip.frame -= 60;
        }


    }

    public void FinishClip(VideoPlayer vp)
    {
        preload.allowSceneActivation = true;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(2);

        //preload.allowSceneActivation = true;
    }
    public void UnPause()
    {
        clip.Play();
        startGame.SetActive(false);
    }
    public void BackToMenuScene()
    {
        SceneManager.LoadScene(0);
    }

}
