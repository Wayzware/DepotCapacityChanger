using Colossal;
using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;
using Game.UI;
using System.Collections.Generic;
using Unity.Entities;

namespace DepotCapacityChanger
{
    [FileLocation(nameof(DepotCapacityChanger))]
    [SettingsUIGroupOrder(CapacityGroup, CapacityGroup)]
    [SettingsUIShowGroupName(CapacityGroup)]
    public class Setting : ModSetting
    {
        public const string CapacitySection = "Modify Depot Capacity";
        public const string ResetSection = "Reset";
        public const string CapacityGroup = "Student Capacity";

        public Setting(IMod mod) : base(mod)
        {
            if (BusSlider == 0) SetDefaults();
        }

        public override void SetDefaults()
        {
            BusSlider = 100;
            TaxiSlider = 100;
            SubwaySlider = 100;
            TramSlider = 100;
            TrainSlider = 100;
        }

        public override void Apply()
        {
            base.Apply();
            var system = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<DepotCapacityChangerSystem>();
            system.Enabled = true;
        }

        [SettingsUISlider(min = 100, max = 1000, step = 25, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(CapacitySection, CapacityGroup)]
        public int BusSlider { get; set; }

        [SettingsUISlider(min = 100, max = 1000, step = 25, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(CapacitySection, CapacityGroup)]
        public int TaxiSlider { get; set; }

        [SettingsUISlider(min = 100, max = 1000, step = 25, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(CapacitySection, CapacityGroup)]
        public int TramSlider { get; set; }

        [SettingsUISlider(min = 100, max = 1000, step = 25, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(CapacitySection, CapacityGroup)]
        public int TrainSlider { get; set; }

        [SettingsUISlider(min = 100, max = 1000, step = 25, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(CapacitySection, CapacityGroup)]
        public int SubwaySlider { get; set; }
    }

    public class LocaleEN : IDictionarySource
    {
        private readonly Setting m_Setting;
        public LocaleEN(Setting setting)
        {
            m_Setting = setting;
        }
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "Depot Capacity Changer" },
                { m_Setting.GetOptionTabLocaleID(Setting.CapacitySection), "Main" },
                { m_Setting.GetOptionGroupLocaleID(Setting.CapacityGroup), "Depot Capacity" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusSlider)), "Bus Depot" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TaxiSlider)), "Taxi Depot" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwaySlider)), "Subway Depot" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramSlider)), "Tram Depot" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainSlider)), "Train Depot" },

                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusSlider)), "Set the vehicle capacity of bus depot buildings relative to their vanilla capacity." },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TaxiSlider)), "Set the vehicle capacity of taxi depot buildings relative to their vanilla capacity." },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwaySlider)), "Set the vehicle capacity of subway depot buildings relative to their vanilla capacity." },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramSlider)), "Set the vehicle capacity of tram depot buildings relative to their vanilla capacity." },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainSlider)), "Set the vehicle capacity of train depot buildings relative to their vanilla capacity." },
            };
        }

        public void Unload()
        {

        }
    }
}
