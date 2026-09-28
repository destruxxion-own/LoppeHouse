using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using MSCLoader;
using System;
using UnityEngine;

namespace LoppeHouse
{
    internal class Electricity : LoppeHouse
    {
#pragma warning disable IDE0044
#pragma warning disable CS0618
        public static GameObject ElectricAppliances, SwitchKitchen, SwitchLiving, SwitchPlayer, 
            SwitchBathroom, SwitchParents, SwitchHallroom, SwitchCloset, SwitchGarage, 
            SwitchGarageDash, SwitchOutdoor, SwitchOutdoorTable, Switch;
        public static GameObject Kitchen, Living, Playerroom, Bathroom, Parentsroom, Hallroom, 
            Closet, Garage, GarageDash, Outdoor, OutdoorTable, TVScreen, HouseScreen;
        public static GameObjectCompare WatchingTV;
        public static PlayMakerFSM SwitchFSM;

        public static FsmBool SwitchOn = new FsmBool("SwitchOn").Value = true;
        public static FsmBool BillPaid = new FsmBool("BillPaid").Value = true;
        public static FsmFloat PayTimer = new FsmFloat("PayTimer").Value = 21600f;
        private static FsmFloat PowerConsumed = new FsmFloat("PowerConsumed").Value = 0f;
        private static FsmBool[] BoolArray = new FsmBool[] { SwitchOn, BillPaid };
        private static FsmFloat KilowattCost = new FsmFloat("KilowattCost").Value = 1.87f;
        private static FsmFloat PowerConsumedCost = new FsmFloat("PowerConsumedCost").Value = 0f;
        private static float CombustionFactorLight = 0.024f;
        private static float CombustionFactorFridge = 0.035f;

