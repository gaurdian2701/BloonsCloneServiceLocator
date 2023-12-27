using ServiceLocator.Utilities;
using System.Collections;
using System.Collections.Generic;
using ServiceLocator.Player;
using ServiceLocator.Sound;
using UnityEngine;
using ServiceLocator.UI;
using System.Runtime.InteropServices.WindowsRuntime;

public class GameService : GenericMonoSingleton<GameService>
{
    [Header("PLAYER")]
    [SerializeField] private PlayerScriptableObject playerScriptableObject;

    [Header("SOUND")]
    [SerializeField] private SoundScriptableObject soundScriptableObject;
    [SerializeField] private AudioSource audioEffects;
    [SerializeField] private AudioSource backgroundMusic;
    public PlayerService playerService { get; private set; }
    public SoundService soundService { get; private set; }

    [Header("UI")]
    [SerializeField] private UIService uiService;
    public UIService UIService => uiService;

    private void Start()
    {
        playerService = new PlayerService(playerScriptableObject);
        soundService = new SoundService(soundScriptableObject, audioEffects, backgroundMusic);
    }

    private void Update()
    {
        playerService.Update();
    }
}

