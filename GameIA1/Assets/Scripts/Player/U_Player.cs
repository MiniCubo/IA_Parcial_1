using UnityEngine;

public class U_Player : MonoBehaviour, IDescription
{
    private Vector3 lastPosition;

    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public float currentSpeed = 0;
    public Transform Transform
    {
        get
        {
            return transform;
        }
    }
    public float Velocity
    {
        get
        {
            return CalculateVelocity();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        lastPosition = transform.position;

        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);
        currentSpeed = translation;

        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);
    }

    private float CalculateVelocity()
    {
        Vector3 velocityVector = transform.position - lastPosition;
        return velocityVector.magnitude;
    }
}

