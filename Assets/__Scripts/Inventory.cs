using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        if(instance !=null)
        {
            Debug.LogWarning("More than one instance of inventory found!");
            return;
        }

        instance = this;
    }
    #endregion

    public int space = 20;

    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if(!item.isDefaultItem) {
            if(items.Count >= space)
            {
                Debug.Log("Not enough room in inventory");
                return false;
            }
            items.Add(item);
        }
        return true;
    }
    public void Remove(Item item)
    {
        items.Remove(item);
    }
}

