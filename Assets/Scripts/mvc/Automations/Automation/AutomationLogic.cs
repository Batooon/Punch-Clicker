﻿using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public interface IAutomationDatabase
{
    CurrentPlayerAutomationData GetAutomationData(int automationId);
    void SaveAutomationData(CurrentPlayerAutomationData automationData, int automationId);
    void Serialize();
}

public class AutomationDatabse : IAutomationDatabase
{
    private static AutomationDatabse _singleton;

    private List<CurrentPlayerAutomationData> AutomationData = new List<CurrentPlayerAutomationData>();

    public static AutomationDatabse GetAutomaitonDatabase()
    {
        if (_singleton == null)
            return _singleton = new AutomationDatabse();

        return _singleton;
    }

    private AutomationDatabse()
    {
        //Deserialize Automation Data
    }

    public CurrentPlayerAutomationData GetAutomationData(int automationId)
    {
        return new CurrentPlayerAutomationData();
        //return AutomationData[automationId];
    }

    public void SaveAutomationData(CurrentPlayerAutomationData automationData, int automationID)
    {
        Debug.Log("Saving Automation Data(Actually no ✪ ω ✪)");
    }

    public void Serialize()
    {
        Debug.Log("Automations Serialized(no)");
    }

    ~AutomationDatabse()
    {
        Serialize();
    }
}

public class AutomationLogic : MonoBehaviour
{
    public event Action<CurrentPlayerAutomationData> AutomationUpgraded;

    [SerializeField]
    private Button _upgradeButton;

    private int _automationId;

    [SerializeReference]
    [SelectImplementation(typeof(IAutomation))]
    private IAutomation _automation;

    private IAutomationBusinessInput _automationInput;

    private void Awake()
    {
        AutomationPresentation automationPresentation = GetComponent<AutomationPresentation>();
        _automationInput = new AutomationBusinessRules(new AutomationPresentator(automationPresentation),
                                                       PlayerDataAccess.GetPlayerDatabase(),
                                                       AutomationDatabse.GetAutomaitonDatabase());
    }

    private void Start()
    {
        _upgradeButton.onClick.AddListener(OnUpgradeButtonPressed);
    }

    public void OnUpgradeButtonPressed()
    {
        _automationInput.TryUpgradeAutomation(_automationId, _automation);
    }

    public void SetAutomationType(IAutomation automation)
    {
        _automation = automation;
    }
}