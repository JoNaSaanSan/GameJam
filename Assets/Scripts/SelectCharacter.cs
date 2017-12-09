using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour {

    public GameObject[] Characters = new GameObject[4];
    Rigidbody rigidbody;
    private int current;

    // Use this for initialization
    void Start () {
        for(int i = 0; i<Characters.Length; i++)
        {
        rigidbody = Characters[i].GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
            Characters[i].SetActive(false);
        }
        current = 0;
        Characters[current].SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void Next()
    {
        Characters[current % Characters.Length].SetActive(false);
        current++;
        Characters[current% Characters.Length].SetActive(true);
        Debug.Log(current % Characters.Length);
    }

    public void Back()
    {
        Characters[current % Characters.Length].SetActive(false);
        current--;
        Characters[current % Characters.Length].SetActive(true);
        Debug.Log(current % Characters.Length);
    }
}
