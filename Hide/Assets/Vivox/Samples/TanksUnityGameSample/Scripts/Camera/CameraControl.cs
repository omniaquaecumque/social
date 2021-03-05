using UnityEngine;
using System.Collections.Generic;


public class CameraControl : MonoBehaviour
{
    static public CameraControl sInstance;

    public float m_DampTime = 0.2f;                   // Approximate time for the camera to refocus.
    public float m_ScreenEdgeBuffer = 4f;             // Space between the top/bottom most target and the screen edge (multiplied by aspect for left and right).
    public float m_MinSize = 6.5f;                    // The smallest orthographic size the camera can be.

    private Camera m_Camera;                        // Used for referencing the camera.
    private float m_ZoomSpeed;                      // Reference speed for the smooth damping of the orthographic size.
    private Vector3 m_MoveVelocity;                 // Reference velocity for the smooth damping of the position.
    private float m_ConvertDistanceToSize;                 // Used to multiply by the offset of the rig to the furthest target.


    private void Awake()
    {
        sInstance = this;
        m_Camera = GetComponentInChildren<Camera>();
    }


    private void Start()
    {
        //SetDistanceToSize();
    }
}