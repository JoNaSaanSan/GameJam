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
        }
        current = 0;
	}
	
	// Update is called once per frame
	void Update () {

        Characters[0].SetActive(false);
        Characters[1].SetActive(false);

    }

    public void Next()
    {
        Characters[current % Characters.Length - 1].SetActive(false);
        Characters[(current+1)% Characters.Length - 1].SetActive(true);
        Debug.Log(current % Characters.Length);
    }

    public void Back()
    {
        Characters[current % Characters.Length - 1].SetActive(false);
        Characters[(current % Characters.Length - 1) -1 ].SetActive(true);
        Debug.Log(current % Characters.Length);
    }
}
