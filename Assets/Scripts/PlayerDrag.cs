using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrag : MonoBehaviour {
    Vector3 dist;
    float posX;
    float posY;
    float yRotation;
    float zRotation;
    Quaternion orgRot;
    bool rotate;

    private void Start()
    {
        orgRot = transform.rotation;
    }

    void OnMouseDown()
    {
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
        rotate = true;
    }

    void OnMouseDrag()
    {
        Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
        yRotation += worldPos.x;
        zRotation += worldPos.y;
        transform.eulerAngles = new Vector3(0, -yRotation, -zRotation);
        Debug.Log(transform.eulerAngles);
        rotate = true;
    }

    private void Update()
    {
        Debug.Log(Input.GetMouseButton(0));

        if(Input.GetMouseButton(1) != false)
        transform.rotation = Quaternion.Slerp(transform.rotation, orgRot, 500 * Time.deltaTime);
    }
}
