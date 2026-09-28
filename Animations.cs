using MSCLoader;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LoppeHouse
{
    internal class Animations : LoppeHouse
    {
        public static GameObject Trigger1, Trigger2, Trigger3, Trigger4, Trigger5, Trigger6, 
            Trigger7, Trigger8, Trigger9, Trigger10, Trigger11, Trigger12, TriggerWardrobe1, 
            TriggerWardrobe2, TriggerWardrobe3, TriggerGLeft, TriggerGRight;
        private static GameObject ObjDoor1, ObjDoor2, ObjDoor3, ObjDoor4, ObjDoor5, ObjDoor6, 
            ObjDoor7, ObjDoor8, ObjDoor9, ObjDoor10, ObjDoor11, ObjDoor12, ObjWardrobe1, 
            ObjWardrobe2, ObjWardrobe3, ObjGarageLeft, ObjGarageRight;        
        private static Animator Door1, Door2, Door3, Door4, Door5, Door6, 
            Door7, Door8, Door9, Door10, Door11, Door12, Wardrobe1, Wardrobe2, Wardrobe3, GarageLeft, GarageRight;
        public static bool IsOpen1 { get; set; }
        public static bool IsOpen2 { get; set; }
        public static bool IsOpen3 { get; set; }
        public static bool IsOpen4 { get; set; }
        public static bool IsOpen5 { get; set; }
        public static bool IsOpen6 { get; set; }
        public static bool IsOpen7 { get; set; }
        public static bool IsOpen8 { get; set; }
        public static bool IsOpen9 { get; set; }
        public static bool IsOpen10 { get; set; }
        public static bool IsOpen11 { get; set; }
        public static bool IsOpen12 { get; set; }
        public static bool IsOpenGL { get; set; }
        public static bool IsOpenGR { get; set; }
        public static bool IsOpenWardrobe1 { get; set; }
        public static bool IsOpenWardrobe2 { get; set; }
        public static bool IsOpenWardrobe3 { get; set; }
        public static void InitializeAnimations()
        {
            try
            {
                var doorsInstance = GameObject.Find("Animations(Clone)");

                ObjDoor1 = doorsInstance.transform.GetChild(0).gameObject;
                ObjDoor2 = doorsInstance.transform.GetChild(1).gameObject;
                ObjDoor3 = doorsInstance.transform.GetChild(2).gameObject;
                ObjDoor4 = doorsInstance.transform.GetChild(3).gameObject;
                ObjDoor5 = doorsInstance.transform.GetChild(4).gameObject;
                ObjDoor6 = doorsInstance.transform.GetChild(5).gameObject;
                ObjDoor7 = doorsInstance.transform.GetChild(6).gameObject;
                ObjDoor8 = doorsInstance.transform.GetChild(7).gameObject;
                ObjDoor9 = doorsInstance.transform.GetChild(8).gameObject;
                ObjDoor10 = doorsInstance.transform.GetChild(9).gameObject;
                ObjDoor11 = doorsInstance.transform.GetChild(10).gameObject;
                ObjDoor12 = doorsInstance.transform.GetChild(11).gameObject;
                ObjWardrobe1 = doorsInstance.transform.GetChild(12).gameObject;
                ObjWardrobe2 = doorsInstance.transform.GetChild(13).gameObject;
                ObjWardrobe3 = doorsInstance.transform.GetChild(14).gameObject;
                ObjGarageLeft = doorsInstance.transform.GetChild(15).gameObject;
                ObjGarageRight = doorsInstance.transform.GetChild(16).gameObject;

                List<GameObject> list = new List<GameObject>()
                {
                    ObjDoor1, ObjDoor2, ObjDoor3, ObjDoor4, ObjDoor5, ObjDoor6, ObjDoor7, ObjDoor8, ObjDoor9, ObjDoor10, ObjDoor11, ObjDoor12, ObjWardrobe1, ObjWardrobe2, ObjWardrobe3,
                };
                for (int i = 0; i < list.Count; i++) list[i].layer = LayerMask.NameToLayer("Parts");

                ObjWardrobe1.name = " ";
                ObjWardrobe2.name = " ";
                ObjWardrobe3.name = " ";
            }
            catch (Exception ex)
            {
                ModConsole.Error($"Error occured while trying to initialize LoppeHouse.Animations (Phase1): <b>{ex.Message}</b>");
            }
            try
            {
                Door1 = ObjDoor1.GetComponent<Animator>();
                Door2 = ObjDoor2.GetComponent<Animator>();
                Door3 = ObjDoor3.GetComponent<Animator>();
                Door4 = ObjDoor4.GetComponent<Animator>();
                Door5 = ObjDoor5.GetComponent<Animator>();
                Door6 = ObjDoor6.GetComponent<Animator>();
                Door7 = ObjDoor7.GetComponent<Animator>();
                Door8 = ObjDoor8.GetComponent<Animator>();
                Door9 = ObjDoor9.GetComponent<Animator>();
                Door10 = ObjDoor10.GetComponent<Animator>();
                Door11 = ObjDoor11.GetComponent<Animator>();
                Door12 = ObjDoor12.GetComponent<Animator>();
                Wardrobe1 = ObjWardrobe1.GetComponent<Animator>();
                Wardrobe2 = ObjWardrobe2.GetComponent<Animator>();
                Wardrobe3 = ObjWardrobe3.GetComponent<Animator>();
                GarageLeft = ObjGarageLeft.GetComponent<Animator>();
                GarageRight = ObjGarageRight.GetComponent<Animator>();
            }
            catch (Exception ex)
            {
                ModConsole.Error($"Error occured while trying to initialize LoppeHouse.Animations (Phase2): <b>{ex.Message}</b>");
            }
            try
            {
                Trigger1 = ObjDoor1.transform.GetChild(0).gameObject;
                Trigger2 = ObjDoor2.transform.GetChild(0).gameObject;
                Trigger3 = ObjDoor3.transform.GetChild(0).gameObject;
                Trigger4 = ObjDoor4.transform.GetChild(0).gameObject;
                Trigger5 = ObjDoor5.transform.GetChild(0).gameObject;
                Trigger6 = ObjDoor6.transform.GetChild(0).gameObject;
                Trigger7 = ObjDoor7.transform.GetChild(0).gameObject;
                Trigger8 = ObjDoor8.transform.GetChild(0).gameObject;
                Trigger9 = ObjDoor9.transform.GetChild(0).gameObject;
                Trigger10 = ObjDoor10.transform.GetChild(0).gameObject;
                Trigger11 = ObjDoor11.transform.GetChild(0).gameObject;
                Trigger12 = ObjDoor12.transform.GetChild(0).gameObject;
                TriggerWardrobe1 = ObjWardrobe1.transform.GetChild(0).gameObject;
                TriggerWardrobe2 = ObjWardrobe2.transform.GetChild(0).gameObject;
                TriggerWardrobe3 = ObjWardrobe3.transform.GetChild(0).gameObject;
                TriggerGLeft = ObjGarageLeft.transform.GetChild(0).gameObject;
                TriggerGRight = ObjGarageRight.transform.GetChild(0).gameObject;
            }
            catch (Exception ex)
            {
                ModConsole.Error($"Error occured while trying to initialize LoppeHouse.Animations (Phase3): <b>{ex.Message}</b>");
            }
        }
        public static void UpdateAnimations()
        {
            List<bool> doorStates = new List<bool> { IsOpen1, IsOpen2, IsOpen3, IsOpen4, IsOpen5, IsOpen6, 
                IsOpen7, IsOpen8, IsOpen9, IsOpen10, IsOpen11, IsOpen12, IsOpenWardrobe1, IsOpenWardrobe2, 
                IsOpenWardrobe3, IsOpenGL, IsOpenGR };
            List<Animator> doors = new List<Animator> { Door1, Door2, Door3, Door4, Door5, Door6, 
                Door7, Door8, Door9, Door10, Door11, Door12, Wardrobe1, Wardrobe2, Wardrobe3, GarageLeft, GarageRight, };
            for (int i = 0; i < doorStates.Count; i++) doors[i].SetBool("isOpen", doorStates[i]);
        }
        public void ReadData()
        {
            if (SaveLoad.ValueExists(this, "IsOpen1")) IsOpen1 = SaveLoad.ReadValue<bool>(this, "IsOpen1");
            else IsOpen1 = false;
            if (SaveLoad.ValueExists(this, "IsOpen2")) IsOpen2 = SaveLoad.ReadValue<bool>(this, "IsOpen2");
            else IsOpen2 = false;
            if (SaveLoad.ValueExists(this, "IsOpen3")) IsOpen3 = SaveLoad.ReadValue<bool>(this, "IsOpen3");
            else IsOpen3 = false;
            if (SaveLoad.ValueExists(this, "IsOpen4")) IsOpen4 = SaveLoad.ReadValue<bool>(this, "IsOpen4");
            else IsOpen4 = false;
            if (SaveLoad.ValueExists(this, "IsOpen5")) IsOpen5 = SaveLoad.ReadValue<bool>(this, "IsOpen5");
            else IsOpen5 = false;
            if (SaveLoad.ValueExists(this, "IsOpen6")) IsOpen6 = SaveLoad.ReadValue<bool>(this, "IsOpen6");
            else IsOpen6 = false;
            if (SaveLoad.ValueExists(this, "IsOpen7")) IsOpen7 = SaveLoad.ReadValue<bool>(this, "IsOpen7");
            else IsOpen7 = false;
            if (SaveLoad.ValueExists(this, "IsOpen8")) IsOpen8 = SaveLoad.ReadValue<bool>(this, "IsOpen8");
            else IsOpen8 = false;
            if (SaveLoad.ValueExists(this, "IsOpen9")) IsOpen9 = SaveLoad.ReadValue<bool>(this, "IsOpen9");
            else IsOpen9 = false;
            if (SaveLoad.ValueExists(this, "IsOpen10")) IsOpen10 = SaveLoad.ReadValue<bool>(this, "IsOpen10");
            else IsOpen10 = false;
            if (SaveLoad.ValueExists(this, "IsOpen11")) IsOpen11 = SaveLoad.ReadValue<bool>(this, "IsOpen11");
            else IsOpen11 = false;
            if (SaveLoad.ValueExists(this, "IsOpen12")) IsOpen12 = SaveLoad.ReadValue<bool>(this, "IsOpen12");
            else IsOpen12 = false;
            if (SaveLoad.ValueExists(this, "IsOpenWardrobe1")) IsOpenWardrobe1 = SaveLoad.ReadValue<bool>(this, "IsOpenWardrobe1");
            else IsOpenWardrobe1 = false;
            if (SaveLoad.ValueExists(this, "IsOpenWardrobe2")) IsOpenWardrobe2 = SaveLoad.ReadValue<bool>(this, "IsOpenWardrobe2");
            else IsOpenWardrobe2 = false;
            if (SaveLoad.ValueExists(this, "IsOpenWardrobe3")) IsOpenWardrobe3 = SaveLoad.ReadValue<bool>(this, "IsOpenWardrobe3");
            else IsOpenWardrobe3 = false;
            if (SaveLoad.ValueExists(this, "IsOpenGL")) IsOpenGL = SaveLoad.ReadValue<bool>(this, "IsOpenGL");
            else IsOpenGL = false;
            if (SaveLoad.ValueExists(this, "IsOpenGR")) IsOpenGR = SaveLoad.ReadValue<bool>(this, "IsOpenGR");
            else IsOpenGR = false; 
        }
        public void WriteData()
        {
            SaveLoad.WriteValue(this, "IsOpen1", IsOpen1);
            SaveLoad.WriteValue(this, "IsOpen2", IsOpen2);
            SaveLoad.WriteValue(this, "IsOpen3", IsOpen3);
            SaveLoad.WriteValue(this, "IsOpen4", IsOpen4);
            SaveLoad.WriteValue(this, "IsOpen5", IsOpen5);
            SaveLoad.WriteValue(this, "IsOpen6", IsOpen6);
            SaveLoad.WriteValue(this, "IsOpen7", IsOpen7);
            SaveLoad.WriteValue(this, "IsOpen8", IsOpen8);
            SaveLoad.WriteValue(this, "IsOpen9", IsOpen9);
            SaveLoad.WriteValue(this, "IsOpen10", IsOpen10);
            SaveLoad.WriteValue(this, "IsOpen11", IsOpen11);
            SaveLoad.WriteValue(this, "IsOpen12", IsOpen12);
            SaveLoad.WriteValue(this, "IsOpenWardrobe1", IsOpenWardrobe1);
            SaveLoad.WriteValue(this, "IsOpenWardrobe2", IsOpenWardrobe2);
            SaveLoad.WriteValue(this, "IsOpenWardrobe3", IsOpenWardrobe3);
            SaveLoad.WriteValue(this, "IsOpenGL", IsOpenGL);
            SaveLoad.WriteValue(this, "IsOpenGR", IsOpenGR);
        }
    }
}