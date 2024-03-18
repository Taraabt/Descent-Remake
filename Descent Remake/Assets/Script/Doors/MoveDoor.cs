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
    float duration = 0;
    float lerpDur = 0;
    [SerializeField] PlayerMovement player;
    Transform parent;


    private void OnEnable()
    {
        if (door.needsKey)
        {
            door.OnDoorOpen += MoveWithKey;
        }
        else
        {
            door.OnDoorOpen += Move;
        }
        door.OnCloseDoor += Close;
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
        

        if (canOpen && !isOpen)
        {
            parent.gameObject.layer = 17;
            if (time < lerpDur)
            {
                transform.localPosition = Vector3.Lerp(startPos, startPos + dirPos * 5, time / lerpDur);
                time += Time.deltaTime;
            }
            else
            {
                isOpen = true;
                canOpen = false;
                duration = door.duration;
            }

        }
        else if (isOpen == true && duration > 0)
        {
            time = 0;
            duration -= Time.deltaTime;

        }
        else if (isOpen == true && duration <= 0 && canOpen == false)
        {
            parent.gameObject.layer = 0;
            if (time < lerpDur)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, time / lerpDur);
                time += Time.deltaTime;
            }
            else
            {
                time = 0;
                canOpen = false;
                isOpen = false;
            }
        }
    }
    private void Start()
    {
        player.GetComponent<PlayerMovement>();
        lerpDur = door.lerpDuration;
        isOpen = false;
        time = 0;
        startPos = transform.localPosition;
        parent = transform.parent;
    }


    void Close()
    {
        canOpen = false;
        isOpen = true;
    }

    public void Move()
    {
        canOpen = true;
        isOpen = false;
        //StartCoroutine(OpenDoor());
    }
    public void MoveWithKey()
    {
        if (player.hasKey == true)
        {
            canOpen = true;
            isOpen = false;
        }
    }


}


//IEnumerator OpenDoor()
//{
//    Debug.Log("opendoor");
//    reloadTimer = 0;
//    if (!isOpen)
//    {
//        isOpen = true;
//        while (reloadTimer < lerpDur)
//        {
//            transform.localPosition = Vector3.Lerp(startPos, startPos + dirPos * 5, reloadTimer /lerpDur );
//            reloadTimer += Time.deltaTime;
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
//    reloadTimer=0;
//    if (isOpen)
//    {
//        while (reloadTimer < lerpDur)
//        {
//            transform.localPosition = Vector3.Lerp(startPos + dirPos * 5, startPos, reloadTimer / lerpDur);
//            reloadTimer += Time.deltaTime;
//            yield return null;
//        }
//        isOpen = false;
//    }
//    else { 
//        yield return null;
//        StopCoroutine(CloseDoor());
//    }

//}