        private static Light LightKitchen, LightLiving, LightPlayer, LightBathroom, LightParents, 
            LightHallroom, LightCloset, LightGarage, LightGarageDash, LightOutdoor, LightOutdoorTable;
        private static PlayMakerFSM OvenstoveFSM, ComputerButtonPower, KitchenTrigger, BatteryChargerFSM;
        private static GameObject Envelope, FridgeLight, FridgeHum, FridgePoint, KitchenTapDrink, ShowerTapDrink, ShowerShower, BatteryCharger;
        private GameObject Fridge, Systems, HouseTeletext, TVHouse, Tv, TvPrograms, HousePrograms, VideoProgram, NightProgram, RallyProgram, TriggerPlayMode, ComputerLights, FusetableInstance;
        private GameObject RemoteInstance, TvRemote, OvenstoveInstance, BathroomInstance, Shower, ShowerDoor, Ovenstove, ShowerValve, KitchenWaterTap, KitchenWaterTapInstance, KWHMeter, FusetableSwitch;
        private GameObject ComputerInstance, Computer, ComputerCase, ComputerMonitor, ComputerPeripherals, ComputerSpeakers, Fusetable, KWHMeterPivot, BatteryInstance, TriggerCharger;
#if DEBUG
        public static void ElectricityGUI()
        {
            GUI.Label(new Rect(20, 140, 200, 50), "Electricity.SwitchOn: " + SwitchOn.Value);
            GUI.Label(new Rect(20, 160, 200, 50), "Electricity.BillPaid: " + BillPaid.Value);
            GUI.Label(new Rect(20, 180, 200, 50), "Electricity.PayTimer: " + PayTimer.Value);
            GUI.Label(new Rect(20, 200, 300, 50), "Electricity.PowerConsumed: " + PowerConsumed.Value);
            GUI.Label(new Rect(20, 220, 300, 50), "Electricity.PowerConsumedCost: " + PowerConsumedCost.Value);
            GUI.Label(new Rect(20, 240, 200, 50), "Electricity.KilowattCost: " + KilowattCost.Value);
            GUI.Label(new Rect(20, 320, 200, 50), "Electricity.LightKitchen: " + Variables.LightKitchen);
            GUI.Label(new Rect(20, 340, 200, 50), "Electricity.LightLiving: " + Variables.LightLiving);
            GUI.Label(new Rect(20, 360, 200, 50), "Electricity.LightPlayer: " + Variables.LightPlayer);
            GUI.Label(new Rect(20, 380, 200, 50), "Electricity.LightBathroom: " + Variables.LightBathroom);
            GUI.Label(new Rect(20, 400, 200, 50), "Electricity.LightParents: " + Variables.LightParents);
            GUI.Label(new Rect(20, 420, 200, 50), "Electricity.LightHallroom: " + Variables.LightHallroom);
            GUI.Label(new Rect(20, 440, 200, 50), "Electricity.LightCloset: " + Variables.LightCloset);
            GUI.Label(new Rect(20, 460, 200, 50), "Electricity.LightGarage: " + Variables.LightGarage);
            GUI.Label(new Rect(20, 480, 200, 50), "Electricity.LightGarageDash: " + Variables.LightGarageDash);
            GUI.Label(new Rect(20, 500, 200, 50), "Electricity.LightOutdoor: " + Variables.LightOutdoor);
        }
#endif
        public static void PowerCore()
        {
            GameObject powerCore = new GameObject("PowerCore");
            var powerFsm = powerCore.AddComponent<PlayMakerFSM>();
            powerFsm.FsmName = "PowerCoreManager";

            powerFsm.AddVariable(BillPaid);
            powerFsm.AddVariable(SwitchOn);
            powerFsm.AddVariable(PayTimer);
            powerFsm.AddVariable(KilowattCost);
            powerFsm.AddVariable(PowerConsumed);
            powerFsm.AddVariable(PowerConsumedCost);

            BillPaid.Name = "BillPaid";
            SwitchOn.Name = "SwitchOn";
            PayTimer.Name = "PayTimer";
            KilowattCost.Name = "KilowattCost";
            PowerConsumed.Name = "PowerConsumed";
            PowerConsumedCost.Name = "PowerConsumedCost";

            GameObject powerBillSheets = GameObject.Find("Sheets");
            GameObject powerBillEnvelope = UnityEngine.Object.Instantiate(powerBillSheets.transform.GetChild(15).gameObject);
            powerBillEnvelope.SetActive(true);
            powerBillEnvelope.name = "PowerBill";
            powerBillEnvelope.transform.parent = GameObject.Find("Sheets").transform;

            var powerBillFsm = powerBillEnvelope.GetPlayMaker("Data");

            var powerBillFsmUnpaid = powerBillFsm.GetState("Set data").GetAction<GetFsmFloat>(0);
            powerBillFsmUnpaid.gameObject.GameObject = powerCore;
            powerBillFsmUnpaid.fsmName = "PowerCoreManager";
            powerBillFsmUnpaid.variableName = "PowerConsumedCost";
            powerBillFsmUnpaid.everyFrame = false;

            var powerBillFsmPrice = powerBillFsm.GetState("Set data").GetAction<GetFsmFloat>(1);
            powerBillFsmPrice.gameObject.GameObject = powerCore;
            powerBillFsmPrice.fsmName = "PowerCoreManager";
            powerBillFsmPrice.variableName = "KilowattCost";

            var payFsm = powerBillEnvelope.transform.GetChild(0).gameObject.GetPlayMaker("Button");
            var payFsmCheckMoney = payFsm.GetState("Check money").GetAction<GetFsmFloat>(0);
            payFsmCheckMoney.gameObject.GameObject = powerCore;
            payFsmCheckMoney.fsmName = "PowerCoreManager";
            payFsmCheckMoney.variableName = "PowerConsumedCost";

            var payFsmPay = payFsm.GetState("Date");

            payFsmPay.RemoveAction(2);
            payFsmPay.AddAction(new SetFloatValue());
            payFsmPay.InsertAction(2, new SetFloatValue()
            {
                floatVariable = PayTimer,
                floatValue = 21600f
            });
            payFsmPay.InsertAction(3, new SetFloatValue()
            {
                floatVariable = PowerConsumed,
                floatValue = 0f
            });
            payFsmPay.RemoveAction(4);

            powerBillEnvelope.SetActive(false);

            GameObject envelopeYard = GameObject.Find("YARD");
            GameObject envelopeMailBox = envelopeYard.transform.GetChild(4).gameObject;

            Envelope = UnityEngine.Object.Instantiate(envelopeMailBox.transform.GetChild(1).gameObject);
            Envelope.name = "PowerBill";
            Envelope.transform.parent = envelopeMailBox.transform;
            Envelope.transform.localPosition = new Vector3(0.014f, -0.0026f, 0.1807f);
            Envelope.transform.localRotation = Quaternion.Euler(0, 0, 0);

            Envelope.GetPlayMaker("Use").enabled = true;
            Envelope.GetPlayMaker("Use").GetState("State 2").GetAction<SetStringValue>(1).stringValue = "PERAJARVI ELECTRICITY";
            Envelope.GetPlayMaker("Use").GetState("Open bill").GetAction<ActivateGameObject>(1).gameObject.GameObject = powerBillEnvelope;
        }
        public static void InitializeElectricity()
        {
            var obj = new Electricity();

            try
            {
                GameObject LightGlobal = ElectricAppliances.transform.GetChild(0).gameObject;
                GameObject SwitchGlobal = ElectricAppliances.transform.GetChild(1).gameObject;

                Kitchen = LightGlobal.transform.GetChild(0).gameObject;
                Living = LightGlobal.transform.GetChild(1).gameObject;
                Playerroom = LightGlobal.transform.GetChild(2).gameObject;
                Bathroom = LightGlobal.transform.GetChild(3).gameObject;
                Parentsroom = LightGlobal.transform.GetChild(4).gameObject;
                Hallroom = LightGlobal.transform.GetChild(5).gameObject;
                Closet = LightGlobal.transform.GetChild(6).gameObject;
                Garage = LightGlobal.transform.GetChild(7).gameObject;
                GarageDash = LightGlobal.transform.GetChild(8).gameObject;
                Outdoor = LightGlobal.transform.GetChild(9).gameObject;
                OutdoorTable = LightGlobal.transform.GetChild(10).gameObject;

                LightKitchen = Kitchen.GetComponent<Light>();
                LightLiving = Living.GetComponent<Light>();
                LightPlayer = Playerroom.GetComponent<Light>();
                LightBathroom = Bathroom.GetComponent<Light>();
                LightParents = Parentsroom.GetComponent<Light>();
                LightHallroom = Hallroom.GetComponent<Light>();
                LightCloset = Closet.GetComponent<Light>();
                LightGarage = Garage.GetComponent<Light>();
                LightGarageDash = GarageDash.GetComponent<Light>();
                LightOutdoor = Outdoor.GetComponent<Light>();
                LightOutdoorTable = OutdoorTable.GetComponent<Light>();

                SwitchKitchen = SwitchGlobal.transform.GetChild(0).gameObject;
                SwitchLiving = SwitchGlobal.transform.GetChild(1).gameObject;
                SwitchPlayer = SwitchGlobal.transform.GetChild(2).gameObject;
                SwitchBathroom = SwitchGlobal.transform.GetChild(3).gameObject;
                SwitchParents = SwitchGlobal.transform.GetChild(4).gameObject;
                SwitchHallroom = SwitchGlobal.transform.GetChild(5).gameObject;
                SwitchCloset = SwitchGlobal.transform.GetChild(6).gameObject;
                SwitchGarage = SwitchGlobal.transform.GetChild(7).gameObject;
                SwitchGarageDash = SwitchGlobal.transform.GetChild(8).gameObject;
                SwitchOutdoor = SwitchGlobal.transform.GetChild(9).gameObject;
                SwitchOutdoorTable = SwitchGlobal.transform.GetChild(10).gameObject;
            }
            catch (Exception ex)
            {
                ModConsole.Error("Error occured while trying to initialize <b>LoppeHouse.Electricity (Phase1)</b>, please send screenshot to Bugs section on NexusMods: " + ex.Message);
            }
            try
            {
                obj.Fridge = GameObject.Find("House(Clone)/Rooms/Kitchen/Fridge");
                obj.Systems = GameObject.Find("Systems");
                obj.TVHouse = GameObject.Find("YARD/Building/LIVINGROOM/TV");
                obj.TvPrograms = GameObject.Find("YARD/Building/Dynamics/HouseElectricity/ElectricAppliances/TV_Programs");
                obj.RemoteInstance = GameObject.Find("tv remote control(itemx)");
                obj.OvenstoveInstance = GameObject.Find("YARD/Building/KITCHEN/OvenStove");
                obj.BathroomInstance = GameObject.Find("YARD/Building/BATHROOM");
                obj.KitchenWaterTapInstance = GameObject.Find("YARD/Building/KITCHEN/KitchenWaterTap");
                obj.ComputerInstance = GameObject.Find("YARD/Building/BEDROOM1/COMPUTER");
                obj.FusetableInstance = GameObject.Find("YARD/Building/Dynamics/FuseTable/Gauge");
                obj.FusetableSwitch = GameObject.Find("YARD/Building/Dynamics/FuseTable/Fusetable/MainSwitch/Pivot/fusetable_switch");
                obj.BatteryInstance = GameObject.Find("YARD/Building/Garage/BatteryCharger");
                obj.TriggerCharger = GameObject.Find("YARD/Building/Garage/BatteryCharger/TriggerCharger");
            }
            catch (Exception ex)
            {
                ModConsole.Error("Error occured while trying to initialize <b>LoppeHouse.Electricity (Phase2)</b>, please send screenshot to Bugs section on NexusMods: " + ex.Message);
            }
            try
            {
                FridgeLight = obj.Fridge.transform.GetChild(0).gameObject;
                FridgeHum = obj.Fridge.transform.GetChild(1).gameObject;
                FridgePoint = obj.Fridge.transform.GetChild(2).gameObject;

                obj.HouseTeletext = UnityEngine.Object.Instantiate(obj.Systems.transform.GetChild(18).gameObject);
                obj.HouseTeletext.name = "HouseTeletext";

                obj.Tv = UnityEngine.Object.Instantiate(obj.TVHouse);
                obj.Tv.transform.localPosition = new Vector3(1371.154f, 10.0348f, 802.0933f);
                obj.Tv.transform.localRotation = Quaternion.Euler(270f, 311f, 0f);
                obj.Tv.transform.localScale = new Vector3(1f, 1.015f, 1f);
                obj.Tv.name = "HouseTV";

                WatchingTV = GameObject.Find("PLAYER/Pivot/AnimPivot/Camera/FPSCamera/FPSCamera/WatchingTV").GetPlayMaker("Target").GetState("Compare TV").GetAction<GameObjectCompare>(0);
                TVScreen = GameObject.Find("YARD/Building/LIVINGROOM/TV/TVScreen");
                HouseScreen = GameObject.Find("HouseTV/TVScreen");

                obj.HousePrograms = UnityEngine.Object.Instantiate(obj.TvPrograms);
                obj.HousePrograms.transform.localPosition = new Vector3(1370.889f, 9.574788f, 801.7827f);
                obj.HousePrograms.name = "HousePrograms";
                
                obj.TvRemote = UnityEngine.Object.Instantiate(obj.RemoteInstance);
                obj.TvRemote.transform.position = new Vector3(1370.45f, 9.817905f, 802.4901f);
                obj.TvRemote.transform.rotation = Quaternion.Euler(270f, 73.16363f, 0f);
                obj.TvRemote.name = "tv remote control(Clone)";

                obj.Ovenstove = UnityEngine.Object.Instantiate(obj.OvenstoveInstance);
                obj.Ovenstove.transform.position = new Vector3(1372.726f, 10.11479f, 800.6766f);
                obj.Ovenstove.transform.rotation = Quaternion.Euler(270f, 220f, 0f);
                GameObject.Find("OvenStove(Clone)/mesh").AddComponent<MeshCollider>();
                
                obj.Shower = UnityEngine.Object.Instantiate(obj.BathroomInstance);
                obj.Shower.transform.position = new Vector3(1368.236f, 10.2734f, 791.7438f);
                obj.Shower.transform.rotation = Quaternion.Euler(270f, 263.11f, 0f);
                obj.Shower.transform.GetChild(0).gameObject.SetActive(true);
                obj.ShowerDoor = obj.Shower.transform.GetChild(2).gameObject;
                GameObject.Destroy(obj.ShowerDoor);
                obj.ShowerValve = GameObject.Find("BATHROOM(Clone)/Shower/Valve");
                ShowerTapDrink = GameObject.Find("BATHROOM(Clone)/Shower/TapDrink");
                ShowerShower = GameObject.Find("BATHROOM(Clone)/Shower/Shower");

                obj.KitchenWaterTap = UnityEngine.Object.Instantiate(obj.KitchenWaterTapInstance);
                obj.KitchenWaterTap.transform.position = new Vector3(1372.767f, 10.18807f, 799.562f);
                obj.KitchenWaterTap.transform.rotation = Quaternion.Euler(270f, 217f, 0f);
                KitchenTapDrink = obj.KitchenWaterTap.transform.GetChild(3).gameObject;
                KitchenTrigger = GameObject.Find("KitchenWaterTap(Clone)/Trigger").GetPlayMaker("Use");

                FsmGameObject activateSoftwareSound = GameObject.Find("Database/DatabaseComputer/Speakers").GetPlayMaker("Data").GetState("Installed 2").GetAction<ActivateGameObject>(2).gameObject.GameObject;
                activateSoftwareSound.Value.active = true;

                obj.Computer = UnityEngine.Object.Instantiate(obj.ComputerInstance);
                obj.Computer.transform.position = new Vector3(1368.926f, 10.0633f, 802.9321f);
                obj.Computer.transform.rotation = Quaternion.Euler(270f, 276f, 0f);
                GameObject.Destroy(obj.Computer.transform.GetChild(0).gameObject);
                obj.ComputerCase = GameObject.Find("COMPUTER(Clone)/Computer/Meshes/386_case(xxxxx)");
                obj.ComputerMonitor = GameObject.Find("COMPUTER(Clone)/Computer/Meshes/386_monitor(xxxxx)");
                obj.ComputerPeripherals = GameObject.Find("COMPUTER(Clone)/Computer/Meshes/386_peripherals(xxxxx)");
                obj.ComputerSpeakers = GameObject.Find("COMPUTER(Clone)/Computer/Meshes/386_speakers(xxxxx)");
                obj.TriggerPlayMode = GameObject.Find("COMPUTER(Clone)/Computer/TriggerPlayMode");
                obj.ComputerLights = GameObject.Find("COMPUTER(Clone)/Computer/Meshes/386_case(xxxxx)/Lights");
                ComputerButtonPower = GameObject.Find("COMPUTER(Clone)/Computer/Meshes/386_case(xxxxx)/ButtonPower").GetPlayMaker("Use");
                
                Switch = UnityEngine.Object.Instantiate(obj.FusetableSwitch);
                Switch.transform.position = new Vector3(1367.574f, 10.86085f, 802.6198f);
                Switch.transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
                Switch.AddComponent<BoxCollider>().isTrigger = true;
                Switch.name = "HouseSwitch";

                obj.Fusetable = UnityEngine.Object.Instantiate(obj.FusetableInstance);
                obj.Fusetable.transform.position = new Vector3(1365.906f, 10.83726f, 800.5015f);
                obj.Fusetable.transform.rotation = Quaternion.Euler(0f, 40f, 0f);
                obj.Fusetable.transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
                obj.Fusetable.name = "HouseFusetable";
                obj.KWHMeterPivot = GameObject.Find("HouseFusetable/Pivot/kwh_meter_disc");
                obj.KWHMeter = GameObject.Find("HouseFusetable/kwh_meter/Pivot/kwh_meter_disc");
                GameObject.Destroy(obj.KWHMeterPivot);
                GameObject.Destroy(obj.KWHMeter);

                obj.TriggerCharger.gameObject.GetPlayMaker("Assembly").SendEvent("FINISHED");
                BatteryCharger = UnityEngine.Object.Instantiate(obj.BatteryInstance);
                BatteryCharger.transform.position = new Vector3(1357.328f, 10.31658f, 804.7039f);
                BatteryCharger.transform.rotation = Quaternion.Euler(270f, 141f, 0f);
                BatteryChargerFSM = GameObject.Find("BatteryCharger(Clone)/TriggerCharger").gameObject.GetPlayMaker("Assembly");

                obj.ComputerCase.SetActive(true);
                obj.ComputerMonitor.SetActive(true);
                obj.ComputerPeripherals.SetActive(true);
                obj.ComputerSpeakers.SetActive(true);
                obj.TriggerPlayMode.SetActive(true);
            }
            catch (Exception ex)
            {
                ModConsole.Error("Error occured while trying to initialize <b>LoppeHouse.Electricity (Phase3)</b>, please send screenshot to Bugs section on NexusMods: " + ex.Message);
            }
            try
            {
                BatteryChargerFSM.GetState("Charge").GetAction<FloatAdd>(10).floatVariable = PowerConsumed;
                BatteryChargerFSM.GetState("Charge").GetAction<BoolTest>(11).boolVariable = SwitchOn;
                BatteryChargerFSM.GetState("Charge").GetAction<BoolTest>(12).boolVariable = BillPaid;
                BatteryChargerFSM.GetState("Done").GetAction<FloatAdd>(0).floatVariable = PowerConsumed;
                BatteryChargerFSM.GetState("Done").GetAction<BoolTest>(3).boolVariable = SwitchOn;
                BatteryChargerFSM.GetState("Done").GetAction<BoolTest>(4).boolVariable = BillPaid;
                BatteryChargerFSM.GetState("No elec").GetAction<BoolAllTrue>(4).boolVariables = BoolArray;

                obj.VideoProgram = obj.HousePrograms.transform.GetChild(0).gameObject;
                obj.NightProgram = obj.HousePrograms.transform.GetChild(1).gameObject;
                obj.RallyProgram = obj.HousePrograms.transform.GetChild(2).gameObject;
                obj.VideoProgram.GetPlayMaker("Program").GetState("Topless").GetAction<FloatAdd>(6).floatVariable = PowerConsumed;
                obj.VideoProgram.GetPlayMaker("Program").GetState("Marjatta").GetAction<FloatAdd>(6).floatVariable = PowerConsumed;
                obj.NightProgram.GetPlayMaker("Program").GetState("State 4").GetAction<FloatAdd>(1).floatVariable = PowerConsumed;
                obj.RallyProgram.GetPlayMaker("Program").GetState("State 4").GetAction<FloatAdd>(2).floatVariable = PowerConsumed;

                obj.TvRemote.GetPlayMaker("Input").GetState("Send code").GetAction<SetFsmInt>(0).gameObject.GameObject = obj.HouseTeletext;
                obj.TvRemote.GetPlayMaker("Use").GetState("Open").GetAction<ActivateGameObject>(1).gameObject.GameObject = obj.HouseTeletext;
                obj.TvRemote.GetPlayMaker("Use").GetState("Close").GetAction<ActivateGameObject>(1).gameObject.GameObject = obj.HouseTeletext;
                obj.TvRemote.GetPlayMaker("Use").GetState("Open").GetAction<SetMaterial>(2).gameObject.GameObject = obj.Tv.transform.GetChild(1).gameObject;
                obj.TvRemote.GetPlayMaker("Use").GetState("Close").GetAction<SetMaterial>(2).gameObject.GameObject = obj.Tv.transform.GetChild(1).gameObject;

                SwitchFSM = obj.Tv.transform.GetChild(3).gameObject.GetPlayMaker("Use");
                SwitchFSM.GetState("Wait player").RemoveAction(3);
                SwitchFSM.GetState("Wait player").GetAction<EnableFSM>(1).gameObject.GameObject = obj.TvRemote;
                SwitchFSM.GetState("Close TV 2").GetAction<BoolAllTrue>(12).boolVariables = BoolArray;
                SwitchFSM.GetState("Video").GetAction<ActivateGameObject>(1).gameObject.GameObject = obj.VideoProgram;
                SwitchFSM.GetState("Video").GetAction<ActivateGameObject>(2).gameObject.GameObject = obj.NightProgram;
                SwitchFSM.GetState("Video").GetAction<ActivateGameObject>(3).gameObject.GameObject = obj.RallyProgram;
                SwitchFSM.GetState("Close TV").GetAction<ActivateGameObject>(7).gameObject.GameObject = obj.HouseTeletext;
                SwitchFSM.GetState("Close TV").GetAction<ActivateGameObject>(8).gameObject.GameObject = obj.VideoProgram;
                SwitchFSM.GetState("Close TV").GetAction<ActivateGameObject>(9).gameObject.GameObject = obj.NightProgram;
                SwitchFSM.GetState("Close TV").GetAction<ActivateGameObject>(10).gameObject.GameObject = obj.RallyProgram;
                SwitchFSM.GetState("Night").GetAction<ActivateGameObject>(1).gameObject.GameObject = obj.VideoProgram;
                SwitchFSM.GetState("Night").GetAction<ActivateGameObject>(2).gameObject.GameObject = obj.NightProgram;
                SwitchFSM.GetState("Night").GetAction<ActivateGameObject>(3).gameObject.GameObject = obj.RallyProgram;
                SwitchFSM.GetState("Close TV 2").GetAction<ActivateGameObject>(7).gameObject.GameObject = obj.VideoProgram;
                SwitchFSM.GetState("Close TV 2").GetAction<ActivateGameObject>(8).gameObject.GameObject = obj.NightProgram;
                SwitchFSM.GetState("Close TV 2").GetAction<ActivateGameObject>(9).gameObject.GameObject = obj.RallyProgram;
                SwitchFSM.GetState("Rally").GetAction<ActivateGameObject>(1).gameObject.GameObject = obj.VideoProgram;
                SwitchFSM.GetState("Rally").GetAction<ActivateGameObject>(2).gameObject.GameObject = obj.NightProgram;
                SwitchFSM.GetState("Rally").GetAction<ActivateGameObject>(3).gameObject.GameObject = obj.RallyProgram;

                OvenstoveFSM = obj.Ovenstove.transform.GetChild(1).gameObject.GetPlayMaker("Data");
                OvenstoveFSM.GetState("State 3").GetAction<BoolTest>(0).boolVariable = BillPaid;
                OvenstoveFSM.GetState("State 3").GetAction<BoolTest>(1).boolVariable = SwitchOn;
                OvenstoveFSM.GetState("Plate 1").GetAction<FloatAdd>(2).floatVariable = PowerConsumed;
                OvenstoveFSM.GetState("Plate 2").GetAction<FloatAdd>(2).floatVariable = PowerConsumed;
                OvenstoveFSM.GetState("Plate 3").GetAction<FloatAdd>(2).floatVariable = PowerConsumed;
                OvenstoveFSM.GetState("Plate 4").GetAction<FloatAdd>(2).floatVariable = PowerConsumed;
                OvenstoveFSM.GetState("Grill").RemoveAction(0);
                OvenstoveFSM.GetState("Grill").RemoveAction(1);
                OvenstoveFSM.GetState("Off").RemoveAction(1);
                OvenstoveFSM.GetState("Fire").RemoveAction(3);
                OvenstoveFSM.GetState("Fire").RemoveAction(3);
                OvenstoveFSM.GetState("Start fire").RemoveAction(0);
                OvenstoveFSM.GetState("Start fire").RemoveAction(0);
                OvenstoveFSM.GetState("Grill 2").RemoveAction(0);
                OvenstoveFSM.GetState("Grill 2").RemoveAction(1);
                OvenstoveFSM.GetState("Off 2").RemoveAction(1);
                OvenstoveFSM.GetState("Fire 2").RemoveAction(3);
                OvenstoveFSM.GetState("Fire 2").RemoveAction(3);
                OvenstoveFSM.GetState("Start fire 2").RemoveAction(0);
                OvenstoveFSM.GetState("Start fire 2").RemoveAction(0);
                OvenstoveFSM.GetState("Grill 3").RemoveAction(0);
                OvenstoveFSM.GetState("Grill 3").RemoveAction(1);
                OvenstoveFSM.GetState("Off 3").RemoveAction(1);
                OvenstoveFSM.GetState("Fire 3").RemoveAction(3);
                OvenstoveFSM.GetState("Fire 3").RemoveAction(3);
                OvenstoveFSM.GetState("Start fire 3").RemoveAction(0);
                OvenstoveFSM.GetState("Start fire 3").RemoveAction(0);
                OvenstoveFSM.GetState("Grill 4").RemoveAction(0);
                OvenstoveFSM.GetState("Grill 4").RemoveAction(1);
                OvenstoveFSM.GetState("Off 4").RemoveAction(1);
                OvenstoveFSM.GetState("Fire 4").RemoveAction(3);
                OvenstoveFSM.GetState("Fire 4").RemoveAction(3);
                OvenstoveFSM.GetState("Start fire 4").RemoveAction(0);
                OvenstoveFSM.GetState("Start fire 4").RemoveAction(0);
                OvenstoveFSM.GetState("Plate 1").RemoveAction(4);
                OvenstoveFSM.GetState("Plate 2").RemoveAction(4);
                OvenstoveFSM.GetState("Plate 3").RemoveAction(4);
                OvenstoveFSM.GetState("Plate 4").RemoveAction(4);
                OvenstoveFSM.GetState("State 1").RemoveAction(1);
                OvenstoveFSM.GetState("State 4").RemoveAction(1);
                OvenstoveFSM.GetState("State 5").RemoveAction(1);
                OvenstoveFSM.GetState("State 6").RemoveAction(1);
                OvenstoveFSM.GetState("State 2").RemoveAction(9);
                OvenstoveFSM.GetState("State 2").RemoveAction(9);
                OvenstoveFSM.GetState("State 2").RemoveAction(9);
                OvenstoveFSM.GetState("State 2").RemoveAction(9);
                OvenstoveFSM.GetState("State 2").GetAction<BoolAllTrue>(9).boolVariables = BoolArray;

                obj.ShowerValve.GetPlayMaker("Switch").GetState("Tap").RemoveAction(2);
                obj.ShowerValve.GetPlayMaker("Switch").GetState("Tap").GetAction<BoolAllTrue>(2).boolVariables = BoolArray;
                obj.ShowerValve.GetPlayMaker("Switch").GetState("Shower").RemoveAction(3);
                obj.ShowerValve.GetPlayMaker("Switch").GetState("Shower").GetAction<BoolAllTrue>(3).boolVariables = BoolArray;

                KitchenTrigger.GetState("Check elec").RemoveAction(0);
                KitchenTrigger.GetState("Check elec").GetAction<BoolAllTrue>(0).boolVariables = BoolArray;

                var triggerPlayModeFSM = obj.TriggerPlayMode.GetPlayMaker("PlayerTrigger");
                triggerPlayModeFSM.GetState("State 1").RemoveAction(0);
                triggerPlayModeFSM.GetState("State 1").RemoveAction(0);
                triggerPlayModeFSM.GetState("State 1").RemoveAction(0);
                triggerPlayModeFSM.GetState("State 1").RemoveAction(0);
                triggerPlayModeFSM.SendEvent("ON");

                obj.ComputerLights.GetPlayMaker("Consumption").GetState("State 1").GetAction<FloatAdd>(0).floatVariable = PowerConsumed;
                UnityEngine.Object.Destroy(obj.ComputerLights.GetPlayMaker("Fuseblow"));

                ComputerButtonPower.GetState("Wait player").GetAction<BoolChanged>(3).boolVariable = SwitchOn;
                ComputerButtonPower.GetState("Wait player").GetAction<BoolChanged>(4).boolVariable = BillPaid;
                ComputerButtonPower.GetState("Wait button").GetAction<BoolChanged>(3).boolVariable = SwitchOn;
                ComputerButtonPower.GetState("Switch On").GetAction<BoolTest>(1).boolVariable = SwitchOn;
                ComputerButtonPower.GetState("Switch On").GetAction<BoolTest>(2).boolVariable = BillPaid;
                ComputerButtonPower.GetState("Switch Off").GetAction<BoolTest>(1).boolVariable = SwitchOn;
                ComputerButtonPower.GetState("Switch Off").GetAction<BoolTest>(2).boolVariable = BillPaid;
                ComputerButtonPower.GetState("State 1").RemoveAction(0);
                ComputerButtonPower.GetState("State 1").RemoveAction(0);
                ComputerButtonPower.GetState("State 1").RemoveAction(0);
                ComputerButtonPower.GetState("State 1").GetAction<BoolAllTrue>(0).boolVariables = BoolArray;
                ComputerButtonPower.GetState("Fuse state").GetAction<BoolTest>(0).boolVariable = BillPaid;

                obj.Fusetable.transform.GetChild(2).GetPlayMaker("Speed").GetState("State 1").GetAction<SetFloatValue>(0).floatValue = PowerConsumed;
                obj.Fusetable.transform.GetChild(2).GetPlayMaker("Speed").GetState("State 2").GetAction<FloatOperator>(0).float1 = PowerConsumed;
                obj.Fusetable.transform.GetChild(2).GetPlayMaker("Speed").GetState("State 1").GetAction<FloatOperator>(1).float2 = -1000f;
            }
            catch (Exception ex)
            {
                ModConsole.Error("Error occured while trying to initialize <b>LoppeHouse.Electricity (Phase4)</b>, please send screenshot to Bugs section on NexusMods: " + ex.Message);
            }
        }
        public static void CheckElectricity()
        {
            PayTimer.Value -= Time.deltaTime * Mathf.Max(1, PowerConsumedCost.Value / 100);
            Variables.HouseElectricity = BillPaid.Value && SwitchOn.Value;

            if (!Variables.HouseElectricity && !Variables.ElectricityChecked)
            {
                LightKitchen.enabled = false;
                LightLiving.enabled = false;
                LightPlayer.enabled = false;
                LightBathroom.enabled = false;
                LightParents.enabled = false;
                LightCloset.enabled = false;
                LightHallroom.enabled = false;
                LightGarage.enabled = false;
                LightGarageDash.enabled = false;
                LightOutdoor.enabled = false;
                LightOutdoorTable.enabled = false;
                FridgeLight.SetActive(false);
                FridgeHum.SetActive(false);

                SwitchFSM.SendEvent("CLOSE");
                OvenstoveFSM.SendEvent("ELEC_CUTOFF");
                ComputerButtonPower.SendEvent("ELEC_CUTOFF");
                SwitchFSM.enabled = false;
                ComputerButtonPower.enabled = false;

                FridgePoint.transform.localPosition = new Vector3(0f, 100f, 0f);
                Variables.ElectricityChecked = true;
            }
            if (Variables.HouseElectricity && Variables.ElectricityChecked)
            {
                LightKitchen.enabled = true;
                LightLiving.enabled = true;
                LightPlayer.enabled = true;
                LightBathroom.enabled = true;
                LightParents.enabled = true;
                LightCloset.enabled = true;
                LightHallroom.enabled = true;
                LightGarage.enabled = true;
                LightGarageDash.enabled = true;
                LightOutdoor.enabled = true;
                LightOutdoorTable.enabled = true;
                FridgeLight.SetActive(true);
                FridgeHum.SetActive(true);

                SwitchFSM.enabled = true;
                ComputerButtonPower.enabled = true;

                FridgePoint.transform.localPosition = new Vector3(0f, 1.38429f, 0f);
                Variables.ElectricityChecked = false;
            }
        }
        public static void CalculateConsumption()
        {
            float updateInterval = 1f;
            Variables.TimerCalculations += Time.deltaTime;

            if (Variables.TimerCalculations >= updateInterval)
            {
                BillPaid.Value = PayTimer.Value > 0;
                if (Envelope != null) Envelope.SetActive(PayTimer.Value < 7200f && Variables.PlayerKeys.Value == 1);

                if (LightKitchen != null && LightKitchen.enabled) PowerConsumed.Value += Time.deltaTime * CombustionFactorLight;
                if (LightLiving != null && LightLiving.enabled) PowerConsumed.Value += Time.deltaTime * CombustionFactorLight;
                if (LightPlayer != null && LightPlayer.enabled) PowerConsumed.Value += Time.deltaTime * CombustionFactorLight;
                if (LightBathroom != null && LightBathroom.enabled) PowerConsumed.Value += Time.deltaTime * CombustionFactorLight;
                if (LightParents != null && LightParents.enabled) PowerConsumed.Value += Time.deltaTime * CombustionFactorLight;
                if (LightCloset != null && LightCloset.enabled) PowerConsumed.Value += Time.deltaTime * CombustionFactorLight;
                if (LightHallroom != null && LightHallroom.enabled) PowerConsumed.Value += Time.deltaTime * CombustionFactorLight;
                if (LightGarage != null && LightGarage.enabled) PowerConsumed.Value += Time.deltaTime * CombustionFactorLight;
                if (LightGarageDash != null && LightGarageDash.enabled) PowerConsumed.Value += Time.deltaTime * CombustionFactorLight;
                if (LightOutdoor != null && LightOutdoor.enabled) PowerConsumed.Value += Time.deltaTime * CombustionFactorLight;
                if (LightOutdoorTable != null && LightOutdoorTable.enabled) PowerConsumed.Value += Time.deltaTime * CombustionFactorLight;
                if (FridgeLight != null && FridgeLight.activeInHierarchy) PowerConsumed.Value += Time.deltaTime * CombustionFactorFridge;
                if (FridgeHum != null && FridgeHum.activeInHierarchy) PowerConsumed.Value += Time.deltaTime * CombustionFactorFridge;
                if (KitchenTapDrink != null && KitchenTapDrink.activeInHierarchy) PowerConsumed.Value += Time.deltaTime * CombustionFactorFridge;
                if (ShowerTapDrink != null && ShowerTapDrink.activeInHierarchy) PowerConsumed.Value += Time.deltaTime * CombustionFactorFridge;
                if (ShowerShower != null && ShowerShower.activeInHierarchy) PowerConsumed.Value += Time.deltaTime * CombustionFactorFridge;

                PowerConsumedCost.Value = PowerConsumed.Value * KilowattCost.Value;
                Variables.TimerCalculations = 0f;
            }
        }
        public static void InitValues()
        {
            Kitchen.SetActive(Variables.LightKitchen);
            Living.SetActive(Variables.LightLiving);
            Playerroom.SetActive(Variables.LightPlayer);
            Bathroom.SetActive(Variables.LightBathroom);
            Parentsroom.SetActive(Variables.LightParents);
            Hallroom.SetActive(Variables.LightHallroom);
            Closet.SetActive(Variables.LightCloset);
            Garage.SetActive(Variables.LightGarage);
            GarageDash.SetActive(Variables.LightGarageDash);
            Outdoor.SetActive(Variables.LightOutdoor);
            OutdoorTable.SetActive(Variables.LightOutdoor);
        }
        public static void OnHouseBuy()
        {
            PowerConsumed.Value = 0f;
            PayTimer.Value = 21600f;
        }
        public void ReadData()
        {
            if (SaveLoad.ValueExists(this, "SwitchOn")) SwitchOn.Value = SaveLoad.ReadValue<bool>(this, "SwitchOn");
            else SwitchOn.Value = true;
            if (SaveLoad.ValueExists(this, "PayTimer")) PayTimer.Value = SaveLoad.ReadValue<float>(this, "PayTimer");
            else PayTimer.Value = 21600f;
            if (SaveLoad.ValueExists(this, "PowerConsumed")) PowerConsumed.Value = SaveLoad.ReadValue<float>(this, "PowerConsumed");
            else PowerConsumed.Value = 0f;
            if (SaveLoad.ValueExists(this, "LightKitchen")) Variables.LightKitchen = SaveLoad.ReadValue<bool>(this, "LightKitchen");
            else Variables.LightKitchen = false;
            if (SaveLoad.ValueExists(this, "LightLiving")) Variables.LightLiving = SaveLoad.ReadValue<bool>(this, "LightLiving");
            else Variables.LightLiving = false;
            if (SaveLoad.ValueExists(this, "LightPlayer")) Variables.LightPlayer = SaveLoad.ReadValue<bool>(this, "LightPlayer");
            else Variables.LightPlayer = false;
            if (SaveLoad.ValueExists(this, "LightBathroom")) Variables.LightBathroom = SaveLoad.ReadValue<bool>(this, "LightBathroom");
            else Variables.LightBathroom = false;
            if (SaveLoad.ValueExists(this, "LightParents")) Variables.LightParents = SaveLoad.ReadValue<bool>(this, "LightParents");
            else Variables.LightParents = false;
            if (SaveLoad.ValueExists(this, "LightHallroom")) Variables.LightHallroom = SaveLoad.ReadValue<bool>(this, "LightHallroom");
            else Variables.LightHallroom = false;
            if (SaveLoad.ValueExists(this, "LightCloset")) Variables.LightCloset = SaveLoad.ReadValue<bool>(this, "LightCloset");
            else Variables.LightCloset = false;
            if (SaveLoad.ValueExists(this, "LightGarage")) Variables.LightGarage = SaveLoad.ReadValue<bool>(this, "LightGarage");
            else Variables.LightGarage = false;
            if (SaveLoad.ValueExists(this, "LightGarageDash")) Variables.LightGarageDash = SaveLoad.ReadValue<bool>(this, "LightGarageDash");
            else Variables.LightGarageDash = false;
            if (SaveLoad.ValueExists(this, "LightOutdoor")) Variables.LightOutdoor = SaveLoad.ReadValue<bool>(this, "LightOutdoor");
            else Variables.LightOutdoor = false;
            if (SaveLoad.ValueExists(this, "LightOutdoorTable")) Variables.LightOutdoorTable = SaveLoad.ReadValue<bool>(this, "LightOutdoorTable");
            else Variables.LightOutdoorTable = false;
        }
        public void WriteData()
        {
            SaveLoad.WriteValue(this, "SwitchOn", SwitchOn.Value);
            SaveLoad.WriteValue(this, "PayTimer", PayTimer.Value);
            SaveLoad.WriteValue(this, "PowerConsumed", PowerConsumed.Value);
            SaveLoad.WriteValue(this, "LightKitchen", Variables.LightKitchen);
            SaveLoad.WriteValue(this, "LightLiving", Variables.LightLiving);
            SaveLoad.WriteValue(this, "LightPlayer", Variables.LightPlayer);
            SaveLoad.WriteValue(this, "LightBathroom", Variables.LightBathroom);
            SaveLoad.WriteValue(this, "LightParents", Variables.LightParents);
            SaveLoad.WriteValue(this, "LightHallroom", Variables.LightHallroom);
            SaveLoad.WriteValue(this, "LightCloset", Variables.LightCloset);
            SaveLoad.WriteValue(this, "LightGarage", Variables.LightGarage);
            SaveLoad.WriteValue(this, "LightGarageDash", Variables.LightGarageDash);
            SaveLoad.WriteValue(this, "LightOutdoor", Variables.LightOutdoor);
            SaveLoad.WriteValue(this, "LightOutdoorTable", Variables.LightOutdoorTable);
        }
    }
}