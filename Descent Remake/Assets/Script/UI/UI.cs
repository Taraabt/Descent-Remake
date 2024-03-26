using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] int stringLenght;
    string scoreString;
    TMP_Text[] texts;
    Image[] images;
    [SerializeField] float minX;
    [SerializeField] float minY;
    public delegate void ScoreUpdate();
    public static ScoreUpdate OnScoreUpdate;
    private void Start()
    {
        OnScoreUpdate += UpdateScore;

        texts = GetComponentsInChildren<TMP_Text>();
        images = GetComponentsInChildren<Image>();
        UpdateScore();

#if UNITY_EDITOR
        foreach (var image in images)
        {
            Debug.Log(image.name);
        }
        foreach (var txt in texts)
        {
            Debug.LogWarning(txt.text);
        }
#endif

    }

    private void UpdateScore()
    {
        scoreString = ScoreManager.score.ToString();
        Char zero = '0';
        string text = scoreString.PadLeft(stringLenght,zero);
        //Debug.Log(text);

        //if (scoreString.Length > stringLenght)
        //{
        //    texts[texts.Length - 1].horizontalAlignment = HorizontalAlignmentOptions.Center;
        //    texts[texts.Length - 1].verticalAlignment = VerticalAlignmentOptions.Middle;
        //    text = ":3";
        //}

        texts[texts.Length - 1].text = text;

    }

    private void Update()
    {
        PlayerGuns pGuns = player.GetComponent<PlayerGuns>();

        if (pGuns.mag1 == null || pGuns.gun1 == null)
            return;

        //Debug.Log(texts[2].text);
        int ammo1 = (int)Mathf.Round(pGuns.mag1.ammo.ammo);
        //Debug.Log("cazzo palle");
        images[1].sprite = pGuns.gun1.gunSprite;
        images[7].fillAmount = Mathf.Lerp(1f, 0f, ammo1 / pGuns.mag1.ammo.baseAmmo);
        texts[0].text = ammo1.ToString();


        int ammo2 = (int)Mathf.Round(pGuns.mag2.ammo.ammo);
        texts[1].text = ammo2.ToString();


        PlayerHp playerHp = player.GetComponent<PlayerHp>();
        float hp = Mathf.Round(playerHp.HP);
        //Debug.Log(images[3].name);
        float x = Mathf.Lerp(minX, 1f, hp / 100);
        float y = Mathf.Lerp(minY, 1f, hp / 100);
        images[3].rectTransform.localScale = new Vector3(x, y, 1f);
        texts[2].text = hp.ToString();
    }

}
