using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyYourself : MonoBehaviour
{
    Vector3 startPos;
    public float distanceTillDeath;
    public bool DieOfDistance;
    

    private void Start()
    {
        if (DieOfDistance) 
            return;

        startPos = transform.position;
        StartCoroutine(DieLater());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<PlayerMovement>() ) 
            return;

        Destroy(gameObject);
    }

    IEnumerator DieLater()
    {
        yield return new WaitUntil(() => Vector3.Distance(startPos, transform.position) > 10f);
        Destroy(gameObject);
    }
}
