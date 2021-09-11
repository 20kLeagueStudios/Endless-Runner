using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPreviewManager : MonoBehaviour
{
    //riferimento al modello 3D della tuta da mettere in preview allo shop
    private Transform previewTutaPrefab = default;
    //riferimento alla skinned mesh della tuta
    private SkinnedMeshRenderer skinnedTutaMesh;
    //riferimenti a tutti i pezzi della mesh della tuta
    private ItemsShopSO skinBodySelected;
    private ItemsShopSO skinGambraDestraSelected;
    private ItemsShopSO skinGambaSinistraselected;
    private ItemsShopSO skinBraccioDestroSelected;
    private ItemsShopSO skinBraccioSinistroSelected;
    private ItemsShopSO skinVetroSelected;
    //riferimento al pilota
    private ItemsShopSO pilotaSelected;
    //riferimento alla posizione in cui mettere il pilota
    private Transform pilotaPosition;
    //indica quanto velocemente dovrà muoversi la preview della tuta
    private float previewRotationSpeed = default;
    //riferimento al container delle skin selezionate nello shop
    //[SerializeField]
    //private Transform selectedItemContainer = default;
    //riferimento all'InventoryManager da cui ottenere i riferimenti sopraelencati
    [SerializeField]
    private InventoryManager im = default;


    private void Start()
    {
        //ottiene tutti i riferimenti dall'InventoryManager
        previewTutaPrefab = im.previewTutaPrefab.transform;
        skinnedTutaMesh = im.skinnedTutaMesh;
        skinBodySelected = im.skinBodySelected;
        skinGambraDestraSelected = im.skinGambraDestraSelected;
        skinGambaSinistraselected = im.skinGambaSinistraselected;
        skinBraccioDestroSelected = im.skinBraccioDestroSelected;
        skinBraccioSinistroSelected = im.skinBraccioSinistroSelected;
        skinVetroSelected = im.skinVetroSelected;
        pilotaSelected = im.pilotaSelected;
        pilotaPosition = im.pilotaPosition;

    }

    void Update()
    {

        previewTutaPrefab.Rotate(0, previewRotationSpeed * Time.deltaTime, 0);

    }

    public void ChangePreviewTexture(ItemsShopSO item)
    {
        string _string = item.itemSO.itemType.ToString();
        //se l'oggetto tuta in preview non è attivo, lo attiva
        if(!previewTutaPrefab.gameObject.activeSelf) previewTutaPrefab.gameObject.SetActive(true);

        if (_string == "Tuta")
        {
            switch (item.itemSO.tutaType)
            {
                case TutaType.Busto:
                    skinnedTutaMesh.sharedMaterials[5].mainTexture = item.itemSO.playerSkin;
                    //AddItemInSelection(item);
                    skinBodySelected = item;

                    break;
                case TutaType.Vetro:
                    skinnedTutaMesh.sharedMaterials[4].mainTexture = item.itemSO.playerSkin;
                    //AddItemInSelection(item);
                    skinVetroSelected = item;

                    break;
                case TutaType.Gambe:

                    skinnedTutaMesh.sharedMaterials[2].mainTexture = item.itemSO.playerSkin;
                    //AddItemInSelection(item);
                    skinGambaSinistraselected = item;

                    break;
                case TutaType.Braccia:

                    skinnedTutaMesh.sharedMaterials[0].mainTexture = item.itemSO.playerSkin;
                    //AddItemInSelection(item);
                    skinGambaSinistraselected = item;

                    break;
                default:
                    break;
            }
            //inventory.tutaMesh.material.mainTexture = item.itemSO.playerSkin;
        }



        else if (_string == "Pilota")
        {
            if (item.itemSO.pilota != null)
            {
                pilotaSelected = item;

                // inventory.pilotaMeshRenderer.enabled = true;

                //s GameObject pilotaprecedente = inventory.pilotaPosition.transform.GetChild(0).gameObject;

                if (pilotaPosition.transform.childCount > 0)
                {
                    Destroy(pilotaPosition.transform.GetChild(0).gameObject);

                }
                GameObject _pilota = Instantiate(item.itemSO.pilota, pilotaPosition.transform.position, pilotaPosition.transform.rotation, pilotaPosition.transform);

                // inventory.pilotaMeshRenderer.material = item.itemSO.accessorioMaterial;
                //  inventory.pilotaMesh.transform.localScale = item.itemSO.pilota.transform.localScale;

            }
            else { return; }

        }

    }

}
