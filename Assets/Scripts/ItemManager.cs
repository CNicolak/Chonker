using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;

public static class ItemManager {
    public static readonly Dictionary<string, Item> itemList =
        new Dictionary<string, Item>
            { 
                { "Fish", new Food(50, "Fish", "Yummy fish that Chonker requires to survive") },
                { "Ball", new Toy(10000, "Ball", "A simple light-coloured ball for Chonker to play with") },
                { "Fireball", new Accessory(100000, "Fireball", "Watch Chonker burn the world...") },
                { "Hat", new Cosmetic(5000, "Hat", "A cute hat for Chonker") },
                { "BlackCat", new Cosmetic(5000, "BlackCat", "A black cosmetic skin for Chonker") }
            };

    public static Item getItem(string itemName) {
        return itemList[itemName];
    }
}
