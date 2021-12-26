using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera arCam;
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private GameObject crosshair;
    private RaycastHit hit;
    private Pose pose;

    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();
    private Touch touch;

    public bool IsPointerOverUI(Touch touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.position.x, touch.position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    void CrosshairCalculation()
    {
        Vector3 origin = arCam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));
        Ray ray = arCam.ScreenPointToRay(origin);

        if (_raycastManager.Raycast(ray, _hits))
        {
            pose = _hits[0].pose;
            crosshair.transform.position = pose.position;
            crosshair.transform.eulerAngles = new Vector3(90, 0, 0);
        }
        else if (Physics.Raycast(ray, out hit))
        {
            crosshair.transform.position = hit.point;
            crosshair.transform.up = hit.normal;
        }
    }

    private void Update()
    {
        CrosshairCalculation();

        touch = Input.GetTouch(0);

        if (Input.touchCount < 0 || touch.phase != TouchPhase.Began)
            return;
        if (IsPointerOverUI(touch)) return;


        Ray ray = arCam.ScreenPointToRay(touch.position);
        if (_raycastManager.Raycast(ray, _hits))
        {
            Pose pose = _hits[0].pose;
            Instantiate(DataHandler.Instance.furniture, pose.position, pose.rotation);
        }

    }
}
