using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CreditsScene : MonoBehaviour
{
    [SerializeField] VideoPlayer clip;
    bool skip, back;
    AsyncOperation preload;


    private void Start()
    {
        preload = SceneManager.LoadSceneAsync(2);
        clip.loopPointReached += FinishClip;
        preload.allowSceneActivation = false;
    }
    private void Update()
    {
        skip = Input.GetKeyDown(KeyCode.RightArrow);
        back = Input.GetKeyDown(KeyCode.LeftArrow);

        if (skip)
        {
            clip.frame += 10;
        }
        else if (back)
        {
            clip.frame -= 10;
        }


    }

    public void FinishClip(VideoPlayer vp)
    {
        preload.allowSceneActivation = true;
    }

    public void BackToMenuScene()
    {
        SceneManager.LoadScene(0);
    }



}
