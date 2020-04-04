using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item {
    protected int _resourceCost;
    protected string _name;

    protected string _description;

    public abstract int resourceCost {
        get; set;
    }

    public abstract string name {
        get; set;
    }

    public abstract string description {
        get; set;
    }

    // effect to trigger when the item is used on Chonker
    public abstract void useItem();
}

public class Food : Item {
    public Food(int cost, string s, string desc) {
        resourceCost = cost;
        name = s;
        description = desc;
    }

    public override int resourceCost {
        get { return _resourceCost; }
        set { _resourceCost = value; }
    }

    public override string name {
        get { return _name; }
        set { _name = value; }
    }

    public override string description {
        get { return _description; }
        set { _description = value; }
    }

    public override void useItem() {
        // chonker eats!
    }
}

public class Toy : Item {
    public Toy(int cost, string s, string desc) {
        resourceCost = cost;
        name = s;
        description = desc;
    }

    public override int resourceCost {
        get { return _resourceCost; }
        set { _resourceCost = value; }
    }

    public override string name {
        get { return _name; }
        set { _name = value; }
    }

    public override string description {
        get { return _description; }
        set { _description = value; }
    }

    public override void useItem() {
        // chonker puts on the item!
    }
}

public class Cosmetic : Item {
    public Cosmetic(int cost, string s, string desc) {
        resourceCost = cost;
        name = s;
        description = desc;
    }

    public override int resourceCost {
        get { return _resourceCost; }
        set { _resourceCost = value; }
    }

    public override string name {
        get { return _name; }
        set { _name = value; }
    }

    public override string description {
        get { return _description; }
        set { _description = value; }
    }

    public override void useItem() {
        // chonker puts on the item!
    }
}

public class Accessory : Item {
    public Accessory(int cost, string s, string desc) {
        resourceCost = cost;
        name = s;
        description = desc;
    }

    public override int resourceCost {
        get { return _resourceCost; }
        set { _resourceCost = value; }
    }

    public override string name {
        get { return _name; }
        set { _name = value; }
    }

    public override string description {
        get { return _description; }
        set { _description = value; }
    }

    public override void useItem() {
        // chonker puts on the item!
    }
}