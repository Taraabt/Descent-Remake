using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAdder : MonoBehaviour
{
    [SerializeField] float scoreAmount;
    private void OnDestroy()
    {
        ScoreManager.score += scoreAmount;
    }
}
