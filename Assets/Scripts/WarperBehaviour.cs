using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarperBehaviour : MonoBehaviour {

    public GameObject manager;
    public GameObject player;
    public int warp_map;
    public float delay;

    // Use this for initialization
    void Start () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        manager.GetComponent<GameManager>().PlayItemGetSound();
        if (!player.GetComponent<PlayerController>().IsWarping())
        StartCoroutine(Wait(delay));
        manager.GetComponent<GameManager>().CallSwitchMap(warp_map);
        StartCoroutine(Wait(1.0f));
        Destroy(gameObject);
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
