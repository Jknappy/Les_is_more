using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    public float total_restart_count = 0;

    public string scene_name;
    public string start_menu = "StartMenu";
    public string level_One = "Level_One";

    public bool found_les = false;

    public Player_health ph;
    public GameObject go;

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

        if (scene_name == "Level_One" && found_les == false)
        {
            FindLes();
        }

        if (scene_name == "StartMenu")
        {
            found_les = false;
        }

        if (found_les)
        {
            if (ph.has_restarted)
            {
                Debug.Log("game master sees you");
                TotalRestart();
            }

            if (ph.reached_goal)
            {
                SceneManager.LoadScene(start_menu);
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
       
        SceneManager.LoadScene(start_menu);
        //Scene current_scene = SceneManager.GetActiveScene();        
    }

    void StartLes()
    {
        //scene_name = level_One;
        SceneManager.LoadScene(level_One);
    }

    void TotalRestart()
    {
        //this was an experiment to see if i could get data to persist between scenes 
        total_restart_count++;
        ph.has_restarted = false;
    }

    void FindLes()
    {
        found_les = true;    
        go = GameObject.Find("Les");
        ph = go.GetComponent<Player_health>();
                               
    }
}

// TO DO
// game manager does not get destroyed in between scenes 
// keep track of how many tries
// how many enemies defeated? 
// Did player reach goal, if so  
// Unlock next character
// Link button press with different character   