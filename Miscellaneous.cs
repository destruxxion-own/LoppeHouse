using HutongGames.PlayMaker.Actions;
using MSCLoader;
using System.Data;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace LoppeHouse
{
    internal class Miscellaneous : Objects
    {
        private static bool isHere;
        private static float timerGUIs = 0f;
        public static void TimerGUI()
        {
            float updateInterval = 0.25f;
            timerGUIs += Time.deltaTime;

            if (timerGUIs >= updateInterval)
            {
                PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = false;
                PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIassemble").Value = false;
                PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIdisassemble").Value = false;
                PlayMakerGlobals.Instance.Variables.GetFsmString("GUIinteraction").Value = string.Empty;

                timerGUIs = 0f;
            }
        }
        public static void CheckPlayerLocation()
        {
            if (HouseCollider != null)
            {
                if (HouseCollider.bounds.Contains(Player.transform.position))
                {
                    if (Variables.ItemsPacked || Telephone.transform.localPosition != new Vector3(-1513.855f, -478.2803f, 9.6475f))
                    {
                        GraphicsSettings.DisableRenderer(CordPin);
                        GraphicsSettings.DisableRenderer(CordPout);
                        GraphicsSettings.DisableRenderer(CordMin);
                        GraphicsSettings.DisableRenderer(CordMout);
                    }
                    else
                    {
                        GraphicsSettings.EnableRenderer(CordPin);
                        GraphicsSettings.EnableRenderer(CordPout);
                        GraphicsSettings.EnableRenderer(CordMin);
                        GraphicsSettings.EnableRenderer(CordMout);
                    }

                    CordPin.SetActive(PhoneIn.activeInHierarchy && CordMin.activeInHierarchy == false);
                    CordPout.SetActive(PhoneOut.activeInHierarchy);
                    CordMin.SetActive(ModemIn.activeInHierarchy && CordPin.activeInHierarchy == false);
                    CordMout.SetActive(ModemOut.activeInHierarchy);
                }
                if (HouseCollider.bounds.Contains(Player.transform.position) && !isHere)
                {
                    GraphicsSettings.DisableRenderer(PhoneIn);
                    GraphicsSettings.DisableRenderer(PhoneOut);
                    GraphicsSettings.DisableRenderer(ModemIn);
                    GraphicsSettings.DisableRenderer(ModemOut);

                    GameObject.Find("COTTAGE/Sauna").transform.localPosition = new Vector3(361.6f, 12.12f, 2203.91f);
                    GameObject.Find("COTTAGE/Sauna").transform.localRotation = Quaternion.Euler(-90f, 147f, 0f);
                    GameObject.Find("COTTAGE/Sauna").transform.GetChild(2).localPosition = new Vector3(1.5124f, -0.466f, 3.003f);
                    GameObject.Find("COTTAGE/Sauna").transform.GetChild(2).localRotation = Quaternion.Euler(270f, 180f, 0f);
                    GameObject.Find("COTTAGE/Sauna").transform.GetChild(1).localPosition = new Vector3(2.029f, 1.171f, 2.115f);
                    GameObject.Find("COTTAGE/Sauna").transform.GetChild(1).localScale = new Vector3(0.46f, 2.35f, 1f);
                    GameObject.Find("COTTAGE/Sauna/Stove/Flame/Smoke").transform.localPosition = new Vector3(-1.44f, 4f, 6.5f);

                    Electricity.WatchingTV.compareTo = Electricity.HouseScreen;
                    Electricity.SwitchFSM.SendEvent("CLOSE");
                    GameObject.Find("PLAYER/Pivot/AnimPivot/Camera/FPSCamera/FPSCamera/Smoking").GetPlayMaker("Start").GetState("Wait player").GetAction<SetBoolValue>(2).boolValue = false;
                    ModemCords.SetActive(true);

                    isHere = true;
                }
                if (!HouseCollider.bounds.Contains(Player.transform.position) && isHere)
                {
                    GraphicsSettings.EnableRenderer(PhoneIn);
                    GraphicsSettings.EnableRenderer(PhoneOut);
                    GraphicsSettings.EnableRenderer(ModemIn);
                    GraphicsSettings.EnableRenderer(ModemOut);

                    GameObject.Find("COTTAGE/Sauna").transform.localPosition = new Vector3(5.81f, 0f, -1.79f);
                    GameObject.Find("COTTAGE/Sauna").transform.localRotation = Quaternion.Euler(270f, 90f, 0f);
                    GameObject.Find("COTTAGE/Sauna").transform.GetChild(2).localPosition = new Vector3(-1.0976f, -0.906f, 3.013f);
                    GameObject.Find("COTTAGE/Sauna").transform.GetChild(2).localRotation = Quaternion.Euler(0f, 90f, 90f);
                    GameObject.Find("COTTAGE/Sauna").transform.GetChild(1).localPosition = new Vector3(0.079f, -0.829f, 2.115f);
                    GameObject.Find("COTTAGE/Sauna").transform.GetChild(1).localScale = new Vector3(1f, 1f, 1f);
                    GameObject.Find("COTTAGE/Sauna/Stove/Flame/Smoke").transform.localPosition = new Vector3(0.56f, 4f, 0.13f);

                    Electricity.WatchingTV.compareTo = Electricity.TVScreen;
                    Electricity.SwitchFSM.SendEvent("CLOSE");
                    GameObject.Find("PLAYER/Pivot/AnimPivot/Camera/FPSCamera/FPSCamera/Smoking").GetPlayMaker("Start").GetState("Wait player").GetAction<SetBoolValue>(2).boolValue = true;
                    if (CaseInstalled.Value == false) ModemCords.SetActive(false);

                    isHere = false;
                }
            }
        }
    }
}
