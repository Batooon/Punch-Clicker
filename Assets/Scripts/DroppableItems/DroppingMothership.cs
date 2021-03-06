﻿using UnityEngine;

namespace DroppableItems
{
    public abstract class DroppingMothership : MonoBehaviour, IItemsMothership
    {
        [SerializeField] protected AudioService _audioService;
        [SerializeField] protected AmountOfObjectsToSpawn _amountOfObjectsToSpawn;
        [SerializeField, Range(0f, 1f)] protected float _chanceToSpawn;
        [Header("ICollectable component required!")]
        [SerializeField] protected GameObject _itemToSpawn;
        [SerializeField] protected ObjectPooler _objectPooler;

        protected BadgeData _badgeData;
        protected PlayerData _playerData;
        public virtual void Init(BadgeData badgeData, PlayerData playerData)
        {
            _badgeData = badgeData;
            _playerData = playerData;
            _objectPooler.Init();
        }

        public abstract void Spawn(Vector3 position);
    }
}
