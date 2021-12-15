using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;

namespace DesertWorld
{
    class DesertMod : Mod
    {
        public DesertMod()
        {
        }
    }

    class DesertWorld : ModWorld
	{
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
            if (genIndex != -1)
            {
                tasks.Insert(genIndex + 1, new PassLegacy("Extreme Desertification", ExtremeDesertification));
            }
        }

        private void ExtremeDesertification(GenerationProgress progress)
        {
            progress.Message = "Extreme Desertification";

            int xStart = 0;
            int xEnd = Main.maxTilesX;
            int yStart = 0;
            int yEnd = Main.maxTilesY;

            // Tiles to replace

            int[] tilesToSand = {
                TileID.Dirt,
                TileID.Grass,
                TileID.CorruptGrass,
                TileID.Mud,
                TileID.JungleGrass,
                TileID.MushroomGrass,
                TileID.Ebonsand,
                TileID.SnowBlock,
                TileID.FleshGrass,
                TileID.Crimsand
            };

            int[] tilesToHardenedSand = {
                TileID.ClayBlock,
                TileID.IceBlock,
                TileID.CorruptIce,
                TileID.FleshIce,
                TileID.Hive,
                TileID.Marble,
                TileID.Granite
            };

            int[] tilesToSandstone = {
                TileID.Stone,
                TileID.Ebonstone,
                TileID.GreenMoss,
                TileID.BrownMoss,
                TileID.RedMoss,
                TileID.BlueMoss,
                TileID.PurpleMoss,
                TileID.Crimstone
            };

            int[] tilesToDesertFossil = {
                TileID.Silt,
                TileID.Slush
            };

            int[] tilesToSpookyWood = {
                TileID.LivingWood,
                TileID.LivingMahogany
            };

            int[] tilesToClear = {
                TileID.Plants,
                TileID.Trees,
                TileID.CorruptPlants,
                TileID.Sunflower,
                TileID.CorruptThorns,
                TileID.Vines,
                TileID.JunglePlants,
                TileID.JungleVines,
                TileID.JungleThorns,
                TileID.MushroomPlants,
                TileID.MushroomTrees,
                TileID.Plants2,
                TileID.JunglePlants2,
                TileID.ImmatureHerbs,
                TileID.BreakableIce,
                TileID.Stalactite,
                TileID.LongMoss,
                TileID.LeafBlock,
                TileID.FleshWeeds,
                TileID.CrimsonVines,
                TileID.DyePlants,
                TileID.PlantDetritus,
                TileID.CrimtaneThorns,
                TileID.LivingMahoganyLeaves,
                TileID.BeeHive
            };

            // Walls to replace

            int[] wallsToHardenedSand = {
                WallID.DirtUnsafe,
                WallID.MudUnsafe,
                WallID.SnowWallUnsafe,
                WallID.HiveUnsafe,
                WallID.SpiderUnsafe,
                WallID.GrassUnsafe,
                WallID.JungleUnsafe,
                WallID.FlowerUnsafe,
                WallID.Grass,
                WallID.IceUnsafe,
                WallID.MarbleUnsafe,
                WallID.GraniteUnsafe,
                WallID.JungleUnsafe1,
                WallID.JungleUnsafe2,
                WallID.JungleUnsafe3,
                WallID.JungleUnsafe4
            };

            int[] wallsToSandstone = {
                WallID.EbonstoneUnsafe,
                WallID.CaveUnsafe,
                WallID.Cave2Unsafe,
                WallID.Cave3Unsafe,
                WallID.Cave4Unsafe,
                WallID.Cave5Unsafe,
                WallID.CrimstoneUnsafe
            };

            int[] wallsToSpookyWood = {
                WallID.LivingWood
            };

            // Loop over all tiles and replaces specific tile types with specific desert tiles

            for (int y = yStart; y < yEnd; y++)
            {
                for (int x = xStart; x < xEnd; x++)
                {
                    Tile currentTile = Main.tile[x, y];
                    if (currentTile.active())
                    {
                        if (tilesToSand.Contains(currentTile.type))
                        {
                            Main.tile[x, y].type = TileID.Sand;
                        }
                        else if (tilesToHardenedSand.Contains(currentTile.type))
                        {
                            Main.tile[x, y].type = TileID.HardenedSand;
                        }
                        else if (tilesToSandstone.Contains(currentTile.type))
                        {
                            Main.tile[x, y].type = TileID.Sandstone;
                        }
                        else if (tilesToDesertFossil.Contains(currentTile.type))
                        {
                            Main.tile[x, y].type = TileID.DesertFossil;
                        }
                        else if (tilesToSpookyWood.Contains(currentTile.type))
                        {
                            Main.tile[x, y].type = TileID.SpookyWood;
                        }
                        else if (tilesToClear.Contains(currentTile.type))
                        {
                            Main.tile[x, y].ClearTile();
                        }
                    }

                    // Replace walls too

                    if (wallsToHardenedSand.Contains(currentTile.wall))
                    {
                        Main.tile[x, y].wall = WallID.HardenedSand;
                    }
                    else if (wallsToSandstone.Contains(currentTile.wall))
                    {
                        Main.tile[x, y].wall = WallID.Sandstone;
                    }
                    else if (wallsToSpookyWood.Contains(currentTile.wall))
                    {
                        Main.tile[x, y].wall = WallID.SpookyWood;
                    }

                }
            }
        }
    }
}
