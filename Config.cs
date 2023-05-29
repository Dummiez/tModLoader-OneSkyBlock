using Terraria.ID;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace OneSkyBlock
{
    //[LabelKey("OneSkyBlockConfig")]
    class OneSkyBlockConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        [Header("$Mods.OneSkyBlock.Configuration.WorldGenerationHeader")]
        [JsonIgnore]
        [DefaultValue(true)]
        [LabelKey("$Mods.OneSkyBlock.Configuration.GenerateDungeon")]
        [TooltipKey("$Mods.OneSkyBlock.Configuration.GenerateDungeonTooltip")]
        public bool GenerateDungeon { get; set; }

        [JsonIgnore]
        [DefaultValue(true)]
        [LabelKey("$Mods.OneSkyBlock.Configuration.GenerateTemple")]
        [TooltipKey("$Mods.OneSkyBlock.Configuration.GenerateTempleTooltip")]
        public bool GenerateTemple { get; set; }

        [JsonIgnore]
        [DefaultValue(false)]
        [LabelKey("$Mods.OneSkyBlock.Configuration.GenerateUnderworld")]
        [TooltipKey("$Mods.OneSkyBlock.Configuration.GenerateUnderworldTooltip")]
        public bool GenerateUnderworld { get; set; }

        [JsonIgnore]
        [DefaultValue(false)]
        [LabelKey("$Mods.OneSkyBlock.Configuration.GeneratePyramids")]
        [TooltipKey("$Mods.OneSkyBlock.Configuration.GeneratePyramidsTooltip")]
        public bool GeneratePyramids { get; set; }

        [JsonIgnore]
        [DefaultValue(false)]
        [LabelKey("$Mods.OneSkyBlock.Configuration.GenerateIslands")]
        [TooltipKey("$Mods.OneSkyBlock.Configuration.GenerateIslandsTooltip")]
        public bool GenerateIslands { get; set; }

        [JsonIgnore]
        [DefaultValue(false)]
        [LabelKey("$Mods.OneSkyBlock.Configuration.GenerateBiomes")]
        [TooltipKey("$Mods.OneSkyBlock.Configuration.GenerateBiomesTooltip")]
        public bool GenerateBiomes { get; }

        [JsonIgnore]
        [DefaultValue(false)]
        [LabelKey("$Mods.OneSkyBlock.Configuration.GenerateOceans")]
        [TooltipKey("$Mods.OneSkyBlock.Configuration.GenerateOceansTooltip")]
        public bool GenerateOceans { get; }

        [Header("$Mods.OneSkyBlock.Configuration.SkyblockSettingsHeader")]
        [JsonIgnore]
        [DefaultValue(5)]
        [Range(1, 100)]
        [LabelKey("$Mods.OneSkyBlock.Configuration.OneBlockMaxDrops")]
        [TooltipKey("$Mods.OneSkyBlock.Configuration.OneBlockMaxDropsTooltip")]
        public int OneBlockMaxDrops { get; set; }
        [JsonIgnore]
        [DefaultValue(1.2f)]
        [Range(0f, 10f)]
        [Increment(0.1f)]
        [LabelKey("$Mods.OneSkyBlock.Configuration.OneBlockBlockRate")]
        [TooltipKey("$Mods.OneSkyBlock.Configuration.OneBlockBlockRateTooltip")]
        public float OneBlockBlockRate { get; set; }
        [JsonIgnore]
        [DefaultValue(1f)]
        [Range(0f, 10f)]
        [Increment(0.1f)]
        [LabelKey("$Mods.OneSkyBlock.Configuration.OneBlockOreRate")]
        [TooltipKey("$Mods.OneSkyBlock.Configuration.OneBlockOreRateTooltip")]
        public float OneBlockOreRate { get; set; }
        [JsonIgnore]
        [DefaultValue(0.7f)]
        [Range(0f, 10f)]
        [Increment(0.1f)]
        [LabelKey("$Mods.OneSkyBlock.Configuration.OneBlockGemRate")]
        [TooltipKey("$Mods.OneSkyBlock.Configuration.OneBlockGemRateTooltip")]
        public float OneBlockGemRate { get; set; }
        [JsonIgnore]
        [DefaultValue(0.9f)]
        [Range(0f, 10f)]
        [Increment(0.1f)]
        [LabelKey("$Mods.OneSkyBlock.Configuration.OneBlockLiquidsRate")]
        [TooltipKey("$Mods.OneSkyBlock.Configuration.OneBlockLiquidsRateTooltip")]
        public float OneBlockLiquidsRate { get; set; }
        [JsonIgnore]
        [DefaultValue(0.8f)]
        [Range(0f, 10f)]
        [Increment(0.1f)]
        [LabelKey("$Mods.OneSkyBlock.Configuration.OneBlockSeedsRate")]
        [TooltipKey("$Mods.OneSkyBlock.Configuration.OneBlockSeedsRateTooltip")]
        public float OneBlockSeedsRate { get; set; }
        [JsonIgnore]
        [DefaultValue(0.9f)]
        [Range(0f, 10f)]
        [Increment(0.1f)]
        [LabelKey("$Mods.OneSkyBlock.Configuration.OneBlockCratesRate")]
        [TooltipKey("$Mods.OneSkyBlock.Configuration.OneBlockCratesRateTooltip")]
        public float OneBlockCratesRate { get; set; }
        [JsonIgnore]
        [DefaultValue(0.8f)]
        [Range(0f, 10f)]
        [Increment(0.1f)]
        [LabelKey("$Mods.OneSkyBlock.Configuration.OneBlockStatuesRate")]
        [TooltipKey("$Mods.OneSkyBlock.Configuration.OneBlockStatuesRateTooltip")]
        public float OneBlockStatuesRate { get; set; }
        [JsonIgnore]
        [DefaultValue(0.9f)]
        [Range(0f, 10f)]
        [Increment(0.1f)]
        [LabelKey("$Mods.OneSkyBlock.Configuration.OneBlockItemsRate")]
        [TooltipKey("$Mods.OneSkyBlock.Configuration.OneBlockItemsRateTooltip")]
        public float OneBlockItemsRate { get; set; }

        [Header("$Mods.OneSkyBlock.Configuration.SkyblockSettingsHeader")]
        [JsonIgnore]
        [DefaultValue(0.5f)]
        [Range(0f, 2f)]
        [Increment(0.025f)]
        [LabelKey("$Mods.OneSkyBlock.Configuration.OneBlockCustomRate")]
        [TooltipKey("$Mods.OneSkyBlock.Configuration.OneBlockCustomRateTooltip")]
        public float OneBlockCustomRate { get; set; }
        //[LabelKey("key: Item or Block to add to drop table\nvalue: drop rate (1 = Common, 0.5 = Uncommon, 0.1 = Rare, 0 = Unobtainable")]
        [LabelKey("$Mods.OneSkyBlock.Configuration.OneBlockCustomDrops")]
        [TooltipKey("$Mods.OneSkyBlock.Configuration.OneBlockCustomDropsTooltip")]
        public List<ItemDefinition> OneBlockCustomDrops { get; set; } = new List<ItemDefinition>();
        //public Dictionary<ItemDefinition, float> OneBlockCustomDrops { get; set; } = new Dictionary<ItemDefinition, float>();

        [Header("$Mods.OneSkyBlock.Configuration.Extras")]
        [JsonIgnore]
        [DefaultValue(false)]
        [LabelKey("$Mods.OneSkyBlock.Configuration.OneBlockMultiplayer")]
        [TooltipKey("$Mods.OneSkyBlock.Configuration.OneBlockMultiplayerTooltip")]
        public bool OneBlockMultiplayer { get; set; }

        [JsonIgnore]
        [DefaultValue(false)]
        [LabelKey("$Mods.OneSkyBlock.Configuration.OneBlockDebug")]
        [TooltipKey("$Mods.OneSkyBlock.Configuration.OneBlockDebugTooltip")]
        public bool OneBlockDebug { get; set; }

        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
        {
            try
            {
                Mod HEROsMod = ModLoader.GetMod("HEROsMod");
                if (HEROsMod != null && HEROsMod.Version >= new Version(0, 2, 2))
                {
                    if (HEROsMod.Call("HasPermission", whoAmI, OneSkyBlock.ModifyOneSkyBlockConfig_Permission) is bool result && result)
                        return true;
                    message = $"Insufficient \"{OneSkyBlock.ModifyOneSkyBlockConfig_Display}\" permission.";
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
            return base.AcceptClientChanges(pendingConfig, whoAmI, ref message);
        }
    }
}