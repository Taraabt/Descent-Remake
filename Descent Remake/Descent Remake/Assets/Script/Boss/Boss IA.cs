using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIA : MonoBehaviour, IHp
{
    public float HP { get; set; }
    [SerializeField] float bossHp;
    Animator myAnimator;
    [SerializeField]ParticleSystem esplosion;
    [SerializeField] float bossView;
    [SerializeField] BossBullet bossBullet;
    [SerializeField] GameObject player;
    float remainingtime = 0;
    [SerializeField]float shootingTimer = 2;
    private void Awake()
    {
        HP= bossHp;
        myAnimator = this.GetComponentInChildren<Animator>();
    }
    public void HpUp(float heal)
    {
        
    }
    private void Start()
    {
        remainingtime = shootingTimer;
    }

    public void TakeDmg(float dmg)
    {
        HP -= dmg;
        Debug.Log("bosshp " + HP);
        if (HP <= 0)
        {
            myAnimator.enabled = false;
            Instantiate(esplosion,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position+transform.forward*170 + transform.up * -150, bossView);
    }

    private void Update()
    {
        remainingtime -= Time.deltaTime;
        Collider[] collider =Physics.OverlapSphere(transform.position + transform.forward * 170+transform.up*-150, bossView, 1 << 16);
        for (int i = 0; i < collider.Length; i++) {
            if (collider[i] != null)
            {
                //transform.LookAt(player.transform.position);
                if (remainingtime <= 0)
                {
                    remainingtime = shootingTimer;
                    Instantiate(bossBullet, transform.position + transform.forward * 10, transform.rotation);
                }
            }
        }
    }

}
