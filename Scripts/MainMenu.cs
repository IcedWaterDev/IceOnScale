using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string playButtonDirection;

    public void StartGame()
    {
        SceneManager.LoadScene(playButtonDirection);
    }
}
