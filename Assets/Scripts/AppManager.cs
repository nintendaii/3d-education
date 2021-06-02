using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    [Header("Screens")]             
    [SerializeField] private GameObject mainScreen = default;
    [SerializeField] private GameObject categoriesScreen = default;
    [SerializeField] private GameObject itemsScreen = default;
    [SerializeField] private GameObject descriptionScreen = default;
                                    
    [Header("Main Menu Screen")]    
    [SerializeField] private Button exploreButton = default;
                                    
    [Header("Categories Screen")] 
    [SerializeField] private Button[] categButtons = default;

    private List<EducationItem> astroItems;
    private List<EducationItem> biologyItems;
    private List<EducationItem> physicstems;
    private List<EducationItem> chemistryItems;
    [Header("Items Screen")] 
    [SerializeField] private GameObject itemsList = default;

    [Header("Description Screen")] 
    [SerializeField] private Button startARModeButton = default;

    [SerializeField] private TMP_Text header;
    [SerializeField] private GameObject content3D;
    [SerializeField] private TMP_Text infoContent;
    
    [Header("Prefabs")]
    [SerializeField] private GameObject earth = default;

    [SerializeField] private GameObject itemPrefab = default;
    void OnEnable()
    {
        mainScreen.SetActive(true);
        categoriesScreen.SetActive(false);
        itemsScreen.SetActive(false);
        descriptionScreen.SetActive(false);
        SubscribeEvents();
        CreateAstroItems();
    }   

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void UnsubscribeEvents()
    {
        exploreButton.onClick.RemoveAllListeners();
        foreach (var btn in categButtons)
        {
            btn.onClick.RemoveAllListeners();
        }
    }

    private void SubscribeEvents()
    {
        exploreButton.onClick.AddListener(OnExplorePress);
        categButtons[0].onClick.AddListener(OnAstroPress);
    }

    private void OnExplorePress()
    {
        mainScreen.SetActive(false);
        categoriesScreen.SetActive(true);
    }

    private void OnAstroPress()
    {
        categoriesScreen.SetActive(false);
        itemsScreen.SetActive(true);
        foreach (Transform child in itemsList.transform) {
            Destroy(child.gameObject);
        }
        foreach (var item in astroItems)
        {
            var it = Instantiate(itemPrefab);
            it.transform.SetParent(itemsList.transform,false);
            it.SetActive(true);
            it.GetComponentInChildren<TMP_Text>().text = item.Title;
            Button btn = it.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(delegate { OnDescriptionOpen(item); });
        }
    }

    private void OnDescriptionOpen(EducationItem item)
    {
        itemsScreen.SetActive(false);
        descriptionScreen.SetActive(true);
        infoContent.text = String.Empty;
        foreach (Transform child in content3D.transform) {
            Destroy(child.gameObject);
        }
        header.text = item.Title;
        infoContent.text = item.Description;
        var it = Instantiate(item.Model);
        it.transform.SetParent(content3D.transform,false);
        it.SetActive(true);
    }

    public void CreateAstroItems()
    {
        astroItems = new List<EducationItem>();
        astroItems.Add(new EducationItem("Earth", Categories.Astronomy, earth, "lorem ipsum blablabla"));
        float[] arr = {1f, 1f, 1f};
        earth.GetComponent<ObjectRotation>().vector = arr;
    }
}
