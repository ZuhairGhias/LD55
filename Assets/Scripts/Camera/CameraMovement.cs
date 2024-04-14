using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform target;
    [Range(1, 3)]
    public float responsiveness = 0.5f;
    private float currentTargetX;
    public float lookAhead = 1;
    public float maxDistance = 1;
    public CameraStates cameraState;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        DebugUtils.HandleErrorIfNullGetComponent(target, this);
        if(maxDistance <= 0)
        {
            maxDistance = 1;
        }
        transform.position = new(target.position.x, transform.position.y, -10);

        if(responsiveness == 0)
        {
            responsiveness = 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraState == CameraStates.Active)
        {
            currentTargetX = Mathf.Max(currentTargetX, target.transform.position.x + lookAhead);
            transform.position = new(Mathf.Lerp(transform.position.x, currentTargetX, responsiveness * Time.deltaTime), transform.position.y, transform.position.z);
        }
    }

    public enum CameraStates
    {
        Active,
        Stop
    }
}
