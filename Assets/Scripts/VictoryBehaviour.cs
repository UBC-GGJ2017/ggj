using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryBehaviour : MonoBehaviour {

    public GameObject manager;
    public GameObject player;

    Animator anim;
    public bool locked;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public bool IsLocked()
    {
        return locked;
    }

    public void SetLocked(bool state)
    {
        locked = state;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!locked)
        {
            manager.GetComponent<GameManager>().AdvanceStage();
        } else
        {
            if (player.GetComponent<PlayerInventory>().HasKey())
            {
                // play animation and wait a few seconds
                StartCoroutine(UnlockDoorSequence());
            }
        }
    }


    IEnumerator UnlockDoorSequence()
    {
        anim.SetBool("Unlocking", true);
        yield return new WaitForSeconds(2);
        locked = false;
    }
}
