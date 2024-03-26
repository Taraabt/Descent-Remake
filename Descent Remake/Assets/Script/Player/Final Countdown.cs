using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalCountdown : MonoBehaviour
{
    public delegate void timer();
    public static timer BossDeath;
    public static float time;
    public static float actualMaxTime;
    [SerializeField] public float maxTime;

    private void Start()
    {
        actualMaxTime = maxTime;
    }
    private void OnEnable()
    {
        BossDeath += CallTimer;
    }
    private void OnDisable()
    {
        BossDeath -= CallTimer;
    }
    public IEnumerator Timer()
    {
        while (time<actualMaxTime) {
            time += Time.deltaTime;
            yield return null;
        }
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(4);
    }
    public void CallTimer()
    {
        StartCoroutine(Timer());
    }
}
