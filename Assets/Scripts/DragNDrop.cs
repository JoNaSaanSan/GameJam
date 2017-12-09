using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour {
    Vector3 dist;
    float posX;
    float posY;
    Vector3 offset;

    void OnMouseDown()
    {
       
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist.z));
    }
   

    void OnMouseDrag()
    {
        Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist.z);
        Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
        Debug.Log(curPos);
        Debug.Log(worldPos);
        transform.position = worldPos;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
