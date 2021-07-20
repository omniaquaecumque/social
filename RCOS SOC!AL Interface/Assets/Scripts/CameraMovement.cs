using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform[] views;
    private float transitionSpeed = 1.5f;
    Transform curView;

    void Start()
    {
        curView = views[0];
        
    }
    
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, curView.position, Time.deltaTime * transitionSpeed);
       
        Vector3 currentAngle = new Vector3(
        Mathf.LerpAngle(transform.rotation.eulerAngles.x, curView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
        Mathf.LerpAngle(transform.rotation.eulerAngles.y, curView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
        Mathf.LerpAngle(transform.rotation.eulerAngles.z, curView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));
        transform.eulerAngles = currentAngle;
    }

    public void SetCurView(int index)
    {
        curView = views[index];
    }
}
