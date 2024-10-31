using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace ADV_2D
{
    public class Game
    {
        private readonly Character _playerPrefab;
        private readonly Vector3 _playerStartPosition;
        private readonly PlayerController _playerController;
        private readonly InputDetector _inputDetector;
        private readonly EnemiesSpawner _enemiesSpawner;
        private readonly PlatformsSpawner _platformsSpawner;
        
        private CinemachineVirtualCamera _camera;
        private Character _playerCharacter;
        private List<Enemy> _enemies;
        private List<EnemyAiController> _enemyControllers;
        private List<Platform> _platforms;
        private bool _isInitialized;

        public Game(CinemachineVirtualCamera camera, Character playerPrefab, Vector3 playerStartPosition, PlayerController playerController,
            InputDetector inputDetector, EnemiesSpawner enemiesSpawner, PlatformsSpawner platformsSpawner)
        {
            _camera = camera;
            _playerPrefab = playerPrefab;
            _playerStartPosition = playerStartPosition;
            _playerController = playerController;
            _inputDetector = inputDetector;
            _enemiesSpawner = enemiesSpawner;
            _platformsSpawner = platformsSpawner;
            StartGame();
            _isInitialized = true;
        }

        public void Update()
        {
            if (_isInitialized)
            {
                _inputDetector.Update();
                _playerController.Update();

                if (_playerCharacter.IsDead)
                {
                    CleanLevel();
                    StartGame();
                }
            }
        }

        public void FixedUpdate()
        {
            if (_isInitialized)
            {
                _playerController.FixedUpdate();

                if (_enemyControllers.Count > 0)
                    foreach (EnemyAiController enemyController in _enemyControllers)
                        enemyController.FixedUpdate();
            }
        }
        
        private void CleanLevel()
        {
            Object.Destroy(_playerCharacter.gameObject);
            
            for (int i = 0; i < _enemyControllers.Count; i++)
            {
                _enemyControllers[i] = null;
            }
            
            foreach (Enemy enemy in _enemies)
            {
                Object.Destroy(enemy.gameObject);
            }
            
            foreach (Platform platform in _platforms)
            {
                Object.Destroy(platform.gameObject);
            }
        }
        
        private void StartGame()
        {
            _playerCharacter = Object.Instantiate(_playerPrefab, _playerStartPosition, Quaternion.identity, null);
            _playerCharacter.Initialize();
            _playerController.SetCharacter(_playerCharacter);
            _camera.Follow = _playerCharacter.transform;
            _enemies = _enemiesSpawner.SpawnEnemies(out _enemyControllers);
            _platforms = _platformsSpawner.SpawnPlatforms();
        }
    }
}