using Game.Components;
using Game.Enemies;
using Infrastructure.GameSystem;
using System;
using System.Collections;
using UnityEngine;
using VContainer;


namespace Game
{
    internal sealed class EnemiesSpawner : MonoBehaviour, ITickable, IGamePauseListener
    {
        [SerializeField] private SpawnNode[] spawnNodes;
        [SerializeField] private EnemiesPool enemiesPool;
        [SerializeField] private Transform gameObjectsContainer;
        private Vector2 _centerSpawnPoint;

        private int _nextNodeIndex = 0;
        private float _currentSpawnTimer = 0f;

        [Inject] private readonly TickableProcessor _tickableProcessor;
        [Inject] private readonly GameManager _gameManager;
        private bool _isPaused = false;


        private void Start()
        {
            var screenMaxPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            _centerSpawnPoint = new Vector2(screenMaxPoint.x, screenMaxPoint.y / 2f);

            _tickableProcessor.AddTickable(this);
            _gameManager.AddListener(this);
            enemiesPool.OnNewObjectInstantiated += SetupEnemy;
            foreach (var pooled in enemiesPool.GetPooledObjects())
            {
                SetupEnemy(pooled);
            }
        }

        private void OnDestroy()
        {
            if (_tickableProcessor)
                _tickableProcessor.RemoveTickable(this);

            if (_gameManager)
                _gameManager.RemoveListener(this);

            enemiesPool.OnNewObjectInstantiated -= SetupEnemy;
        }


        private void SetupEnemy(EnemyEntity entity)
        {
            entity.Construct(_tickableProcessor);
            _tickableProcessor.AddTickable(entity);
            entity.Get<DestroyComponent>().OnDestroy += () => enemiesPool.DespawnObject(entity);
        }


        void ITickable.Tick(float deltaTime)
        {
            if (_nextNodeIndex >= spawnNodes.Length) return;

            _currentSpawnTimer += deltaTime;
            if (_currentSpawnTimer >= spawnNodes[_nextNodeIndex].SpawnOnTimeSeconds)
            {
                SpawnNode();
                _nextNodeIndex++;
            }
        }

        private void SpawnNode()
        {
            var node = spawnNodes[_nextNodeIndex];
            if (node.EnemySpawnSettings.SpawnCount > 0)
            {
                StartCoroutine(SpawnGroupCoroutine(node));
            }


            else
            {
                var spawnPoint = _centerSpawnPoint + node.SpawnPositionRelativeScreenEdge;
                SpawnOne(node.EnemyPrefab, spawnPoint);
            }
        }

        private void SpawnOne(EnemyEntity enemyPrefab, Vector2 spawnPoint)
        {
            var enemy = enemiesPool.SpawnObject(enemyPrefab, gameObjectsContainer);
            enemy.transform.localPosition = spawnPoint;
        }



        void IGamePauseListener.OnGamePaused()
        {
            _isPaused = true;
        }

        void IGamePauseListener.OnGameUnPaused()
        {
            _isPaused = false;
        }

        private IEnumerator SpawnGroupCoroutine(SpawnNode node)
        {
            int countOfSpawn = node.EnemySpawnSettings.SpawnCount;
            float currentSpawnTime = node.EnemySpawnSettings.DelayBetweenSpawn;
            var spawnPoint = _centerSpawnPoint + node.SpawnPositionRelativeScreenEdge;

            while (countOfSpawn > 0)
            {
                if (!_isPaused)
                {
                    if (currentSpawnTime >= node.EnemySpawnSettings.DelayBetweenSpawn)
                    {
                        SpawnOne(node.EnemyPrefab, spawnPoint);
                        currentSpawnTime = 0f;
                        countOfSpawn--;
                    }

                    currentSpawnTime += Time.deltaTime;
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }






    [Serializable]
    internal struct SpawnNode
    {
        public EnemyEntity EnemyPrefab;
        public float SpawnOnTimeSeconds;    //On which second spawn from level start
        public Vector2 SpawnPositionRelativeScreenEdge;
        public EnemySpawnSettings EnemySpawnSettings;
    }

    [Serializable]
    internal struct EnemySpawnSettings
    {
        public int SpawnCount;
        public float DelayBetweenSpawn;
    }
}
