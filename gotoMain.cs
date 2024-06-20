using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class gotoMain : MonoBehaviour
{
    public void Go_to_lobby() 
    { 
        SceneManager.LoadScene("Lobby Scene");
        Debug.Log("change");
    }
    public void Restart()
    {
        SceneManager.LoadScene("Game Scene");
        Debug.Log("change");
    }
}
