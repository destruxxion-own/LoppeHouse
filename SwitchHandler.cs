using System;
using System.Collections.Generic;
using UnityEngine;

namespace LoppeHouse
{
    internal class SwitchHandler
    {
        private static Dictionary<GameObject, Func<bool>> switchStates;
        public static void InitializeSwitchStates()
        {
            switchStates = new Dictionary<GameObject, Func<bool>>
        {

            { Electricity.SwitchKitchen, () => Variables.LightKitchen },
            { Electricity.SwitchLiving, () => Variables.LightLiving },
            { Electricity.SwitchPlayer, () => Variables.LightPlayer },
            { Electricity.SwitchBathroom, () => Variables.LightBathroom },
            { Electricity.SwitchParents, () => Variables.LightParents },
            { Electricity.SwitchHallroom, () => Variables.LightHallroom },
            { Electricity.SwitchCloset, () => Variables.LightCloset },
            { Electricity.SwitchGarage, () => Variables.LightGarage },
            { Electricity.SwitchGarageDash, () => Variables.LightGarageDash },
            { Electricity.SwitchOutdoor, () => Variables.LightOutdoor },
            { Electricity.SwitchOutdoorTable, () => Variables.LightOutdoorTable },
        };
        }
        public static void UpdateSwitchRotations()
        {
            foreach (var kvp in switchStates)
            {
                UpdateSwitchRotation(kvp.Key, kvp.Value());
            }

            if (Electricity.SwitchOn.Value)
            {
                Electricity.Switch.transform.position = new Vector3(1365.935f, 10.80085f, 800.5176f);
                Electricity.Switch.transform.rotation = Quaternion.Euler(303f, 222f, 180f);
            }
            else
            {
                Electricity.Switch.transform.position = new Vector3(1365.935f, 10.78185f, 800.5176f);
                Electricity.Switch.transform.rotation = Quaternion.Euler(291f, 42f, 0f);
            }
        }
        private static void UpdateSwitchRotation(GameObject switchObject, bool isOn)
        {
            Transform switchTransform = switchObject.transform.GetChild(0).transform;
            if (isOn) switchTransform.localRotation = Quaternion.Euler(-15.55046f, 0f, 0f);
            else switchTransform.localRotation = Quaternion.Euler(15.55046f, 0f, 0f);
        }
    }
}