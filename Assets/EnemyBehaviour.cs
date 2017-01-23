using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public GameObject manager;
    public GameObject player;

    // Use this for initialization
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        player.transform.position = manager.GetComponent<GameManager>().GetPlayerStartPos();
        manager.GetComponent<GameManager>().CallRestart();
    }
}
