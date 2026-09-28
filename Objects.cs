using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using MSCLoader;
using UnityEngine;
using System;
using System.Data;
using UnityEngine.SocialPlatforms;

namespace LoppeHouse
{
    internal class Objects : LoppeHouse
    {
        public static GameObject Player, ModemCords, PhoneIn, PhoneOut, ModemIn, ModemOut, CordPin, CordPout, CordMin, CordMout;
        public static GameObject House, Garage, Anims, Box, SaleSheet, BoxTrigger, BoxTriggerHouse, Clock;
        public static AudioSource Assemble, Disassemble, SwitchSound, DoorOpenSound, DoorCloseSound, GarageDoorOpen, GarageDoorClose;
        public static FsmBool CaseInstalled;
        public static BoxCollider HouseCollider, BoxBounds;
        public static GameObject Mailbox, Fines, Suski, Map, Murderer, RallyTV, Telephone, CordSocket, Catalog;
        private GameObject Scale, Fireplace, Logging, Savegame, FridgePivot, SleepTrigger, Cords, Doors, FloorJack, ToolsLOD;
        private GameObject NoRainSource, DoorFront, DoorHandle, FridgePaper, Coach, CoachS, Chair1, Chair2;
        private LODGroup LODGroup;

        public static void InitializeObjects()
        {
            var obj = new Objects();

            try
            {
                Player = GameObject.Find("PLAYER");
                Mailbox = GameObject.Find("YARD/PlayerMailBox");
                Fines = GameObject.Find("YARD/Building/Dynamics/Fines");
                Suski = GameObject.Find("JOBS/Suski/SuskiStages/Sleep3");
                Clock = GameObject.Find("YARD/Building/Dynamics/SuomiClock");
                Map = GameObject.Find("YARD/Building/LIVINGROOM/LOD_livingroom/MAP");
                RallyTV = GameObject.Find("RALLY/RallyTV/TVHome");
                Telephone = GameObject.Find("YARD/Building/LIVINGROOM/Telephone");
                CordSocket = GameObject.Find("YARD/Building/LIVINGROOM/Telephone/Cord/Socket");
                ModemCords = GameObject.Find("YARD/Building/LIVINGROOM/Telephone/Cord/ModemCords");
                PhoneIn = GameObject.Find("YARD/Building/LIVINGROOM/Telephone/Cord/PhoneCordIn");
                PhoneOut = GameObject.Find("YARD/Building/LIVINGROOM/Telephone/Cord/PhoneCordOut");
                ModemIn = GameObject.Find("YARD/Building/LIVINGROOM/Telephone/Cord/ModemCords/ModemCordIn");
                ModemOut = GameObject.Find("YARD/Building/LIVINGROOM/Telephone/Cord/ModemCords/ModemCordOut");
                Catalog = GameObject.Find("parts magazine(itemx)");
                Murderer = GameObject.Find("YARD").transform.GetChild(10).gameObject;
            }
            catch (Exception ex)
            {
                ModConsole.Error("Error occured while trying to initialize <b>LoppeHouse.Objects (Phase1)</b>, please send screenshot to Bugs section on NexusMods: " + ex.Message);
            }
            try
            {
                obj.Scale = GameObject.Find("YARD/Building/BEDROOM2/LOD_bedroom2/SCALE");
                obj.Fireplace = GameObject.Find("COTTAGE/Fireplace");
                obj.Logging = GameObject.Find("COTTAGE/Logging");
                obj.Savegame = GameObject.Find("YARD/Building/LIVINGROOM/LOD_livingroom/SAVEGAME");
                obj.FridgePivot = GameObject.Find("YARD/Building/KITCHEN/Fridge/Pivot");
                obj.SleepTrigger = GameObject.Find("YARD/Building/BEDROOM1/LOD_bedroom1/Sleep");
                obj.DoorFront = GameObject.Find("YARD/Building/LIVINGROOM/DoorFront");
                var eFridge = new GameObject("FridgeEmpty");
                var iFridge = UnityEngine.Object.Instantiate(obj.FridgePivot);
                iFridge.transform.SetParent(eFridge.transform);
                eFridge.transform.position = new Vector3(1371.44f, 10.2848f, 798.6646f);
                eFridge.transform.rotation = Quaternion.Euler(270f, 40f, 0f);
                eFridge.transform.localScale = new Vector3(1f, -1f, 1f);
                obj.FridgePaper = GameObject.Find("FridgeEmpty/Pivot(Clone)/fridge_paper");
                obj.Coach = GameObject.Find("House(Clone)/Rooms/Livingroom/Coach");
                obj.CoachS = GameObject.Find("House(Clone)/Rooms/Livingroom/CoachS");
                obj.Chair1 = GameObject.Find("House(Clone)/Rooms/Closet/Table/Chair1");
                obj.Chair2 = GameObject.Find("House(Clone)/Rooms/Closet/Table/Chair2");
                obj.Cords = GameObject.Find("House(Clone)/Rooms/Hallroom/Cords");
                obj.Doors = GameObject.Find("YARD/Building/BATHROOM/DoorBathroom");
                obj.LODGroup = GameObject.Find("YARD/Building").GetComponent<LODGroup>();
                obj.FloorJack = GameObject.Find("ITEMS").transform.GetChild(2).gameObject;
                GameObject.Find("YARD/Building/LIVINGROOM/LOD_livingroom").SetActive(true);
                if (Variables.NewGame) obj.ToolsLOD = GameObject.Find("REPAIRSHOP").transform.GetChild(11).gameObject;
                else obj.ToolsLOD = GameObject.Find("REPAIRSHOP").transform.GetChild(15).gameObject;

                Variables.NewGame = false;
            }
            catch (Exception ex)
            {
                ModConsole.Error("Error occured while trying to initialize <b>LoppeHouse.Objects (Phase2)</b>, please send screenshot to Bugs section on NexusMods: " + ex.Message);
            }
            try
            {
                GameObject.Instantiate(obj.Scale, new Vector3(1371.014f, 9.324783f, 797.7036f), Quaternion.Euler(270f, 310f, 0f));
                GameObject.Instantiate(obj.Fireplace, new Vector3(1369.6f, 9.81167f, 802.5104f), Quaternion.Euler(0f, 310f, 0f));
                GameObject.Instantiate(obj.Logging, new Vector3(1370.724f, 8.638234f, 795.1633f), Quaternion.Euler(0f, 305f, 0f));
                GameObject.Instantiate(obj.Savegame, new Vector3(1367.651f, 9.995152f, 800.2473f), Quaternion.Euler(317.3913f, 270f, 270f));

                GameObject Sleep = GameObject.Instantiate(obj.SleepTrigger);
                Sleep.transform.position = new Vector3(1368.629f, 10.10125f, 804.4318f);
                Sleep.transform.rotation = Quaternion.Euler(0f, 322f, 0f);
                Sleep.transform.GetChild(0).gameObject.transform.localPosition = new Vector3(-0.87f, -0.145f, 1.102f);

                Transform doorFront = GameObject.Instantiate(obj.DoorFront).gameObject.transform;
                obj.DoorHandle = GameObject.Find("DoorFront(Clone)/Pivot/Handle");
                doorFront.position = new Vector3(1367.462f, 10.43116f, 795.6053f);
                doorFront.rotation = Quaternion.Euler(270f, 130.2113f, 0f);
                doorFront.localScale = new Vector3(1f, 1.03f, 1.03f);
                PlayMakerFSM doorFrontFSM = obj.DoorHandle.GetPlayMaker("Use");
                doorFrontFSM.GetState("Test cops").GetAction<BoolTest>(0).boolVariable = false;
                doorFrontFSM.GetState("Cop 1 anim").RemoveAction(0);
                doorFrontFSM.GetState("Check key").GetAction<IntCompare>(0).integer1 = Variables.PlayerKeys;

                GameObject.Find("STORE").transform.GetChild(1).gameObject.SetActive(true);
                obj.NoRainSource = GameObject.Find("STORE/LOD/NoRain");
                Transform NoRainHouse = GameObject.Instantiate(obj.NoRainSource).gameObject.transform;
                NoRainHouse.position = new Vector3(1365.646f, 12.25988f, 802.1425f);
                NoRainHouse.localScale = new Vector3(17.67333f, 12.67478f, 1f);
                NoRainHouse.name = "NoRain";

                Transform door1 = GameObject.Instantiate(obj.Doors).gameObject.transform;
                door1.position = new Vector3(1368.76f, 10.33985f, 798.5154f);
                door1.rotation = Quaternion.Euler(90f, 220f, 0f);
                door1.localScale = new Vector3(1f, 1.03f, 1.05f);
                Transform door2 = GameObject.Instantiate(obj.Doors).gameObject.transform;
                door2.position = new Vector3(1368.072f, 10.32985f, 797.537f);
                door2.rotation = Quaternion.Euler(90f, 40.5f, 0f);
                door2.localScale = new Vector3(1f, 1.03f, 1.06f);
                Transform door3 = GameObject.Instantiate(obj.Doors).gameObject.transform;
                door3.position = new Vector3(1367.412f, 10.32985f, 801.312f);
                door3.rotation = Quaternion.Euler(90f, 130f, 0f);
                door3.localScale = new Vector3(1f, 1.03f, 1.06f);
                Transform door4 = GameObject.Instantiate(obj.Doors).gameObject.transform;
                door4.position = new Vector3(1368.48f, 10.33985f, 801.9792f);
                door4.rotation = Quaternion.Euler(90f, 40f, 0f);
                door4.localScale = new Vector3(-1f, 1.03f, 1.05f);
                Transform door5 = GameObject.Instantiate(obj.Doors).gameObject.transform;
                door5.position = new Vector3(1363.722f, 10.32985f, 802.732f);
                door5.rotation = Quaternion.Euler(90f, 220f, 0f);
                door5.localScale = new Vector3(1f, 1.04f, 1.05f);

                GameObject FloorJack = GameObject.Instantiate(obj.FloorJack);
                FloorJack.transform.position = new Vector3(1361.116f, 9.406562f, 801.4134f);
                FloorJack.transform.rotation = Quaternion.Euler(0f, 128.7163f, 0f);
                FloorJack.name = "Floor Jack(Clone)";

                obj.ToolsLOD.SetActive(true);
                GameObject Tools = GameObject.Instantiate(GameObject.Find("REPAIRSHOP/LOD/ToolCart/Drawer").transform.GetChild(0).gameObject);
                Tools.transform.position = new Vector3(1357.304f, 10.75467f, 805.9493f);
                Tools.transform.rotation = Quaternion.Euler(0f, 131.68f, 0f);
                Tools.SetActive(true);
                GameObject.Find("REPAIRSHOP").GetPlayMaker("LOD").SendEvent("FINISHED");
            }
            catch (Exception ex)
            {
                ModConsole.Error("Error occured while trying to initialize <b>LoppeHouse.Objects (Phase3)</b>, please send screenshot to Bugs section on NexusMods: " + ex.Message);
            }
            try
            {
                GameObject Sounds = House.transform.GetChild(5).gameObject;
                Assemble = Sounds.transform.GetChild(0).GetComponent<AudioSource>();
                Disassemble = Sounds.transform.GetChild(1).GetComponent<AudioSource>();
                SwitchSound = Sounds.transform.GetChild(2).GetComponent<AudioSource>();
                DoorOpenSound = Sounds.transform.GetChild(3).GetComponent<AudioSource>();
                DoorCloseSound = Sounds.transform.GetChild(4).GetComponent<AudioSource>();
                GarageDoorOpen = Sounds.transform.GetChild(5).GetComponent<AudioSource>();
                GarageDoorClose = Sounds.transform.GetChild(6).GetComponent<AudioSource>();

                obj.Coach.layer = LayerMask.NameToLayer("Parts");
                obj.CoachS.layer = LayerMask.NameToLayer("Parts");
                obj.Chair1.layer = LayerMask.NameToLayer("Parts");
                obj.Chair2.layer = LayerMask.NameToLayer("Parts");

                CordPin = obj.Cords.transform.GetChild(0).gameObject;
                CordPout = obj.Cords.transform.GetChild(1).gameObject;
                CordMin = obj.Cords.transform.GetChild(2).gameObject;
                CordMout = obj.Cords.transform.GetChild(3).gameObject;

                HouseCollider = House.GetComponent<BoxCollider>();
                BoxTrigger = House.transform.GetChild(3).gameObject;
                BoxTriggerHouse = House.transform.GetChild(4).gameObject;
                BoxBounds = Box.GetComponent<BoxCollider>();
                Box.name = "Box(Clone)";
                Box.MakePickable();

                CaseInstalled = GameObject.Find("Database/DatabaseComputer/Case").GetPlayMaker("Data").FsmVariables.GetFsmBool("Installed").Value;
                GameObject.Find("YARD").GetPlayMaker("LOD").GetState("OFF").GetAction<ActivateGameObject>(5).Enabled = false;

                UnityEngine.Object.Destroy(obj.FridgePaper);
                UnityEngine.Object.Destroy(obj.LODGroup);
                UnityEngine.Object.Destroy(GameObject.Find("COTTAGE").GetComponent<LODGroup>());
            }
            catch (Exception ex)
            {
                ModConsole.Error("Error occured while trying to initialize <b>LoppeHouse.Objects (Phase4)</b>, please send screenshot to Bugs section on NexusMods: " + ex.Message);
            }
        }        
        public static void CheckBurnt()
        {
            FsmVariables HouseFire = GameObject.Find("YARD/Building/HOUSEFIRE").GetComponent<PlayMakerFSM>().FsmVariables;

            FsmBool Fire1 = HouseFire.GetFsmBool("FireBathroom");
            FsmBool Fire2 = HouseFire.GetFsmBool("FireBedroom1");
            FsmBool Fire3 = HouseFire.GetFsmBool("FireBedroom2");
            FsmBool Fire4 = HouseFire.GetFsmBool("FireKitchen");
            FsmBool Fire5 = HouseFire.GetFsmBool("FireLivingroom");
            FsmBool Fire6 = HouseFire.GetFsmBool("FireSauna");

            if (Fire1.Value == true || Fire2.Value == true || Fire3.Value == true ||
                Fire4.Value == true || Fire5.Value == true || Fire6.Value == true)
            {
                Variables.HouseBurnt = true;
            }
            else Variables.HouseBurnt = false;
        }
        public static void PackItems()
        {
            Telephone.SetActive(false);
            Mailbox.SetActive(false);
            Map.SetActive(false);
            Fines.SetActive(false);
            Catalog.SetActive(false);
            Clock.SetActive(false);

            Box.transform.GetChild(0).gameObject.SetActive(true);
            Box.transform.GetChild(1).gameObject.SetActive(false);
        }
        public static void UnpackItemsHouse()
        {
            Telephone.SetActive(true);
            Mailbox.SetActive(true);
            Map.SetActive(true);
            Catalog.SetActive(true);
            Clock.SetActive(true);
            if (Fines.activeInHierarchy) Fines.SetActive(true);

            Clock.transform.localPosition = new Vector3(-1380.205f, 793.3945f, 11.57282f);
            Clock.transform.localRotation = Quaternion.Euler(310f, 270f, 270f);
            Telephone.transform.localPosition = new Vector3(-1513.855f, -478.2803f, 9.6475f); 
            Telephone.transform.localRotation = Quaternion.Euler(0f, 0f, 358f);
            CordSocket.transform.localPosition = new Vector3(-1.391999f, 0.02400057f, 0.1429995f);         
            PhoneIn.transform.localPosition = new Vector3(-1.370999f, 0.0449999f, 0.1159997f);
            PhoneOut.transform.localPosition = new Vector3(-1.477999f, 0.1869999f, -0.05100053f);
            ModemIn.transform.localPosition = new Vector3(-1.379f, 0.04300142f, 0.1209993f);
            ModemOut.transform.localPosition = new Vector3(-1.01f, 0.15f, -0.05f);
            Map.transform.localPosition = new Vector3(-1513.475f, -478.575f, 10.199f);
            Map.transform.localRotation = Quaternion.Euler(0f, 0f, 311f);
            Mailbox.transform.position = new Vector3(1326.076f, 6.884768f, 781.9263f); 
            Mailbox.transform.rotation = Quaternion.Euler(-90f, 0f, 310f);   
            Fines.transform.localPosition = new Vector3(-1382.144f, 792.8969f, 10.74006f);
            Murderer.transform.position = new Vector3(1367.576f, 9.974738f, 802.0633f);
            Suski.transform.position = new Vector3(1367.381f, 10.10125f, 804.7681f);
            RallyTV.transform.position = new Vector3(1371.085f, 10.54366f, 802.0133f);
            Catalog.transform.localPosition = new Vector3(1379.711f, 10.08f, 789.088f);
            Catalog.transform.localRotation = Quaternion.Euler(0f, 204.43f, 0f);

            Box.transform.GetChild(0).gameObject.SetActive(false);
            Box.transform.GetChild(1).gameObject.SetActive(true);
        }
        public static void UnpackItemsPlayerHouse()
        {
            Telephone.SetActive(true);
            Mailbox.SetActive(true);
            Map.SetActive(true);
            Catalog.SetActive(true);
            Clock.SetActive(true);
            if (Fines.activeInHierarchy) Fines.SetActive(true);
 
            Clock.transform.localPosition = new Vector3(1.024578f, 2.574493f, 1.99282f);
            Clock.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            Telephone.transform.localPosition = new Vector3(-1.619954f, 3.609738f, -0.2812682f);
            Telephone.transform.localRotation = Quaternion.Euler(0f, 0f, 47.4f);
            CordSocket.transform.localPosition = new Vector3(-0.000999386f, 0.00100057f, 0.00799947f);
            PhoneIn.transform.localPosition = new Vector3(-0.0009992123f, 0.0009999275f, 0.008999646f);
            PhoneOut.transform.localPosition = new Vector3(-0.0009992123f, 0.0009999275f, 0.008999467f);
            ModemIn.transform.localPosition = new Vector3(-0.0009995191f, 0.001001422f, 0.008999288f);
            ModemOut.transform.localPosition = new Vector3(0f, 0f, 0.01f);
            Map.transform.localPosition = new Vector3(-1.445f, 3.335f, 0.7389995f);
            Map.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            Mailbox.transform.position = new Vector3(59.457f, -0.867f, -26.536f);
            Mailbox.transform.rotation = Quaternion.Euler(-90f, 0f, -177.226f);
            Fines.transform.localPosition = new Vector3(-0.644181f, 2.227185f, 0.9100659f);
            Murderer.transform.position = new Vector3(-10.388f, 0.094f, 11.532f);
            Suski.transform.position = new Vector3(-4.555f, -0.032f, 8.779f);
            RallyTV.transform.position = new Vector3(-4.698f, 0.1f, 5.741f);
            Catalog.transform.localPosition = new Vector3(1.4798f, 0.4261f, -11.267f);
            Catalog.transform.localRotation = Quaternion.Euler(0f, 278.4277f, 0f);

            Box.transform.GetChild(0).gameObject.SetActive(false);
            Box.transform.GetChild(1).gameObject.SetActive(true);
        }
        public void ResetBox()
        {
            Box.transform.position = new Vector3(-2.962838f, -0.4208454f, 10.28614f);
            Box.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        public void ReadData()
        {
            if (SaveLoad.ValueExists(this, "ClockPosition")) Clock.transform.localPosition = SaveLoad.ReadValue<Vector3>(this, "ClockPosition");
            else Clock.transform.localPosition = new Vector3(1.024578f, 2.574493f, 1.99282f);
            if (SaveLoad.ValueExists(this, "ClockRotation")) Clock.transform.localRotation = SaveLoad.ReadValue<Quaternion>(this, "ClockRotation");
            else Clock.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            if (SaveLoad.ValueExists(this, "TelephonePosition")) Telephone.transform.localPosition = SaveLoad.ReadValue<Vector3>(this, "TelephonePosition");
            else Telephone.transform.localPosition = new Vector3(-1.619954f, 3.609738f, -0.2812682f);
            if (SaveLoad.ValueExists(this, "TelephoneRotation")) Telephone.transform.localRotation = SaveLoad.ReadValue<Quaternion>(this, "TelephoneRotation");
            else Telephone.transform.localRotation = Quaternion.Euler(0f, 0f, 47.4f);
            if (SaveLoad.ValueExists(this, "CordSocketPosition")) CordSocket.transform.localPosition = SaveLoad.ReadValue<Vector3>(this, "CordSocketPosition");
            else CordSocket.transform.localPosition = new Vector3(-0.000999386f, 0.00100057f, 0.00799947f);
            if (SaveLoad.ValueExists(this, "PhoneInPosition")) PhoneIn.transform.localPosition = SaveLoad.ReadValue<Vector3>(this, "PhoneInPosition");
            else PhoneIn.transform.localPosition = new Vector3(-0.0009992123f, 0.0009999275f, 0.008999646f);
            if (SaveLoad.ValueExists(this, "PhoneOutPosition")) PhoneOut.transform.localPosition = SaveLoad.ReadValue<Vector3>(this, "PhoneOutPosition");
            else PhoneOut.transform.localPosition = new Vector3(-0.0009992123f, 0.0009999275f, 0.008999467f);
            if (SaveLoad.ValueExists(this, "ModemInPosition")) ModemIn.transform.localPosition = SaveLoad.ReadValue<Vector3>(this, "ModemInPosition");
            else ModemIn.transform.localPosition = new Vector3(-0.0009995191f, 0.001001422f, 0.008999288f);
            if (SaveLoad.ValueExists(this, "ModemOutPosition")) ModemOut.transform.localPosition = SaveLoad.ReadValue<Vector3>(this, "ModemOutPosition");
            else ModemOut.transform.localPosition = new Vector3(0f, 0f, 0.01f);
            if (SaveLoad.ValueExists(this, "MailboxPosition")) Mailbox.transform.position = SaveLoad.ReadValue<Vector3>(this, "MailboxPosition");
            else Mailbox.transform.position = new Vector3(59.457f, -0.867f, -26.536f);
            if (SaveLoad.ValueExists(this, "MailboxRotation")) Mailbox.transform.rotation = SaveLoad.ReadValue<Quaternion>(this, "MailboxRotation");
            else Mailbox.transform.rotation = Quaternion.Euler(-90f, 0f, -177.226f);
            if (SaveLoad.ValueExists(this, "MapPosition")) Map.transform.localPosition = SaveLoad.ReadValue<Vector3>(this, "MapPosition");
            else Map.transform.localPosition = new Vector3(-1.445f, 3.335f, 0.7389995f);
            if (SaveLoad.ValueExists(this, "MapRotation")) Map.transform.localRotation = SaveLoad.ReadValue<Quaternion>(this, "MapRotation");
            else Map.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            if (SaveLoad.ValueExists(this, "FinesPosition")) Fines.transform.localPosition = SaveLoad.ReadValue<Vector3>(this, "FinesPosition");
            else Fines.transform.localPosition = new Vector3(-0.644181f, 2.227185f, 0.9100659f);
            if (SaveLoad.ValueExists(this, "MurdererPosition")) Murderer.transform.position = SaveLoad.ReadValue<Vector3>(this, "MurdererPosition");
            else Murderer.transform.position = new Vector3(-10.388f, 0.094f, 11.532f);
            if (SaveLoad.ValueExists(this, "SuskiPosition")) Suski.transform.position = SaveLoad.ReadValue<Vector3>(this, "SuskiPosition");
            else Suski.transform.position = new Vector3(-4.555f, -0.032f, 8.779f);
            if (SaveLoad.ValueExists(this, "RallyTVPosition")) RallyTV.transform.position = SaveLoad.ReadValue<Vector3>(this, "RallyTVPosition");
            else RallyTV.transform.position = new Vector3(-4.698f, 0.1f, 5.741f);
            if (SaveLoad.ValueExists(this, "CatalogPosition")) Catalog.transform.localPosition = SaveLoad.ReadValue<Vector3>(this, "CatalogPosition");
            else Catalog.transform.localPosition = new Vector3(1.4798f, 0.4261f, -11.267f);
            if (SaveLoad.ValueExists(this, "CatalogRotation")) Catalog.transform.localRotation = SaveLoad.ReadValue<Quaternion>(this, "CatalogRotation");
            else Catalog.transform.localRotation = Quaternion.Euler(0f, 278.4277f, 0f);
        }
        public void WriteData()
        {
            SaveLoad.WriteValue(this, "ClockPosition", Clock.transform.localPosition);
            SaveLoad.WriteValue(this, "ClockRotation", Clock.transform.localRotation);
            SaveLoad.WriteValue(this, "TelephonePosition", Telephone.transform.localPosition);
            SaveLoad.WriteValue(this, "TelephoneRotation", Telephone.transform.localRotation);
            SaveLoad.WriteValue(this, "CordSocketPosition", CordSocket.transform.localPosition);
            SaveLoad.WriteValue(this, "PhoneInPosition", PhoneIn.transform.localPosition);
            SaveLoad.WriteValue(this, "PhoneOutPosition", PhoneOut.transform.localPosition);
            SaveLoad.WriteValue(this, "ModemInPosition", ModemIn.transform.localPosition);
            SaveLoad.WriteValue(this, "ModemOutPosition", ModemOut.transform.localPosition);
            SaveLoad.WriteValue(this, "MailboxPosition", Mailbox.transform.position);
            SaveLoad.WriteValue(this, "MailboxRotation", Mailbox.transform.rotation);
            SaveLoad.WriteValue(this, "MapPosition", Map.transform.localPosition);
            SaveLoad.WriteValue(this, "MapRotation", Map.transform.localRotation);
            SaveLoad.WriteValue(this, "FinesPosition", Fines.transform.localPosition);
            SaveLoad.WriteValue(this, "MurdererPosition", Murderer.transform.position);
            SaveLoad.WriteValue(this, "SuskiPosition", Suski.transform.position);
            SaveLoad.WriteValue(this, "RallyTVPosition", RallyTV.transform.position);
            SaveLoad.WriteValue(this, "CatalogPosition", Catalog.transform.localPosition);
            SaveLoad.WriteValue(this, "CatalogRotation", Catalog.transform.localRotation);
        }
    }
}
