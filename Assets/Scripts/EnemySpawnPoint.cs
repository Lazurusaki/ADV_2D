using UnityEngine;

namespace ADV_2D
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] private Transform[] _patrolPoints;

        public Transform[] PatrolPoints => _patrolPoints;
    }
}
