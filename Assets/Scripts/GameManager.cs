using UnityEngine;
using System.Collections;

using System.Collections.Generic;       //Allows us to use Lists. 

public class GameManager : MonoBehaviour
{
    public GameObject[] maps;
    public int current_map = 0;

	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
//	toggle to be removed once it is accurate

	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    

		//Sets this to not be destroyed when reloading scene
		//DontDestroyOnLoad(gameObject);

		//Call the InitGame function to initialize the first level 
		InitGame();
	}

	//Initializes the game for each level.
	void InitGame()
	{
		

	}

    int GetNextMap()
    {
        return (current_map + 1) % maps.Length;
    }

    float GetNextMapOffset()
    {
        return maps[GetNextMap()].transform.position.x - maps[current_map].transform.position.x;
    }


	//Update is called every frame.
	void Update()
	{
		bool teleport = Input.GetKeyDown(KeyCode.Space);

		if (teleport)
		{
            float xdelta = GetNextMapOffset();
            var camera = GameObject.FindWithTag ("MainCamera");
			var player = GameObject.FindWithTag ("Player");
			Vector2 playerPos = player.transform.position;
//			print (string.Format("{0}, {1}", camera.transform.position.x, camera.transform.position.y));
				if (Physics2D.OverlapCircle (new Vector2 (playerPos.x + xdelta, playerPos.y), 0.001f)) {
					print ("collided");
				} else {
                    camera.transform.Translate (Vector3.right * xdelta);
					player.transform.Translate (Vector3.right * xdelta);
                    current_map = GetNextMap();
				}
			}
	}
}