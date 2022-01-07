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
    private ItemsShopSO skinGambaSinistraSelected;
    private ItemsShopSO skinBraccioDestroSelected;
    private ItemsShopSO skinBraccioSinistroSelected;
    //private ItemsShopSO skinVetroSelected;
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
    //lista di tutti i materiali iniziali della tuta in preview
    //private List<Material> startSharedMaterials = new List<Material>();
    private Material[] startSharedMaterials;
    //riferimento al materiale da dare alle parti di tuta da non far vedere
    [SerializeField]
    private Material transparentMaterial = default;


    private void Start()
    {
        //ottiene tutti i riferimenti dall'InventoryManager
        previewTutaPrefab = im.previewTutaPrefab.transform;
        skinnedTutaMesh = im.skinnedTutaMesh;
        skinBodySelected = im.skinBodySelected;
        skinGambraDestraSelected = im.skinGambraDestraSelected;
        skinGambaSinistraSelected = im.skinGambaSinistraselected;
        skinBraccioDestroSelected = im.skinBraccioDestroSelected;
        skinBraccioSinistroSelected = im.skinBraccioSinistroSelected;
        //skinVetroSelected = im.skinVetroSelected;
        pilotaSelected = im.pilotaSelected;
        pilotaPosition = im.pilotaPosition;
        //ottiene il riferimento a tutti i materiali iniziali della tuta
        //foreach(Material m in skinnedTutaMesh.sharedMaterials) { startSharedMaterials.Add(m); Debug.Log(startSharedMaterials.Count + ") " + m); }
        startSharedMaterials = skinnedTutaMesh.sharedMaterials;
        Debug.Log("Materiali nell'array di start: " + startSharedMaterials.Length);
    }

    void Update()
    {

        previewTutaPrefab.Rotate(0, previewRotationSpeed * Time.deltaTime, 0);

    }

    public void ChangePreviewTexture(ItemsShopSO item)
    {

        string _string = item.itemSO.itemType.ToString();

        //array locale dei materiali da dare alla fine allo skinnedMeshRenderer della tuta
        Material[] newSharedMaterials = skinnedTutaMesh.sharedMaterials;
        Debug.Log("Materiali nell'array locale: " + newSharedMaterials.Length);
        //se l'oggetto tuta in preview non è attivo, lo attiva
        if (!previewTutaPrefab.gameObject.activeSelf) previewTutaPrefab.gameObject.SetActive(true);

        if (_string == "Tuta")
        {
            //switch (item.itemSO.tutaType)
            //{
            //    case TutaType.Busto:
            //        skinnedTutaMesh.sharedMaterials[5].mainTexture = item.itemSO.playerSkin;
            //        //AddItemInSelection(item);
            //        skinBodySelected = item;

            //        break;
            //    case TutaType.Vetro:
            //        skinnedTutaMesh.sharedMaterials[4].mainTexture = item.itemSO.playerSkin;
            //        //AddItemInSelection(item);
            //        skinVetroSelected = item;

            //        break;
            //    case TutaType.Gambe:

            //        skinnedTutaMesh.sharedMaterials[2].mainTexture = item.itemSO.playerSkin;
            //        //AddItemInSelection(item);
            //        skinGambaSinistraselected = item;

            //        break;
            //    case TutaType.Braccia:

            //        skinnedTutaMesh.sharedMaterials[0].mainTexture = item.itemSO.playerSkin;
            //        //AddItemInSelection(item);
            //        skinGambaSinistraselected = item;

            //        break;
            //    default:
            //        break;
            //}
            //inventory.tutaMesh.material.mainTexture = item.itemSO.playerSkin;

            //se l'oggetto selezionato è del tipo busto...
            if (item.itemSO.tutaType == TutaType.Busto)
            {
                //...riporta questa parte al materiale originale...
                //skinnedTutaMesh.sharedMaterials[5] = startSharedMaterials[5];
                newSharedMaterials[5] = startSharedMaterials[5];
                //...ne cambia la texture in base al materiale dell'oggetto stesso...
                newSharedMaterials[5].mainTexture = item.itemSO.playerSkin;
                //...e indica il nuovo busto selezionato
                skinBodySelected = item;

            } //altrimenti, il materiale del busto diventa trasparente
            else { newSharedMaterials[5] = transparentMaterial; Debug.Log("Reso trasparente busto"); }
            
            /*
            //se l'oggetto selezionato è del tipo vetro...
            if (item.itemSO.tutaType == TutaType.Vetro)
            {

                Debug.LogError("E' stato inserito un valore di tipo di tuta errato. Non esistono skin per il vetro!");

                return;

                
                //...riporta questa parte al materiale originale...
                newSharedMaterials[4] = startSharedMaterials[4];
                //...ne cambia la texture in base al materiale dell'oggetto stesso...
                newSharedMaterials[4].mainTexture = item.itemSO.playerSkin;
                //...e indica il nuovo vetro selezionato
                skinVetroSelected = item;
                
            } //altrimenti, il materiale del vetro diventa trasparente
            //else { newSharedMaterials[4] = transparentMaterial; Debug.Log("Reso trasparente vetro"); }
            */
            
            //se l'oggetto selezionato è del tipo gambe...
            if (item.itemSO.tutaType == TutaType.Gambe)
            {
                //...riporta questa parte al materiale originale...
                newSharedMaterials[2] = startSharedMaterials[2];
                //...ne cambia la texture in base al materiale dell'oggetto stesso...
                newSharedMaterials[2].mainTexture = item.itemSO.playerSkin;
                //...riporta questa parte al materiale originale...
                newSharedMaterials[3] = startSharedMaterials[3];
                //...ne cambia la texture in base al materiale dell'oggetto stesso...
                newSharedMaterials[3].mainTexture = item.itemSO.playerSkin;
                //...e indica il nuovo set di gambe selezionato
                skinGambaSinistraSelected = item;

            } //altrimenti, il materiale del set di gambe diventa trasparente
            else { newSharedMaterials[2] = transparentMaterial; newSharedMaterials[3] = transparentMaterial; Debug.Log("Reso trasparente gambe"); }

            //se l'oggetto selezionato è del tipo braccia...
            if (item.itemSO.tutaType == TutaType.Braccia)
            {
                //...riporta questa parte al materiale originale...
                newSharedMaterials[0] = startSharedMaterials[0];
                //...ne cambia la texture in base al materiale dell'oggetto stesso...
                newSharedMaterials[0].mainTexture = item.itemSO.playerSkin;
                //...riporta questa parte al materiale originale...
                newSharedMaterials[1] = startSharedMaterials[1];
                //...ne cambia la texture in base al materiale dell'oggetto stesso...
                newSharedMaterials[1].mainTexture = item.itemSO.playerSkin;
                //...e indica il nuovo set di braccia selezionato
                skinBraccioSinistroSelected = item;

            } //altrimenti, il materiale del set di braccia diventa trasparente
            else { newSharedMaterials[0] = transparentMaterial; newSharedMaterials[1] = transparentMaterial; Debug.Log("Reso trasparente braccia"); }
            //se esiste il pilota, lo disattiva
            if (pilotaPosition.transform.childCount > 0) { pilotaPosition.transform.GetChild(0).gameObject.SetActive(false); Debug.Log("Disattivato pilota"); }

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
                //fa diventare trasparenti tutti i pezzi della tuta
                for(int m = 0; m < newSharedMaterials.Length; m++) { newSharedMaterials[m] = transparentMaterial; }

                // inventory.pilotaMeshRenderer.material = item.itemSO.accessorioMaterial;
                //  inventory.pilotaMesh.transform.localScale = item.itemSO.pilota.transform.localScale;

            }

        }
        //mette i nuovi materiali allo skinned mesh della tuta
        skinnedTutaMesh.sharedMaterials = newSharedMaterials;

    }
    /// <summary>
    /// Finisce la preview della tuta nello shop(viene richiamato dal bottone "Back" del menù dello shop)
    /// </summary>
    public void StopShopPreview()
    {
        //disattiva la tuta in preview
        previewTutaPrefab.gameObject.SetActive(false);
        //e gli rida i materiali iniziali
        //for (int i = 0; i < startSharedMaterials.Count; i++) { skinnedTutaMesh.sharedMaterials[i] = startSharedMaterials[i]; }
        skinnedTutaMesh.sharedMaterials = startSharedMaterials;
        //infine, disattiva il pilota
        if (pilotaPosition.transform.childCount > 0) { pilotaPosition.transform.GetChild(0).gameObject.SetActive(false); }

    }

}
