using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using static Item;

public class ShopView : MonoBehaviour
{

    public GameObject resourceText;
    public GameObject shopController;

    public GameObject itemPanel;
    public GameObject itemNameText;
    public GameObject itemCostText;
    public GameObject itemDescriptionText;

    public GameObject insufficientPanel;
    public GameObject purchasedPanel;
    public GameObject alreadyPurchasedPanel;

    public GameObject chonkerSprite;


    private bool closeButtonClicked = false;

    void Start() {
        if (PlayerPrefs.HasKey("looks")) {
            // load new sprite
            int lookInt = PlayerPrefs.GetInt("looks");

        // Added in
         if(!PlayerPrefs.HasKey("BlackCat"))
            PlayerPrefs.SetInt("BlackCat", 0); 

         if(!PlayerPrefs.HasKey("Hat"))
            PlayerPrefs.SetInt("Hat", 0);  

        if(!PlayerPrefs.HasKey("Ball"))
            PlayerPrefs.SetInt("Ball", 0);

        if(!PlayerPrefs.HasKey("FireBall"))
            PlayerPrefs.SetInt("FireBall", 0);

         if(!PlayerPrefs.HasKey("Fish"))
            PlayerPrefs.SetInt("Fish", 0);  

        }
    }

    // Update is called once per frame    
    void Update() {
        // Update resource text from shop controller
        resourceText.GetComponent<Text>().text = "" + shopController.GetComponent<ShopController>().resource;

        // Register mouse clicks on shop items
        if (closeButtonClicked)
            closeButtonClicked = false;
        else if (Input.GetMouseButtonUp(0) && !existActivePanel() /* This checks if the panel is active, we don't want to register mouseclicks if so*/ ) {
            Vector2 v = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(v), Vector2.zero);
            
            if (hit) {
                Item selectedItem = shopController.GetComponent<ShopController>().getItemInfo(hit.collider.gameObject.name);
                openItemInfoPanel(selectedItem);
            }
        }
    }

    // was originally displayItemInfo
    public void openItemInfoPanel(Item selectedItem) {
        itemPanel.SetActive(true);
        
        if (itemPanel.activeInHierarchy) {
            itemNameText.GetComponent<Text>().text = selectedItem.name;
            itemCostText.GetComponent<Text>().text = "" + selectedItem.resourceCost;
            itemDescriptionText.GetComponent<Text>().text = selectedItem.description;
        }
    }

    public void openInsufficientPanel() {
        insufficientPanel.SetActive(true);
    }

    public void openItemPurchasedPanel() {
        purchasedPanel.SetActive(true);
    }

    public void openAlreadyPurchasedPanel() {
        alreadyPurchasedPanel.SetActive(true);
    }

    public void closePanel(GameObject panel) {
        // Debug.Log("close button clicked");
        closeButtonClicked = true;
        panel.SetActive(false);
    }

    public void purchaseItem() {
        // Debug.Log("purchase button clicked");
        itemPanel.SetActive(false);
        shopController.GetComponent<ShopController>().purchaseItem();
    }

    private bool existActivePanel() {
        return itemPanel.activeInHierarchy || insufficientPanel.activeInHierarchy || purchasedPanel.activeInHierarchy;
    }
}
