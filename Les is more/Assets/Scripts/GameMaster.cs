using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    public float total_restart_count = 0;

    public string scene_name;
    public string start = "StartMenu";
    public string level_One = "Level_One";

    public bool found_players = false;

    public bool picked_les;
    public bool picked_sleepy;
    public bool picked_angry;

    public Player_health ph;
    public GameObject player;
    public GameObject player_child;

    public GameObject[] enemies;
    public Transform[] enemies_child;

    public Next_Level nl;
    public GameObject next_level;

    public Move_Camera mc;
    public GameObject main_camera;

    public StartMenu sm;
    public GameObject start_menu;

    public Collect_Coin[] cc;
    public GameObject[] collect_coin;

    public Collect_Health[] ch;
    public GameObject[] collect_health;

    public AudioSource end_game_music;

    public bool pause = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //if (instance != null)
        //{
        //    Debug.LogError("more than one build manager in scene");
        //    return;
        //}
        //instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {       
        StartCoroutine(Load_Start_Menu());
    }

    // Update is called once per frame
    void Update()
    {
        Scene current_scene = SceneManager.GetActiveScene();
        scene_name = current_scene.name;

        if (scene_name == "Level_One" && found_players == false)
        {
            FindLes();
            FindEnemies();
        }

        if (scene_name == "StartMenu")
        {
            Find_Start_Menu();
            found_players = false;
        }

        if (scene_name == "Level_One" && found_players == true)
        {
            Find_Collect_Coin();
            Find_Collect_Health();
            Find_Next_Level();
            Find_Main_Camera();

            if (ph.has_restarted)
            {
                nl.level_count = 1;
                ph.player_coin_count = 0;
                nl.current_coin_count = 0;
                nl.coin_count = 1;

                //TotalRestart();
            }

            if (nl.coin_count == 7)
            {
                //win screen?               
                SceneManager.LoadScene(start);
                nl.beat_game = true;
            }

            if (nl.beat_game)
            {
                end_game_music.Play();
            }
        }

        if (scene_name == "StartMenu")
        {
            if (sm.chose_les)
            {
                picked_les = true;
                picked_sleepy = false;
                picked_angry = false;

                if (sm.start_les)
                {
                    StartLes();
                }
            }

            if (sm.chose_sleepy)
            {
                picked_sleepy = true;
                picked_les = false;
                picked_angry = false;

                if (sm.start_les)
                {
                    StartLes();
                }
            }

            if (sm.chose_angry)
            {
                picked_angry = true;
                picked_les = false;
                picked_sleepy = false;

                if (sm.start_les)
                {
                    StartLes();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }

        if (pause == true)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                ResumeGame();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(start);
            }
        }
    }

    IEnumerator Load_Start_Menu()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            yield return 0;
        }
        //scene_name = start_menu;
       
        SceneManager.LoadScene(start);
        end_game_music.Play();
        //Scene current_scene = SceneManager.GetActiveScene();        
    }

    public void StartLes()
    {
        //scene_name = level_One;
        end_game_music.Stop();
        SceneManager.LoadScene(level_One);
    }

    void TotalRestart()
    {
        //this was an experiment to see if i could get data to persist between scenes 
        //total_restart_count++;
        //ph.respawn_enemies = true;
        //ph.has_restarted = false;
    }

    void FindLes()
    {
        found_players = true;    
        player = GameObject.Find("Players");

        if (picked_les)
        {
            player_child = player.transform.GetChild(0).gameObject;
            player_child.SetActive(true);
        }
        else if (picked_sleepy)
        {
            player_child = player.transform.GetChild(1).gameObject;
            player_child.SetActive(true);
        }
        else if (picked_angry)
        {
            player_child = player.transform.GetChild(2).gameObject;
            player_child.SetActive(true);
        }

        //finds parent object, need to find child and then be able to grab child component 
        ph = player_child.GetComponent<Player_health>();                               
    }

    void FindEnemies()
    {
        
        enemies = GameObject.FindGameObjectsWithTag("enemy");

        if (picked_les)
        {          
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].transform.GetChild(0).gameObject.SetActive(true);
            }

        }
        else if (picked_sleepy)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        else if (picked_angry)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].transform.GetChild(2).gameObject.SetActive(true);
            }
        }

    }

    void Find_Main_Camera()
    {
        found_players = true;
        main_camera = GameObject.Find("Main Camera");
        mc = main_camera.GetComponent<Move_Camera>();
        if (ph.has_restarted)
        {
            mc.x_axis_offset = 16f;
            mc.transform.position = new Vector3(0, 0, -10);
        }
    }

    void Find_Next_Level()
    {
        found_players = true;
        next_level = GameObject.Find("Goal");
        nl = next_level.GetComponent<Next_Level>();

        nl.level_count = 1;
        //nl.current_coin_count = 0;

        if (ph.has_restarted)
        {
            nl.first_offset = 24f;
            nl.transform.position = new Vector2(8, -.5f);
        }
    }

    void Find_Collect_Coin()
    {
        collect_coin = GameObject.FindGameObjectsWithTag("Coin");
        cc = new Collect_Coin[collect_coin.Length];
        for (int i = 0; i < collect_coin.Length; i++)
        {

            cc[i] = collect_coin[i].GetComponent<Collect_Coin>();
            cc[i].player = player_child;
            cc[i].ph = player_child.GetComponent<Player_health>();

        }
    }

    void Find_Collect_Health()
    {
        collect_health = GameObject.FindGameObjectsWithTag("Health");
        ch = new Collect_Health[collect_health.Length];
        for (int i = 0; i < collect_health.Length; i++)
        {

            ch[i] = collect_health[i].GetComponent<Collect_Health>();
            ch[i].player = player_child;
            ch[i].ph = player_child.GetComponent<Player_health>();
        }
    }

    void Find_Start_Menu()
    {
        start_menu = GameObject.Find("StartMenu");
        sm = start_menu.GetComponent<StartMenu>();
    }

    void PauseGame()
    {
        pause = true;
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        pause = false;
        Time.timeScale = 1;
    }

}

  