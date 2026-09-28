using MSCLoader;
using System;
using UnityEngine;

namespace LoppeHouse
{
    internal class AssemblySystem : LoppeHouse
    {
        public static GameObject HoodTrigger, BootlidTrigger, DoorLTrigger,
            DoorRTrigger, FenderLTrigger, FenderRTrigger, BumperFTrigger,
         BumperRTrigger, GrilleTrigger, GrilleGTTrigger;
        public static GameObject Hood, Bootlid, DoorL, DoorR, FenderL, FenderR, 
            BumperF, BumperR, Grille, GrilleGT;
        public static bool HoodInstalled { get; set; }
        public static bool BootlidInstalled { get; set; }
        public static bool DoorLInstalled { get; set; }
        public static bool DoorRInstalled { get; set; }
        public static bool FenderLInstalled { get; set; }
        public static bool FenderRInstalled { get; set; }
        public static bool BumperFInstalled { get; set; }
        public static bool BumperRInstalled { get; set; }
        public static bool GrilleInstalled { get; set; }
        public static bool GrilleGTInstalled { get; set; }

        public static void InitializeGarage()
        {
            try
            {
                GameObject TriggersGlobal = Objects.Garage.transform.GetChild(2).gameObject;

                HoodTrigger = TriggersGlobal.transform.GetChild(0).gameObject;
                BootlidTrigger = TriggersGlobal.transform.GetChild(1).gameObject;
                DoorLTrigger = TriggersGlobal.transform.GetChild(2).gameObject;
                DoorRTrigger = TriggersGlobal.transform.GetChild(3).gameObject;
                FenderLTrigger = TriggersGlobal.transform.GetChild(4).gameObject;
                FenderRTrigger = TriggersGlobal.transform.GetChild(5).gameObject;
                BumperFTrigger = TriggersGlobal.transform.GetChild(6).gameObject;
                BumperRTrigger = TriggersGlobal.transform.GetChild(7).gameObject;
                GrilleTrigger = TriggersGlobal.transform.GetChild(8).gameObject;
                GrilleGTTrigger = TriggersGlobal.transform.GetChild(9).gameObject;
            }
            catch (Exception ex)
            {
                ModConsole.Error("Error occured while trying to initialize <b>LoppeHouse.AssemblySystem " +
                    "(Phase1)</b>, please send screenshot to Bugs section on NexusMods: " + ex.Message);
            }
            try
            {
                Hood = GameObject.Find("hood(Clone)");
                Bootlid = GameObject.Find("bootlid(Clone)");
                DoorL = GameObject.Find("door left(Clone)");
                DoorR = GameObject.Find("door right(Clone)");
                FenderL = GameObject.Find("fender left(Clone)");
                FenderR = GameObject.Find("fender right(Clone)");
                Grille = GameObject.Find("grille(Clone)");
                GrilleGT = GameObject.Find("grille gt(Clone)");
                BumperF = GameObject.Find("bumper front(Clone)");
                BumperR = GameObject.Find("bumper rear(Clone)");
            }
            catch (Exception ex)
            {
                ModConsole.Error("Error occured while trying to initialize <b>LoppeHouse.AssemblySystem " +
                    "(Phase2)</b>, please send screenshot to Bugs section on NexusMods: " + ex.Message);
            }
        }
        public static void CheckObjectState()
        {
            if (HoodInstalled)
            {
                Hood.transform.position = new Vector3(1363.255f, 11.81021f, 807.8898f);
                Hood.transform.rotation = Quaternion.Euler(9.4f, 220f, 0f);
                ComponentsHandler(Hood, true, false, false, false);
            }
            else ComponentsHandler(Hood, false, true, true, false);
            if (BootlidInstalled)
            {
                Bootlid.transform.position = new Vector3(1364.287f, 11.90092f, 807.1696f);
                Bootlid.transform.rotation = Quaternion.Euler(300f, 220f, 0f);
                ComponentsHandler(Bootlid, true, false, false, true);
            }
            else ComponentsHandler(Bootlid, false, true, true, true);
            if (FenderLInstalled)
            {
                FenderL.transform.position = new Vector3(1363.206f, 9.725391f, 807.921f);
                FenderL.transform.rotation = Quaternion.Euler(276f, 220f, 270f);
                ComponentsHandler(FenderL, true, false, false, false);
            }
            else ComponentsHandler(FenderL, false, true, true, false);
            if (FenderRInstalled)
            {
                FenderR.transform.position = new Vector3(1363.24f, 10.45222f, 807.8775f);
                FenderR.transform.rotation = Quaternion.Euler(276f, 220f, 90f);
                ComponentsHandler(FenderR, true, false, false, false);
            }
            else ComponentsHandler(FenderR, false, true, true, false);
            if (DoorLInstalled)
            {
                DoorL.transform.position = new Vector3(1363.787f, 9.836574f, 807.3782f);
                DoorL.transform.rotation = Quaternion.Euler(270f, 130f, 0f);
                ComponentsHandler(DoorL, true, false, false, true);
            }
            else ComponentsHandler(DoorL, false, true, true, true);
            if (DoorRInstalled)
            {
                DoorR.transform.position = new Vector3(1364.551f, 10.82688f, 806.7392f);
                DoorR.transform.rotation = Quaternion.Euler(270f, 310f, 0f);
                ComponentsHandler(DoorR, true, false, false, true);
            }
            else ComponentsHandler(DoorR, false, true, true, true);
            if (BumperFInstalled)
            {

                BumperF.transform.position = new Vector3(1364.908f, 9.936577f, 806.5324f);
                BumperF.transform.rotation = Quaternion.Euler(342f, 120.1235f, 272.0235f);
                ComponentsHandler(BumperF, true, false, false, false);
            }
            else ComponentsHandler(BumperF, false, true, true, false);
            if (BumperRInstalled)
            {
                BumperR.transform.position = new Vector3(1365.118f, 9.936577f, 806.3376f);
                BumperR.transform.rotation = Quaternion.Euler(20f, 306.1235f, 268.0236f);
                ComponentsHandler(BumperR, true, false, false, false);
            }
            else ComponentsHandler(BumperR, false, true, true, false);
            if (GrilleInstalled)
            {
                Grille.transform.position = new Vector3(1364.861f, 11.17262f, 806.6309f);
                Grille.transform.rotation = Quaternion.Euler(359.4f, 130f, 270f);
                ComponentsHandler(Grille, true, false, false, false);
            }
            else ComponentsHandler(Grille, false, true, true, false);
            if (GrilleGTInstalled)
            {
                GrilleGT.transform.position = new Vector3(1365.096f, 11.19561f, 806.4432f);
                GrilleGT.transform.rotation = Quaternion.Euler(359.4f, 130f, 270f);
                ComponentsHandler(GrilleGT, true, false, false, false);
            }
            else ComponentsHandler(GrilleGT, false, true, true, false);
        }
        private static void ComponentsHandler(GameObject obj, bool value1, bool value2, bool value3, bool isChild)
        {
            if (obj.GetComponent<Rigidbody>() != null)
            {
                obj.GetComponent<Rigidbody>().isKinematic = value1;
                obj.GetComponent<Rigidbody>().useGravity = value2;
            }
            foreach (var collider in obj.GetComponents<Collider>()) collider.enabled = value3;
            if (isChild) foreach (Collider colliderOptional in obj.GetComponentsInChildren<Collider>(true)) colliderOptional.enabled = value3;
        }
        public void ReadData()
        {
            if (SaveLoad.ValueExists(this, "HoodInstalled")) HoodInstalled = SaveLoad.ReadValue<bool>(this, "HoodInstalled");
            else HoodInstalled = false;
            if (SaveLoad.ValueExists(this, "BootlidInstalled")) BootlidInstalled = SaveLoad.ReadValue<bool>(this, "BootlidInstalled");
            else BootlidInstalled = false;
            if (SaveLoad.ValueExists(this, "DoorLInstalled")) DoorLInstalled = SaveLoad.ReadValue<bool>(this, "DoorLInstalled");
            else DoorLInstalled = false;
            if (SaveLoad.ValueExists(this, "DoorRInstalled")) DoorRInstalled = SaveLoad.ReadValue<bool>(this, "DoorRInstalled");
            else DoorRInstalled = false;
            if (SaveLoad.ValueExists(this, "FenderLInstalled")) FenderLInstalled = SaveLoad.ReadValue<bool>(this, "FenderLInstalled");
            else FenderLInstalled = false;
            if (SaveLoad.ValueExists(this, "FenderRInstalled")) FenderRInstalled = SaveLoad.ReadValue<bool>(this, "FenderRInstalled");
            else FenderRInstalled = false;
            if (SaveLoad.ValueExists(this, "BumperFInstalled")) BumperFInstalled = SaveLoad.ReadValue<bool>(this, "BumperFInstalled");
            else BumperFInstalled = false;
            if (SaveLoad.ValueExists(this, "BumperRInstalled")) BumperRInstalled = SaveLoad.ReadValue<bool>(this, "BumperRInstalled");
            else BumperRInstalled = false;
            if (SaveLoad.ValueExists(this, "GrilleInstalled")) GrilleInstalled = SaveLoad.ReadValue<bool>(this, "GrilleInstalled");
            else GrilleInstalled = false;
            if (SaveLoad.ValueExists(this, "GrilleGTInstalled")) GrilleGTInstalled = SaveLoad.ReadValue<bool>(this, "GrilleGTInstalled");
            else GrilleGTInstalled = false;
        }
        public void WriteData()
        {
            SaveLoad.WriteValue(this, "HoodInstalled", HoodInstalled);
            SaveLoad.WriteValue(this, "BootlidInstalled", BootlidInstalled);
            SaveLoad.WriteValue(this, "DoorLInstalled", DoorLInstalled);
            SaveLoad.WriteValue(this, "DoorRInstalled", DoorRInstalled);
            SaveLoad.WriteValue(this, "FenderLInstalled", FenderLInstalled);
            SaveLoad.WriteValue(this, "FenderRInstalled", FenderRInstalled);
            SaveLoad.WriteValue(this, "BumperFInstalled", BumperFInstalled);
            SaveLoad.WriteValue(this, "BumperRInstalled", BumperRInstalled);
            SaveLoad.WriteValue(this, "GrilleInstalled", GrilleInstalled);
            SaveLoad.WriteValue(this, "GrilleGTInstalled", GrilleGTInstalled);
        }
    }
}
