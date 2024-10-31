using System.Collections.Generic;
using UnityEngine;

namespace ADV_2D
{
    public class EnemiesSpawner
    {
        private readonly Enemy _enemyPrefab;
        private readonly Transform _spawnPointsContainer;

        public EnemiesSpawner(Enemy enemyPrefab, Transform spawnPointsContainer)
        {
            _enemyPrefab = enemyPrefab;
            _spawnPointsContainer = spawnPointsContainer;
        }

        public List<Enemy> SpawnEnemies(out List<EnemyAiController> aiControllers)
        {
            aiControllers = new List<EnemyAiController>();
            List<Enemy> enemies = new List<Enemy>();

            foreach (Transform point in _spawnPointsContainer)
            {
                if (point.TryGetComponent(out EnemySpawnPoint spawnPoint))
                {
                    Enemy enemy = Object.Instantiate(_enemyPrefab, point.position, Quaternion.identity, null);
                    enemy.Initialize(spawnPoint.PatrolPoints);
                    enemies.Add(enemy);
                    EnemyAiController enemyAiController = new EnemyAiController(enemy, spawnPoint.PatrolPoints);
                    aiControllers.Add(enemyAiController);
                }
            }

            return enemies;
        }
    }
}