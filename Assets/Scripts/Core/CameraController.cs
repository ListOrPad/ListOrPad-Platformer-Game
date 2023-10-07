using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region --before Cinemachine
    /*

    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float smoothTime;

    private float lookAhead;
    private float lookUp = 5;
    private Vector3 velocity = Vector3.zero;


    private PlayerMovement movement;

    private void Awake()
    {
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void LateUpdate()
    {
        lookAhead = Mathf.Lerp(lookAhead, aheadDistance * player.localScale.x, Time.deltaTime * cameraSpeed);
        //if climbing: lock cam movement on x axis until player is over some distance from wall
        if (movement.IsClimbing)
        {
            //then cam slowly follow player up
            transform.position = Vector3.SmoothDamp(transform.position, player.position, ref velocity, smoothTime);
        }
        else
        {
            transform.position = new Vector3(player.position.x + lookAhead, player.position.y + lookUp, transform.position.z);
        }
    }

    */
    #endregion
    private PlayerMovement movement;
    private CinemachineFramingTransposer transposer;
    private CinemachineVirtualCamera virtualCamera;
    private void Awake()
    {
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        transposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }
    private void LateUpdate()
    {
        if(transposer != null)
        {
            if (movement.IsFacingRight)
            {
                transposer.m_ScreenX = 0.45f;
            }
            else if (!movement.IsFacingRight)
            {
                transposer.m_ScreenX = 0.55f;
            }
        }
        
    }
}
