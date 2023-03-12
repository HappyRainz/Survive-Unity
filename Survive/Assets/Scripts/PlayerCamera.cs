using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public InputManager inputManager;

    public Transform cameraPivot;
    public Camera cameraObject;
    public GameObject Player;

    Vector3 cameraFollowVelocity = Vector3.zero;
    Vector3 targetPosition;
    Vector3 cameraRotation;
    Quaternion targetRotation;

    [Header("Camera Speeds")]
    float cameraSmoothTime = 0.2f;

    float lookAmountVertical;
    float lookAmountHorizontal;
    float maximumPivotAngle = 15;
    float minimumPivotAngle = -15;

    private void Awake()
    {
        Player.GetComponent<InputManager>();
    }
    
    public void HandleAllCameraMovement()
    {
        FollowPlayer();
        RotateCamera();
    }

    private void FollowPlayer()
    {
        targetPosition = Vector3.SmoothDamp(transform.position, Player.transform.position, ref cameraFollowVelocity, cameraSmoothTime * Time.deltaTime);
        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        lookAmountVertical = lookAmountVertical + (inputManager.horizontalCameraInput);
        lookAmountHorizontal = lookAmountHorizontal + (inputManager.verticalCameraInput * -1);
        lookAmountHorizontal = Mathf.Clamp(lookAmountHorizontal, minimumPivotAngle, maximumPivotAngle);

        cameraRotation = Vector3.zero;
        cameraRotation.y = lookAmountVertical;
        targetRotation = Quaternion.Euler(cameraRotation);
        targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, cameraSmoothTime);
        transform.rotation = targetRotation;

        cameraRotation = Vector3.zero;
        cameraRotation.x = lookAmountHorizontal;
        targetRotation = Quaternion.Euler(cameraRotation);
        targetRotation = Quaternion.Slerp(cameraPivot.localRotation, targetRotation, cameraSmoothTime);
        cameraPivot.localRotation = targetRotation;

    }
}
