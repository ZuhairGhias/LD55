using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All player input should be collected here, regardless of the action. Actions should not be initiated directly in this script.
/// For example, use player controller.
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        DebugUtils.HandleErrorIfNullGetComponent(controller, this);
    }

    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis(HORIZONTAL);
        float y = Input.GetAxis(VERTICAL);
        Vector2 movement = new(x, y);
        movement.Normalize();

        controller.Move(movement);
    }
}
