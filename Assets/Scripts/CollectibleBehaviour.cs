using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour {

    public GameObject manager;
    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        manager.GetComponent<GameManager>().PlayItemGetSound();
        Destroy(gameObject);
        player.GetComponent<PlayerInventory>().Collect(PlayerInventory.ITEM_TYPES.KEY_BASIC);
    }
}
