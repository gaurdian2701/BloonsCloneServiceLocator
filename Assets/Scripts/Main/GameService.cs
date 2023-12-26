using ServiceLocator.Utilities;
using System.Collections;
using System.Collections.Generic;
using ServiceLocator.Player;
using UnityEngine;


public class GameService : GenericMonoSingleton<GameService>
{
    public PlayerScriptableObject playerScriptableObject;
    public PlayerService playerService { get; private set; }

    private void Start()
    {
        playerService = new PlayerService(playerScriptableObject);
    }

    private void Update()
    {
        playerService.Update();
    }
}

