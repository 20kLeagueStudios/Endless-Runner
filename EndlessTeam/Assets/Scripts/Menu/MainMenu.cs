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

    public ItemsShopSO[] itemShop;

    public Material playerMaterial;

    public void Play()
    {
        Debug.Log("Funge");
        SceneManager.LoadScene("1");
    }

    private void Start()
    {
        foreach (ItemsShopSO item in itemShop)
        {
            GameObject container = Instantiate(shopButtonPrefab) as GameObject;
            container.GetComponent<Image>().sprite = item.itemImage;
            container.transform.SetParent(shopButtonContainer.transform, false);

            container.transform.GetChild(0).GetComponent<TMP_Text>().text = item.itemName;
            container.transform.GetChild(1).GetComponent<TMP_Text>().text = item.itemCost.ToString();

        }
    }


}
