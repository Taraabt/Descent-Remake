using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource m_Source;

    public AudioClip music;

    public void Start()
    {
        m_Source.clip = music;
    }
}
