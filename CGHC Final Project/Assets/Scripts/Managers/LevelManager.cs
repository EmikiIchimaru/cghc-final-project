﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{   
    public static Action<PlayerMotor> OnPlayerSpawn;
    
    [Header("Settings")]
    public Transform levelStartPoint; 
    public Area currentArea;
    public Area[] allAreas;
    private int areaIndex = 0;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject deathFX;

    private AudioManager audio;

    private PlayerMotor _currentPlayer;

    private void Awake()
    {        
        SpawnPlayer(playerPrefab);
        audio = AudioManager.Instance;
    }

    private void Update()
    {
        /* if (Input.GetKeyDown(KeyCode.R))
	    {
            RevivePlayer();
        } */
        //in the event of physics bug, destroying the player and respawning solves the bug, but not reviving

        if (Input.GetKeyDown(KeyCode.R))
	    {
            Destroy(_currentPlayer.gameObject);
            SpawnPlayer(playerPrefab);
        }

        //press N to skip to next checkpoint
        if (Input.GetKeyDown(KeyCode.N))
	    {
            areaIndex++;
            levelStartPoint = allAreas[areaIndex].checkpoint.transform;
            Destroy(_currentPlayer.gameObject);
            SpawnPlayer(playerPrefab);
        }
    }

    // Spawns our player in the spawnPoint   
    private void SpawnPlayer(GameObject player)
    {
        if (player != null)
        {
            _currentPlayer = Instantiate(player, levelStartPoint.position, Quaternion.identity).GetComponent<PlayerMotor>();
            _currentPlayer.GetComponent<Health>().ResetLife();
            OnPlayerSpawn?.Invoke(_currentPlayer);
        }
    }

    // Revives our player
    private void RevivePlayer()
    {
        if (_currentPlayer != null)
        {
            _currentPlayer.gameObject.SetActive(true);
            _currentPlayer.SpawnPlayer(levelStartPoint);
            _currentPlayer.GetComponent<Health>().ResetLife(); 
            _currentPlayer.GetComponent<PlayerController>().SetHorizontalForce(0f);
            _currentPlayer.GetComponent<PlayerController>().SetVerticalForce(0f);          
        }

        if (currentArea != null)
        {
            currentArea.ResetArea();
        }
    }

    private void PlayerDeath(PlayerMotor player)
    {
        //_currentPlayer = player;
        _currentPlayer.gameObject.SetActive(false);
        GameObject dfx = Instantiate(deathFX, _currentPlayer.gameObject.transform.position, Quaternion.identity);
        if (audio != null)
        {
            audio.Play("death");
        }
        Destroy(dfx, 2f);
        Invoke("RevivePlayer",2f);
    }

    private void OnEnable()
    {
        Health.OnDeath += PlayerDeath;       
    }

    private void OnDisable()
    {
        Health.OnDeath -= PlayerDeath;
    }
    public void UpdateSpawnPoint(Vector3 newSpawnPoint)
    {
        // Update the spawn point to the provided position
        levelStartPoint.position = newSpawnPoint;
    }
}
