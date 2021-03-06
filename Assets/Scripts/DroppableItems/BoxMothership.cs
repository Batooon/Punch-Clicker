﻿using UnityEngine;
using UnityEngine.Events;

namespace DroppableItems
{
    public class BoxMothership : DroppingMothership
    {
        [SerializeField] private UnityEvent _boxOpenEvent;
        [SerializeField] private BoxPromtPanelPresentation _promtPanel;
        [SerializeField] private BoxReward _boxReward;
        [SerializeField] private uint _goldRewardMultiplier;

        public override void Init(BadgeData badgeData, PlayerData playerData)
        {
            base.Init(badgeData, playerData);
            _boxReward.Init(playerData);
        }

        public override void Spawn(Vector3 position)
        {
            if (Random.value <= _chanceToSpawn) 
            {
                GameObject boxObject = _objectPooler.GetPooledObjects();
                if (boxObject != null)
                {
                    boxObject.transform.position = transform.position;
                    boxObject.transform.rotation = _itemToSpawn.transform.rotation;
                    boxObject.SetActive(true);
                }
                Box box = boxObject.GetComponent<Box>();
                _promtPanel.Init(_badgeData.CoinsReward * _goldRewardMultiplier);
                box.Init(_boxOpenEvent);
            }
        }
    }
}
