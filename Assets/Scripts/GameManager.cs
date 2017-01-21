using UnityEngine;
using System.Collections;

using System.Collections.Generic;       //Allows us to use Lists. 

public class GameManager : MonoBehaviour
{

	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
//	toggle to be removed once it is accurate
	public int xdelta; 
	private int mapNumber = 1;

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

	//Update is called every frame.
	void Update()
	{
		bool teleport = Input.GetKeyDown(KeyCode.Space);
//		bool raycast = Input.GetKeyDown (KeyCode.Space);
//		if (raycast) {
//			var player = GameObject.FindWithTag ("Player");
//
//			Debug.DrawLine(player.transform.position, player.transform.position + Vector3.right, Color.white, 10);
//		}
		if (teleport)
		{
			var camera = GameObject.FindWithTag ("MainCamera");
			var player = GameObject.FindWithTag ("Player");
			Vector2 playerPos = player.transform.position;
//			print (string.Format("{0}, {1}", camera.transform.position.x, camera.transform.position.y));
			if (mapNumber == 1) {
				Debug.DrawRay (player.transform.position +  Vector3.right * xdelta, Vector3.right, Color.white, 10f);
				if (Physics2D.Raycast(playerPos +  Vector2.right * xdelta, Vector2.right, 10f))
					print("There is something in front of the object!");
				
				camera.transform.Translate (Vector3.right * xdelta);
				player.transform.Translate (Vector3.right * xdelta);
				mapNumber = 2;
			} else if (mapNumber == 2) {
				Debug.DrawRay (playerPos +  Vector2.left * xdelta, Vector2.left, Color.white, 10f);
				if (Physics2D.Raycast(playerPos +  Vector2.left * xdelta, Vector2.left, 10f))
					print("There is something in front of the object!");
				
				camera.transform.Translate (Vector3.left * xdelta);
				player.transform.Translate (Vector3.left * xdelta);
				mapNumber = 1;
			}
		}
	}
}