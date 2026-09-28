using MSCLoader;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using FridgeAPI;
using MSCLoader.Helper;
using System.Data;
using UnityEngine.SocialPlatforms;

namespace LoppeHouse
{
    public class LoppeHouse : Mod
    {
        public override string ID => "LoppeHouse";
        public override string Name => "LoppeHouse";
        public override string Author => "destruxxion";
        public override string Version => "2.1.2";
        public override string Description => "Mod adds beautiful house in Loppe with garage, sauna and other cool features!";

        public static SettingsSliderInt ShadowSlider;
        private bool collisionEnabled = true;
        private bool raycastEnabled = true;
        private bool electricityCheckEnabled = true;
        private bool consumptionEnabled = true;
        private bool positionCheckEnabled = true;
        public override void ModSetup()
        {
            SetupFunction(Setup.OnMenuLoad, Mod_OnMenuLoad);
            SetupFunction(Setup.OnNewGame, Mod_OnNewGame);
            SetupFunction(Setup.OnLoad, Mod_OnLoad);
            SetupFunction(Setup.OnSave, Mod_OnSave);
            SetupFunction(Setup.Update, Mod_Update);
            SetupFunction(Setup.ModSettings, Mod_Settings);
            SetupFunction(Setup.OnModEnabled, Mod_OnModEnabled);
            SetupFunction(Setup.PreLoad, Mod_PreLoad);
#if DEBUG
            SetupFunction(Setup.OnGUI, Mod_OnGUI);
#endif
        }
#if DEBUG
        private void Mod_OnGUI()
        {
            GUI.Label(new Rect(20, 80, 200, 50), "Variables.HouseElectricity: " + GameObject.Find("SATSUMA(557kg, 248)").transform.position);
            GUI.Label(new Rect(20, 100, 200, 50), "Variables.ItemsPacked: " + Variables.ItemsPacked);
            GUI.Label(new Rect(20, 120, 200, 50), "Variables.HouseBurnt: " + Variables.HouseBurnt);
            Electricity.ElectricityGUI();
        }
#endif
        private void Mod_Settings()
        {
            var obj = new Objects();
            Settings.AddHeader(this, "General Settings");
            Settings.AddText(this, "You can manually reset saved values for LoppeHouse" +
                " if you need, but save file resets automatically when starting a new game.");
            Settings.AddButton(this, "Reset Save File", ReloadSave);
            Settings.AddText(this, "Resets box to it's initial position in livingroom");
            Settings.AddButton(this, "Reset Box Position", obj.ResetBox);
            Settings.AddText(this, "Use this button to set your own images for posters, paintings, " +
                "flag and wallpapers in game. No need to go to main menu anymore!");
            Settings.AddButton(this, "Load Textures", LoadTextures);
            Settings.AddHeader(this, "Graphics Settings");
            Settings.AddText(this, "Here you can change some graphics settings, more settings in development...");
            ShadowSlider = Settings.AddSlider("Shadows", "Light Shadows Quality", 0, 2, 1, 
                GraphicsSettings.UpdateShadowQuality, GraphicsSettings.ShadowValues.ToArray());
        }
        private void Mod_OnModEnabled()
        {
            if (ModLoader.CurrentScene == CurrentScene.MainMenu)
            {
                if (GameObject.Find("ElectricAppliances(Clone)") == null)
                {
                    Mod_OnMenuLoad();
                    ModConsole.Print("<b>LoppeHouse</b>: AssetBundle succesfully initialized!");
                }
                else ModConsole.Print("<color=yellow><b>LoppeHouse</b>: All objects are already loaded!</color>");
            }
            else
            {
                ModConsole.Print("<color=yellow><b>WARNING:</b> You enabled <b>LoppeHouse</b> in game, " +
                    "you have to restart My Summer Car.</color>");
            }
        }
        private void Mod_OnNewGame() 
        {
            Variables.NewGame = true;
            ModConsole.Print("<color=green><b>LoppeHouse</b>: Savefile reset.</color>");
        }
        private void Mod_OnMenuLoad()
        {
            if (ModLoader.IsModPresent("MOP"))
            {
                ModConsole.Error("You are using <b>LoppeHouse</b> with <b>Modern Optimization Plugin</b>, " +
                    "install rule file if you don't have one. Developer is not responsible for any " +
                    "errors with <b>LoppeHouse</b> if you are using MOP!");
            }
            if (!ModLoader.IsModPresent("FridgeAPI")) ModConsole.Error("FridgeAPI isn't " +
                "found or disabled, <b>LoppeHouse</b> won't work.");
            else
            {
                try
                {
                    AssetBundle ab = LoadAssets.LoadBundle(this, "loppehouse.unity3d");
                    GameObject garageSource = ab.LoadAsset<GameObject>("Garage.prefab");
                    GameObject houseSource = ab.LoadAsset<GameObject>("House.prefab");
                    GameObject boxSource = ab.LoadAsset<GameObject>("Box.prefab");
                    GameObject saleSource = ab.LoadAsset<GameObject>("SaleSheet.prefab");
                    GameObject animationsSource = ab.LoadAsset<GameObject>("Animations.prefab");
                    GameObject electricitySource = ab.LoadAsset<GameObject>("ElectricAppliances.prefab");

                    Objects.House = UnityEngine.Object.Instantiate(houseSource);
                    Objects.Garage = UnityEngine.Object.Instantiate(garageSource);
                    Objects.Box = UnityEngine.Object.Instantiate(boxSource);
                    Objects.SaleSheet = UnityEngine.Object.Instantiate(saleSource);
                    Objects.Anims = UnityEngine.Object.Instantiate(animationsSource);
                    Electricity.ElectricAppliances = UnityEngine.Object.Instantiate(electricitySource);

                    UnityEngine.Object.DontDestroyOnLoad(Objects.Garage);
                    UnityEngine.Object.DontDestroyOnLoad(Objects.House);
                    UnityEngine.Object.DontDestroyOnLoad(Objects.Box);
                    UnityEngine.Object.DontDestroyOnLoad(Objects.SaleSheet);
                    UnityEngine.Object.DontDestroyOnLoad(Objects.Anims);
                    UnityEngine.Object.DontDestroyOnLoad(Electricity.ElectricAppliances);

                    ab.Unload(false);

                    Animations.InitializeAnimations();
                    var animData = new Animations();
                    animData.ReadData();
                    Animations.UpdateAnimations();

                    ModConsole.Print("<b>LoppeHouse</b>: AssetBundle succesfully initialized!");
                }
                catch (Exception ex)
                {
                    ModConsole.Error("An error occured, <b>LoppeHouse</b> can't load:" + ex.Message);
                }
            }
        }
        private void Mod_PreLoad()
        {
            // UnityEngine.Object.Destroy(GameObject.Find("DINGONBIISI"));
            if (ModLoader.IsModPresent("Minimap"))
            {
                var obj = new GameObject("DINGONBIISI");
                obj.transform.SetParent(GameObject.Find("MAP/Buildings").transform);
                obj.transform.localPosition = new Vector3(1375.279f, 8.288667f, 842.4467f);
            }
        }
        private void Mod_OnLoad()
        {
            Objects.CheckBurnt();
            if (!Variables.HouseBurnt)
            {
                try
                {
                    var Fridge = GameObject.Find("House(Clone)/Rooms/Kitchen/Fridge");
                    var FridgePoint = new GameObject("Freezer");
                    FridgePoint.transform.SetParent(Fridge.transform, false);
                    FridgePoint.transform.localPosition = new Vector3(0f, 1.38429f, 0f);
                    FridgePoint.transform.localScale = new Vector3(0.84f, 0.65f, 1f);
                    FridgePoint.AddComponent<BoxCollider>().isTrigger = true;
                    FridgePoint.AddComponent<MeshRenderer>();
                    var fridgeBehaviour = FridgePoint.AddComponent<Fridge>();
                    fridgeBehaviour.fridgeType = FridgeType.Fridge;

                    InteractionHandler.InitializeCamera();
                    Objects.InitializeObjects();
                    Electricity.PowerCore();
                    Electricity.InitializeElectricity();
                    AssemblySystem.InitializeGarage();
                    LoadTextures();
                    ValuesSave();
                    SwitchHandler.InitializeSwitchStates();
                    SwitchHandler.UpdateSwitchRotations();
                    SlidingDoors.InitializeTriggers();
                    SlidingDoors.CheckCollision();
                    GraphicsSettings.UpdateShadowQuality();
                    Miscellaneous.CheckPlayerLocation();
                    AssemblySystem.CheckObjectState();
                    Electricity.InitValues();

                    if (Variables.PlayerKeys.Value == 1) UnityEngine.Object.Destroy(Objects.SaleSheet);
                    if (Variables.ItemsPacked)
                    {
                        Objects.Telephone.SetActive(false);
                        Objects.Mailbox.SetActive(false);
                        Objects.Map.SetActive(false);
                        Objects.Fines.SetActive(false);
                        Objects.Catalog.SetActive(false);
                        Objects.Clock.SetActive(false);

                        Objects.Box.transform.GetChild(0).gameObject.SetActive(true);
                        Objects.Box.transform.GetChild(1).gameObject.SetActive(false);
                    }
                    Variables.ModLoaded = true;

                    ModConsole.Print("LoppeHouse: Mod successfully initialized.");
                }
                catch (Exception ex)
                {
                    ModConsole.Error($"An unexpected error occured while trying to initialize LoppeHouse: <b>{ex.StackTrace}</b>");
                }
            }
            else
            {
                UnityEngine.Object.Destroy(Objects.House);
                UnityEngine.Object.Destroy(Objects.Garage);
                UnityEngine.Object.Destroy(Objects.Box);
                UnityEngine.Object.Destroy(Objects.SaleSheet);
                UnityEngine.Object.Destroy(Electricity.ElectricAppliances);
                ModConsole.Error("You burnt your house, <b>LoppeHouse</b> won't work anymore.");
            }
        }
        private void Mod_Update()
        {
            if (ModLoader.CurrentScene == CurrentScene.Game && Variables.ModLoaded)
            {
                if (collisionEnabled)
                {
                    try
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0)) SlidingDoors.CheckCollision();
                    }
                    catch (Exception ex)
                    {
                        collisionEnabled = false;
                        ModConsole.Error("An error occurred in <b>SlidingDoors.CheckCollision</b>, disabling this method. " +
                            "You can continue playing. Send the output.log file created in your <b>Assets/LoppeHouse</b> folder " +
                            "to Bugs section on NexusMods.");
                        ExceptionHandler(ex, "SlidingDoors.CheckCollision");
                    }
                }
                if (raycastEnabled)
                {
                    try
                    {
                        InteractionHandler.HandleRaycast();
                    }
                    catch (Exception ex)
                    {
                        raycastEnabled = false;
                        ModConsole.Error("An error occurred in <b>InteractionHandler.HandleRaycast</b>, disabling this method. " +
                            "You can continue playing. Send the output.log file created in your <b>Assets/LoppeHouse</b> folder " +
                            "to Bugs section on NexusMods.");
                        ExceptionHandler(ex, "InteractionHandler.HandleRaycast");
                    }
                }
                if (electricityCheckEnabled)
                {
                    try
                    {
                        Electricity.CheckElectricity();
                    }
                    catch (Exception ex)
                    {
                        electricityCheckEnabled = false;
                        ModConsole.Error("An error occurred in <b>Electricity.CheckElectricity</b>, disabling this method. " +
                            "You can continue playing. Send the output.log file created in your <b>Assets/LoppeHouse</b> folder " +
                            "to Bugs section on NexusMods.");
                        ExceptionHandler(ex, "Electricity.CheckElectricity");
                    }
                }
                if (consumptionEnabled)
                {
                    try
                    {
                        Electricity.CalculateConsumption();
                    }
                    catch (Exception ex)
                    {
                        consumptionEnabled = false;
                        ModConsole.Error("An error occurred in <b>Electricity.CalculateConsumption</b>, disabling this method. " +
                            "You can continue playing. Send the output.log file created in your <b>Assets/LoppeHouse</b> folder " +
                            "to Bugs section on NexusMods.");
                        ExceptionHandler(ex, "Electricity.CalculateConsumption");
                    }
                }
                if (positionCheckEnabled)
                {
                    try
                    {
                        Miscellaneous.CheckPlayerLocation();
                    }
                    catch (Exception ex)
                    {
                        positionCheckEnabled = false;
                        ModConsole.Error("An error occurred in <b>Miscellaneous.CheckPlayerLocation</b>, disabling this method. " +
                            "You can continue playing. Send the output.log file created in your <b>Assets/LoppeHouse</b> folder " +
                            "to Bugs section on NexusMods.");
                        ExceptionHandler(ex, "Miscellaneous.CheckPlayerLocation");
                    }
                }

