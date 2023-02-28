using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookAt : MonoBehaviour
{


    public Animator mAnim;
    public GameObject mFader;

    public GameObject camController;

    //move variables
    Vector3 movPosition;
    Vector3 camPosition;
    GameObject hitObject;

    int colliderId;

    GameObject prevHitObject;

    bool fading = false;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeactivateFader", 1.15f);
    }

    // Update is called once per frame
    void Update(){
        if(!fading)
            SetDirection();

    }

    void SetDirection(){
        Vector3 fwd = camController.transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(camController.transform.position, fwd, out hit))
        {
            if (hit.collider.tag == "Button")
            {
                bool finish = false;
                if (hit.colliderInstanceID == colliderId)
                {
                    Color rend = hit.collider.gameObject.GetComponent<Button>().image.color;
                    hit.collider.gameObject.GetComponent<Button>().image.color = new Color(rend.r, rend.g-0.01f, rend.b - 0.01f, rend.a);
                    if(rend.g <= 0.0f)
                    {
                        finish = true;
                    }
                    prevHitObject = hit.collider.gameObject;
                }
                else
                {
                    if (prevHitObject){
                        prevHitObject.GetComponent<Button>().image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    }
                }

                colliderId = hit.colliderInstanceID;
                if (finish){
                    if (hitObject){
                        hitObject.GetComponent<Button>().image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    }
                    hitObject = hit.collider.gameObject;
                    mFader.SetActive(true);
                    Invoke("Play", 1.15f);
                    mAnim.Play("FadeOut");
                    fading = true;

                }
                Debug.DrawRay(camController.transform.position, fwd, Color.green, Mathf.Infinity);
            }
            else
            {
                Debug.DrawRay(camController.transform.position, fwd, Color.red, Mathf.Infinity);
                if (prevHitObject){
                    prevHitObject.GetComponent<Collider>().gameObject.GetComponent<Button>().image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
            }
        }
    }

    void Play(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    void DeactivateFader()
    {
        mFader.SetActive(false);
    }

}
