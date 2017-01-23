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
    public bool[] gravity_reversed;
    public AudioClip[] songs;

    public AudioSource sfx_source;
    public AudioClip[] sfx;

    public int first_map = 0;
    private int current_map = 0;

    private float[] song_times;

    private AudioSource audio;
    private Vector2 player_start_location;
    private bool game_clear;
    private bool player_gravity_reversed = false;
    private float player_start_gravity;

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
        current_map = first_map;
        audio = GetComponent<AudioSource>();
        song_times = new float[songs.Length];
        SwitchSong(current_map);
        player.transform.Translate(Vector3.right * GetMapOffsetToFirst(current_map).x);
        cam.transform.Translate(Vector3.right * GetMapOffsetToFirst(current_map).x);
        player_start_location = player.transform.position;
        player_start_gravity = player.GetComponent<Rigidbody2D>().gravityScale;
        SetMapGravity(first_map);
    }

    int GetNextMap()
    {
        return (current_map + 1) % maps.Length;
    }

    AudioClip GetNextSong()
    {
        return songs[GetNextMap()];
    }

    Vector2 GetMapOffset(int map)
    {
        return new Vector2(maps[map].transform.position.x - maps[current_map].transform.position.x,
                           maps[map].transform.position.y - maps[current_map].transform.position.y);
    }

       Vector2 GetMapOffsetToFirst(int map)
    {
        return new Vector2(maps[map].transform.position.x - maps[0].transform.position.x,
                           maps[map].transform.position.y - maps[0].transform.position.y);
    }


    //Update is called every frame.
    void Update()
	{
        if (game_clear) return;

		bool teleport = Input.GetKeyDown(KeyCode.Space);
        bool restart = Input.GetKeyDown(KeyCode.R);

        if (restart && !player.GetComponent<PlayerController>().IsWarping())
        {
            StartCoroutine(Restart());
            return;
        }

		if (teleport)
		{
            Vector2 next_offset = GetMapOffset(GetNextMap());
			Vector2 playerPos = player.transform.position;
				if (Physics2D.OverlapCircle (new Vector2 (playerPos.x + next_offset.x, playerPos.y + next_offset.y), 0.001f)) {
                    sfx_source.clip = sfx[4];
                    sfx_source.Play();
				} else if (!player.GetComponent<PlayerController>().IsWarping()){
                  StartCoroutine(SwitchMap(GetNextMap()));
            }
			}
	}

    public void CallSwitchMap(int map)
    {
        StartCoroutine(SwitchMap(map));
    }

    public IEnumerator SwitchMap(int map)
    {
        ZeroGravity();
        Vector2 offset = GetMapOffset(map);
        player.GetComponent<PlayerController>().SetWarping(true);
        GetComponent<CustomImageEffect>().FadeOut();
        yield return new WaitForSeconds(0.3f);
        cam.transform.Translate(Vector3.right * offset.x);
        player.transform.Translate(Vector3.right * offset.x);
        PlayWarpSound();
        SwitchSong(map);
        current_map = map;
        GetComponent<CustomImageEffect>().FadeIn();
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<PlayerController>().SetWarping(false);
        SetMapGravity(map);
    }

    void ZeroGravity()
    {
        player.GetComponent<Rigidbody2D>().gravityScale = 0f;
    }

    void SetMapGravity(int map)
    {
        if (gravity_reversed[map] && player.GetComponent<Rigidbody2D>().gravityScale > 0 || !gravity_reversed[map] && player.GetComponent<Rigidbody2D>().gravityScale < 0)
        {
            player_gravity_reversed = !player_gravity_reversed;
            player.GetComponent<Rigidbody2D>().gravityScale *= -1;
            player.GetComponent<PlayerController>().FlipY();
        }
        if (player.GetComponent<Rigidbody2D>().gravityScale == 0)
        {
            player.GetComponent<Rigidbody2D>().gravityScale = (gravity_reversed[map]) ? -player_start_gravity : player_start_gravity;
        }
        if (player.GetComponent<Rigidbody2D>().gravityScale < 0 && player.GetComponent<PlayerController>().IsFacingUp() || player.GetComponent<Rigidbody2D>().gravityScale > 0 && !player.GetComponent<PlayerController>().IsFacingUp())
        {
            player.GetComponent<PlayerController>().FlipY();
        } 
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void CallRestart()
    {
        StartCoroutine(Restart());
    }


    void ResetItems()
    {
        player.GetComponent<PlayerInventory>().Clear();
        ArrayList items = GetComponent<ItemManager>().GetDisabledItems();
        for (int i = 0; i < items.Count; i++)
        {
            GameObject obj = (GameObject)items[i];
            obj.SetActive(true);
        }
        GetComponent<ItemManager>().Clear();
    }    

    IEnumerator Restart()
    {
        SetMapGravity(first_map);
        ResetItems();
        Vector2 offset = GetMapOffset(first_map);
        player.GetComponent<PlayerController>().SetWarping(true);
        GetComponent<CustomImageEffect>().FadeOut();
        yield return new WaitForSeconds(0.3f);
        cam.transform.Translate(Vector3.right * offset.x);
        player.transform.position = player_start_location;
        PlayDeathSound();
        SwitchSong(first_map);
        current_map = first_map;
        GetComponent<CustomImageEffect>().FadeIn();
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<PlayerController>().SetWarping(false);
    }
    
    public void PlayWarpSound()
    {
        // toggle between two sound effects
        if (sfx_source.clip == sfx[0])
        {
            sfx_source.clip = sfx[1];
        }
        else
        {
            sfx_source.clip = sfx[0];
        }
        sfx_source.Play();
    }

    public void PlayVictorySound()
    {
        sfx_source.clip = sfx[2];
        sfx_source.Play();
    }

    public void PlayDeathSound()
    {
        sfx_source.clip = sfx[5];
        sfx_source.Play();
    }

    public void PlayItemGetSound()
    {
        sfx_source.clip = sfx[3];
        sfx_source.Play();
    }

    void SwitchSong(int map)
    {
        song_times[current_map] = audio.time;
        audio.clip = songs[map];
        audio.time = song_times[map];
        audio.Play();
    }

    public void AdvanceStage()
    {
        SceneManager.LoadScene(nextStage.name);
    }

    public void SetGameClear(bool state)
    {
        game_clear = state;
    }

    public bool GameIsClear()
    {
        return game_clear;
    }

    public Vector2 GetPlayerStartPos()
    {
        return player_start_location;
    }
}