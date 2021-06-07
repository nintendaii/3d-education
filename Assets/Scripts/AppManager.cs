using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private Button homeButton = default;
                                    
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
    [SerializeField] private GameObject digestiveSys = default;

    [SerializeField] private GameObject itemPrefab = default;
    private GameObject current3Dmodel = default;
    void OnEnable()
    {
        mainScreen.SetActive(true);
        categoriesScreen.SetActive(false);
        itemsScreen.SetActive(false);
        descriptionScreen.SetActive(false);
        SubscribeEvents();
        CreateAstroItems();
        CreateBiologyItems();
    }   

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void UnsubscribeEvents()
    {
        exploreButton.onClick.RemoveAllListeners();
        startARModeButton.onClick.RemoveAllListeners();
        homeButton.onClick.RemoveAllListeners();
        foreach (var btn in categButtons)
        {
            btn.onClick.RemoveAllListeners();
        }
    }

    private void SubscribeEvents()
    {
        exploreButton.onClick.AddListener(OnExplorePress);
        startARModeButton.onClick.AddListener(ARMode);
        homeButton.onClick.AddListener(Home);
        categButtons[0].onClick.AddListener(OnAstroPress);
        categButtons[1].onClick.AddListener(OnBiologyPress);
    }

    private void Home()
    {
        categoriesScreen.SetActive(true);
        mainScreen.SetActive(false);
        itemsScreen.SetActive(false);
        descriptionScreen.SetActive(false);
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
    private void OnBiologyPress()
    {
        categoriesScreen.SetActive(false);
        itemsScreen.SetActive(true);
        foreach (Transform child in itemsList.transform) {
            Destroy(child.gameObject);
        }
        foreach (var item in biologyItems)
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
        current3Dmodel = item.Model;
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

    private void ARMode()
    {
        ARController.modelToSpawn = current3Dmodel;
        SceneManager.LoadScene("ARMode");
    }

    public void CreateAstroItems()
    {
        astroItems = new List<EducationItem>();
        astroItems.Add(new EducationItem("Earth", Categories.Astronomy, earth, ItemsInfo.earthInfo));
    }
    public void CreateBiologyItems()
    {
        biologyItems = new List<EducationItem>();
        biologyItems.Add(new EducationItem("Digestive System", Categories.Biology, digestiveSys, ItemsInfo.earthInfo));
    }
}
