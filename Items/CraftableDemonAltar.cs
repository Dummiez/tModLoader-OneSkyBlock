using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ObjectData;
using Terraria;
using Terraria.Localization;
using System.Collections.Generic;

namespace OneSkyBlock.Items
{
    public class CraftableDemonAltar : ModItem
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Will not drop anything if you break it!");//Language.GetTextValue("Mods.OneSkyBlock.CraftableAltar.Tooltip"));
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tooltip = new(Mod, "OneSkyBlock: DemonAltar", Language.GetTextValue("Mods.OneSkyBlock.CraftableAltar.Tooltip"));
            tooltips.Add(tooltip);
        }
        public override void SetDefaults()
        {
            TileObjectData.newTile.FullCopyFrom(TileID.DemonAltar);
            Item.SetNameOverride(Language.GetTextValue("Mods.OneSkyBlock.CraftableAltar.DisplayName"));
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.createTile = TileID.DemonAltar; //ModContent.TileType<CraftableDemonAltar>();	
            Item.placeStyle = 0;
        }

        public override void AddRecipes()
        {
            // Demon alter recipe
            Recipe.Create(TileID.DemonAltar)
                .AddIngredient(ItemID.EbonstoneBlock, 15)
                .AddIngredient(ItemID.RottenChunk, 10)
                .AddIngredient(ItemID.Deathweed, 5)
                .AddIngredient(ItemID.BattlePotion, 1)
                .AddIngredient(ItemID.ThornsPotion, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
            Recipe.Create(TileID.DemonAltar)
                .AddIngredient(ItemID.CrimstoneBlock, 15)
                .AddIngredient(ItemID.Vertebrae, 10)
                .AddIngredient(ItemID.Deathweed, 5)
                .AddIngredient(ItemID.BattlePotion, 1)
                .AddIngredient(ItemID.ThornsPotion, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
            // Other recipes
            Recipe.Create(ItemID.Hellforge)
                .AddIngredient(ItemID.Hellstone, 15)
                .AddIngredient(ItemID.Obsidian, 10)
                .AddIngredient(ItemID.LavaBucket, 2)
                .AddIngredient(ItemID.Furnace, 1)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            Recipe.Create(ItemID.GuideVoodooDoll)
                .AddIngredient(ItemID.FamiliarWig)
                .AddIngredient(ItemID.FamiliarShirt)
                .AddIngredient(ItemID.FamiliarPants)
                .AddIngredient(ItemID.Silk, 2)
                .AddIngredient(ItemID.AshBlock, 1)
                .AddTile(TileID.Loom)
                .Register();
            Recipe.Create(ItemID.LivingLoom)
                .AddIngredient(ItemID.Wood, 10)
                .AddIngredient(ItemID.WhiteString)
                .AddTile(TileID.WorkBenches)
                .Register();
            Recipe.Create(ItemID.LihzahrdAltar)
                .AddIngredient(ItemID.LihzahrdBrick, 15)
                .AddIngredient(ItemID.FallenStar, 10)
                .AddIngredient(ItemID.HellstoneBar, 5)
                .AddIngredient(ItemID.ChlorophyteBar, 5)
                .AddTile(TileID.AdamantiteForge)
                .Register();
            Recipe.Create(ItemID.LihzahrdPowerCell)
                .AddIngredient(ItemID.LeadBar, 5)
                .AddIngredient(ItemID.GoldBar, 5)
                .AddIngredient(ItemID.LihzahrdBrick, 3)
                .AddIngredient(ItemID.FallenStar, 1)
                .AddIngredient(ItemID.ChlorophyteBar, 1)
                .AddTile(TileID.AdamantiteForge)
                .Register();
            Recipe.Create(ItemID.WoodenCrate)
                .AddIngredient(ItemID.Wood, 25)
                .AddIngredient(ItemID.CopperBar, 3)
                .AddIngredient(ItemID.TinBar, 3)
                .AddIngredient(ItemID.Amethyst, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
            Recipe.Create(ItemID.IronCrate)
                .AddIngredient(ItemID.IronBar, 10)
                .AddIngredient(ItemID.LeadBar, 5)
                .AddIngredient(ItemID.Topaz, 1)
                .AddTile(TileID.Anvils)
                .Register();
            Recipe.Create(ItemID.GoldenCrate)
                .AddIngredient(ItemID.GoldBar, 10)
                .AddIngredient(ItemID.PlatinumBar, 5)
                .AddIngredient(ItemID.Sapphire, 1)
                .AddTile(TileID.Anvils)
                .Register();
            Recipe.Create(ItemID.JungleFishingCrate)
                .AddIngredient(ItemID.MudBlock, 30)
                .AddIngredient(ItemID.JungleGrassSeeds, 3)
                .AddIngredient(ItemID.Moonglow, 1)
                .AddIngredient(ItemID.Emerald, 1)
                .AddTile(TileID.Anvils)
                .Register();
            Recipe.Create(ItemID.CorruptFishingCrate)
                .AddIngredient(ItemID.EbonstoneBlock, 25)
                .AddIngredient(ItemID.DemoniteBar, 10)
                .AddIngredient(ItemID.RottenChunk, 5)
                .AddIngredient(ItemID.Topaz, 1)
                .AddTile(TileID.Anvils)
                .Register();
            Recipe.Create(ItemID.CrimsonFishingCrate)
                .AddIngredient(ItemID.CrimstoneBlock, 25)
                .AddIngredient(ItemID.CrimtaneBar, 10)
                .AddIngredient(ItemID.Vertebrae, 5)
                .AddIngredient(ItemID.Topaz, 1)
                .AddTile(TileID.Anvils)
                .Register();
            Recipe.Create(ItemID.FloatingIslandFishingCrate)
                .AddIngredient(ItemID.Cloud, 25)
                .AddIngredient(ItemID.Feather, 5)
                .AddIngredient(ItemID.Diamond, 1)
                .AddTile(TileID.Anvils)
                .Register();
            Recipe.Create(ItemID.DungeonFishingCrate)
                .AddIngredient(ItemID.Bone, 20)
                .AddIngredient(ItemID.Obsidian, 10)
                .AddIngredient(ItemID.Meteorite, 10)
                .AddTile(TileID.Anvils)
                .Register();
            Recipe.Create(ItemID.OceanCrate)
                .AddIngredient(ItemID.SandBlock, 30)
                .AddIngredient(ItemID.WaterBucket, 10)
                .AddIngredient(ItemID.BouncyGlowstick, 5)
                .AddIngredient(ItemID.Fish, 1)
                .AddTile(TileID.Anvils)
                .Register();
            Recipe.Create(ItemID.HallowedFishingCrateHard)
                .AddIngredient(ItemID.PearlstoneBlock, 30)
                .AddIngredient(ItemID.PixieDust, 5)
                .AddIngredient(ItemID.UnicornHorn, 5)
                .AddCondition(Condition.Hardmode)
                //.AddCondition(NetworkText.FromLiteral("after hardmode"), recipe => Main.hardMode)
                .AddTile(TileID.Anvils)
                .Register();
            Recipe.Create(ItemID.OasisCrate)
                .AddIngredient(ItemID.SandBlock, 40)
                .AddIngredient(ItemID.HardenedSand, 20)
                .AddIngredient(ItemID.Cactus, 5)
                .AddIngredient(ItemID.AntlionMandible, 1)
                .AddTile(TileID.Anvils)
                .Register();
            Recipe.Create(ItemID.LifeFruit)
                .AddIngredient(ItemID.HallowedBar, 5)
                .AddIngredient(ItemID.LifeCrystal, 2)
                .AddIngredient(ItemID.JungleFishingCrate, 1)
                .AddCondition(Condition.DownedMechBossAny)
                //.AddCondition(NetworkText.FromLiteral("any mechanical boss defeated"), r => NPC.downedMechBossAny)
                .Register();
        }
    }
}