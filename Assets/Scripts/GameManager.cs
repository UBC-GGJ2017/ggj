using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

using System.Collections.Generic;       //Allows us to use Lists. 

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;

    public Object nextStage;

    public GameObject[] maps;
    public int current_map = 0;

    private Vector2 next_offset;

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
        player.transform.Translate(Vector3.right * GetMapOffsetToFirst(current_map).x);
        cam.transform.Translate(Vector3.right * GetMapOffsetToFirst(current_map).x);
    }

    int GetNextMap()
    {
        return (current_map + 1) % maps.Length;
    }

    void CalculateNextMapOffset()
    {
        next_offset = new Vector2(maps[GetNextMap()].transform.position.x - maps[current_map].transform.position.x,
                           maps[GetNextMap()].transform.position.y - maps[current_map].transform.position.y);
    }

       Vector2 GetMapOffsetToFirst(int map)
    {
        return new Vector2(maps[map].transform.position.x - maps[0].transform.position.x,
                           maps[map].transform.position.y - maps[0].transform.position.y);
    }


    //Update is called every frame.
    void Update()
	{
        Debug.Log(cam.transform.position.x);
		bool teleport = Input.GetKeyDown(KeyCode.Space);

		if (teleport)
		{
            CalculateNextMapOffset();
			Vector2 playerPos = player.transform.position;
				if (Physics2D.OverlapCircle (new Vector2 (playerPos.x + next_offset.x, playerPos.y + next_offset.y), 0.001f)) {
					print ("collided");
				} else if (!player.GetComponent<PlayerController>().IsWarping()){
                  StartCoroutine(SwitchMap());
            }
			}
	}

    IEnumerator SwitchMap()
    {
        player.GetComponent<PlayerController>().SetWarping(true);
        GetComponent<CustomImageEffect>().FadeOut();
        yield return new WaitForSeconds(0.3f);
        cam.transform.Translate(Vector3.right * next_offset.x);
        player.transform.Translate(Vector3.right * next_offset.x);
        current_map = GetNextMap();
        GetComponent<CustomImageEffect>().FadeIn();
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<PlayerController>().SetWarping(false);
    }

    public void AdvanceStage()
    {
        SceneManager.LoadScene(nextStage.name);
    }
}