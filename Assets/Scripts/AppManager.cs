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

    private List<EducationItem> geographyItems;
    private List<EducationItem> anatomyItems;
    private List<EducationItem> physicsItems;
    private List<EducationItem> archItems;
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
    [SerializeField] private GameObject heart = default;
    [SerializeField] private GameObject atom = default;
    [SerializeField] private GameObject louis = default;
    [SerializeField] private GameObject chichen = default;

    [SerializeField] private GameObject itemPrefab = default;
    private GameObject current3Dmodel = default;
    void OnEnable()
    {
        mainScreen.SetActive(true);
        categoriesScreen.SetActive(false);
        itemsScreen.SetActive(false);
        descriptionScreen.SetActive(false);
        SubscribeEvents();
        CreateAnatomyItems();
        CreateGeographyItems();
        CreatePhysicsItems();
        CreateArchitectureItems();
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
        categButtons[0].onClick.AddListener(OnGeographyPress);
        categButtons[1].onClick.AddListener(OnAnatomyPress);
        categButtons[2].onClick.AddListener(OnPhysicsPress);
        categButtons[3].onClick.AddListener(OnArchitecturePress);
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

    private void OnGeographyPress()
    {
        categoriesScreen.SetActive(false);
        itemsScreen.SetActive(true);
        foreach (Transform child in itemsList.transform) {
            Destroy(child.gameObject);
        }
        foreach (var item in geographyItems)
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
    private void OnAnatomyPress()
    {
        categoriesScreen.SetActive(false);
        itemsScreen.SetActive(true);
        foreach (Transform child in itemsList.transform) {
            Destroy(child.gameObject);
        }
        foreach (var item in anatomyItems)
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
    private void OnPhysicsPress()
    {
        categoriesScreen.SetActive(false);
        itemsScreen.SetActive(true);
        foreach (Transform child in itemsList.transform) {
            Destroy(child.gameObject);
        }
        foreach (var item in physicsItems)
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
    private void OnArchitecturePress()
    {
        categoriesScreen.SetActive(false);
        itemsScreen.SetActive(true);
        foreach (Transform child in itemsList.transform) {
            Destroy(child.gameObject);
        }
        foreach (var item in archItems)
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

    public void CreateGeographyItems()
    {
        geographyItems = new List<EducationItem>();
        geographyItems.Add(new EducationItem("Earth", Categories.Astronomy, earth, ItemsInfo.earthInfo));
    }
    public void CreateAnatomyItems()
    {
        anatomyItems = new List<EducationItem>();
        anatomyItems.Add(new EducationItem("Digestive System", Categories.Anatotmy, digestiveSys, ItemsInfo.digestiveSys));
        anatomyItems.Add(new EducationItem("Heart", Categories.Anatotmy, heart, ItemsInfo.heart));
    }
    public void CreatePhysicsItems()
    {
        physicsItems = new List<EducationItem>();
        physicsItems.Add(new EducationItem("Atom", Categories.Physics, atom, ItemsInfo.atom));
    }
    public void CreateArchitectureItems()
    {
        archItems = new List<EducationItem>();
        archItems.Add(new EducationItem("Louis XIV de France", Categories.Architecture, louis, ItemsInfo.louis));
        archItems.Add(new EducationItem("Chichen Itza", Categories.Architecture, chichen, ItemsInfo.chichen));
    }
}
