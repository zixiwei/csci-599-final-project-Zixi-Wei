using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{

    #region Initialization

   
    private void Start()
    {
        RotateCamera();
    }

    #endregion

    #region Fields/Properties

    [Header("General")]

    
    [SerializeField]
    [Tooltip("The target which the camera rotates around.")]
    private GameObject Target;

    
    [SerializeField]
    [Tooltip("The position offset applied to the target's position.")]
    private Vector3 TragetOffset = Vector3.zero;



    [Header("Zoom")]

    
    [SerializeField]
    [Tooltip("The minimum distance a camera can keep with the target when zooming in.")]
    private float MinZoom = 5f;


    [SerializeField]
    [Tooltip("The maximum distance a camera can keep with the target when zooming out.")]
    private float MaxZoom = 10f;

    
    [SerializeField]
    [Tooltip("The current distance the camera is keeping with the target.")]
    private float Zoom = 10f;



    [Header("Rotation")]


    [SerializeField]
    [Tooltip("The maximum latitude a camera can be at.")]
    private float MaxLatitude = 90f;

    
    [SerializeField]
    [Tooltip("The minimum latitude a camera can be at.")]
    private float MinLatitude = -90f;


    [SerializeField]
    [Tooltip("The latitude used to compute the rotation of the camera.")]
    private float Latitude = 0f;

    
    [SerializeField]
    [Tooltip("The longitude used to compute the rotation of the camera.")]
    private float Longitude = 0f;

    
    [SerializeField]
    [Tooltip("The speed at which the camera rotates around the target.")]
    private float RotationSpeed = 10f;

    #endregion

    #region Methods

    
    private void Update()
    {
        
        if (EventSystem.current.IsPointerOverGameObject())
            return;

      
        if (Input.GetMouseButton(0) || Input.GetAxis("Mouse ScrollWheel") != 0)
        {
         
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                Longitude -= Input.GetAxis("Mouse X") * RotationSpeed * Zoom / MaxZoom;
                Latitude -= Input.GetAxis("Mouse Y") * RotationSpeed * Zoom / MaxZoom;

                Latitude = Mathf.Clamp(Latitude, MinLatitude, MaxLatitude);
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                float smoothedTime = Mathf.Sqrt(Time.deltaTime / 0.02f);
                Zoom *= 1f - Mathf.Clamp(Input.GetAxis("Mouse ScrollWheel") * smoothedTime * 1f, -.8f, .4f);
                Zoom = Mathf.Clamp(Zoom, MinZoom, MaxZoom);
            }

            RotateCamera();
        }
    }

    private void RotateCamera()
    {
        
        Vector3 center = Vector3.zero;
        if (Target != null)
            center = Target.transform.position;

  
        Quaternion rotation = Quaternion.Euler(Latitude, -Longitude, 0);

        
        Vector3 position = center - (Quaternion.Euler(Latitude, -Longitude, 0) * Vector3.forward * Zoom);
  
        transform.rotation = rotation;
        transform.position = position;

       
        transform.position += TragetOffset;
    }

    #endregion

}
