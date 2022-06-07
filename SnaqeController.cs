using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class SnaqeController : MonoBehaviour
{

    bool tailcontrol = false;
    Vector3 tailRec2;
    Vector3 tailRec3;
    public bool baitspawn = false;
    bool updateYes = true;

    public List<Transform> nailObj;
    Transform _tSelf;
    bool leftActive = true;
    bool BackActive = false;
    Vector3 _position = new Vector3(0,1,0);


    Vector3 recPosition;
    // Start is called before the first frame update

    GameObject firstObj;

    void ChangeSpeed()
    {

        CancelInvoke("move");
        InvokeRepeating("move", 0, .5F);
    }

    void Start()
    {
        _tSelf = gameObject.transform;
        //StartCoroutine(move());
       InvokeRepeating("move", 1, .1f);
    }
    public GameObject SpawnTail;
    bool isAlive = true;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeSpeed();
        }

    
        if (updateYes == true && isAlive)
        {

            if (Input.GetKeyDown(KeyCode.E) || baitspawn == true)
        {
                firstObj = Instantiate(SpawnTail);
                firstObj.transform.position= new Vector3(10, 6, 0);
                nailObj.Add(firstObj.transform);
                baitspawn = false;
        }


        if (Input.GetKeyDown(KeyCode.W) && BackActive == true)
        {
                updateYes = false;
                BackActive = false;
                _position = new Vector3(0, 1, 0);
            
            leftActive = true;
        }
        if (Input.GetKeyDown(KeyCode.S) && BackActive == true)
        {
                updateYes = false;
                BackActive = false;
                _position = new Vector3(0, -1, 0);
            
            leftActive = true;
        }
        if (Input.GetKeyDown(KeyCode.A) && leftActive == true)
        {
                updateYes = false;
                leftActive = false;

                _position = new Vector3(-1, 0, 0);
            BackActive = true;
            
        }
        if (Input.GetKeyDown(KeyCode.D) && leftActive == true)
            {
                updateYes = false;
                leftActive = false;

                _position = new Vector3(1, 0, 0);
            BackActive = true;
            
        }
           // leftActive = false;

        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            print("Stop");
            if(Time.timeScale == 1) 
            { 
                Time.timeScale = 0;
            }
            else if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }


        }

    }



    

    //HeadMove
    void move()
    {       
        
        
        if (isAlive) { 
        if(tailcontrol == false) { 
        recPosition = _tSelf.position;
        print("move : "+ _position.x+_position.y);
        _tSelf.Translate(_position);
        tailcontrol = true;
        Invoke("tail", 0.01f);
        }
        }
       
    }
    //kuyrukTakip
    void tail()
    {
        
        for (int i = 0; i<nailObj.Count; i++) 
        {
            
            if (tailcontrol == true) 
            {
                tailRec2 = nailObj[i].position;
                nailObj[i].position = recPosition;
                tailcontrol = false;
                //print("0 INDEX CALISTI");
            }
            else { }
            if(i > 0) 
            {
                tailRec3 = nailObj[i].position;
                nailObj[i].position = tailRec2;
                tailRec2 = tailRec3;
                //print("TA = " + tailRec2 + " and : " + tailRec3);
            }
            else { }
            if(i>=nailObj.Count-1) { updateYes = true; }
            else { }



        }

    }

//cosmatic

    void nailReflect()
    {
        for(int i = 0;i<nailObj.Count;i++)
        {
            
            nailObj[i].transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;  
        }

    }


//BasicRestart
    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //hit

    void OnTriggerEnter(Collider collision)
    {
        print("--------------------_______________________");
        if (collision != null && collision.CompareTag("nail"))

        {
            Invoke("restart", 2);
            nailReflect();
            isAlive = false;


        }
    }

    //hit Any Systems


    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
        print("--------------------_______________________");
        if (collision != null && collision.transform.gameObject.CompareTag("nail"))

        {
            Time.timeScale = 0;
        }
    }*/






    //GameObject hitObj;

    /* void LineTrace()
     {


         hitObj = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.position + (gameObject.transform.right*2)).transform.gameObject;

         if(hitObj != null)
         {
             if (hitObj.CompareTag("nail"))
             {
                 print("hit _ hit _ hit _ hit !");
             }
         }

     }
    */


}
