using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHp : MonoBehaviour, IHp
{
    [SerializeField] Rigidbody rb;

    public float HP { get; set; }

    [SerializeField] float mHp;

    public delegate void Dead();
    static public event Dead OnDeath;


    private void Awake()
    {
        HP = mHp;
    }

    public void HpUp(float heal)
    {
        HP += heal;
    }
    public void TakeDmg(float dmg)
    {
        HP -= dmg;
        Debug.Log(HP);
        if (HP <= 0)
        {
            HP = 0;
            if (OnDeath == null)
                return;
            OnDeath.Invoke();
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(4);
            rb.velocity = Vector3.zero;
        }
    }
}