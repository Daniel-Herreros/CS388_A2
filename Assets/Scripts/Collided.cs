using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collided : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision) {
        Debug.Log("DOOR01");
        if (collision.gameObject.CompareTag("Player")) {
            Debug.Log("YEEES");
        }
    }
}
