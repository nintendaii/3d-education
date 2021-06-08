using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARController : MonoBehaviour
{
    public static GameObject modelToSpawn;
    [SerializeField] private GameObject placementIndicator;
    [SerializeField] private Button lablesButton;
    [SerializeField] private Button homeButton;

    private Pose placementPose;
    private GameObject spawnedObject;
    private ARRaycastManager arRaycastManager;
    private bool isPoseValid = false;
    [CanBeNull] private GameObject labels;
    [CanBeNull] private Transform lt;
    private bool isLabels = false;
    void Start()
    {
        Vector3 sc = modelToSpawn.transform.localScale;
        modelToSpawn.transform.localScale = new Vector3(sc.x/2,sc.y/2,sc.z/2);
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        lablesButton.onClick.AddListener(SwitchLables);
        homeButton.onClick.AddListener(Home);
        lt = modelToSpawn.transform.Find("Labels");
        if (lt==null)
        {
            lablesButton.gameObject.SetActive(false);
        }
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
        float[] v0 = {0,0,0};
        spawnedObject.GetComponent<ObjectRotation>().vector = v0;
        labels = spawnedObject.transform.Find("Labels").gameObject;
    }

    private void Home()
    {
        SceneManager.LoadScene("Main");
    }
}
