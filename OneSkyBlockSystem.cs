using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Threading;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;

namespace OneSkyBlock
{
    public class OneSkyBlockTile : GlobalTile
    {
        static readonly Dictionary<short, double> scaleItemWeight = new()
        {
            { ItemID.CopperOre, 0 },
            { ItemID.TinOre, 0 },
            { ItemID.IronOre, 0 },
            { ItemID.LeadOre, 0 },
            { ItemID.SilverOre, 0 },
            { ItemID.TungstenOre, 0 },
            { ItemID.GoldOre, 0 },
            { ItemID.PlatinumOre, 0 },
            { ItemID.DemoniteOre, 0 },
            { ItemID.CrimtaneOre, 0 },
            { ItemID.Meteorite, 0 },
            { ItemID.Hellstone, 0 },
            { ItemID.PalladiumOre, 0 },
            { ItemID.CobaltOre, 0 },
            { ItemID.MythrilOre, 0 },
            { ItemID.OrichalcumOre, 0 },
            { ItemID.TitaniumOre, 0 },
            { ItemID.AdamantiteOre, 0 },
            { ItemID.ChlorophyteOre, 0 },
            { ItemID.LunarOre, 0 },
            { ItemID.WaterBucket, 0 }
        };
        static readonly Dictionary<short, int> blockMultiplier = new()
        {
            { ItemID.DirtBlock, 10 },
            { ItemID.StoneBlock, 5 },
            { ItemID.Wood, 5},
            { ItemID.WaterBucket, 5 },
            { ItemID.SandBlock, 3 },
            { ItemID.MudBlock, 3 },
            { ItemID.SnowBlock, 2 },
            { ItemID.ClayBlock, 2 },
            { ItemID.IceBlock, 2 },
        };
        public override bool CanDrop(int i, int j, int type)
        {
            int playerCount = ModContent.GetInstance<OneSkyBlockConfig>().OneBlockMultiplayer ? Main.player.Count(p => p.whoAmI < Main.maxPlayers && p.active) : 1;
            for (int p = 0; p < playerCount; p++)
            {
                if (i == (Main.spawnTileX + (p * 5)) && j == Main.spawnTileY)
                    return false;
                //return i != (Main.spawnTileX + (p * 5)) || j != Main.spawnTileY;
            }
            return true;
        }

