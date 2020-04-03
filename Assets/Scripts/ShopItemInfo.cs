using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;

public class ShopItemInfo : MonoBehaviour
{
    private Dictionary<string, Item> shopInventory;
    // Start is called before the first frame update
    void Start() {
        // find a way to persist this, such that it is not called everytime the scene is created?
        shopInventory = new Dictionary<string, Item>();
        shopInventory.Add("Fish", new Food(50, "Fish", "Yummy fish that Chonker requires to survive"));
        shopInventory.Add("Ball", new Toy(300, "Ball", "A simple light-coloured ball for Chonker to play with"));
        shopInventory.Add("Fireball", new Cosmetic(100000, "Fireball", "Watch Chonker burn the world...")); 
    }

    public Item getItem(string itemName) {
        return shopInventory[itemName];
    }
}
