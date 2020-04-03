using System.Collections;
using UnityEngine;
using static Item;

public class ShopController : MonoBehaviour
{
    // The value of this should be obtained from the General Controller
    [SerializeField]
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

            // TODO: Add item to inventory!

            // display panel
            shopView.GetComponent<ShopView>().openItemPurchasedPanel();
        }
    }
}
