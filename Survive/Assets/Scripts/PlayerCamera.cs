using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public Transform cameraPivot;
    public Camera cameraObject;
    public GameObject Player;

    Vector3 cameraFollowVelocity = Vector3.zero;
    Vector3 targetPosition;

    [Header("Camera Speeds")]
    float cameraSmoothTime = 0.2f;
    
    public void HandleAllCameraMovement()
    {
        FollowPlayer();
        //rotate the camera
    }

    private void FollowPlayer()
    {
        targetPosition = Vector3.SmoothDamp(transform.position, Player.transform.position, ref cameraFollowVelocity, cameraSmoothTime * Time.deltaTime);
        transform.position = targetPosition;
    }

}
