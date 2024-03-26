using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreManager
{
    public static float score = 0f;

    static ScoreManager() {
        score = 0;
    }
    public static void AddScore(float scoretoadd)
    {
        score+= scoretoadd;
    }
}
