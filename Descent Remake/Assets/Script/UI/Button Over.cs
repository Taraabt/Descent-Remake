using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOver : MonoBehaviour
{
    [SerializeField]Sprite overImage;
    [SerializeField] Sprite outerImage;

    public void OnPointerEnter()
    {
        Image image=this.GetComponent<Image>();
        image.sprite = outerImage;
    }
    public void OnPointerExit()
    {
        Image image = this.GetComponent<Image>();
        image.sprite = overImage;
    }
}
