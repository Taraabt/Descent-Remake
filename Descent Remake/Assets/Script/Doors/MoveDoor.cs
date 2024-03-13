using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class MoveDoor : MonoBehaviour
{

    [SerializeField] Door door;
    Vector3 startPos;
    [SerializeField] Vector3 dirPos;
    bool canOpen, isOpen;
    float time = 0;
    float duration =0;
    float lerpDur=0;
    [SerializeField]PlayerMovement player;


    private void OnEnable()
    {
        if(door.needsKey) {
            door.OnDoorOpen += MoveWithKey;
        }
        else
        {
            door.OnDoorOpen += Move;
        }
    }

    private void OnDisable()
    {
        if (door.needsKey)
        {
            door.OnDoorOpen -= MoveWithKey;
        }
        else
        {
            door.OnDoorOpen -= Move;
        }
    }

    private void Update()
    {
        Debug.Log(canOpen);
        if(canOpen&&!isOpen){
            if (time < lerpDur){
                transform.localPosition = Vector3.Lerp(startPos, startPos + dirPos * 5, time / lerpDur);
                time += Time.deltaTime;
            }else{
                isOpen = true;
                duration = door.duration;
            }
        }else if(isOpen==true&&duration>0){
            time = 0;
            duration -= Time.deltaTime;
        }else if (isOpen == true&&duration<=0)
        {
            if (time < lerpDur)
            {
                transform.localPosition = Vector3.Lerp(startPos + dirPos * 5, startPos, time / lerpDur);
                time += Time.deltaTime;
            }
            else
            {
                time=0;
                canOpen = false;
                isOpen = false;
            }
        }

    }
    private void Start()
    {
        player.GetComponent<PlayerMovement>();
        lerpDur=door.lerpDuration;
        isOpen = false;
        time=0;
        startPos=transform.localPosition;
    }
    //IEnumerator OpenDoor()
    //{
    //    Debug.Log("opendoor");
    //    time = 0;
    //    if (!isOpen)
    //    {
    //        isOpen = true;
    //        while (time < lerpDur)
    //        {
    //            transform.localPosition = Vector3.Lerp(startPos, startPos + dirPos * 5, time /lerpDur );
    //            time += Time.deltaTime;
    //            yield return null;
    //        }
    //        new WaitForSeconds(duration);
    //        StartCoroutine(CloseDoor());
    //    }
    //    else
    //    {
    //        yield return null;
    //        StopCoroutine(OpenDoor());
    //    }
    //}
    //IEnumerator CloseDoor()
    //{
    //    Debug.Log("closedoor");
    //    time=0;
    //    if (isOpen)
    //    {
    //        while (time < lerpDur)
    //        {
    //            transform.localPosition = Vector3.Lerp(startPos + dirPos * 5, startPos, time / lerpDur);
    //            time += Time.deltaTime;
    //            yield return null;
    //        }
    //        isOpen = false;
    //    }
    //    else { 
    //        yield return null;
    //        StopCoroutine(CloseDoor());
    //    }

    //}

    public void Move()
    {
            canOpen = true;
        //StartCoroutine(OpenDoor());
    }
    public void MoveWithKey()
    {
        if (player.hasKey == true)
        {
            canOpen = true;
        }
    }


}