        public override bool KillSound(int i, int j, int type, bool fail) // Disable mining sound for skyblock, it's annoying
        {
            int playerCount = ModContent.GetInstance<OneSkyBlockConfig>().OneBlockMultiplayer ? Main.player.Count(p => p.whoAmI < Main.maxPlayers && p.active) : 1;
            for (int p = 0; p < playerCount; p++)
            {
                if (i == (Main.spawnTileX + (p * 5)) && j == Main.spawnTileY)
                    return false;
            }
            return true;
        }
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            var ModSkyBlockConfig = ModContent.GetInstance<OneSkyBlockConfig>();
            int playerCount = ModSkyBlockConfig.OneBlockMultiplayer ? Main.player.Count(p => p.whoAmI < Main.maxPlayers && p.active) : 1;
            for (int p = 0; p < playerCount; p++)
            {
                //noItem = true;

                if (i == (Main.spawnTileX + (p * 5)) && j == Main.spawnTileY && fail == false)
                {
                    effectOnly = true;
                    WeightedRandomBag<short> skyDrops = new();

                    if (NPC.downedBoss1 == false || NPC.downedSlimeKing == false) // Eye of Cthulu not killed
                    {   // Basic blocks
                        skyDrops.AddEntry(ItemID.DirtBlock, 2.0 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.StoneBlock, 2.0 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.Wood, 2.0 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.SandBlock, 1.0 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.ClayBlock, 1.0 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.MudBlock, 1.0 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.SnowBlock, 1.0 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.IceBlock, 0.5 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.Cobweb, 0.5 * ModSkyBlockConfig.OneBlockItemsRate);
                        // Ores
                        skyDrops.AddEntry(ItemID.CopperOre, 2.0 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.CopperOre]);
                        skyDrops.AddEntry(ItemID.TinOre, 2.0 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.TinOre]);
                        skyDrops.AddEntry(ItemID.IronOre, 1.5 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.IronOre]);
                        skyDrops.AddEntry(ItemID.LeadOre, 1.5 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.LeadOre]);
                        skyDrops.AddEntry(ItemID.SilverOre, 1.0 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.SilverOre]);
                        skyDrops.AddEntry(ItemID.TungstenOre, 1.0 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.TungstenOre]);
                        skyDrops.AddEntry(ItemID.GoldOre, 0.5 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.GoldOre]);
                        skyDrops.AddEntry(ItemID.PlatinumOre, 0.5 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.PlatinumOre]);
                        // Gems
                        skyDrops.AddEntry(ItemID.Amethyst, 0.2 * ModSkyBlockConfig.OneBlockGemRate);
                        skyDrops.AddEntry(ItemID.Topaz, 0.18 * ModSkyBlockConfig.OneBlockGemRate);
                        skyDrops.AddEntry(ItemID.Sapphire, 0.16 * ModSkyBlockConfig.OneBlockGemRate);
                        skyDrops.AddEntry(ItemID.Emerald, 0.14 * ModSkyBlockConfig.OneBlockGemRate);
                        skyDrops.AddEntry(ItemID.Ruby, 0.12 * ModSkyBlockConfig.OneBlockGemRate);
                        skyDrops.AddEntry(ItemID.Diamond, 0.1 * ModSkyBlockConfig.OneBlockGemRate);
                        skyDrops.AddEntry(ItemID.Amber, 0.1 * ModSkyBlockConfig.OneBlockGemRate);
                        // Seeds
                        skyDrops.AddEntry(ItemID.Cactus, 0.06 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.BlinkrootSeeds, 0.02 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.DaybloomSeeds, 0.02 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.DeathweedSeeds, 0.02 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.FireblossomSeeds, 0.02 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.MoonglowSeeds, 0.02 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.ShiverthornSeeds, 0.02 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.WaterleafSeeds, 0.02 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.GrassSeeds, 0.1 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.JungleGrassSeeds, 0.1 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.MushroomGrassSeeds, 0.1 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.CorruptSeeds, 0.05 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.CrimsonSeeds, 0.05 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.HallowedSeeds, 0.01 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.Acorn, 0.25 * ModSkyBlockConfig.OneBlockSeedsRate);
                        //Buckets
                        skyDrops.AddEntry(ItemID.WaterBucket, 0.3 * ModSkyBlockConfig.OneBlockLiquidsRate + scaleItemWeight[ItemID.WaterBucket]);
                        skyDrops.AddEntry(ItemID.LavaBucket, 0.1 * ModSkyBlockConfig.OneBlockLiquidsRate);
                        // Misc
                        skyDrops.AddEntry(ItemID.Cloud, 0.5 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.HardenedSand, 0.5 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.SiltBlock, 0.5 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.SlushBlock, 0.5 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.Cactus, 0.1 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.Hive, 0.2 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.DesertFossil, 0.25 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.DartTrap, 0.02 * ModSkyBlockConfig.OneBlockItemsRate);
                        // Coins
                        skyDrops.AddEntry(ItemID.CopperCoin, 2.0 * ModSkyBlockConfig.OneBlockItemsRate);
                        skyDrops.AddEntry(ItemID.SilverCoin, 0.2 * ModSkyBlockConfig.OneBlockItemsRate);
                        skyDrops.AddEntry(ItemID.GoldCoin, 0.02 * ModSkyBlockConfig.OneBlockItemsRate);
                        // Event Summons, Life Crystals, Crates
                        skyDrops.AddEntry(ItemID.GoblinBattleStandard, 0.01 * ModSkyBlockConfig.OneBlockItemsRate);
                        skyDrops.AddEntry(ItemID.LifeCrystal, 0.05 * ModSkyBlockConfig.OneBlockItemsRate);
                        skyDrops.AddEntry(ItemID.WoodenCrate, 0.05 * ModSkyBlockConfig.OneBlockCratesRate);
                        skyDrops.AddEntry(ItemID.IronCrate, 0.025 * ModSkyBlockConfig.OneBlockCratesRate);
                        skyDrops.AddEntry(ItemID.GoldenCrate, 0.01 * ModSkyBlockConfig.OneBlockCratesRate);
                        // Statues
                        skyDrops.AddEntry(ItemID.BombStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.HeartStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.StarStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.ZombieArmStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.BatStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.BloodZombieStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.BoneSkeletonStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.ChestStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.CorruptStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.CrabStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.DripplerStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.EyeballStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.GoblinStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.GraniteGolemStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.HarpyStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.HopliteStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.HornetStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.ImpStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.JellyfishStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.MedusaStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.PigronStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.PiranhaStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.SharkStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.SkeletonStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.SlimeStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.UndeadVikingStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.UnicornStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.WallCreeperStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.WraithStatue, 0.002 * ModSkyBlockConfig.OneBlockStatuesRate);
                        // Boss Summons
                        skyDrops.AddEntry(ItemID.SlimeCrown, 0.001 * ModSkyBlockConfig.OneBlockItemsRate);
                        skyDrops.AddEntry(ItemID.SuspiciousLookingEye, 0.001 * ModSkyBlockConfig.OneBlockItemsRate);
                        skyDrops.AddEntry(ItemID.WormFood, 0.001 * ModSkyBlockConfig.OneBlockItemsRate);
                        skyDrops.AddEntry(ItemID.BloodySpine, 0.001 * ModSkyBlockConfig.OneBlockItemsRate);
                        skyDrops.AddEntry(ItemID.BloodMoonStarter, 0.001 * ModSkyBlockConfig.OneBlockItemsRate);
                    }
                    else
                    {
                        // Blocks
                        skyDrops.AddEntry(ItemID.DirtBlock, 1.0 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.StoneBlock, 1.0 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.Wood, 1.0 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.SandBlock, 1.0 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.ClayBlock, 1.0 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.MudBlock, 1.0 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.SnowBlock, 1.0 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.IceBlock, 0.5 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.Cobweb, 0.5 * ModSkyBlockConfig.OneBlockItemsRate);
                        // Ores
                        skyDrops.AddEntry(ItemID.CopperOre, 0.25 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.CopperOre]);
                        skyDrops.AddEntry(ItemID.TinOre, 0.25 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.TinOre]);
                        skyDrops.AddEntry(ItemID.IronOre, 0.5 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.IronOre]);
                        skyDrops.AddEntry(ItemID.LeadOre, 0.5 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.LeadOre]);
                        skyDrops.AddEntry(ItemID.SilverOre, 0.75 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.SilverOre]);
                        skyDrops.AddEntry(ItemID.TungstenOre, 0.75 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.TungstenOre]);
                        skyDrops.AddEntry(ItemID.GoldOre, 1.0 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.GoldOre]);
                        skyDrops.AddEntry(ItemID.PlatinumOre, 1.0 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.PlatinumOre]);
                        // Gems
                        skyDrops.AddEntry(ItemID.Amethyst, 0.1 * ModSkyBlockConfig.OneBlockGemRate);
                        skyDrops.AddEntry(ItemID.Topaz, 0.09 * ModSkyBlockConfig.OneBlockGemRate);
                        skyDrops.AddEntry(ItemID.Sapphire, 0.08 * ModSkyBlockConfig.OneBlockGemRate);
                        skyDrops.AddEntry(ItemID.Emerald, 0.07 * ModSkyBlockConfig.OneBlockGemRate);
                        skyDrops.AddEntry(ItemID.Ruby, 0.06 * ModSkyBlockConfig.OneBlockGemRate);
                        skyDrops.AddEntry(ItemID.Diamond, 0.05 * ModSkyBlockConfig.OneBlockGemRate);
                        skyDrops.AddEntry(ItemID.Amber, 0.05 * ModSkyBlockConfig.OneBlockGemRate);
                        // Seeds
                        skyDrops.AddEntry(ItemID.Cactus, 0.06 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.BlinkrootSeeds, 0.02 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.DaybloomSeeds, 0.02 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.DeathweedSeeds, 0.02 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.FireblossomSeeds, 0.02 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.MoonglowSeeds, 0.02 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.ShiverthornSeeds, 0.02 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.WaterleafSeeds, 0.02 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.GrassSeeds, 0.05 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.JungleGrassSeeds, 0.05 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.MushroomGrassSeeds, 0.05 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.CorruptSeeds, 0.05 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.CrimsonSeeds, 0.05 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.HallowedSeeds, 0.1 * ModSkyBlockConfig.OneBlockSeedsRate);
                        skyDrops.AddEntry(ItemID.Acorn, 0.2 * ModSkyBlockConfig.OneBlockSeedsRate);
                        // Buckets
                        skyDrops.AddEntry(ItemID.WaterBucket, 0.3 * ModSkyBlockConfig.OneBlockLiquidsRate + scaleItemWeight[ItemID.WaterBucket]);
                        skyDrops.AddEntry(ItemID.LavaBucket, 0.2 * ModSkyBlockConfig.OneBlockLiquidsRate);
                        skyDrops.AddEntry(ItemID.HoneyBucket, 0.1 * ModSkyBlockConfig.OneBlockLiquidsRate);
                        // Misc
                        skyDrops.AddEntry(ItemID.Cloud, 0.5 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.HardenedSand, 0.5 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.SiltBlock, 0.5 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.SlushBlock, 0.5 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.Cactus, 0.1 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.Hive, 0.2 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.DesertFossil, 0.05 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.DartTrap, 0.01 * ModSkyBlockConfig.OneBlockItemsRate);
                        // Coins
                        skyDrops.AddEntry(ItemID.CopperCoin, 0.5 * ModSkyBlockConfig.OneBlockItemsRate);
                        skyDrops.AddEntry(ItemID.SilverCoin, 0.2 * ModSkyBlockConfig.OneBlockItemsRate);
                        skyDrops.AddEntry(ItemID.GoldCoin, 0.02 * ModSkyBlockConfig.OneBlockItemsRate);
                        // Event Summons, Life Crystals, Crates
                        skyDrops.AddEntry(ItemID.GoblinBattleStandard, 0.001 * ModSkyBlockConfig.OneBlockItemsRate);
                        skyDrops.AddEntry(ItemID.LifeCrystal, 0.05 * ModSkyBlockConfig.OneBlockItemsRate);
                        skyDrops.AddEntry(ItemID.FallenStar, 0.05 * ModSkyBlockConfig.OneBlockItemsRate);
                        // Statues
                        skyDrops.AddEntry(ItemID.BombStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.HeartStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.StarStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.ZombieArmStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.BatStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.BloodZombieStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.BoneSkeletonStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.ChestStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.CorruptStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.CrabStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.DripplerStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.EyeballStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.GoblinStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.GraniteGolemStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.HarpyStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.HopliteStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.HornetStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.ImpStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.JellyfishStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.MedusaStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.PigronStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.PiranhaStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.SharkStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.SkeletonStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.SlimeStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.UndeadVikingStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.UnicornStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.WallCreeperStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        skyDrops.AddEntry(ItemID.WraithStatue, 0.001 * ModSkyBlockConfig.OneBlockStatuesRate);
                        // Boss Summons
                        skyDrops.AddEntry(ItemID.SlimeCrown, 0.001 * ModSkyBlockConfig.OneBlockItemsRate);
                        skyDrops.AddEntry(ItemID.SuspiciousLookingEye, 0.001 * ModSkyBlockConfig.OneBlockItemsRate);
                        skyDrops.AddEntry(ItemID.WormFood, 0.001 * ModSkyBlockConfig.OneBlockItemsRate);
                        skyDrops.AddEntry(ItemID.BloodySpine, 0.001 * ModSkyBlockConfig.OneBlockItemsRate);
                        skyDrops.AddEntry(ItemID.BloodMoonStarter, 0.001 * ModSkyBlockConfig.OneBlockItemsRate);

                        if (Main.hardMode)
                        {
                            // Crate rates for hardmode
                            skyDrops.AddEntry(ItemID.WoodenCrateHard, 0.05 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.IronCrateHard, 0.01 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.GoldenCrateHard, 0.002 * ModSkyBlockConfig.OneBlockCratesRate);

                            skyDrops.AddEntry(ItemID.JungleFishingCrateHard, 0.001 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.FloatingIslandFishingCrateHard, 0.001 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.CorruptFishingCrateHard, 0.001 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.CrimsonFishingCrateHard, 0.001 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.HallowedFishingCrateHard, 0.001 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.DungeonFishingCrateHard, 0.001 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.FrozenCrateHard, 0.001 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.OasisCrateHard, 0.001 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.OceanCrateHard, 0.001 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.LavaCrateHard, 0.001 * ModSkyBlockConfig.OneBlockCratesRate);

                            skyDrops.AddEntry(ItemID.QueenSlimeCrystal, 0.001 * ModSkyBlockConfig.OneBlockItemsRate);
                            skyDrops.AddEntry(ItemID.MechanicalWorm, 0.001 * ModSkyBlockConfig.OneBlockItemsRate);
                            skyDrops.AddEntry(ItemID.MechanicalEye, 0.001 * ModSkyBlockConfig.OneBlockItemsRate);
                            skyDrops.AddEntry(ItemID.MechanicalSkull, 0.001 * ModSkyBlockConfig.OneBlockItemsRate);
                        }
                        else
                        {   // Crate rates for non hardmode
                            skyDrops.AddEntry(ItemID.WoodenCrate, 0.05 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.IronCrate, 0.01 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.GoldenCrate, 0.002 * ModSkyBlockConfig.OneBlockCratesRate);

                            skyDrops.AddEntry(ItemID.JungleFishingCrate, 0.001 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.FloatingIslandFishingCrate, 0.001 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.CorruptFishingCrate, 0.001 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.CrimsonFishingCrate, 0.001 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.HallowedFishingCrate, 0.001 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.DungeonFishingCrate, 0.001 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.FrozenCrate, 0.001 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.OasisCrate, 0.001 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.OceanCrate, 0.001 * ModSkyBlockConfig.OneBlockCratesRate);
                            skyDrops.AddEntry(ItemID.LavaCrate, 0.001 * ModSkyBlockConfig.OneBlockCratesRate);
                        }
                    }
                    if (NPC.downedBoss2)
                    {
                        // Unlockable hardmode ores for 
                        skyDrops.AddEntry(ItemID.DemoniteOre, 0.75 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.DemoniteOre]);
                        skyDrops.AddEntry(ItemID.CrimtaneOre, 0.75 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.CrimtaneOre]);
                        skyDrops.AddEntry(ItemID.EbonstoneBlock, 1.0 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.Meteorite, 0.75 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.Meteorite]);
                        skyDrops.AddEntry(ItemID.Obsidian, 0.75 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.CrimstoneBlock, 1.0 * ModSkyBlockConfig.OneBlockBlockRate);
                        //skyDrops.AddEntry(ItemID.BlueBrickWall, 0.5);
                        //skyDrops.AddEntry(ItemID.GreenBrickWall, 0.5);
                        //skyDrops.AddEntry(ItemID.RedBrickWall, 0.5);
                    }
                    if (NPC.downedBoss2 && NPC.downedBoss3)
                    {
                        skyDrops.AddEntry(ItemID.Marble, 1.0 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.Granite, 1.0 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.Sandstone, 1.0 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.AshBlock, 1.0 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.Hellstone, 0.75 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.Hellstone]);
                    }
                    if (NPC.downedBoss2 && NPC.downedBoss3 && Main.hardMode)
                    {
                        skyDrops.AddEntry(ItemID.CobaltOre, 0.75 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.CobaltOre]);
                        skyDrops.AddEntry(ItemID.PalladiumOre, 0.75 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.PalladiumOre]);
                        skyDrops.AddEntry(ItemID.PearlstoneBlock, 1.0 * ModSkyBlockConfig.OneBlockBlockRate);

                    }
                    if (Main.hardMode && NPC.downedMechBossAny)
                    {
                        skyDrops.AddEntry(ItemID.MythrilOre, 0.75 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.MythrilOre]);
                        skyDrops.AddEntry(ItemID.OrichalcumOre, 0.75 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.OrichalcumOre]);
                    }
                    if (Main.hardMode && Convert.ToInt32(NPC.downedMechBoss1) + Convert.ToInt32(NPC.downedMechBoss2) + Convert.ToInt32(NPC.downedMechBoss3) >= 2)
                    {
                        skyDrops.AddEntry(ItemID.TitaniumOre, 0.75 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.TitaniumOre]);
                        skyDrops.AddEntry(ItemID.AdamantiteOre, 0.75 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.AdamantiteOre]);
                    }
                    if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                    {
                        skyDrops.AddEntry(ItemID.ChlorophyteOre, 0.75 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.ChlorophyteOre]);
                    }
                    if (Main.hardMode && NPC.downedPlantBoss)
                    {
                        skyDrops.AddEntry(ItemID.LihzahrdBrick, 1 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.SuperDartTrap, 0.05 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.SpearTrap, 0.02 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.SpikyBallTrap, 0.02 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.FlameTrap, 0.02 * ModSkyBlockConfig.OneBlockBlockRate);
                        skyDrops.AddEntry(ItemID.WoodenSpike, 0.02 * ModSkyBlockConfig.OneBlockBlockRate);
                    }
                    if (Main.hardMode && NPC.downedGolemBoss)
                    {
                        skyDrops.AddEntry(ItemID.PlatinumCoin, 0.01 * ModSkyBlockConfig.OneBlockItemsRate);
                    }
                    if (Main.hardMode && NPC.downedMoonlord)
                    {
                        skyDrops.AddEntry(ItemID.LunarOre, 0.75 * ModSkyBlockConfig.OneBlockOreRate + scaleItemWeight[ItemID.LunarOre]);
                    }

                    if (ModSkyBlockConfig.OneBlockCustomDrops.Count > 0)
                        foreach (var customItem in ModSkyBlockConfig.OneBlockCustomDrops)
                        {
                            skyDrops.AddEntry((short)customItem.Type, ModSkyBlockConfig.OneBlockCustomRate);
                        }

                    short dropTile = skyDrops.GetRandom();

                    if (ModSkyBlockConfig.OneBlockOreRate > 0)
                    {
                        foreach (var oreProgress in scaleItemWeight)
                        {
                            //Main.NewText(ItemID.Search.GetName(oreProgress.Key) + " increased: " + oreProgress.Value);
                            scaleItemWeight[oreProgress.Key] += ModSkyBlockConfig.OneBlockOreRate / 400;
                            if (dropTile == oreProgress.Key)
                            {
                                scaleItemWeight[oreProgress.Key] = 0;
                                //Main.NewText(ItemID.Search.GetName(oreProgress.Key) + " was reset, accumulation: " + oreProgress.Value);
                            }

                        }
                    }
                    WeightedRandomBag<int> quantityCount = new();
                    WeightedRandomBag<int> quantityCountMult = new();
                    int maxDrop = ModSkyBlockConfig.OneBlockMaxDrops;
                    for (int d = 1; d <= maxDrop; d++)
                    {
                        quantityCount.AddEntry(d, (double)(100 / d) / d);
                    }
                    if (blockMultiplier.ContainsKey(dropTile))
                    {
                        for (int d = 1; d <= blockMultiplier[dropTile]; d++)
                        {
                            quantityCountMult.AddEntry(d, (double)(10 / d) / d);
                        }
                    }
                    else
                    {
                        quantityCountMult.AddEntry(1, 1);
                    }
                    //if (Main.netMode == NetmodeID.MultiplayerClient)
                    //               {
                    //	Main.LocalPlayer.QuickSpawnItem(new EntitySource_TileBreak(i, j), dropTile, quantityCount.GetRandom() * quantityCountBlockMult.GetRandom());
                    //               }
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 8, 8, dropTile, quantityCount.GetRandom() * quantityCountMult.GetRandom());
                    }
                }
            }
            //int playerCount = Main.player.Count(p => p.whoAmI < Main.maxPlayers && p.active);
            for (int p = 0; p < playerCount; p++)
            {
                Tile cloudTile = Framing.GetTileSafely(Main.spawnTileX + (p * 5), Main.spawnTileY); //Tile cloudTile = new();
                if (!cloudTile.HasTile)
                {
                    WorldGen.PlaceTile(Main.spawnTileX + (p * 5), Main.spawnTileY, TileID.Cloud, false);
                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, Main.spawnTileX + (p * 5), Main.spawnTileY, TileID.Cloud);
                    //ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("[" + i + " " + j + "] ID: " + type + " Break: " + fail + " Effect: " + effectOnly + " Item: " + noItem + " PlrCount: " + playerCount), Color.White);
                }
            }
        }
    }

    public class OneSkyBlockDrop : GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if (item.type == ItemID.PlanteraBossBag && !ModContent.GetInstance<OneSkyBlockConfig>().GenerateTemple)
            {
                foreach (var rule in itemLoot.Get())
                {
                    if (rule is OneFromOptionsNotScaledWithLuckDropRule oneFromOptionsDrop && oneFromOptionsDrop.dropIds.Contains(ItemID.GreaterHealingPotion))
                    {
                        var original = oneFromOptionsDrop.dropIds.ToList();
                        original.Add(ItemID.LihzahrdAltar);
                        oneFromOptionsDrop.dropIds = original.ToArray();
                    }
                }
            }
        }
    }
    public class OneSkyBlockShop : GlobalNPC
    {
        public override void SetupTravelShop(int[] shop, ref int nextSlot)
        {
            shop[nextSlot++] = ItemID.Extractinator; //.SetDefaults(ItemID.Extractinator);
            //base.SetupTravelShop(shop, ref nextSlot);
        }
    }

    public class OneSkyBlockSystem : ModSystem
    {
        public override void PreUpdateNPCs()
        {
            int playerCount = ModContent.GetInstance<OneSkyBlockConfig>().OneBlockMultiplayer ? Main.player.Count(p => p.whoAmI < Main.maxPlayers && p.active) : 1;
            //ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("plrCount: " + playerCount + " count: " + Main.player.Count(p => p.whoAmI < Main.maxPlayers && p.active)), Color.White);
            for (int p = 0; p < playerCount; p++)
            {
                Tile cloudTile = Framing.GetTileSafely(Main.spawnTileX + (p * 5), Main.spawnTileY);
                if (!cloudTile.HasTile)
                {
                    WorldGen.PlaceTile(Main.spawnTileX + (p * 5), Main.spawnTileY, TileID.Cloud, false);
                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, Main.spawnTileX + (p * 5), Main.spawnTileY, TileID.Cloud);
                }
            }
        }
    }
    internal class OneSkyBlockUpdate : ModSystem
    {
        static readonly HttpClient client = new();
        static readonly string url = "https://raw.githubusercontent.com/Dummiez/tModLoader-OneSkyBlock/main/build.txt";
        public override void OnWorldLoad() => CheckLatestVersion();
        public static void CheckLatestVersion(bool worldLoaded = false)
        {
            if (!NetworkInterface.GetIsNetworkAvailable() || Main.netMode == NetmodeID.SinglePlayer && !worldLoaded)
                return;
            Task.Run(async () => {
                while (Main.gameMenu)
                    Thread.Sleep(new TimeSpan(0, 0, 1));
                try
                {
                    HttpResponseMessage response;
                    response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var fetchVersion = content.Replace(" ", "");
                        if (fetchVersion.Contains("version="))
                        {
                            fetchVersion = fetchVersion.Split(new[] { "version=" }, StringSplitOptions.None)[1].Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)[0];
                            var LatestVersion = new Version(fetchVersion);
                            if (LatestVersion > OneSkyBlock.Instance.Version)
                            {
                                if (worldLoaded)
                                {
                                    Main.NewText($"A new version of {OneSkyBlock.Instance.Name} ({OneSkyBlock.Instance.DisplayName}) is available: v{LatestVersion}", Color.LightBlue);
                                    if (content.Contains("patchNotes = "))
                                    {
                                        string patchNote = content.Split(new[] { "patchNotes = " }, StringSplitOptions.None)[1].Split(new[] { "\".*?\"" }, StringSplitOptions.None)[0].Replace("\"", "").Trim('\r', '\n').Replace("\\n", "\r\n");
                                        Main.NewText(patchNote, Color.LightBlue);
                                    }
                                }
                                else Console.WriteLine($"[{OneSkyBlock.Instance.Name}] A new version is available: v{LatestVersion}");
                            }
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"[{OneSkyBlock.Instance.Name}] {e}");
                }
            });
        }
    }
    internal class OneSkyBlockEnter : ModPlayer
    {
        public override void OnEnterWorld() => OneSkyBlockUpdate.CheckLatestVersion(true);
    }
}