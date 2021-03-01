 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject startwindows;
    
    /*// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/

    public void PlayGame()
    {
        startwindows.SetActive(true);
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
