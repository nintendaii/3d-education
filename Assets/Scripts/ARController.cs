using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARController : MonoBehaviour
{
    [SerializeField] public static GameObject modelToSpawn;
    [SerializeField] private GameObject placementIndicator;
    [SerializeField] private Button lablesButton;
    [SerializeField] private Button homeButton;

    private Pose placementPose;
    private GameObject spawnedObject;
    private ARRaycastManager arRaycastManager;
    private bool isPoseValid = false;
    private GameObject labels;
    private bool isLabels = false;
    void Start()
    {
        Vector3 sc = modelToSpawn.transform.localScale;
        modelToSpawn.transform.localScale = new Vector3(sc.x/2,sc.y/2,sc.z/2);
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        lablesButton.onClick.AddListener(SwitchLables);
        homeButton.onClick.AddListener(Home);
    }

    private void SwitchLables()
    {
        isLabels = !isLabels;
        labels.SetActive(isLabels);
    }

    void Update()
    {
        if(spawnedObject == null && isPoseValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }

        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    private void UpdatePlacementIndicator()
    {
        if (spawnedObject==null && isPoseValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        isPoseValid = hits.Count > 0;
        if(isPoseValid)
        {
            placementPose = hits[0].pose;
        }
    }

    private void PlaceObject()
    {
        spawnedObject = Instantiate(modelToSpawn, placementPose.position, placementPose.rotation);
        labels = spawnedObject.transform.Find("Labels").gameObject;
        float[] v0 = {0,0,0};
        spawnedObject.GetComponent<ObjectRotation>().vector = v0;
    }

    private void Home()
    {
        SceneManager.LoadScene("Main");
    }
}
