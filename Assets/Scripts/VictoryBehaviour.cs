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
            if (player.GetComponent<PlayerInventory>().HasKey()  && (!anim.GetBool("Unlocking")))
            {
                // play animation and wait a few seconds
                StartCoroutine(UnlockDoorSequence());
                player.GetComponent<PlayerInventory>().ConsumeKey();
            }
        }
    }


    IEnumerator UnlockDoorSequence()
    {
        anim.SetBool("Unlocking", true);
        player.GetComponent<PlayerController>().SetControlsLocked(true);
        yield return new WaitForSeconds(2);
        locked = false;
        manager.GetComponent<GameManager>().AdvanceStage();
    }
}
