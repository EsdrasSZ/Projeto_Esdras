using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEditor.SearchService.Scene;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] 
    private GameObject playerAnCameraPrefab;

    [SerializeField]
    private string locationToLoad;

    [SerializeField] 
    private String guiScene;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Initialization")
        {
            StartGameFromInitialization();
        } 
        else
        {
            StartGameFromLevel();
        }
    }

    private void StartGameFromLevel()
    {
        SceneManager.LoadScene(guiScene, LoadSceneMode.Additive);
        
        Vector3 starPosition = GameObject.Find("PlayerStart").transform.position;

        Instantiate(playerAnCameraPrefab, starPosition, Quaternion.identity);
    }

    private void StartGameFromInitialization()
    {
        SceneManager.LoadScene("Splash");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    // Start is called before the first frame update
    public void StartGame()
    {
    
        DontDestroyOnLoad(this.gameObject);

        SceneManager.LoadScene(guiScene);
        //SceneManager.LoadScene(locationToLoad, LoadSceneMode.Additive);

        SceneManager.LoadSceneAsync(locationToLoad, LoadSceneMode.Additive).completed += operation =>
        {
            UnityEngine.SceneManagement.Scene locationScene = default;

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name == locationToLoad)
                {
                    locationScene = SceneManager.GetSceneAt(i);
                    break;
                }
            }

            if (locationScene != default) SceneManager.SetActiveScene(locationScene);
            
            Vector3 starPosition = GameObject.Find("PlayerStart").transform.position;

            Instantiate(playerAnCameraPrefab, starPosition, Quaternion.identity);
        };
            
       

    }

    public void CallVictory()
    {
        SceneManager.LoadScene("Victory", LoadSceneMode.Additive);
    }

    public void CallgameOver()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
    }

    public void LoadEnding()
    {
        SceneManager.LoadScene("Ending");
    }
    
}
