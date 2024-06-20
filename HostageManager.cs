using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageManager : MonoBehaviour
{
    public static HostageManager Instance { get; set; }
    public GameObject Hostage; 

    private void Start()
    {
       
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
           // Destroy(gameObject);

        }
        else
        {
            Instance = this;
        }
    }


    

}
