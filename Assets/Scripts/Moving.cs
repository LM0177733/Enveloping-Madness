using UnityEngine;

/// <summary>
/// Provides movement and physics control for a GameObject using a Rigidbody component, including translation, rotation,
/// force application, and physics property adjustments.
/// </summary>
/// <remarks>Attach this component to a GameObject with a Rigidbody to enable programmatic control over its
/// movement and physics behavior. The class exposes methods for moving, rotating, and applying forces, as well as for
/// configuring Rigidbody properties such as mass, drag, and gravity. It is designed for use in Unity's physics update
/// loop and supports both direct manipulation and input-driven actions. Ensure that the Rigidbody component is present
/// on the same GameObject for all features to function correctly.</remarks>
public class Moving : MonoBehaviour
{
    public float speed = 20f;
    public float turn_speed = 5f;
    private Rigidbody rb;
    private bool left_should_rotate = false;
    private bool right_should_rotate = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();


        rb.sleepThreshold = 0f;
    }
    // The FixedUpdate method is called at a fixed interval and is used for physics updates. It handles the rotation of the GameObject based on horizontal input and moves it forward based on the speed variable.
    void FixedUpdate()
    {




        Vector3 nextPosition = rb.position + transform.forward * speed * Time.fixedDeltaTime;
        rb.MovePosition(nextPosition);

        if (right_should_rotate)
        {
            Right_Rotate();
            right_should_rotate = false;
        }
        else if (left_should_rotate)
        {
            Left_Rotate();
            left_should_rotate = false;
        }
        // jump();
        // MoveForward();


    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            right_should_rotate = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            left_should_rotate = true;
        }


    }
    /// <summary>
    /// Rotates the attached Rigidbody by 90 degrees around the Y axis.
    /// </summary>
    /// <remarks>This method applies an incremental rotation to the Rigidbody's current orientation. It should
    /// be called when a 90-degree turn is required, such as for character or object rotation in a game. The method
    /// assumes that the Rigidbody component is properly initialized and attached.</remarks>
    public void Right_Rotate()
    {
        Quaternion targetRotation = rb.rotation * Quaternion.Euler(0, 90, 0);
        rb.MoveRotation(targetRotation);
        
    }
    public void Left_Rotate()
    {
        Quaternion targetRotation = rb.rotation * Quaternion.Euler(0, -90, 0);
        rb.MoveRotation(targetRotation);
    }
    public void MoveForward()
    {
        Vector3 next_position = transform.position + transform.forward * speed * Time.fixedDeltaTime;
        if (rb != null)
        {
            rb.MovePosition(next_position);
        }
    }
    public void jump()
    {

        if (Input.GetKeyDown("space"))
        {
           
            MoveForward();
        }
    }
  
    // The SetSpeed and SetTurnSpeed methods allow external scripts to modify the speed and turn speed of the GameObject, providing flexibility in controlling its movement behavior.
    public void SetSpeed(float new_speed)
    {
        speed = new_speed;
    }
    // The SetTurnSpeed method allows external scripts to modify the turn speed of the GameObject, enabling dynamic adjustments to its rotation behavior during runtime.
    public void SetTurnSpeed(float new_turn_speed)
    {
        turn_speed = new_turn_speed;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public float GetTurnSpeed()
    {
        return turn_speed;
    }
    public void MoveForward(float distance)
    {
        Vector3 next_position = transform.position + transform.forward * distance;
        if (rb != null)
        {
            rb.MovePosition(next_position);
        }
    }

   
    public void StopMovement()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    public void ResetPosition(Vector3 new_position)
    {
        rb.position = new_position;
        rb.rotation = Quaternion.identity;
        StopMovement();
    }
    public void ResetRotation(Quaternion new_rotation)
    {
        rb.rotation = new_rotation;
        StopMovement();
    }
    // The ApplyForce method allows external scripts to apply a force to the Rigidbody, enabling additional movement effects such as jumps or pushes.
    public void ApplyForce(Vector3 force)
    {
        rb.AddForce(force);
    }
    // The ApplyTorque method allows external scripts to apply a torque to the Rigidbody, enabling rotational effects such as spins or twists.
    public void ApplyTorque(Vector3 torque)
    {
        rb.AddTorque(torque);
    }
    // The SetVelocity method allows external scripts to directly set the velocity of the Rigidbody, providing precise control over the GameObject's movement.
    public void SetVelocity(Vector3 new_velocity)
    {
        rb.linearVelocity = new_velocity;
    }
    /// <summary>
    /// Sets the angular velocity of the rigidbody.
    /// </summary>
    /// <param name="new_angular_velocity">The new angular velocity to apply, specified as a Vector3 in local space. Units are in radians per second.</param>
    /// <remarks>Use this method to directly control the rotational speed of the GameObject. The angular velocity will cause the GameObject to rotate around its local axes based on the provided vector. Ensure that the Rigidbody component is attached to the same GameObject for this method to work correctly.</remarks>
    // angular velocity is a measure of how quickly an object is rotating around its axes. In Unity, you can set the angular velocity of a Rigidbody to control its rotation. The SetAngularVelocity method allows you to directly assign a new angular velocity to the Rigidbody, which will cause it to rotate at the specified speed and direction. The angular velocity is typically represented as a Vector3, where each component corresponds to the rotation around the respective axis (x, y, z). Adjusting the angular velocity can create various rotational effects, such as spinning or tumbling, depending on the values provided.
    public void SetAngularVelocity(Vector3 new_angular_velocity)
    {
        rb.angularVelocity = new_angular_velocity;
    }
    public Vector3 GetVelocity()
    {
        return rb.linearVelocity;
    }
    public Vector3 GetAngularVelocity()
    {
        return rb.angularVelocity;
    }
    public void EnableGravity(bool enable)
    {
        rb.useGravity = enable;
    }
    public void EnableKinematic(bool enable)
    {
        rb.isKinematic = enable;
    }
    public void EnableCollision(bool enable)
    {
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = enable;
        }
    }
    /// <summary>
    /// Sets the mass of the associated Rigidbody component to the specified value.
    /// </summary>
    /// <param name="new_mass">The new mass value to assign to the Rigidbody component. Must be a positive number.</param>
    /// <remarks>Ensure that the Rigidbody component is attached to the same GameObject as this script for the mass change to take effect. Adjusting the mass can affect the physics behavior of the GameObject, such as its response to forces and collisions.</remarks>
