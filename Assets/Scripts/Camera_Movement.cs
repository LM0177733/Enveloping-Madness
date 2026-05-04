using UnityEngine;


/// <summary>
/// Controls the position and rotation of the camera to follow a specified target object with a configurable offset.
/// </summary>
/// <remarks>Attach this component to a camera to have it automatically follow and align with the target object's
/// transform each frame. The camera's position is updated in LateUpdate to ensure it follows the target after all
/// movement calculations are complete. This is commonly used for third-person or follow-camera setups in
/// Unity.</remarks>
public class Camera_Movement : MonoBehaviour
{
    public Transform targetObject;
    public Vector3 offset = new Vector3(0, 1.5f, 0);

    void LateUpdate()
    {
        if (targetObject != null)
        {
            transform.position = targetObject.position + offset;
            transform.rotation = targetObject.rotation;
        }
    }
}