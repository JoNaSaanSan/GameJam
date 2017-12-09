using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour {

    public GameObject[] Characters = new GameObject[4];
    public GameObject[] Hats = new GameObject[4];
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
        Characters[current % Characters.Length].SetActive(true);
        Debug.Log(current % Characters.Length);
    }

    public void Back()
    {
        Debug.Log((current % Characters.Length + Characters.Length) % Characters.Length);
        Characters[(current % Characters.Length + Characters.Length) % Characters.Length].SetActive(false);
        current--;
        Characters[(current % Characters.Length + Characters.Length) % Characters.Length].SetActive(true);
        
    }

    public void AddHat(int i)
    {
        if(Characters[current].transform.GetChildCount() == 2)
        Destroy(Characters[current].transform.GetChild(1).gameObject);
        GameObject hat = Instantiate(Hats[i], Characters[current].transform.GetChild(0).transform.position, Characters[current].transform.rotation);
        hat.transform.parent = Characters[current].transform;
    }

}
