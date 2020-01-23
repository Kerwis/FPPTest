using System;
using System.Collections.Generic;
using UnityEngine;
using Upgrades;
using Random = UnityEngine.Random;
using Type = Upgrades.Type;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static bool isResume;
        public List<Platform.Platform> lowPlatforms = new List<Platform.Platform>();
        public List<Platform.Platform> highPlatforms = new List<Platform.Platform>();
        [SerializeField] private Upgrades.PowerUp dobuleJumpPrefab;
        [SerializeField] private Upgrades.PowerUp sprintPrefab;
        private SaveManager saveManager = new SaveManager();
        private string nickname;
        private readonly string lastGameNickSaveName = "lastGameNick";

        public void ResumeGame()
        {
            nickname = saveManager.LoadLastGame();
            ResumeObjects();
        }

        private void ResumeObjects()
        {
            //PowerUps
            Upgrades.PowerUp powerUp = Instantiate(dobuleJumpPrefab);
            //TODO TryParse handle
            InitPowerUp(powerUp, lowPlatforms, int.Parse(saveManager.ResumeObject(powerUp)));
            powerUp = Instantiate(sprintPrefab);
            //TODO TryParse handle
            InitPowerUp(powerUp, highPlatforms, int.Parse(saveManager.ResumeObject(powerUp)));
        }

        public void StartNewGame(string playerName)
        {
            nickname = playerName;
            saveManager.InitSaveManager(nickname + "save");
            InitObjects();
        }

        private void InitObjects()
        {
            //PowerUps
            InitPowerUp(Instantiate(dobuleJumpPrefab), lowPlatforms, Random.Range(0, lowPlatforms.Count));
            InitPowerUp(Instantiate(sprintPrefab), highPlatforms, Random.Range(0, highPlatforms.Count));
        }

        private void InitPowerUp(PowerUp powerUp, List<Platform.Platform> platforms, int index)
        {
            platforms[index].PowerUp(powerUp.PowerType);
            powerUp.SetUp(platforms[index].powerUpSpawnPoint);
            saveManager.RegisterObject(powerUp, index.ToString());
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (isResume)
                ResumeGame();
            else
                StartNewGame("Test");
        }

        private void Update()
        {
        
        }

        private void OnDestroy()
        {
            saveManager.SaveAll();
        }
    }
}
