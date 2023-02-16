using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    Vector3[] mPosiblePositions = new[] {   new Vector3( 8f, 1.0f,  8f), 
                                            new Vector3(-6f, 1.0f,  4f),
                                            new Vector3(-8f, 1.0f,  0f),
                                            new Vector3( 2f, 1.0f, -2f),
                                            new Vector3(-6f, 1.0f, -6f),
                                            new Vector3( 6f, 1.0f, -8f)};

    public GameObject mDoor;


    // Start is called before the first frame update
    void Start()
    {
        Random.Range(0, 5);
        transform.position = mPosiblePositions[Random.Range(0, 5)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.Destroy(this);
            GameObject.Destroy(mDoor);
        }
    }


}
