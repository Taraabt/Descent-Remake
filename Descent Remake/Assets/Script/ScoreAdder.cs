using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAdder : MonoBehaviour
{
    [SerializeField] float scoreAmount;
    [SerializeField] bool destroyThis;
    private void OnDestroy()
    {
        ScoreManager.score += scoreAmount;
        UI.OnScoreUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (destroyThis == false)
            return;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (destroyThis == false) 
            return;
        Destroy(gameObject);
    }
}
