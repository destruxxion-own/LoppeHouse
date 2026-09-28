using UnityEngine;

namespace LoppeHouse
{
    internal class InteractionHandler : Objects
    { 
        private static Camera Cam;
        private static bool isPacked, isPackedAtHouse;
        public static RaycastHit hit;
        public static void InitializeCamera()
        {
            Cam = Camera.main;
        }
        public static void HandleRaycast()
        {
            if (Cam != null)
            {
                Ray ray = Cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
                if (Physics.Raycast(ray, out hit, 1f))
                {
                    if (SaleSheet != null)
                    {
                        HandleSaleSheet(hit);
                    }
                    if (Variables.PlayerKeys.Value == 1)
                    {
                        HandleBox(hit);
                        HandleSwitches(hit);
                        HandleAnims(hit);
                        HandleAssemblySystem(hit);
                    }
                }
            }
        }
        private static void HandleAssemblySystem(RaycastHit hit)
        {
            if (hit.collider.gameObject == AssemblySystem.HoodTrigger)
            {
                if (AssemblySystem.HoodTrigger.GetComponent<BoxCollider>().bounds.Intersects(AssemblySystem.Hood.GetComponent<Collider>().bounds))
                {
                    if (!AssemblySystem.HoodInstalled) PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIassemble").Value = true;
                    else PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIdisassemble").Value = true;

                    if (Input.GetKeyDown(KeyCode.Mouse0) && !AssemblySystem.HoodInstalled)
                    {
                        Assemble.Play();
                        AssemblySystem.HoodInstalled = true;
                        AssemblySystem.CheckObjectState();
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1) && AssemblySystem.HoodInstalled)
                    {
                        Disassemble.Play();
                        AssemblySystem.HoodInstalled = false;
                        AssemblySystem.CheckObjectState();
                    }
                }
            }
            if (hit.collider.gameObject == AssemblySystem.BootlidTrigger)
            {
                if (AssemblySystem.Bootlid.GetComponent<Collider>().enabled == false) AssemblySystem.Bootlid.GetComponent<Collider>().enabled = true;
                if (AssemblySystem.BootlidTrigger.GetComponent<BoxCollider>().bounds.Intersects(AssemblySystem.Bootlid.GetComponent<Collider>().bounds))
                {
                    if (!AssemblySystem.BootlidInstalled) PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIassemble").Value = true;
                    else PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIdisassemble").Value = true;

                    if (Input.GetKeyDown(KeyCode.Mouse0) && !AssemblySystem.BootlidInstalled)
                    {
                        Assemble.Play();
                        AssemblySystem.BootlidInstalled = true;
                        AssemblySystem.CheckObjectState();
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1) && AssemblySystem.BootlidInstalled)
                    {
                        Disassemble.Play();
                        AssemblySystem.BootlidInstalled = false;
                        AssemblySystem.CheckObjectState();
                    }
                }
            }
            if (hit.collider.gameObject == AssemblySystem.DoorLTrigger)
            {
                if (AssemblySystem.DoorLTrigger.GetComponent<BoxCollider>().bounds.Intersects(AssemblySystem.DoorL.GetComponent<Collider>().bounds))
                {
                    if (!AssemblySystem.DoorLInstalled) PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIassemble").Value = true;
                    else PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIdisassemble").Value = true;

                    if (Input.GetKeyDown(KeyCode.Mouse0) && !AssemblySystem.DoorLInstalled)
                    {
                        Assemble.Play();
                        AssemblySystem.DoorLInstalled = true;
                        AssemblySystem.CheckObjectState();
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1) && AssemblySystem.DoorLInstalled)
                    {
                        Disassemble.Play();
                        AssemblySystem.DoorLInstalled = false;
                        AssemblySystem.CheckObjectState();
                    }
                }
            }
            if (hit.collider.gameObject == AssemblySystem.DoorRTrigger)
            {
                if (AssemblySystem.DoorRTrigger.GetComponent<BoxCollider>().bounds.Intersects(AssemblySystem.DoorR.GetComponent<Collider>().bounds))
                {
                    if (!AssemblySystem.DoorRInstalled) PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIassemble").Value = true;
                    else PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIdisassemble").Value = true;

                    if (Input.GetKeyDown(KeyCode.Mouse0) && !AssemblySystem.DoorRInstalled)
                    {
                        Assemble.Play();
                        AssemblySystem.DoorRInstalled = true;
                        AssemblySystem.CheckObjectState();
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1) && AssemblySystem.DoorRInstalled)
                    {
                        Disassemble.Play();
                        AssemblySystem.DoorRInstalled = false;
                        AssemblySystem.CheckObjectState();
                    }
                }
            }
            if (hit.collider.gameObject == AssemblySystem.FenderLTrigger)
            {
                if (AssemblySystem.FenderLTrigger.GetComponent<BoxCollider>().bounds.Intersects(AssemblySystem.FenderL.GetComponent<Collider>().bounds))
                {
                    if (!AssemblySystem.FenderLInstalled) PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIassemble").Value = true;
                    else PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIdisassemble").Value = true;

                    if (Input.GetKeyDown(KeyCode.Mouse0) && !AssemblySystem.FenderLInstalled)
                    {
                        Assemble.Play();
                        AssemblySystem.FenderLInstalled = true;
                        AssemblySystem.CheckObjectState();
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1) && AssemblySystem.FenderLInstalled)
                    {
                        Disassemble.Play();
                        AssemblySystem.FenderLInstalled = false;
                        AssemblySystem.CheckObjectState();
                    }
                }
            }
            if (hit.collider.gameObject == AssemblySystem.FenderRTrigger)
            {
                if (AssemblySystem.FenderRTrigger.GetComponent<BoxCollider>().bounds.Intersects(AssemblySystem.FenderR.GetComponent<Collider>().bounds))
                {
                    if (!AssemblySystem.FenderRInstalled) PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIassemble").Value = true;
                    else PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIdisassemble").Value = true;

                    if (Input.GetKeyDown(KeyCode.Mouse0) && !AssemblySystem.FenderRInstalled)
                    {
                        Assemble.Play();
                        AssemblySystem.FenderRInstalled = true;
                        AssemblySystem.CheckObjectState();
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1) && AssemblySystem.FenderRInstalled)
                    {
                        Disassemble.Play();
                        AssemblySystem.FenderRInstalled = false;
                        AssemblySystem.CheckObjectState();
                    }
                }
            }
            if (hit.collider.gameObject == AssemblySystem.BumperFTrigger)
            {
                if (AssemblySystem.BumperFTrigger.GetComponent<BoxCollider>().bounds.Intersects(AssemblySystem.BumperF.GetComponent<Collider>().bounds))
                {
                    if (!AssemblySystem.BumperFInstalled) PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIassemble").Value = true;
                    else PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIdisassemble").Value = true;

                    if (Input.GetKeyDown(KeyCode.Mouse0) && !AssemblySystem.BumperFInstalled)
                    {
                        Assemble.Play();
                        AssemblySystem.BumperFInstalled = true;
                        AssemblySystem.CheckObjectState();
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1) && AssemblySystem.BumperFInstalled)
                    {
                        Disassemble.Play();
                        AssemblySystem.BumperFInstalled = false;
                        AssemblySystem.CheckObjectState();
                    }
                }
            }
            if (hit.collider.gameObject == AssemblySystem.BumperRTrigger)
            {
                if (AssemblySystem.BumperRTrigger.GetComponent<BoxCollider>().bounds.Intersects(AssemblySystem.BumperR.GetComponent<Collider>().bounds))
                {
                    if (!AssemblySystem.BumperRInstalled) PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIassemble").Value = true;
                    else PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIdisassemble").Value = true;

                    if (Input.GetKeyDown(KeyCode.Mouse0) && !AssemblySystem.BumperRInstalled)
                    {
                        Assemble.Play();
                        AssemblySystem.BumperRInstalled = true;
                        AssemblySystem.CheckObjectState();
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1) && AssemblySystem.BumperRInstalled)
                    {
                        Disassemble.Play();
                        AssemblySystem.BumperRInstalled = false;
                        AssemblySystem.CheckObjectState();
                    }
                }
            }
            if (hit.collider.gameObject == AssemblySystem.GrilleTrigger)
            {
                if (AssemblySystem.GrilleTrigger.GetComponent<BoxCollider>().bounds.Intersects(AssemblySystem.Grille.GetComponent<Collider>().bounds))
                {
                    if (!AssemblySystem.GrilleInstalled) PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIassemble").Value = true;
                    else PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIdisassemble").Value = true;

                    if (Input.GetKeyDown(KeyCode.Mouse0) && !AssemblySystem.GrilleInstalled)
                    {
                        Assemble.Play();
                        AssemblySystem.GrilleInstalled = true;
                        AssemblySystem.CheckObjectState();
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1) && AssemblySystem.GrilleInstalled)
                    {
                        Disassemble.Play();
                        AssemblySystem.GrilleInstalled = false;
                        AssemblySystem.CheckObjectState();
                    }
                }
            }
            if (hit.collider.gameObject == AssemblySystem.GrilleGTTrigger)
            {
                if (AssemblySystem.GrilleGTTrigger.GetComponent<BoxCollider>().bounds.Intersects(AssemblySystem.GrilleGT.GetComponent<Collider>().bounds))
                {
                    if (!AssemblySystem.GrilleGTInstalled) PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIassemble").Value = true;
                    else PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIdisassemble").Value = true;

                    if (Input.GetKeyDown(KeyCode.Mouse0) && !AssemblySystem.GrilleGTInstalled)
                    {
                        Assemble.Play();
                        AssemblySystem.GrilleGTInstalled = true;
                        AssemblySystem.CheckObjectState();
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1) && AssemblySystem.GrilleGTInstalled)
                    {
                        Disassemble.Play();
                        AssemblySystem.GrilleGTInstalled = false;
                        AssemblySystem.CheckObjectState();
                    }
                }
            }
        }
        private static void HandleBox(RaycastHit hit)
        {
            if (hit.collider.gameObject == Box)
            {
                PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;
                if (BoxBounds.bounds.Contains(BoxTriggerHouse.transform.position) || BoxBounds.bounds.Contains(BoxTrigger.transform.position))
                {
                    if (Variables.ItemsPacked) PlayMakerGlobals.Instance.Variables.GetFsmString("GUIinteraction").Value = "Unpack Items";
                    else PlayMakerGlobals.Instance.Variables.GetFsmString("GUIinteraction").Value = "Pack Items";
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (BoxBounds.bounds.Contains(BoxTriggerHouse.transform.position))
                    {
                        if (!Variables.ItemsPacked)
                        {
                            if (!isPacked)
                            {
                                PackItems();
                                Variables.ItemsPacked = true;
                                isPackedAtHouse = true;
                            }
                        }
                        else
                        {
                            UnpackItemsPlayerHouse();
                            Variables.ItemsPacked = false;
                            isPackedAtHouse = false;
                        }
                    }
                    if (BoxBounds.bounds.Contains(BoxTrigger.transform.position))
                    {
                        if (!Variables.ItemsPacked)
                        {
                            if (isPackedAtHouse)
                            {
                                PackItems();
                                Variables.ItemsPacked = true;
                                isPacked = false;
                            }
                        }
                        else
                        {
                            UnpackItemsHouse();
                            Variables.ItemsPacked = false;
                            isPacked = true;
                        }
                    }
                }
            }
        }
        private static void HandleSwitches(RaycastHit hit)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (hit.collider.gameObject == Electricity.SwitchKitchen)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;
                    Variables.LightKitchen = !Variables.LightKitchen;
                    Electricity.Kitchen.SetActive(Variables.LightKitchen);
                    Objects.SwitchSound.Play();
                }
                if (hit.collider.gameObject == Electricity.SwitchLiving)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;
                    Variables.LightLiving = !Variables.LightLiving;
                    Electricity.Living.SetActive(Variables.LightLiving);
                    Objects.SwitchSound.Play();
                }
                if (hit.collider.gameObject == Electricity.SwitchPlayer)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;
                    Variables.LightPlayer = !Variables.LightPlayer;
                    Electricity.Playerroom.SetActive(Variables.LightPlayer);
                    Objects.SwitchSound.Play();
                }
                if (hit.collider.gameObject == Electricity.SwitchBathroom)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;
                    Variables.LightBathroom = !Variables.LightBathroom;
                    Electricity.Bathroom.SetActive(Variables.LightBathroom);
                    Objects.SwitchSound.Play();
                }
                if (hit.collider.gameObject == Electricity.SwitchParents)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;
                    Variables.LightParents = !Variables.LightParents;
                    Electricity.Parentsroom.SetActive(Variables.LightParents);
                    Objects.SwitchSound.Play();
                }
                if (hit.collider.gameObject == Electricity.SwitchHallroom)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;
                    Variables.LightHallroom = !Variables.LightHallroom;
                    Electricity.Hallroom.SetActive(Variables.LightHallroom);
                    Objects.SwitchSound.Play();
                }
                if (hit.collider.gameObject == Electricity.SwitchCloset)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;
                    Variables.LightCloset = !Variables.LightCloset;
                    Electricity.Closet.SetActive(Variables.LightCloset);
                    Objects.SwitchSound.Play();
                }
                if (hit.collider.gameObject == Electricity.SwitchGarage)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;
                    Variables.LightGarage = !Variables.LightGarage;
                    Electricity.Garage.SetActive(Variables.LightGarage);
                    Objects.SwitchSound.Play();
                }
                if (hit.collider.gameObject == Electricity.SwitchGarageDash)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;
                    Variables.LightGarageDash = !Variables.LightGarageDash;
                    Electricity.GarageDash.SetActive(Variables.LightGarageDash);
                    Objects.SwitchSound.Play();
                }
                if (hit.collider.gameObject == Electricity.SwitchOutdoor)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;
                    Variables.LightOutdoor = !Variables.LightOutdoor;
                    Electricity.Outdoor.SetActive(Variables.LightOutdoor);
                    Objects.SwitchSound.Play();
                }
                if (hit.collider.gameObject == Electricity.SwitchOutdoorTable)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;
                    Variables.LightOutdoorTable = !Variables.LightOutdoorTable;
                    Electricity.OutdoorTable.SetActive(Variables.LightOutdoorTable);
                    Objects.SwitchSound.Play();
                }
                if (hit.collider.gameObject == Electricity.Switch)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;
                    Electricity.SwitchOn.Value = !Electricity.SwitchOn.Value;
                    Objects.SwitchSound.Play();
                }

                SwitchHandler.UpdateSwitchRotations();
            }
        }
        private static void HandleAnims(RaycastHit hit)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (hit.collider.gameObject == Animations.Trigger1)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;

                    Animations.IsOpen1 = !Animations.IsOpen1;
                    if (Animations.IsOpen1) DoorOpenSound.Play();
                    if (!Animations.IsOpen1) DoorCloseSound.Play();
                }
                if (hit.collider.gameObject == Animations.Trigger2)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;

                    Animations.IsOpen2 = !Animations.IsOpen2;
                    if (Animations.IsOpen2) DoorOpenSound.Play();
                    if (!Animations.IsOpen2) DoorCloseSound.Play();
                }
                if (hit.collider.gameObject == Animations.Trigger3)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;

                    Animations.IsOpen3 = !Animations.IsOpen3;
                    if (Animations.IsOpen3) DoorOpenSound.Play();
                    if (!Animations.IsOpen3) DoorCloseSound.Play();
                }
                if (hit.collider.gameObject == Animations.Trigger4)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;

                    Animations.IsOpen4 = !Animations.IsOpen4;
                    if (Animations.IsOpen4) DoorOpenSound.Play();
                    if (!Animations.IsOpen4) DoorCloseSound.Play();
                }
                if (hit.collider.gameObject == Animations.Trigger5)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;

                    Animations.IsOpen5 = !Animations.IsOpen5;
                    if (Animations.IsOpen5) DoorOpenSound.Play();
                    if (!Animations.IsOpen5) DoorCloseSound.Play();
                }
                if (hit.collider.gameObject == Animations.Trigger6)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;

                    Animations.IsOpen6 = !Animations.IsOpen6;
                    if (Animations.IsOpen6) DoorOpenSound.Play();
                    if (!Animations.IsOpen6) DoorCloseSound.Play();
                }
                if (hit.collider.gameObject == Animations.Trigger7)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;

                    Animations.IsOpen7 = !Animations.IsOpen7;
                    if (Animations.IsOpen7) DoorOpenSound.Play();
                    if (!Animations.IsOpen7) DoorCloseSound.Play();
                }
                if (hit.collider.gameObject == Animations.Trigger8)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;

                    Animations.IsOpen8 = !Animations.IsOpen8;
                    if (Animations.IsOpen8) DoorOpenSound.Play();
                    if (!Animations.IsOpen8) DoorCloseSound.Play();
                }
                if (hit.collider.gameObject == Animations.Trigger9)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;

                    Animations.IsOpen9 = !Animations.IsOpen9;
                    if (Animations.IsOpen9) DoorOpenSound.Play();
                    if (!Animations.IsOpen9) DoorCloseSound.Play();
                }
                if (hit.collider.gameObject == Animations.Trigger10)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;

                    Animations.IsOpen10 = !Animations.IsOpen10;
                    if (Animations.IsOpen10) DoorOpenSound.Play();
                    if (!Animations.IsOpen10) DoorCloseSound.Play();
                }
                if (hit.collider.gameObject == Animations.Trigger11)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;

                    Animations.IsOpen11 = !Animations.IsOpen11;
                    if (Animations.IsOpen11) DoorOpenSound.Play();
                    if (!Animations.IsOpen11) DoorCloseSound.Play();
                }
                if (hit.collider.gameObject == Animations.Trigger12)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;

                    Animations.IsOpen12 = !Animations.IsOpen12;
                    if (Animations.IsOpen12) DoorOpenSound.Play();
                    if (!Animations.IsOpen12) DoorCloseSound.Play();
                }
                if (hit.collider.gameObject == Animations.TriggerGRight)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;

                    Animations.IsOpenGR = !Animations.IsOpenGR;
                    if (Animations.IsOpenGR) GarageDoorOpen.Play();
                    if (!Animations.IsOpenGR) GarageDoorClose.Play();
                }
                if (hit.collider.gameObject == Animations.TriggerGLeft)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;

                    Animations.IsOpenGL = !Animations.IsOpenGL;
                    if (Animations.IsOpenGL) GarageDoorOpen.Play();
                    if (!Animations.IsOpenGL) GarageDoorClose.Play();
                }
                if (hit.collider.gameObject == Animations.TriggerWardrobe1)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;

                    Animations.IsOpenWardrobe1 = !Animations.IsOpenWardrobe1;
                    if (Animations.IsOpenWardrobe1) DoorOpenSound.Play();
                    if (!Animations.IsOpenWardrobe1) DoorCloseSound.Play();
                }
                if (hit.collider.gameObject == Animations.TriggerWardrobe2)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;

                    Animations.IsOpenWardrobe2 = !Animations.IsOpenWardrobe2;
                    if (Animations.IsOpenWardrobe2) DoorOpenSound.Play();
                    if (!Animations.IsOpenWardrobe2) DoorCloseSound.Play();
                }
                if (hit.collider.gameObject == Animations.TriggerWardrobe3)
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIuse").Value = true;

                    Animations.IsOpenWardrobe3 = !Animations.IsOpenWardrobe3;
                    if (Animations.IsOpenWardrobe3) DoorOpenSound.Play();
                    if (!Animations.IsOpenWardrobe3) DoorCloseSound.Play();
                }

                Animations.UpdateAnimations();
            }
        }
        private static void HandleSaleSheet(RaycastHit hit)
        {
            if (hit.collider.gameObject == SaleSheet)
            {
                PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIbuy").Value = true;
                PlayMakerGlobals.Instance.Variables.GetFsmString("GUIsubtitle").Value = "Wanna get a house with incredible lake view? There it is! Costs only 19900MK";

                if (PlayMakerGlobals.Instance.Variables.GetFsmFloat("PlayerMoney").Value < 19900f) PlayMakerGlobals.Instance.Variables.GetFsmString("GUIinteraction").Value = "Not enough money";
                else
                {
                    PlayMakerGlobals.Instance.Variables.GetFsmString("GUIinteraction").Value = "House 19900MK";
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        Variables.PlayerKeys.Value = 1;
                        Electricity.OnHouseBuy();
                        PlayMakerGlobals.Instance.Variables.GetFsmBool("GUIbuy").Value = false;
                        PlayMakerGlobals.Instance.Variables.GetFsmString("GUIinteraction").Value = "";
                        PlayMakerGlobals.Instance.Variables.GetFsmString("GUIsubtitle").Value = "";
                        PlayMakerGlobals.Instance.Variables.GetFsmFloat("PlayerMoney").Value -= 19900f;
                        Object.Destroy(SaleSheet);
                    }
                }
            }
        }
    }
}
