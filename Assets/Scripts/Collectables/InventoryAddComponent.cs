using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAddComponent : MonoBehaviour
{
    [SerializeField] private string _id;
    [SerializeField] private int _count;

    public void Add()
    {
       var hero = GameObject.FindGameObjectWithTag("Player").GetComponent<Hero>();
        if (hero != null)
            hero.AddInInventory(_id,_count);
    }
}
