using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using Firebase.Database;


public class AppController_AR : MonoBehaviour
{
    public static AppController_AR instance;
    public Button btnBack;
    public ARRaycastManager raycastManager;
    public GameObject objectToPlacePrefab;
    public GameObject objectInstance;
    public Camera raycastCamera;
    public List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public ARPlaneManager AR_Session_Plane;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        btnBack.onClick.AddListener(ExitAR);
        if (AppController.instance.currentModel != null)
        {
            objectToPlacePrefab = AppController.instance.currentModel;
        }
    }



    public void ExitAR()
    {
        ConnectScene.instance.ActiveApp();
        AppController.instance.LoadScene("AppScene1");
    }


    private void Update()
    {
        if (raycastManager == null)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
            if (raycastManager.Raycast(ray, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
            {
                Pose pose = hits[0].pose;
                if (objectInstance == null)
                {
                    objectInstance = Instantiate<GameObject>(objectToPlacePrefab, pose.position, pose.rotation);
                    AR_Session_Plane.enabled = false;
                }
                else
                {
                    objectInstance.transform.SetPositionAndRotation(pose.position, pose.rotation);
                }
            }
        }
    }
}
