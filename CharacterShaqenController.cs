using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShaqenController : MonoBehaviour
{
    // UE_EMRE YOUTUBE UNITY CAMERA SHAQE VIDEO !!! //
    [Header("Target Object Transforms")]
    public Camera targetCamera;
    private Vector3 beginPosition;
    private Quaternion beginRotation;


    [Space]

    [Header("Shaqe Time Settings")]
    [Range(.002f, .07f)]
    public float TimerShaqe = .02f;
    [Range(.5f, 5)]
    public float durationShaqe = .5f;
    [Range(.01f, .05f)]
    public float speedTimeShaqe = .01f;
    private float beginTimeShaqe;
    private float increaseShaqe;
  

    [Space]

    [Header("Shaqe Values Settings")]
    [Range(0f, 5)]
    public float shaqeX = .5f;
    [Range(0f, 5)]
    public float shaqeY = .5f;
    [Range(0f, 5)]
    public float shaqeZ = .5f;
    [Range(1, .001f)]
    public float minSmoothShaqe = .2f;


    [Space]

    [Header("Choose")]

    public bool isPositionShaqe = true;
    public bool isRotationShaqe = false;


    //Shaqe Event in
    private float sX;
    private float sY;
    private float sZ;
    private Vector3 carpeShaqeXYZ;
    private float lerpSmoothValue;
    private Vector3 inShaqenChanceTotal;



    private bool shaqenBegin = false;
    private Vector3 SaveShaqenXYZ;



    [Space]

    [Header("WalkAnim")]
    public bool branchWalking = false;
    public bool isWalkMode = false;
    [Range(.2f, 7.05f)]
    public float maxRotWalk = .6f;
    [Range(.001f, .095f)]
    public float maxWalkSpeed = .01f;
    [Range(.02f, .35f)]
    public float maxHeighShaqe = .01f;
    private bool walkSide = true;
    private float walkTaking = 0;
    public AudioSource stepSoundSource;
    public AudioClip stepSoundClip;



    private Vector3 CameraOriginPosition;
    




    void Start()
    {
        if (!stepSoundSource)
        {
            stepSoundSource = gameObject.AddComponent<AudioSource>();
        }

        increaseShaqe = speedTimeShaqe;
        beginTimeShaqe = speedTimeShaqe;

        SaveShaqenXYZ = new Vector3(shaqeX, shaqeY, shaqeZ);

        if (!targetCamera)
        {
            if (Camera.main) {
                targetCamera = Camera.main;
                    }
            else
            {
                targetCamera = gameObject.GetComponentInChildren<Camera>();
            }
        }
        beginPosition = targetCamera.transform.localPosition;
        beginRotation = targetCamera.transform.localRotation;
        CameraOriginPosition = targetCamera.transform.localPosition;


    }


    void BeginShaqenEvent()
    {
        targetCamera.transform.localPosition = beginPosition;
        targetCamera.transform.localRotation = beginRotation;

        speedTimeShaqe = beginTimeShaqe;

        SaveShaqenXYZ = new Vector3(shaqeX, shaqeY, shaqeZ);

        shaqenBegin = true;
        if (isWalkMode)
        {
          isPositionShaqe = false;
          isRotationShaqe = false;
            TimerShaqe = Time.fixedDeltaTime;
        }

        StartCoroutine(Shaqen());


    }
    void CancelShaqen()
    {
        shaqenBegin = false;
        targetCamera.transform.localPosition = beginPosition;
        targetCamera.transform.localRotation = beginRotation;

        speedTimeShaqe = beginTimeShaqe;

        SaveShaqenXYZ = new Vector3(shaqeX, shaqeY, shaqeZ);
        StopCoroutine(Shaqen());
        

    }



    IEnumerator Shaqen()
    {
        while (shaqenBegin || isWalkMode) 
        {

        yield return new WaitForSeconds(TimerShaqe);
            if(speedTimeShaqe < durationShaqe && !isWalkMode)
            {
                //sY = CameraOriginPosition.y + sY;
                sX = Random.Range(-SaveShaqenXYZ.x, SaveShaqenXYZ.x);
                sY = Random.Range(-SaveShaqenXYZ.y, SaveShaqenXYZ.y);
                sZ = Random.Range(-SaveShaqenXYZ.z, SaveShaqenXYZ.z);
                inShaqenChanceTotal = new Vector3(sX, sY, sZ);
                lerpSmoothValue = Random.Range(minSmoothShaqe, 1);
                speedTimeShaqe += increaseShaqe;
                
                carpeShaqeXYZ = targetCamera.transform.localPosition;
                if (isPositionShaqe) 
                { 
                targetCamera.transform.localPosition = Vector3.Lerp(carpeShaqeXYZ, inShaqenChanceTotal, lerpSmoothValue);
                }
                if (isRotationShaqe)
                {
                    targetCamera.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(carpeShaqeXYZ), Quaternion.Euler(inShaqenChanceTotal), lerpSmoothValue);
                }
                
            }
            else if(!isWalkMode)
            {
                CancelShaqen();
            }




            if (isWalkMode && branchWalking)
            {
                targetCamera.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, maxRotWalk * -1), Quaternion.Euler(0, 0, maxRotWalk), walkTaking);
                targetCamera.transform.localPosition = Vector3.Lerp(new Vector3(0, CameraOriginPosition.y, 0), new Vector3(0, CameraOriginPosition.y-maxHeighShaqe, 0), walkTaking);
                if (walkSide)
                {
                    walkTaking += maxWalkSpeed;
                    if (walkTaking > 1)
                    {
                        walkSide = false;
                        if (stepSoundSource && stepSoundClip)
                        {
                            stepSoundSource.PlayOneShot(stepSoundClip);
                        }
                        
                    }
                }
                if (!walkSide)
                {
                    walkTaking -= maxWalkSpeed;
                    if (walkTaking < 0)
                    {
                        walkSide = true;
                        if (stepSoundSource && stepSoundClip)
                        {
                            stepSoundSource.PlayOneShot(stepSoundClip);
                        }

                    }
                }
                
                print("walk : " + maxRotWalk);
            }




        }
    }


    private void Update()
    {

        //TEST SHAQE CONTROL
        if(Input.GetKeyDown(KeyCode.X))
        {
            if (!shaqenBegin)
            {
                BeginShaqenEvent();
            }
        }
        
    }

   




}
