using HutongGames.PlayMaker;

namespace LoppeHouse
{
    internal class Variables
    {
        public static float TimerCalculations;
        public static FsmInt PlayerKeys = new FsmInt("PlayerKeys").Value = 0;
        public static bool NewGame { get; set; }
        public static bool ModLoaded { get; set; }
        public static bool ItemsPacked { get; set; }
        public static bool HouseBurnt { get; set; }
        public static bool HouseElectricity { get; set; }
        public static bool ElectricityChecked { get; set; }
        public static bool LightKitchen { get; set; }
        public static bool LightLiving { get; set; }
        public static bool LightPlayer { get; set; }
        public static bool LightBathroom { get; set; }
        public static bool LightParents { get; set; }
        public static bool LightHallroom { get; set; }
        public static bool LightCloset { get; set; }
        public static bool LightGarage { get; set; }
        public static bool LightGarageDash { get; set; }
        public static bool LightOutdoor { get; set; }
        public static bool LightOutdoorTable { get; set; }
    }
}