                GraphicsSettings.CheckLOD();
                Miscellaneous.TimerGUI();
            }
        }
        private void Mod_OnSave()
        {
            GameObject.Find("YARD/Building/Garage/BatteryCharger/TriggerCharger").gameObject.GetPlayMaker("Assembly").SendEvent("FINISHED");
            GameObject.Find("BatteryCharger(Clone)/TriggerCharger").gameObject.GetPlayMaker("Assembly").SendEvent("FINISHED");

            SaveLoad.WriteValue(this, "PlayerKeys", Variables.PlayerKeys.Value);
            SaveLoad.WriteValue(this, "ItemsPacked", Variables.ItemsPacked);
            SaveLoad.WriteValue(this, "BoxPosition", Objects.Box.transform.position);
            SaveLoad.WriteValue(this, "BoxRotation", Objects.Box.transform.rotation);

            var objData = new Objects();
            objData.WriteData();
            var elecData = new Electricity();
            elecData.WriteData();
            var animData = new Animations();
            animData.WriteData();
            var assemblyData = new AssemblySystem();
            assemblyData.WriteData();
        }
        private void ReloadSave()
        {
            if (ModLoader.CurrentScene == CurrentScene.MainMenu)
            {
                if (SaveLoad.ValueExists(this, "PlayerKeys"))
                {
                    try
                    {
                        List<String> saveValues = new List<String>
{
    "PlayerKeys",
    "ItemsPacked",
    "SwitchOn",
    "PayTimer",
    "PowerConsumed",
    "LightKitchen",
    "LightLiving",
    "LightPlayer",
    "LightBathroom",
    "LightParents",
    "LightHallroom",
    "LightCloset",
    "LightGarage",
    "LightGarageDash",
    "LightOutdoor",
    "LightOutdoorTable",
    "IsOpen1",
    "IsOpen2",
    "IsOpen3",
    "IsOpen4",
    "IsOpen5",
    "IsOpen6",
    "IsOpen7",
    "IsOpen8",
    "IsOpen9",
    "IsOpen10",
    "IsOpen11",
    "IsOpen12",
    "IsOpenGL",
    "IsOpenGR",
    "IsOpenWardrobe1",
    "IsOpenWardrobe2",
    "IsOpenWardrobe3",
    "BoxPosition",
    "BoxRotation",
    "ClockPosition",
    "ClockRotation",
    "TelephonePosition",
    "TelephoneRotation",
    "CordSocketPosition",
    "PhoneInPosition",
    "PhoneOutPosition",
    "ModemInPosition",
    "ModemOutPosition",
    "MailboxPosition",
    "MailboxRotation",
    "MapPosition",
    "MapRotation",
    "FinesPosition",
    "MurdererPosition",
    "SuskiPosition",
    "RallyTVPosition",
    "CatalogPosition",
    "CatalogRotation",
    "HoodInstalled",
    "BootlidInstalled",
    "DoorLInstalled",
    "DoorRInstalled",
    "FenderLInstalled",
    "FenderRInstalled",
    "BumperFInstalled",
    "BumperRInstalled",
    "GrilleInstalled",
    "GrilleGTInstalled",
};
                        for (int i = 0; i < saveValues.Count; i++) SaveLoad.DeleteValue(this, saveValues[i]);
                        ModConsole.Print("<color=green>LoppeHouse: Savefile reset.</color>");
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler(ex, "ReloadSave");
                    }
                }
                else ModConsole.Print("<color=yellow>LoppeHouse: Savefile is already reset.</color>");
            }
            else ModConsole.Warning("<color=yellow><b>LoppeHouse</b>: In order to reset savefile, you need to be in the main menu.</color>");
        }
        private void ValuesSave()
        {
            if (SaveLoad.ValueExists(this, "PlayerKeys")) Variables.PlayerKeys.Value = SaveLoad.ReadValue<int>(this, "PlayerKeys");
            else Variables.PlayerKeys.Value = 0;
            if (SaveLoad.ValueExists(this, "ItemsPacked")) Variables.ItemsPacked = SaveLoad.ReadValue<bool>(this, "ItemsPacked");
            else Variables.ItemsPacked = false;
            if (SaveLoad.ValueExists(this, "BoxPosition")) Objects.Box.transform.position = SaveLoad.ReadValue<Vector3>(this, "BoxPosition");
            else Objects.Box.transform.position = new Vector3(-2.962838f, -0.4208454f, 10.28614f);
            if (SaveLoad.ValueExists(this, "BoxRotation")) Objects.Box.transform.rotation = SaveLoad.ReadValue<Quaternion>(this, "BoxRotation");
            else Objects.Box.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

            var objData = new Objects();
            objData.ReadData();
            var elecData = new Electricity();
            elecData.ReadData();
            var animData = new Animations();
            animData.ReadData();
            var assemblyData = new AssemblySystem();
            assemblyData.ReadData();
        }
        private void ApplyTexture(string modAssetsFolder, string fileName, string gameObjectPath, bool useChild = false, int childIndex = 0)
        {
            string filePath = Path.Combine(modAssetsFolder, fileName);
            if (File.Exists(filePath))
            {
                Texture2D texture = LoadAssets.LoadTexture(this, fileName, false);
                Material material = new Material(Shader.Find("Diffuse"))
                {
                    mainTexture = texture
                };
                GameObject targetObject = GameObject.Find(gameObjectPath).gameObject;
                if (useChild)
                {
                    targetObject = targetObject.transform.GetChild(childIndex).gameObject;
                }
                targetObject.GetComponent<Renderer>().sharedMaterial = material;
            }
        }
        private void LoadTextures()
        {
            string modAssetsFolder = ModLoader.GetModAssetsFolder(this);

            ApplyTexture(modAssetsFolder, "Flag.png", "Garage(Clone)/Objects/GarageFlag");
            ApplyTexture(modAssetsFolder, "GaragePoster.png", "d_GaragePoster");
            ApplyTexture(modAssetsFolder, "HousePoster1.png", "d_HousePoster1");
            ApplyTexture(modAssetsFolder, "HousePoster2.png", "d_HousePoster2");
            ApplyTexture(modAssetsFolder, "WallLivingroom.png", "WallLivingroom");
            ApplyTexture(modAssetsFolder, "WallPlayer.png", "WallPlayerroom");
            ApplyTexture(modAssetsFolder, "WallHall.png", "WallHallroom");
            ApplyTexture(modAssetsFolder, "Painting1.png", "d_HousePainting1", true, 1);
            ApplyTexture(modAssetsFolder, "Painting2.png", "d_HousePainting2", true, 1);
            ApplyTexture(modAssetsFolder, "Painting3.png", "d_HousePainting3", true, 1);
            ApplyTexture(modAssetsFolder, "Painting4.png", "d_HousePainting4", true, 1);
            ApplyTexture(modAssetsFolder, "Painting5.png", "d_HousePainting5", true, 1);
        }
        public void ExceptionHandler(Exception ex, string str)
        {
            string modAssetsFolder = ModLoader.GetModAssetsFolder(this);
            string outputFilePath = Path.Combine(modAssetsFolder, $"{str}_output.txt");

            StringBuilder report = new StringBuilder();
            report.AppendLine("\t=== LoppeHouse Bug Report ===\n");
            report.AppendLine($"Error occured in {str}: " + ex.Message);
            report.AppendLine(ex.StackTrace);
            report.AppendLine("\n\t=== List of installed mods ===\n");

            foreach (var mod in ModLoader.LoadedMods)
            {
                report.AppendLine("- " + mod.Name + " (v" + mod.Version + ")");
            }
            try
            {
                File.WriteAllText(outputFilePath, report.ToString());
                ModConsole.Log($"<color=yellow><b>LoppeHouse</b>: Created <b>{str}_output.txt</b> file in <b>{modAssetsFolder}</b></color>");
            }
            catch (Exception fileEx)
            {
                ModConsole.Error($"Failed to create {str}_output.txt: " + fileEx.Message);
            }
        }
    }
}