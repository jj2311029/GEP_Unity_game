using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public Player player;

    public HostageManager Hostage;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);

        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;
            
            if (objectHitByRaycast.GetComponent<HostageManager>())
            {
                Hostage = objectHitByRaycast.gameObject.GetComponent<HostageManager>();
                
                
                if (Input.GetKeyDown(KeyCode.F))
                {
                    player.setHostageSave(true);
                    Destroy(objectHitByRaycast.gameObject);
                   
                }

            }
        }
    }




}


