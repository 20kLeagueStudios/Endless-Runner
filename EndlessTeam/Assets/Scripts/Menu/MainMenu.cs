using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject shopButtonPrefab;
    public GameObject shopButtonContainer;

    public InventoryManager inventory;

    public ItemsShopSO[] itemShop;

    public Material playerMaterial;

    //public List<ItemsShopSO> a;

    public void Play()
    {
        Debug.Log("Funge");
        SceneManager.LoadScene("1");
    }

    private void Start()
    {
        //inventory.itemSelected = a;

        foreach (ItemsShopSO item in itemShop)
        {
            GameObject shopButton = Instantiate(shopButtonPrefab) as GameObject;
            shopButton.GetComponent<Image>().sprite = item.itemSO.itemImage;
            shopButton.transform.SetParent(shopButtonContainer.transform, false);

            shopButton.transform.GetChild(0).GetComponent<TMP_Text>().text = item.itemSO.itemName;
            shopButton.transform.GetChild(1).GetComponent<TMP_Text>().text = item.itemSO.ToString();

        }
    }


}
