using UnityEngine;
using Unity.AI.Navigation; 

/// <summary>
/// Manages maze-related functionality and navigation mesh updates within the scene.
/// </summary>
/// <remarks>This class is intended to be attached to a GameObject in a Unity scene. It provides methods for
/// updating the navigation mesh, which is used for AI pathfinding. The class relies on a NavMeshSurface component to
/// build and update the navigation mesh as needed.</remarks>
public class MazeManager : MonoBehaviour
{
    public NavMeshSurface surface;

    public void UpdateNavMesh()
    {        
        surface.BuildNavMesh();
    }
}
