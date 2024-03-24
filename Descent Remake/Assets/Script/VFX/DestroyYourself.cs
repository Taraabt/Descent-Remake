using System.Collections;
using UnityEngine;

public class DestroyYourself : MonoBehaviour
{
    Gun gun;
    Vector3 startPos;
    
    void Awake()
    {
        gun = transform.root.GetComponent<PlayerGuns>().gun1;
        StartCoroutine(GameEndMyself());
    }

    IEnumerator GameEndMyself()
    {
        startPos = transform.position;
        yield return new WaitUntil(() => Vector3.Distance(transform.position, startPos) >= gun.MaxHitScanLenght);
        Destroy(gameObject,1);
    }
}
