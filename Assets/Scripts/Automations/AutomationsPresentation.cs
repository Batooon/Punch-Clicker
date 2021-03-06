﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Automations
{
    public class AutomationsPresentation : MonoBehaviour,IObserver
    {
        [SerializeField] private TextMeshProUGUI _clickPowerText;
        [SerializeField] private TextMeshProUGUI _automationsPowerText;

        private AutomationsData _automationsData;
        private List<GameObject> _automationObjects = new List<GameObject>();

        public void Init(AutomationsData automationsData)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.GetComponent<AutomationLogic>() != null)
                    _automationObjects.Add(child.gameObject);
            }

            _automationsData = automationsData;
        }

        private void OnEnable()
        {
            /*_automationsData.Attach(this);

            OnAutomationsPowerChanged(_automationsData.AutomationsPower);
            OnClickPowerChanged(_automationsData.ClickPower);*/
        }

        private void OnDisable()
        {
            //_automationsData.Detach(this);
        }

        private void OnAutomationsPowerChanged(long newPower)
        {
            _automationsPowerText.text = newPower.ConvertValue();
        }

        private void OnClickPowerChanged(long newClickPower)
        {
            _clickPowerText.text = newClickPower.ConvertValue();
        }

        public void SetUIValues(string clickPower, string automationsPower)
        {
            _clickPowerText.text = clickPower;
            _automationsPowerText.text = automationsPower;
        }

        public void UnlockNewAutomation(int newAutomationId)
        {
            _automationObjects[newAutomationId].SetActive(true);
        }

        public void Fetch(ISubject subject)
        {
            OnAutomationsPowerChanged(_automationsData.AutomationsPower);
            OnClickPowerChanged(_automationsData.ClickPower);
        }
    }
}
