using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private float distanceAway;
    [SerializeField]
    private float distanceUp;
    [SerializeField]
    private float smooth;
    [SerializeField]
    private Transform followedObject;
    private Vector3 toPosition;

	
	
	// Update is called once per frame
	void LateUpdate () {

        //berechnen Position der Camera
        //followedObject.position = position vom Player
        //+ Distance von Camera zum Player
        toPosition = followedObject.position + (Vector3.up * distanceUp - Vector3.forward * distanceAway);

        // Vector3.Lerp(start pos, end pos, smooth);
        transform.position = Vector3.Lerp(transform.position, toPosition , Time.deltaTime * smooth);

        //Schaurichtung 
        transform.LookAt(followedObject);
	}
}
