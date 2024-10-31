using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace ADV_2D
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private Character _playerPrefab;
        [SerializeField] private Transform _playerStart;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Transform _enemySpawnPointsContainer;
        [SerializeField] private Platform _platformPrefab;
        [SerializeField] private Transform _platformSpawnPointsContainer;

        private InputDetector _inputDetector;
        private PlayerController _playerController;
        private Game _game;

        private void Awake()
        {
            InputDetector inputDetector = new InputDetector();
            PlayerController playerController = new PlayerController(inputDetector);
            EnemiesSpawner enemiesSpawner = new EnemiesSpawner(_enemyPrefab, _enemySpawnPointsContainer);
            PlatformsSpawner platformsSpawner = new PlatformsSpawner(_platformPrefab, _platformSpawnPointsContainer);
            _game = new Game(_camera, _playerPrefab, _playerStart.position, playerController, inputDetector, enemiesSpawner, platformsSpawner);
        }

        private void Update()
        {
            _game.Update();
        }

        private void FixedUpdate()
        {
            _game.FixedUpdate();
        }
    }
}