using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {
    public static bool PLAYER_1 = true;
    public static bool PLAYER_2 = !PLAYER_1;


    public GameObject player1;
    public GameObject player2;

    public bool associatedPlayer;
    public char locationOnPlayer;   //t,m,b,s
    public float damageToDeal;
    public float armor;




	// Use this for initialization
	void Start () {
	    if(player1 == null)
        {
            player1 = GameObject.FindGameObjectWithTag("player_1");
        }	
        if(player2 == null)
        {
            player2 = GameObject.FindGameObjectWithTag("player_2");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        DamageDealer dd = (DamageDealer) collision.collider.gameObject.GetComponent(typeof(DamageDealer));
        if (dd == null)
        {
            return;
        }

        if(dd.associatedPlayer != associatedPlayer)
        {
            PlayerBehaviour attackedPlayer = null;
            if(associatedPlayer == PLAYER_1 && player2 != null)
            {
                attackedPlayer = (PlayerBehaviour)player2.GetComponent(typeof(PlayerBehaviour));
            }
            else if(associatedPlayer == PLAYER_2 && player1 != null)
            {
                attackedPlayer = (PlayerBehaviour)player1.GetComponent(typeof(PlayerBehaviour));
            }

            if(attackedPlayer == null)
            {
                return;
            }

            DamageDealer selfDD = (DamageDealer) collision.contacts[0].thisCollider.gameObject.GetComponent(typeof(DamageDealer));



            float damage = selfDD.damageToDeal * (1.0f - dd.armor);
            damage *= collision.relativeVelocity.magnitude;
            print("damageToDeal: " + selfDD.damageToDeal);
            print("damage: " + damage);

            attackedPlayer.receiveDamage(dd.locationOnPlayer, collision.contacts[0].point, damage);
        }
    }
}
