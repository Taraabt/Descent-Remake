using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyYourself : MonoBehaviour
{
    [SerializeField] float Distance = 0.3f;
    Gun pGun;
    Vector3 startPos;
    ProjectileMove projectileMove;

    void Awake()
    {
        projectileMove = GetComponent<ProjectileMove>();
        pGun = transform.root.GetComponent<PlayerGuns>().gun1;
        startPos = transform.position;
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, startPos) >= Distance)
        {
            transform.parent = null;
        }
        else if (Vector3.Distance(transform.position, startPos) >= pGun.MaxHitScanLenght)
        {
            projectileMove.speed = 0;
            Destroy(gameObject, 1);
        }
    }
}
