using System.Collections.Generic;
using UnityEngine;

namespace  ADV_2D
{
    public class PlatformsSpawner
    {
        private readonly Platform _platformPrefab;
        private readonly Transform _spawnPointsContainer;

        public PlatformsSpawner(Platform platformPrefab, Transform spawnPointsContainer)
        {
            _platformPrefab = platformPrefab;
            _spawnPointsContainer = spawnPointsContainer;
        }

        public List<Platform> SpawnPlatforms()
        {
            List<Platform> platforms = new List<Platform>();
            
            foreach (Transform point in _spawnPointsContainer)
            {
                if (point != _spawnPointsContainer.transform)
                {
                    Platform platform = Object.Instantiate(_platformPrefab, point.position, Quaternion.identity, null);
                    platform.Initialize();
                    platforms.Add(platform);
                }
            }

            return platforms;
        }
    }
}

