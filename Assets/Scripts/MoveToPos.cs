using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPos : MonoBehaviour
{


    public GameObject camController;
    public GameObject mFader;
    public GameObject mFadeOut;
    public Animator mAnim;

    public GameObject mKey;
    public Key mKeyComp;

    public LayerMask IgnoreMe;

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
        Invoke("DeactivateFader", 1.15f);
    }

    // Update is called once per frame
    void Update(){

        if (!hasToMov){
            SetDirection();
        } else {
            MoveToPosition();
        }
        

        //When the player arrives at the exit
        float distance = Vector3.Distance(movPosition, new Vector3(10, 1.5f, -8));

        if (distance < 0.1)
        {
            mFadeOut.SetActive(true);
            Invoke("Play", 1.15f);
            mAnim.Play("FadeOut");
        }



        distance = Vector3.Distance(camController.transform.position, mKey.transform.position);
        Debug.Log(distance);
        if (distance < 0.1)
        {
            mKeyComp.PlayerCollided();
        }
    }

    void SetDirection(){
        Vector3 fwd = camController.transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(camController.transform.position, fwd, out hit, 100000.0f, ~IgnoreMe))
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

            DrawLine(camController.transform.position, hit.point, Color.blue, 0.1f);
        }
    }

    void MoveToPosition(){


        Vector3 dir = movPosition - camPosition;
        dir.Normalize();
        camController.transform.position += dir*speed;

        
        float distance = Vector3.Distance(movPosition, camController.transform.position);
        
        if (distance <= 0.1f && distance >= -0.1f){
            hasToMov = false;
        }

    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        //lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.SetPosition(0, start - new Vector3(0.0f, 1.0f, 0.0f));
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }

    void DeactivateFader()
    {
        mFader.SetActive(false);
    }

    void Play() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
