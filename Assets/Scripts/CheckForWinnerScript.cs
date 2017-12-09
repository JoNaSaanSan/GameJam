using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForWinnerScript : MonoBehaviour {
    public GameObject player1;
    public GameObject player2;

    // Use this for initialization
    void Start () {
        if (player1 == null)
        {
            player1 = GameObject.FindGameObjectWithTag("player_1");
        }
        if (player2 == null)
        {
            player2 = GameObject.FindGameObjectWithTag("player_2");
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (player1 != null && player2 != null)
        {
            PlayerBehaviour player1Behaviour = (PlayerBehaviour) player1.GetComponent(typeof(PlayerBehaviour));
            PlayerBehaviour player2Behaviour = (PlayerBehaviour) player2.GetComponent(typeof(PlayerBehaviour));

            bool player1Lost = false;
            bool player2Lost = false;

            if(player1Behaviour.topHealth <= 0.0f || player1Behaviour.midHealth <= 0.0f || player1Behaviour.bottomHealth <= 0.0f)
            {
                player1Lost = true;
            }

            if (player2Behaviour.topHealth <= 0.0f || player2Behaviour.midHealth <= 0.0f || player2Behaviour.bottomHealth <= 0.0f)
            {
                player2Lost = true;
            }

            if(player1Lost && !player2Lost)
            {
                print("player 1 lost");
            }
            else if(!player1Lost && player2Lost)
            {
                print("player 2 lost");
            }
            else if(player1Lost && player2Lost)
            {
                print("draw");
            }

            
        }
    }
}
