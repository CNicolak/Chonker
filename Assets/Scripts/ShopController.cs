using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Item;

public class ShopController : MonoBehaviour
{
    // The value of this should be obtained from the General Controller
    private int _resource;

    public GameObject shopView;
    public GameObject shopItemInfo;

    // stores currently selected item
    private Item _selectedItem;

    public int resource {
        get { return _resource; }
        set { _resource = value; }
    }

    public Item selectedItem {
        get { return _selectedItem; }
        set { _selectedItem = value; }
    }

    // Start is called before the first frame update
    void Start() {
        resource = PlayerPrefs.GetInt("currency");
    }

    // Update is called once per frame
    void Update() {
        
    }

    public Item getItemInfo(string itemName) {
        selectedItem = shopItemInfo.GetComponent<ShopItemInfo>().getItem(itemName);
        if (selectedItem != null) return selectedItem;

        // somehow the item is not in the shop. return a placeholder.
        else return new Cosmetic(0, "Item Not Found", "Your item is missing!");
    }

    public void purchaseItem() {
        if (resource < selectedItem.resourceCost) {
            // insufficient
            shopView.GetComponent<ShopView>().openInsufficientPanel();
        }
        else {
            // update resource, display success panel
            resource -= selectedItem.resourceCost;

            // Add item to inventory
            if (!PlayerPrefs.HasKey(selectedItem.name))
                PlayerPrefs.SetInt(selectedItem.name, 1);
            else
                PlayerPrefs.SetInt(selectedItem.name, PlayerPrefs.GetInt(selectedItem.name) + 1);
            
            // display panel
            shopView.GetComponent<ShopView>().openItemPurchasedPanel();
        }
    }

    public void quitShop() {
        PlayerPrefs.SetInt("currency", resource);
        SceneManager.LoadScene("Game");
    }
}
