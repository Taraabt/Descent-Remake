using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostage : MonoBehaviour, ICollectable
{
    [SerializeField] float score;

    private void OnTriggerEnter(Collider other)
    {
        Collect();

    }
    public void Collect()
    {
        ScoreManager.score += score;
        Destroy(gameObject);
    }
}
