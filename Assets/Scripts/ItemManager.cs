using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    private ArrayList disabled_items;

	// Use this for initialization
	void Start () {
        disabled_items = new ArrayList();
	}

    public void Enqueue(GameObject item)
    {
        disabled_items.Add(item);
    }

    public void Clear()
    {
        disabled_items.Clear();
    }

    public ArrayList GetDisabledItems()
    {
        return disabled_items;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
