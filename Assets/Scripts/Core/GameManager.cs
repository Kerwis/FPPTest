using System.Collections.Generic;
using UnityEngine;
using Upgrades;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Upgrades.PowerUp dobuleJumpPrefab;
    
        [SerializeField]
        private Upgrades.PowerUp sprintPrefab;

        [SerializeField]
        private SaveManager saveManager;
    
        public List<Platform.Platform> lowPlatforms = new List<Platform.Platform>();
        public List<Platform.Platform> highPlatforms = new List<Platform.Platform>();
        private string nickname;


        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            StartNewGame("Test");
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
            Platform.Platform powerUpPlatform = lowPlatforms[Random.Range(0, lowPlatforms.Count)];

            powerUpPlatform.PowerUp(Type.DoubleJump);
            Upgrades.PowerUp powerUp = Instantiate(dobuleJumpPrefab, powerUpPlatform.powerUpSpawnPoint.position,
                powerUpPlatform.powerUpSpawnPoint.rotation, powerUpPlatform.powerUpSpawnPoint);
            saveManager.RegisterObject(powerUp);

            powerUpPlatform = highPlatforms[Random.Range(0, highPlatforms.Count)];
            powerUpPlatform.PowerUp(Type.Sprint);
            powerUp = Instantiate(sprintPrefab, powerUpPlatform.powerUpSpawnPoint.position,
                powerUpPlatform.powerUpSpawnPoint.rotation, powerUpPlatform.powerUpSpawnPoint);

        }

        void Update()
        {
        
        }
    }
}
