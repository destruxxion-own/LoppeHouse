using System.Collections.Generic;
using UnityEngine;

namespace LoppeHouse
{
    internal class GraphicsSettings : Objects
    {
        private static float timer = 0f;
        private readonly static float distanceThreshold = 100f;

        public static List<string> ShadowValues = new List<string>
        {
            "No Shadows",
            "Hard Shadows",
            "Soft Shadows"
        };
        public static void UpdateShadowQuality()
        {
            LightShadows shadowQuality;

            switch (ShadowSlider.GetValue())
            {
                case 0:
                    shadowQuality = LightShadows.None;
                    break;
                case 1:
                    shadowQuality = LightShadows.Hard;
                    break;
                case 2:
                    shadowQuality = LightShadows.Soft;
                    break;
                default:
                    shadowQuality = LightShadows.Hard;
                    break;
            }

            var lights = new[]
            {
                Electricity.Kitchen,
                Electricity.Living,
                Electricity.Playerroom,
                Electricity.Bathroom,
                Electricity.Parentsroom,
                Electricity.Hallroom,
                Electricity.Closet,
                Electricity.Garage,
                Electricity.GarageDash,
                Electricity.Outdoor,
                Electricity.OutdoorTable
            };

            foreach (var lightObject in lights)
            {
                var lightComponent = lightObject.GetComponent<Light>();
                if (lightComponent != null)
                {
                    lightComponent.shadows = shadowQuality;
                }
            }
        }
        public static void CheckLOD()
        {
            float updateInterval = 3f;
            timer += Time.deltaTime;

            if (timer >= updateInterval)
            {
                if (House != null)
                {
                    float distance = Vector3.Distance(House.transform.position, Player.transform.position);

                    if (distance <= distanceThreshold)
                    {
                        EnableRenderer(Garage);
                        EnableRenderer(GameObject.Find("Animations(Clone)"));
                        EnableRenderer(Electricity.ElectricAppliances);
                        Electricity.ElectricAppliances.SetActive(true);
                    }
                    else
                    {
                        DisableRenderer(Garage);
                        DisableRenderer(GameObject.Find("Animations(Clone)"));
                        DisableRenderer(Electricity.ElectricAppliances);
                        Electricity.ElectricAppliances.SetActive(false);
                    }

                    timer = 0f;
                }
            }
        }
        public static void DisableRenderer(GameObject parent)
        {
            MeshRenderer[] childRenderers = parent.GetComponentsInChildren<MeshRenderer>(true);
            foreach (MeshRenderer renderer in childRenderers)
            {
                renderer.enabled = false;
            }
        }
        public static void EnableRenderer(GameObject parent)
        {
            MeshRenderer[] childRenderers = parent.GetComponentsInChildren<MeshRenderer>(true);
            foreach (MeshRenderer renderer in childRenderers)
            {
                renderer.enabled = true;
            }
        }
    }
}
