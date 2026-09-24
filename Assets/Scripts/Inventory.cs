using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string id;
    public int count;
}

public class Inventory : MonoBehaviour
{
    public int maxWeight = 100;
    public List<Item> items = new();

    public void Add(string id, int amount = 1)
    {
        var it = items.Find(x => x.id == id);
        if (it != null) it.count += amount;
        else items.Add(new Item { id = id, count = amount });
        Debug.Log($"+ {amount}x {id}");
    }

    public bool Has(string id, int amount = 1)
    {
        var it = items.Find(x => x.id == id);
        return it != null && it.count >= amount;
    }

    public bool Consume(string id, int amount = 1)
    {
        var it = items.Find(x => x.id == id);
        if (it == null || it.count < amount) return false;
        it.count -= amount;
        if (it.count <= 0) items.Remove(it);
        return true;
    }

    public bool TryCraft(string product)
    {
        switch (product)
        {
            case "axe":
                if (Has("stone", 2) && Has("fiber", 3) && Has("wood", 1))
                { Consume("stone", 2); Consume("fiber", 3); Consume("wood", 1); Add("axe", 1); return true; }
                break;
            case "campfire":
                if (Has("wood", 5) && Has("stone", 3))
                { Consume("wood", 5); Consume("stone", 3); Add("campfire", 1); return true; }
                break;
            case "spear":
                if (Has("wood", 2) && Has("stone", 1) && Has("fiber", 2))
                { Consume("wood", 2); Consume("stone", 1); Consume("fiber", 2); Add("spear", 1); return true; }
                break;
        }
        Debug.Log($"Craft başarısız: {product} - malzeme yetersiz");
        return false;
    }
}
