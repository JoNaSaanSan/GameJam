using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    // Starting health
    public static float MAX_HEALTH = 100.0f;

    // Define how much damage is received at the top/bottom of the egg here (relative to the center of the egg)
    public static float TOP_DAMAGE_SCALAR = 0.4f;
    public static float BOTTOM_DAMAGE_SCALAR = 0.6f;


    [System.Serializable]
    public class MoveSettings
    {
        public float runVelocity = 12;
        public float rotateVelocity = 100;
        public float jumpVelocity = 16;
        public float distanceToGround = 1.3f;
        public LayerMask ground;
    }

    [System.Serializable]
    public class InputSettings
    {
        public string FORWARD_AXIS = "Vertical";
        public string SIDEWAYS_AXIS = "Horizontal";
        public string TURN_AXIS = "Mouse X";
        public string JUMP_AXIS = "Jump";
    }   

    public MoveSettings moveSettings;
    public InputSettings inputSettings;

    private Rigidbody playerRigidbody;
    private Vector3 velocity;
    private Quaternion targetRotation;
    private float forwardInput, sidewaysInput, turnInput, jumpInput;


    // Player specific attributes
    public float relPivotPointY = 1.0f / 3.0f;
    public float playerHealth = 100.0f;


    //Spawn 
    public Transform spawnPoint;

    private void Start()
    {
        //print("start");
        if(gameObject.tag == "player_1")
        {
            inputSettings.FORWARD_AXIS = "Vertical1";
            inputSettings.SIDEWAYS_AXIS = "Horizontal1";
        }
        else if(gameObject.tag == "player_2")
        {
            inputSettings.FORWARD_AXIS = "Vertical";
            inputSettings.SIDEWAYS_AXIS = "Horizontal";
        }
        Spawn();
    }

    public void Spawn()
    {
        // transform.position = spawnPoint.position;
        playerHealth = MAX_HEALTH;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (gameObject.tag == "DeathZone")
        {
            Spawn();
        }

        // Determine enemy damage dealer
        bool enemyDamageDealer;
        if(gameObject.tag == "player_1")
        {
            enemyDamageDealer = DamageDealer.PLAYER_2;
        }
        else if(gameObject.tag == "player_2")
        {
            enemyDamageDealer = DamageDealer.PLAYER_1;
        }
        else
        {
            return;
        }

        // Get collision object as damage dealer object
        DamageDealer dd = (DamageDealer)collisionInfo.collider.gameObject.GetComponent(typeof(DamageDealer));
        if(dd == null)
        {
            return;
        }

        // If the object we collided to is assotiated with the enemy player, then we receive damage
        if(dd.associatedPlayer == enemyDamageDealer)
        {
            // The direct damage the player receives from the DamageDealer object
            //TODO: geschwindigkeit auch berücksichtigen
            float receivedDamage = dd.damageToDeal * MAX_HEALTH;
            //print("mag:" + (Mathf.Sqrt(collisionInfo.relativeVelocity.sqrMagnitude)));

            // Adjust the damage, as the egg is more robust at the ends
            ContactPoint cp = collisionInfo.contacts[0];
            Vector3 localCollisionPos = gameObject.transform.InverseTransformPoint(cp.point);
            float dmgScalar = calculateDamageScalar(localCollisionPos.y);
            receivedDamage *= dmgScalar;

            // Adjust health and check for negative health
            playerHealth -= receivedDamage;

            //print("Health: " + playerHealth);
        }    
    }


    float calculateDamageScalar(float localCollisionYPosition)
    {
        Vector3 boundingBoxSize = GetComponent<MeshFilter>().mesh.bounds.size;
        Vector3 objectSize = Vector3.Scale(transform.localScale, boundingBoxSize);
        float bottomLength = objectSize.y * relPivotPointY;
        float topLength = objectSize.y - bottomLength;

        if (localCollisionYPosition > 0.0f)
        {
            return 1.0f - ((1.0f - TOP_DAMAGE_SCALAR) * (localCollisionYPosition / topLength));
        }  
        else if(localCollisionYPosition < 0.0f)
        {
            return 1.0f + ((1.0f - TOP_DAMAGE_SCALAR) * (localCollisionYPosition / topLength));
        }
        else
        {
            return 1.0f;
        }
    }

    void Awake()
    {
        velocity = Vector3.zero;
        forwardInput = sidewaysInput = turnInput = jumpInput = 0;
        targetRotation = transform.rotation;
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        GetInput();
       // Turn();
    }

    void FixedUpdate()
    {
        // This test creates spikes on the player 2 egg wherever the mouse is

        /*
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Casts the ray and get the first game object hit
        Physics.Raycast(ray, out hit);
        Debug.Log("This hit at " + hit.point + ", obj: " + hit.transform.tag);
        if (hit.transform.tag == "player_2")
        {
            GameObject testSpike = GameObject.CreatePrimitive(PrimitiveType.Cube);
            testSpike.AddComponent<DamageDealer>();
            DamageDealer dd = (DamageDealer)testSpike.GetComponent(typeof(DamageDealer));
            dd.associatedPlayer = DamageDealer.PLAYER_1;
            dd.damageToDeal = 0.3f;
            testSpike.transform.position = hit.point;
        }
        */

        Run();
        Jump();
    }


    
    void GetInput()
    {
        if (inputSettings.FORWARD_AXIS.Length != 0)
            forwardInput = Input.GetAxis(inputSettings.FORWARD_AXIS);
        if (inputSettings.SIDEWAYS_AXIS.Length != 0)
            sidewaysInput = Input.GetAxis(inputSettings.SIDEWAYS_AXIS);
        if (inputSettings.TURN_AXIS.Length != 0)
            turnInput = Input.GetAxis(inputSettings.TURN_AXIS);
        if (inputSettings.JUMP_AXIS.Length != 0)
            jumpInput = Input.GetAxisRaw(inputSettings.JUMP_AXIS);
    }

    void Run()
    {
        velocity.z = forwardInput * moveSettings.runVelocity;
        velocity.x = sidewaysInput * moveSettings.runVelocity;
        velocity.y = playerRigidbody.velocity.y;
        //playerRigidbody.velocity = transform.TransformDirection(velocity);
        playerRigidbody.AddForce(velocity);
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > 0)
        {
            targetRotation *= Quaternion.AngleAxis(moveSettings.rotateVelocity *
            turnInput * Time.deltaTime, Vector3.up);
        }
        transform.rotation = targetRotation;
    }

    void Jump()
    {
        if (jumpInput != 0 && Grounded())
        {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x,
            moveSettings.jumpVelocity, playerRigidbody.velocity.z);
        }
    }

    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 
            moveSettings.distanceToGround, moveSettings.ground);
    }
}
