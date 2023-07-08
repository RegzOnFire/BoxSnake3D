using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{

    // Settings
    public float MoveSpeed = 5;
    public float SteerSpeed = 100;
    public int Gap = 10,BodysCountInStarting = 10;
    public float SteerChanger = 1;
    public bool SteerLock,UseNewSpawnPosition,UseTouchMove;

    // References
    public GameObject BodyPrefab;
    public Transform BodySpawnPosition1;

    // Lists
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();

    private Vector3 BodySpawnPosition2;
    [SerializeField]
    public float steerDirection,TouchDirection;

    // Start is called before the first frame update
    void Start()
    {
        
        //to increase Bodies in starting
        for (int I = 1; I <= BodysCountInStarting; I++ )
        {
            GrowSnake();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // When Player On air He cant Steer ( This Condition Is Affected By Player_Gravity Script )
        if(!SteerLock)
        {

            // When TouchMove True Player Can steer with touch only (This Codition is affected by TouchController Script)
            if (!UseTouchMove)
            {
                steerDirection = Input.GetAxis("Horizontal"); // Returns value -1, 0, or 1

            }

            if (UseTouchMove)
            {
                steerDirection = TouchDirection;  //(TouchController Script)
            }

            transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime * SteerChanger); // ( Affected by CollisionS Script ) SteerChanger is Used to Change The Steer Of The Player ex. 1 is for positive -1 is for negative
        }

    }

    private void FixedUpdate()
    {
        // Move forward
        transform.position += transform.forward * MoveSpeed * Time.fixedDeltaTime;

        // Store position history
        PositionsHistory.Insert(0, transform.position);

        // Move body parts
        int index = 0;
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionsHistory[Mathf.Clamp(index * Gap, 0, PositionsHistory.Count - 1)];

            // Move body towards the point along the snakes path
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * MoveSpeed * Time.fixedDeltaTime;

            // Rotate body towards the point along the snakes path
            body.transform.LookAt(point);

            index++;

            BodySpawnPosition2 = point; //where Body Spawn
        }
    }

    public void GrowSnake()
    {
        // Instantiate body instance and
        // add it to the list
        if (!UseNewSpawnPosition)
        {
            GameObject body = Instantiate(BodyPrefab, BodySpawnPosition1.position, Quaternion.identity); //This Will called at starting
            BodyParts.Add(body);
        }

        if (UseNewSpawnPosition)
        {
            GameObject body = Instantiate(BodyPrefab, BodySpawnPosition2, Quaternion.identity); //This will called when eating fruits
            BodyParts.Add(body);
        }
        
    }


}