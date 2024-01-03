using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Utilities;
using ServiceLocator.Sound;
using ServiceLocator.Wave;
using ServiceLocator.Player;

/*  This script demonstrates the implementation of Object Pool design pattern.
 *  If you're interested in learning about Object Pooling, you can find
 *  a dedicated course on Outscal's website.
 *  Link: https://outscal.com/courses
 * */

namespace ServiceLocator.Wave.Bloon
{
    public class BloonPool : GenericObjectPool<BloonController>
    {
        private BloonView bloonPrefab;
        private List<BloonScriptableObject> bloonScriptableObjects;
        private Transform bloonContainer;

        private SoundService soundService;
        private WaveService waveService;
        private PlayerService playerService;

        public BloonPool(WaveScriptableObject waveScriptableObject, SoundService soundService, WaveService waveService, PlayerService playerService)
        {
            this.bloonPrefab = waveScriptableObject.BloonPrefab;
            this.bloonScriptableObjects = waveScriptableObject.BloonScriptableObjects;
            bloonContainer = new GameObject("Bloon Container").transform;

            this.soundService = soundService;
            this.waveService = waveService;
            this.playerService = playerService;
        }

        public BloonController GetBloon(BloonType bloonType)
        {
            BloonController bloon = GetItem();
            BloonScriptableObject scriptableObjectToUse = bloonScriptableObjects.Find(so => so.Type == bloonType);
            bloon.Init(scriptableObjectToUse);
            return bloon;
        }

        protected override BloonController CreateItem() => new BloonController(bloonPrefab, bloonContainer, soundService, waveService, playerService);
    }
}