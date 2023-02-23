using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPos : MonoBehaviour
{


    public GameObject camController;

    //move variables
    Vector3 movPosition;
    Vector3 camPosition;
    bool hasToMov = false;
    GameObject hitObject;
    float speed = 0.1f;

    int colliderId;

    GameObject prevHitObject;


    // Start is called before the first frame update
    void Start(){
    }

    // Update is called once per frame
    void Update(){

        if (!hasToMov){
            SetDirection();
        } else {
            MoveToPosition();
        }

    }

    void SetDirection(){
        Vector3 fwd = camController.transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(camController.transform.position, fwd, out hit))
        {
            if (hit.collider.tag == "MovePoint")
            {
                bool finish = false;
                if (hit.colliderInstanceID == colliderId)
                {
                    Renderer rend = hit.collider.gameObject.GetComponent<Renderer>();
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, rend.material.color.a - 0.01f);
                    if(rend.material.color.a <= 0.0f)
                    {
                        finish = true;
                    }
                    prevHitObject = hit.collider.gameObject;
                }
                else
                {
                    if (prevHitObject){
                        Renderer rend = prevHitObject.GetComponent<Renderer>();
                        prevHitObject.GetComponent<Renderer>().material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, 1.0f);
                    }
                }

                colliderId = hit.colliderInstanceID;
                if (finish){
                    if (hitObject){
                        hitObject.SetActive(true);
                        Renderer rend = hitObject.GetComponent<Renderer>();
                        hitObject.GetComponent<Renderer>().material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, 1.0f);
                    }
                    hitObject = hit.collider.gameObject;
                    movPosition = hitObject.transform.position;
                    hasToMov = true;
                    hitObject.SetActive(false);
                    camPosition = camController.transform.position;
                }
                Debug.DrawRay(camController.transform.position, fwd, Color.green, Mathf.Infinity);
            }
            else
            {
                Debug.DrawRay(camController.transform.position, fwd, Color.red, Mathf.Infinity);
                if (prevHitObject){
                    Renderer rend = prevHitObject.GetComponent<Renderer>();
                    prevHitObject.GetComponent<Collider>().gameObject.GetComponent<Renderer>().material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, 1.0f);
                }
            }
        }
    }

    void MoveToPosition(){


        Vector3 dir = movPosition - camPosition;
        dir.Normalize();
        camController.transform.position += dir*speed;

        float distance = movPosition.magnitude - camController.transform.position.magnitude;

        
        if (distance <= 0.1f && distance >= -0.1f){
            hasToMov = false;
        }

    }

}
