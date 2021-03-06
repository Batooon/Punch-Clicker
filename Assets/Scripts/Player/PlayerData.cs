﻿using System;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData : ISubject
{
    [SerializeField, HideInInspector] public int BadgePoints;
    [SerializeField, HideInInspector] public bool IsProgressResetting;
    [SerializeField] private int _level;
    [SerializeField] private BigInteger _gold;
    [SerializeField] private int _levelProgress;
    [SerializeField] private int _maxLevelProgress;
    [SerializeField] private bool _isReturningPlayer;
    [SerializeField] private int _bossCountdownTime;
    [SerializeField] private bool _needToIncreaseLevel;
    [SerializeField] private int _damageBonus;
    [SerializeField, HideInInspector] private int _startingLevel;
    [SerializeField, HideInInspector] private BigInteger _startingGold;
    [SerializeField, HideInInspector] private int _startingBossCountdownTime;
    [SerializeField, HideInInspector] private int _startingLevelProgress;
    [SerializeField, HideInInspector] private int _startingMaxLevelProgress;
    [SerializeField] private JsonDateTime _jsonDateTime = new JsonDateTime();

    /*public event Action<int> LevelChanged;
    public event Action<long> GoldChanged;
    public event Action<int> LevelProgressChanged;
    public event Action<bool> NeedToIncreaseLevelChanged;*/

    public int Level
    {
        get => _level;
        set
        {
            _level = value;
            Notify();
        }
    }

    public BigInteger Gold
    {
        get => _gold;
        set
        {
            _gold = value;
            Notify();
        }
    }

    public int LevelProgress
    {
        get => _levelProgress;
        set
        {
            _levelProgress = value;
            Notify();
        }
    }

    public int MaxLevelProgress
    {
        get => _maxLevelProgress;
        set => _maxLevelProgress = value;
    }

    public bool IsReturningPlayer
    {
        get => _isReturningPlayer;
        set => _isReturningPlayer = value;
    }

    public int BossCountdownTime
    {
        get => _bossCountdownTime;
        set => _bossCountdownTime = value;
    }

    public bool NeedToIncreaseLevel
    {
        get => _needToIncreaseLevel;
        set
        {
            _needToIncreaseLevel = value;
            Notify();
        }
    }

    public int DamageBonus
    {
        get => _damageBonus;
        set => _damageBonus = value;
    }

    public DateTime LastTimeInGame
    {
        get => _jsonDateTime;
        set => _jsonDateTime = value;
    }

    private List<IObserver> _observers = new List<IObserver>();

    public void Init()
    {
        _startingLevel = _level;
        _startingGold = _gold;
        _startingBossCountdownTime = _bossCountdownTime;
        _startingLevelProgress = _levelProgress;
        _startingMaxLevelProgress = _maxLevelProgress;
    }

    public void SaveData(string fileName)
    {
        LastTimeInGame = DateTime.Now;
        FileOperations.Serialize(this, fileName);
    }

    public void FireAllChangedEvents()
    {/*
        LevelChanged?.Invoke(_level);
        GoldChanged?.Invoke(_gold);
        LevelProgressChanged?.Invoke(_levelProgress);
        NeedToIncreaseLevelChanged?.Invoke(_needToIncreaseLevel);*/
    }

    public void ResetData()
    {
        Level = _startingLevel;
        Gold = _startingGold;
        BossCountdownTime = _startingBossCountdownTime;
        LevelProgress = _startingLevelProgress;
        MaxLevelProgress = _startingMaxLevelProgress;
    }

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in _observers)
            observer.Fetch(this);
    }
}

[Serializable]
public struct JsonDateTime
{
    public long value;

    public static implicit operator DateTime(JsonDateTime jsonDateTime)
    {
        return DateTime.FromFileTimeUtc(jsonDateTime.value);
    }

    public static implicit operator JsonDateTime(DateTime dateTime)
    {
        JsonDateTime jsonTime = new JsonDateTime
        {
            value = dateTime.ToFileTimeUtc()
        };
        return jsonTime;
    }
}
