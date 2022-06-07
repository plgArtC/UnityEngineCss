using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TopUp Control in Mouse System

//Object sway and chose obj set scale
public class TopUp : MonoBehaviour
{
    private bool invokeLimit = true;
    private RaycastHit2D hit;


    public Vector3 minScale = new Vector3(.2f, .2f, .2f);
    public Vector3 maxScale = new Vector3(1f, 1f, 1f);
    public Vector3 defaultScale = new Vector3(1f, 1f, 1f);
    public float wait = .2f;
    public float incrase = .2f;


    float lerper = .2f;
    bool lerperControl = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit)
            {
                if (invokeLimit && hit.transform.gameObject.CompareTag("Tus"))
                {


                    invokeLimit = false;
                    StartCoroutine(ScaleLerper(hit.transform));
                }
                else if (!invokeLimit && !hit.transform.gameObject.CompareTag("Tus"))
                {
                    invokeLimit = true;
                }
                else { }


            }
        }
        else if (!Input.GetKeyDown(KeyCode.Mouse0) && !invokeLimit)
        {
            if (hit) 
            { 
                invokeLimit = true;
                hit.transform.localScale = defaultScale;
                StopCoroutine(ScaleLerper(hit.transform));
            }
        }

    }


   

    


    IEnumerator ScaleLerper(Transform sObj)
    {
        while (!invokeLimit) { 
            
        if (sObj) {
        if (lerper > 1)
        {
            lerperControl = false;
        }
        if(lerper < 0.2)
        {
            lerperControl = true;
        }
        if (lerperControl)
        {
                    lerper += incrase;
        }
        if (!lerperControl)
        {
                    lerper -= incrase;
                }

            sObj.localScale = Vector3.Lerp(minScale, maxScale, lerper);
            sObj.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                yield return new WaitForSeconds(wait);
            }

         }
    }

}
