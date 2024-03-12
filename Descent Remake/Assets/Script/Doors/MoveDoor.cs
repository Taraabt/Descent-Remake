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
    bool isOpen;
    float time = 0;
    float duration =0;
    float lerpDur=0;


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
        Debug.Log(isOpen);
        if(isOpen) {
            if (time < lerpDur){
                transform.localPosition = Vector3.Lerp(startPos, startPos + dirPos * 5, time / lerpDur);
                time += Time.deltaTime;
            }else{
                isOpen = false;
                duration = door.duration;
            }
        }else if(isOpen==false){
            if (duration>0){
                time = 0;
                lerpDur = door.lerpDuration;
                duration -= Time.deltaTime;
            }
            else if(time < lerpDur)
            {
                transform.localPosition = Vector3.Lerp(startPos + dirPos * 5, startPos , time / lerpDur);
                time += Time.deltaTime;
            }
        }

    }
    private void Start()
    {
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
        if (!isOpen)
        {
            isOpen = true;
        }
        //StartCoroutine(OpenDoor());
    }
    public void MoveWithKey()
    {
        //if playerhaskey
        //StartCoroutine(MoveObj());
    }


}
