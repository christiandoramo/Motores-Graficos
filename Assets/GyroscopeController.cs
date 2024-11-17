using UnityEngine;



public class GyroscopeController : MonoBehaviour

{

    private Rigidbody rb;

    private float xMovement;

    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float xBoundary = 7.5f; // Movement boundary in X direction



    void Start()

    {

        rb = GetComponent<Rigidbody>();

    }



    void Update()

    {

        // Determine the movement speed based on device tilt and invert direction

        xMovement = -Input.acceleration.x * moveSpeed;



        // Constrain position within the X boundary

        float constrainedX = Mathf.Clamp(transform.position.x, -xBoundary, xBoundary);

        transform.position = new Vector3(constrainedX, transform.position.y, transform.position.z);

    }



    void FixedUpdate()

    {

        // Apply velocity to Rigidbody

        Vector3 newVelocity = new Vector3(xMovement, rb.linearVelocity.y, rb.linearVelocity.z);

        rb.linearVelocity = newVelocity;

    }

}