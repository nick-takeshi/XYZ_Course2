using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickInventoryController : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private InnventoryItemVidget _prefab;

    private readonly CompositeDisposable _trash = new CompositeDisposable();

    private GameSession _session;
    private List<InnventoryItemVidget> _createdItem = new List<InnventoryItemVidget>();
    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        _trash.Retain(_session.QuickInventory.Subscribe(Rebuild));
        Rebuild();
    }

    public void Rebuild()
    {
        var _inventory = _session.QuickInventory.Inventory;

        // create require items

        for (var i = _createdItem.Count; i < _inventory.Length; i++)
        {
            var item = Instantiate(_prefab, _container);
            _createdItem.Add(item);
        }

        // update data and activate

        for (var i = 0; i < _inventory.Length; i++)
        {
            _createdItem[i].SetData(_inventory[i], i);
            _createdItem[i].gameObject.SetActive(true);
        }

        //hide unused items
        for (var i = _inventory.Length; i < _createdItem.Count; i++)
        {
            _createdItem[i].gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        _trash.Dispose();
    }
}
