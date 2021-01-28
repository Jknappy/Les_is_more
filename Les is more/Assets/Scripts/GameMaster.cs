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

    public GameObject enemies;
    public GameObject enemies_child;

    public Next_Level nl;
    public GameObject next_level;

    public Move_Camera mc;
    public GameObject main_camera;

    public StartMenu sm;
    public GameObject start_menu;

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
            Find_Next_Level();
            Find_Main_Camera();
        }

        if (scene_name == "StartMenu")
        {
            Find_Start_Menu();
            found_players = false;
        }

        if (scene_name == "Level_One" && found_players == true)
        {
            //if (ph.has_restarted)
            //{
            //    Debug.Log("game master sees you");
            //    TotalRestart();
            //}

            //if (ph.reached_goal)
            //{
            //    SceneManager.LoadScene(start_menu);
            //}
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
        //Scene current_scene = SceneManager.GetActiveScene();        
    }

    public void StartLes()
    {
        //scene_name = level_One;
        SceneManager.LoadScene(level_One);
    }

    void TotalRestart()
    {
        //this was an experiment to see if i could get data to persist between scenes 
        total_restart_count++;
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
        enemies = GameObject.Find("Enemies");

        if (picked_les)
        {
            enemies_child = enemies.transform.GetChild(0).gameObject;
            enemies_child.SetActive(true);
        }
        else if (picked_sleepy)
        {
            enemies_child = enemies.transform.GetChild(1).gameObject;
            enemies_child.SetActive(true);
        }
        else if (picked_angry)
        {
            enemies_child = enemies.transform.GetChild(2).gameObject;
            enemies_child.SetActive(true);
        }

    }

    void Find_Main_Camera()
    {
        found_players = true;
        main_camera = GameObject.Find("Main Camera");
        mc = main_camera.GetComponent<Move_Camera>();
    }

    void Find_Next_Level()
    {
        found_players = true;
        next_level = GameObject.Find("Goal");
        nl = next_level.GetComponent<Next_Level>();
    }

    void Find_Start_Menu()
    {
        start_menu = GameObject.Find("StartMenu");
        sm = start_menu.GetComponent<StartMenu>();
    }

}

//find all enemies in purgatory and call their respawn function 

// TO DO
// game manager does not get destroyed in between scenes 
// keep track of how many tries
// how many enemies defeated? 
// Did player reach goal, if so  
// Unlock next character
// Link button press with different character   