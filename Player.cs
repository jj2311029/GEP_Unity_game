using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int HP = 100;
    public GameObject bloodyScreen;

    public TextMeshProUGUI playerHealthUI;
    public GameObject GameOverUI;

    public bool isDead=false;
    public bool HostageSave = false;
    public void setHostageSave(bool save)
    {
        HostageSave = save;
    }


    private void Start()
    {
        //playerHealthUI.text = $"Health: {HP}";
        
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            print("player Dead");
            PlayerDead();
            isDead = true;
        }
        else
        {
            print("player Hit");
            StartCoroutine(BloodyScreenEffect());
            playerHealthUI.text = $"Health: {HP}";
            SoundManager.Instance.PlayerChannel.PlayOneShot(SoundManager.Instance.playerHurt);
        }
    }

    private void PlayerDead()
    {
        SoundManager.Instance.PlayerChannel.PlayOneShot(SoundManager.Instance.playerDie);
        
        GetComponent<PlayerMove>().enabled = false;

        GetComponentInChildren<Animator>().enabled = true;
        playerHealthUI.gameObject.SetActive(false);

        GetComponent<ScreenFader>().StartFade();
        StartCoroutine(ShowGameOverUI());
    }

    private IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(1f);
        GameOverUI.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("LoseScene");
    }


    private IEnumerator BloodyScreenEffect()
    {
        if(bloodyScreen.activeInHierarchy==false)
        {
            bloodyScreen.SetActive(true);
        }

        //yield return new WaitForSeconds(4f);

        var image = bloodyScreen.GetComponentInChildren<Image>();

        // Set the initial alpha value to 1 (fully visible).
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the new alpha value using Lerp.
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            // Update the color with the new alpha value.
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;

            yield return null; ; // Wait for the next frame.
        }

        if (bloodyScreen.activeInHierarchy)
        {
            bloodyScreen.SetActive(false);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyHand")) 
        {
            if (isDead == false)
            {
                TakeDamage(other.gameObject.GetComponent<EnemyHand>().damage);
            }
            
        }
        if(other.CompareTag("EndPoint"))
        {
            if(HostageSave)
            {
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("Complete Scene");
            }
            
        }
    }

}
