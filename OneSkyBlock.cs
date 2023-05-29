using System;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.WorldBuilding;
using Terraria.GameContent.Generation;

namespace OneSkyBlock
{
    public class OneSkyBlock : Mod
    {
        internal static OneSkyBlock Instance;
        internal const string ModifyOneSkyBlockConfig_Permission = "ModifyOneSkyBlockConfig";
        internal const string ModifyOneSkyBlockConfig_Display = "Modify OneSkyBlock Config";
        public override void Load()
        {
            Instance = this;
            /*
            cheatSheet = ModLoader.GetMod("CheatSheet");
            herosMod = ModLoader.GetMod("HEROsMod");
            */
        }
        public override void Unload()
        {
            Instance = null;
        }
        public override void PostSetupContent()
        {
            ModLoader.TryGetMod("HEROsMod", out Mod HEROsMod);
            HEROsMod?.Call(
                    "AddPermission",
                    ModifyOneSkyBlockConfig_Permission,
                    ModifyOneSkyBlockConfig_Display
                );
        }
    }
    class WeightedRandomBag<T>
    {
        private struct Entry
        {
            public double accumulatedWeight;
            public T item;
        }

        private readonly List<Entry> entries = new();
        private double accumulatedWeight;
        private readonly Random rand = new();
        public void AddEntry(T item, double weight)
        {
            accumulatedWeight += weight;
            entries.Add(new Entry { item = item, accumulatedWeight = accumulatedWeight });
        }
        public T GetRandom()
        {
            double r = rand.NextDouble() * accumulatedWeight;

            foreach (Entry entry in entries)
            {
                if (entry.accumulatedWeight >= r)
                {
                    return entry.item;
                }
            }
            return default;
        }
    }

