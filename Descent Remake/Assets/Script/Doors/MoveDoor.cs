using UnityEngine;

public class MoveDoor : MonoBehaviour
{


    [Tooltip("this is the direction to move this pice of the door (remeber +1 x = right +1 y = up +1 z = forward)")]
    [SerializeField] Vector3 dirPos;

    [Tooltip("this is the distance from the start position that this pice of the door has to reach (default: 5)")]
    [SerializeField] float distanceFromStart = 5;


    Door door;
    Vector3 startPos;
    Vector3 endPos;
    bool canOpen, isOpen;
    float time = 0;
    float duration = 0;
    float lerpDur = 0;
    PlayerMovement player;
    Transform parent;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        parent = transform.parent;
        door = parent.GetComponent<Door>();
    }

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
        door.OnStayDoor += Stay;
    }

    private void Start()
    {
        lerpDur = door.lerpDuration;
        isOpen = false;
        time = 0;
        startPos = transform.localPosition;
        endPos = startPos + dirPos * distanceFromStart;

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
            if (time <= lerpDur)
            {
                transform.localPosition = Vector3.Lerp(startPos, endPos, time / lerpDur);
                time += Time.deltaTime;
            }
            else
            {
                isOpen = true;
                //canOpen = false;
                time = lerpDur;
                transform.localPosition = endPos;
                duration = door.duration;
            }

        }
        else if ((isOpen == true || canOpen == false) && duration > 0)
        {
            if (time <= lerpDur)
            {
                transform.localPosition = Vector3.Lerp(startPos, endPos, time / lerpDur);
                time += Time.deltaTime;
            }
            else
            {
                time = lerpDur;
                transform.localPosition = endPos;
                isOpen = true;
            }
          
            
            duration -= Time.deltaTime;

        }
        else if (isOpen == true && duration <= 0 && canOpen == false)
        {
            
            parent.gameObject.layer = 0;
            if (time >= 0)
            {
                transform.localPosition = Vector3.Lerp(startPos, endPos, time / lerpDur);
                time -= Time.deltaTime;
            }
            else
            {
                time = 0;
                time = lerpDur;
                transform.localPosition = startPos;
                //canOpen = false;
                isOpen = false;
            }
        }
    }



    void Close()
    {
        canOpen = false;
        isOpen = true;
        //time = lerpDur;
    }

    private void Stay()
    {
        canOpen = true;
        duration = door.duration;
    }

    public void Move()
    {
        duration = door.duration;
        //time = 0;
        canOpen = true;
        isOpen = false;
        //StartCoroutine(OpenDoor());
    }
    public void MoveWithKey()
    {
        if (player.hasKey == true)
        {
            duration = door.duration;
            //time = 0;
            canOpen = true;
            isOpen = false;
        }
    }

}


//IEnumerator OpenDoor()
//{
//     ("opendoor");
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
//     ("closedoor");
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