using Game.Components;
using Game.Enemies;
using Infrastructure.GameSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using ITickable = Infrastructure.GameSystem.ITickable;


namespace Game
{
    internal sealed class EnemiesSpawner : MonoBehaviour, ITickable, IGamePauseListener
    {
        [Inject] private readonly GameMachine _gameMachine;
        [Inject] private readonly IObjectResolver _objectResolver;

        [Inject] private readonly TickableProcessor _tickableProcessor;
        private Vector2 _centerSpawnPoint;
        private float _currentSpawnTimer;
        private bool _isPaused;

        private int _nextNodeIndex;
        [SerializeField] private EnemiesPool enemiesPool;
        [SerializeField] private Transform gameObjectsContainer;
        [SerializeField] private SpawnNode[] spawnNodes;


        void IGamePauseListener.OnGamePaused()
        {
            _isPaused = true;
        }

        void IGamePauseListener.OnGameUnPaused()
        {
            _isPaused = false;
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


        private void Start()
        {
            var screenMaxPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            _centerSpawnPoint = new Vector2(screenMaxPoint.x, screenMaxPoint.y / 2f);

            _tickableProcessor.AddTickable(this);
            _gameMachine.AddListener(this);


            InitializePool(spawnNodes);

            var allPooled = enemiesPool.GetPooledObjects();
            foreach (var pooled in allPooled) SetupEnemy(pooled);

            enemiesPool.OnNewObjectInstantiated += SetupEnemy;
        }

        private void InitializePool(SpawnNode[] nodes)
        {
            Dictionary<EnemyEntity, int> prefabs = new();
            for (var i = 0; i < nodes.Length; i++)
            {
                var node = nodes[i];
                prefabs.TryAdd(node.EnemyPrefab,0);
                prefabs[node.EnemyPrefab] += node.EnemySpawnSettings.SpawnCount + 1;
            }

            foreach (var prefab in prefabs)
            {
                var amount = Mathf.Min(prefab.Value, 10);
                enemiesPool.PopulateWith(prefab.Key, amount);
            }
        }

        private void OnDestroy()
        {
            if (_tickableProcessor)
                _tickableProcessor.RemoveTickable(this);

            if (_gameMachine)
                _gameMachine.RemoveListener(this);

            enemiesPool.OnNewObjectInstantiated -= SetupEnemy;
        }


        private void SetupEnemy(EnemyEntity entity)
        {
            _objectResolver.InjectGameObject(entity.gameObject);
            _tickableProcessor.AddTickable(entity);
            entity.GetEntityComponent<DestroyComponent>().OnDestroy += () => enemiesPool.DespawnObject(entity);
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

        private IEnumerator SpawnGroupCoroutine(SpawnNode node)
        {
            var countOfSpawn = node.EnemySpawnSettings.SpawnCount;
            var currentSpawnTime = node.EnemySpawnSettings.DelayBetweenSpawn;
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
        public float SpawnOnTimeSeconds; //On which second spawn from level start
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