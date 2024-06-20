using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioSource ShootingChannel;

    public AudioClip P1911Shot;
    public AudioClip MCXShot;
    

    public AudioSource reloadingSound1911;
    public AudioSource reloadingSoundMCX;
    
    public AudioSource emptyMagazinesound1911;

    public AudioClip enemyHurt;
    public AudioClip enemyDeath;

    public AudioSource enemyChannel;

    public AudioSource PlayerChannel;
    public AudioClip playerHurt;
    public AudioClip playerDie;


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

    public void PlayShootingSound(WeaponModel weapon)
    {
        switch(weapon)
        {
            case WeaponModel.Pistol_1911:
                ShootingChannel.PlayOneShot(P1911Shot);
                break;
            case WeaponModel.MCX:
                ShootingChannel.PlayOneShot(MCXShot);
                break;
        }
    }


    public void PlayReloadSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Pistol_1911:
                reloadingSound1911.Play();
                break;
            case WeaponModel.MCX:
                reloadingSoundMCX.Play();
                break;
        }
    }
}