    public class OneSkyBlockResetWorld : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            foreach (var genTask in tasks)
            {
                Console.WriteLine("GenTask: {0}", genTask.Name);
            }
            GenPass resetTask = tasks.Find(x => x.Name == "Reset");
            /*
            GenPass dungeonTask = (ModContent.GetInstance<OneSkyBlockConfig>().GenerateDungeon) ? tasks.Find(x => x.Name == "Dungeon") : null;
            GenPass jungleTask1 = (ModContent.GetInstance<OneSkyBlockConfig>().GenerateTemple) ? tasks.Find(x => x.Name == "Jungle Temple") : null;
            GenPass jungleTask2 = (ModContent.GetInstance<OneSkyBlockConfig>().GenerateTemple) ? tasks.Find(x => x.Name == "Lihzahrd Altars") : null;
            GenPass islandTask1 = (ModContent.GetInstance<OneSkyBlockConfig>().GenerateIslands) ? tasks.Find(x => x.Name == "Floating Island") : null;
            GenPass islandTask2 = (ModContent.GetInstance<OneSkyBlockConfig>().GenerateIslands) ? tasks.Find(x => x.Name == "Floating Island Houses") : null;
            */
            GenPass hellTask1 = (ModContent.GetInstance<OneSkyBlockConfig>().GenerateUnderworld) ? tasks.Find(x => x.Name == "Underworld") : null;
            GenPass hellTask2 = (ModContent.GetInstance<OneSkyBlockConfig>().GenerateUnderworld) ? tasks.Find(x => x.Name == "Hellforge") : null;
            GenPass pyramidTask = (ModContent.GetInstance<OneSkyBlockConfig>().GeneratePyramids) ? tasks.Find(x => x.Name == "Pyramids") : null;
            GenPass microTask = (ModContent.GetInstance<OneSkyBlockConfig>().GenerateBiomes) ? tasks.Find(x => x.Name == "Micro Biomes") : null;
            //GenPass oceanTask = tasks.Find(x => x.Name == "Oasis");
            tasks.Clear();
            tasks.Add(resetTask);
            /*
            if (ModContent.GetInstance<OneSkyBlockConfig>().GenerateDungeon)
            {
                tasks.Add(dungeonTask);
            }
            if (ModContent.GetInstance<OneSkyBlockConfig>().GenerateIslands)
            {
                tasks.Add(islandTask1);
                tasks.Add(islandTask2);
            }
            */
            if (ModContent.GetInstance<OneSkyBlockConfig>().GenerateUnderworld)
            {
                tasks.Add(hellTask1);
                tasks.Add(hellTask2);
            }
            if (ModContent.GetInstance<OneSkyBlockConfig>().GeneratePyramids)
            {
                tasks.Add(pyramidTask);
            }
            if (ModContent.GetInstance<OneSkyBlockConfig>().GenerateBiomes)
            {
                tasks.Add(microTask);
            }
            int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Reset"));
            tasks.Insert(genIndex + 1, new PassLegacy("Erasing World", ResetWorldPass, 1f));
            //tasks.Add(oceanTask);
            //tasks.Insert(genIndex + 1, new PassLegacy("Generating Temple", NewTemplePass, 1f));
        }
        private void ResetWorldPass(GenerationProgress progress, GameConfiguration config)
        {
            progress.Message = Language.GetTextValue("Mods.OneSkyBlock.WorldGen.PrepareChallenge");
            progress.Set(0f);
            Main.spawnTileX = Main.maxTilesX / 2;
            Main.spawnTileY = (Main.maxTilesY / 4) - 30;
            Main.worldSurface = Main.maxTilesY / 4;
            Main.rockLayer = Main.worldSurface + (Main.spawnTileY / 1.5);
            WorldGen.PlaceTile(Main.spawnTileX, Main.spawnTileY, TileID.Cloud, false, false);
            Thread.Sleep(500); // lol
            Random rand = new();
            int dungeonSide = Main.dungeonX < Main.maxTilesX / 2 ? -1 : 1;
            //int next = (int)(Main.GlobalTimeWrappedHourly);

            if (ModContent.GetInstance<OneSkyBlockConfig>().GenerateIslands)
            {
                progress.Message = $"{Language.GetTextValue("Mods.OneSkyBlock.WorldGen.GenerateIslands")} (1/2)";
                progress.Set(0.3f);
                var islandSuccess = false;
                var totalAttempts = 0;
                while (!islandSuccess && totalAttempts < 100000)
                {
                    List<int> randomX = new()
                    {
                        (int)(Main.spawnTileX / (1.11 + (rand.NextDouble() / 3))),
                        (int)(Main.spawnTileX / (2 + (rand.NextDouble() / 2))),
                        (int)(Main.spawnTileX * (1.1 + (rand.NextDouble() / 3))),
                        (int)(Main.spawnTileX * (1.5 + (rand.NextDouble() / 2)))
                    };
                    List<int> islandX = randomX.OrderBy(a => Guid.NewGuid()).ToList();

                    int islandY1 = (int)(Main.spawnTileY / (2 + (rand.NextDouble() * 2)));
                    int islandY2 = (int)(Main.spawnTileY / (2 + (rand.NextDouble() * 2)));
                    int islandY3 = (int)(Main.spawnTileY / (2 + (rand.NextDouble() * 2)));
                    int islandY4 = (int)(Main.spawnTileY / (2 + (rand.NextDouble() * 2)));

                    //static IEnumerable<int> AdjacentTiles(int i, int j)
                    //{
                    //    for (int x = i - 100; x <= i + 100; x++)
                    //        for (int y = j - 100; y <= j + 100; y++)
                    //            if (x != i || y != j && Framing.GetTileSafely(x, y).HasTile)
                    //                yield return Framing.GetTileSafely(x, y).TileType;
                    //}
                    foreach (var islandTask in islandX)
                    {
                        Console.WriteLine("Island: {0}", islandTask);
                    }
                    try
                    {
                        WorldGen.CloudIsland(islandX[0], islandY1);
                        //if (AdjacentTiles(islandX[0], islandY1).Contains(TileID.Dirt))
                        //{
                        //    WorldGen.PlaceTile(islandX[0], islandY1, TileID.Grass);
                        //    WorldGen.SpreadGrass(islandX[0], islandY1);
                        //}
                        WorldGen.IslandHouse(islandX[0], islandY1, 2);

                        WorldGen.DesertCloudIsland(islandX[1], islandY2);
                        WorldGen.IslandHouse(islandX[1], islandY2, 3);

                        WorldGen.CloudLake(islandX[2], islandY3);

                        WorldGen.SnowCloudIsland(islandX[3], islandY4);
                        WorldGen.IslandHouse(islandX[3], islandY4, 1);
                        islandSuccess = true;
                        progress.Message = $"{Language.GetTextValue("Mods.OneSkyBlock.WorldGen.GenerateIslands")} (2/2)";
                        Thread.Sleep(250);
                    }
                    catch (Exception)
                    {
                        islandSuccess = false;
                        totalAttempts++;
                        progress.Message = $"{Language.GetTextValue("Mods.OneSkyBlock.WorldGen.GenerateIslands")} (1/2) - {totalAttempts}";
                    }
                }

            }

            if (ModContent.GetInstance<OneSkyBlockConfig>().GenerateTemple)
            {
                progress.Message = Language.GetTextValue("Mods.OneSkyBlock.WorldGen.GenerateJungle");
                progress.Set(0.6f);
                //tasks.Add(jungleTask1);
                //tasks.Add(jungleTask2);
                //ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);
                WorldGen.makeTemple(dungeonSide == 1 ? Main.maxTilesX / 5 : (int)(Main.maxTilesX / 1.25), (int)Main.rockLayer);
            }
            if (ModContent.GetInstance<OneSkyBlockConfig>().GenerateDungeon)
            {
                progress.Message = $"{Language.GetTextValue("Mods.OneSkyBlock.WorldGen.GenerateDungeon")} (1/2)";
                progress.Set(0.8f);
                Main.dungeonX = dungeonSide == 1 ? (int)(Main.maxTilesX / 1.25) : Main.maxTilesX / 5;
                Main.dungeonY = Main.spawnTileY + 30;
                WorldGen.PlaceTile(Main.dungeonX, Main.dungeonY, TileID.Stone);
                Thread.Sleep(250);
                var dungeonSuccess = false;
                while (!dungeonSuccess)
                {
                    try
                    {
                        WorldGen.MakeDungeon(Main.dungeonX, Main.dungeonY);
                        dungeonSuccess = true;
                        progress.Message = $"{Language.GetTextValue("Mods.OneSkyBlock.WorldGen.GenerateDungeon")} (2/2)";
                        Thread.Sleep(250);
                    }
                    catch (Exception)
                    {
                        Main.dungeonX = rand.Next(Main.maxTilesX);
                        Main.dungeonY = Main.spawnTileY + 30;
                        dungeonSuccess = false;
                        progress.Message = $"{Language.GetTextValue("Mods.OneSkyBlock.WorldGen.GenerateDungeon")} (1/2) - {Main.dungeonX}";
                    }
                }
            }
            Thread.Sleep(250);
            progress.Message = Language.GetTextValue("Mods.OneSkyBlock.WorldGen.FinishLoading");
            progress.Set(1f);
            Thread.Sleep(500);
        }
    }
}