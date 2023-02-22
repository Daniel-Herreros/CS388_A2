using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPos : MonoBehaviour
{


    public GameObject camController;

    //move variables
    Vector3 movPosition;
    Vector3 camPosition;
    bool hasToMov;
    GameObject hitObject;
    float speed = 0.1f;


    // Start is called before the first frame update
    void Start(){
        Debug.Log(speed);
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
                if(hitObject)
                    hitObject.SetActive(true);
                movPosition = hit.point;
                hasToMov = true;
                hitObject = hit.collider.gameObject;
                hitObject.SetActive(false);
                camPosition = camController.transform.position;
            }
            Debug.DrawRay(camController.transform.position, fwd, Color.green, Mathf.Infinity);
        }
        else
        {
            Debug.DrawRay(camController.transform.position, fwd, Color.red, Mathf.Infinity);
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
