using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMove : MonoBehaviour {

    public float speed;
    public CharacterController controller;
    private Vector3 position;

    // public AnimationClip run;
    //  public AnimationClip idle;

    public Animation anim;

    public static bool attack;
    public static bool die;

    public static Vector3 cursorPosition;


	// Use this for initialization
	void Start () {
        position = transform.position;
       
	}
	
	// Update is called once per frame
	void Update () {

        locateCursorPosition();

        if ( !attack && !die)
        {
            if (Input.GetMouseButtonDown(0))
            {
                locatePosition();
            }

            moveToPosition();
        } else
        {

        }

		
	}

    void locatePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {

            

            if (hit.collider.tag != "Player" && hit.collider.tag != "Enemy")
            {
                position = hit.point;
                //position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                //get position of user click
            }


        }

      
    }

    void locateCursorPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            cursorPosition = hit.point; 
        }


    }

    void moveToPosition()
    {
      
        //game obj is moving
        if (Vector3.Distance(transform.position, position) > 1) {

                 Quaternion newRotation = Quaternion.LookRotation(position - transform.position);

                 newRotation.x = 0f;
                 newRotation.z = 0f;
                    // lock rotation

                  transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);
                    // smoothly transform rotation to new one

                  controller.SimpleMove(transform.forward * speed);
           // anim.Play("run");
            anim.CrossFade("run");
            
                  
        } // gameObj is not moving
        else {


            // anim.Play("idle");
            anim.CrossFade("idle");
        }

        
    }
       
}
