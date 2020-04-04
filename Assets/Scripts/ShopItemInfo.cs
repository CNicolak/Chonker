using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;
using static ItemManager;
public class ShopItemInfo : MonoBehaviour
{
    private Dictionary<string, Item> shopInventory = ItemManager.itemList;
    // Start is called before the first frame update

    public Item getItem(string itemName) {
        return shopInventory[itemName];
    }
}
