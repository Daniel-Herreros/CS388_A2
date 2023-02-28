using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckParentTransparency : MonoBehaviour
{


    public GameObject mParent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        Color c = this.GetComponent<RawImage>().color;
        c.a = mParent.GetComponent<Image>().color.a;
        this.GetComponent<RawImage>().color = c;
    }
}
