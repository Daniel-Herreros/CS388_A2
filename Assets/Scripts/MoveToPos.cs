using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPos : MonoBehaviour
{


    public GameObject camController;

    //move variables
    Vector3 movPosition;
    bool hasToMov;
    float moveTime = 0;
    

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
                movPosition = hit.point;
                hasToMov = true;
            }
            Debug.DrawRay(camController.transform.position, fwd, Color.green, Mathf.Infinity);
        }
        else
        {
            Debug.DrawRay(camController.transform.position, fwd, Color.red, Mathf.Infinity);
        }
    }

    void MoveToPosition(){
        moveTime += Time.deltaTime;
        Vector3.Lerp(camController.transform.position, movPosition, moveTime);

        if (moveTime >= 1.0f){
            hasToMov = false;
        }
    }

}