// mass is a measure of an object's resistance to acceleration when a force is applied. In Unity, the mass of a Rigidbody affects how it responds to forces and collisions. A higher mass will make the object less affected by forces, while a lower mass will make it more responsive. Setting the mass appropriately can help achieve the desired physics behavior for your GameObject.
    public void SetMass(float new_mass)
    {
        rb.mass = new_mass;
    }
    /// <summary>
    /// Gets the mass of the associated rigidbody.
    /// </summary>
    /// <returns>The mass of the rigidbody, in kilograms.</returns>
    public float GetMass()
    {
        return rb.mass;
    }
    /// <summary>
    /// Sets the drag value for the associated Rigidbody component.
    /// </summary>
    /// <remarks>Use this method to adjust the resistance applied to the Rigidbody's movement. Higher values
    /// result in greater resistance to motion.</remarks>
    /// <param name="new_drag">The new drag value to apply. Must be non-negative.</param>
    public void SetDrag(float new_drag)
    {
        rb.linearDamping = new_drag;
    }
    public float GetDrag()
    {
        return rb.linearDamping;
    }
    /// <summary>
    /// Sets the angular drag value for the associated Rigidbody component.
    /// </summary>
    /// <remarks>Angular drag affects how quickly the Rigidbody stops rotating. Higher values result in faster
    /// rotational deceleration.</remarks>
    /// <param name="new_angular_drag">The new angular drag value to apply. Must be non-negative.</param>

    public void SetAngularDrag(float new_angular_drag)
    {
        rb.angularDamping = new_angular_drag;
    }
    public float GetAngularDrag()
    {
        return rb.angularDamping;
    }
    public bool IsKinematic()
    {
        return rb.isKinematic;
    }
    public bool IsGravityEnabled()
    {
        return rb.useGravity;
    }
    public bool IsCollisionEnabled()
    {
        Collider collider = GetComponent<Collider>();
        return collider != null && collider.enabled;
    }
    public void ResetAll()
    {
        StopMovement();
        ResetPosition(Vector3.zero);
        ResetRotation(Quaternion.identity);
        SetMass(1f);
        SetDrag(0f);
        SetAngularDrag(0.05f);
        EnableGravity(true);
        EnableKinematic(false);
        EnableCollision(true);
    }

    // The PrintStatus method logs the current state of the GameObject, including its position, rotation, velocity, angular velocity, mass, drag, angular drag, and the status of gravity, kinematic mode, and collision.
    public void PrintStatus()
    {
        Debug.Log($"Position: {transform.position}, Rotation: {transform.rotation.eulerAngles}, Velocity: {GetVelocity()}, Angular Velocity: {GetAngularVelocity()}, Mass: {GetMass()}, Drag: {GetDrag()}, Angular Drag: {GetAngularDrag()}, Gravity Enabled: {IsGravityEnabled()}, Kinematic: {IsKinematic()}, Collision Enabled: {IsCollisionEnabled()}");
    }
}
