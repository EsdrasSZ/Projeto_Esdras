using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEditor.SearchService.Scene;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject playerAnCameraPrefab;


    [SerializeField]
    private string locationToLoad;

    [SerializeField] private String guiScene;



    // Start is called before the first frame update
    void Start()
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
    
}
