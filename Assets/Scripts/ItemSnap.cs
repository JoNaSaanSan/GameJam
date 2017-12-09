using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSnap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //This is the moving wall
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "12")
        this.transform.position = other.transform.position;
    }
}
