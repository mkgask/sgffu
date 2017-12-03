using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using sgffu.Config;
using sgffu.World;
using sgffu.Utility;
using sgffu.Filesystem;
using sgffu.EventMessage;
using StrOpe = StringOperationUtil.OptimizedStringOperation;

namespace sgffu.Terrain {

    public class TerrainService
    {
        public static GameObject terrain_parent;

        public static TerrainConfig terrain_config;

        private static TerrainCollection terrain_collection;

        private static WorldConfig world_config;


        private static int preview_left_top_x = 0;

        private static int preview_left_top_z = 0;

        private static int preview_right_bottom_x = 0;

        private static int preview_right_bottom_z = 0;

        private static Texture2D texture;

        public static void reset(GameObject game_object, TerrainConfig terrain_config, WorldConfig world_config)
        {
            terrain_parent = game_object;
            TerrainService.terrain_config = terrain_config;
            TerrainService.world_config = world_config;
            terrain_collection = TerrainCollectionFactory.create(terrain_config, world_config.world_size);
            TerrainEntityRepository.reset(world_config.world_name);

            texture = ConfigData.instantiate_texture2D(
                StrOpe.i + "/Resources/" + terrain_config.texture_filepath,
                terrain_config.detail_resolution,
                terrain_config.detail_resolution
            );
        }

        public static IEnumerator update(int player_x, int player_z)
        {
            int effective_range = terrain_config.chunk_effective_range;
            int left_top_x = player_x - effective_range;
            int left_top_z = player_z - effective_range;
            int right_bottom_x = player_x + effective_range;
            int right_bottom_z = player_z + effective_range;
            int chunk_size = terrain_config.chunk_size;
            //Debug.Log("TerrainService.update: " + player_x + ", " + player_z);

            //Debug.Log("TerrainService.update: effective_range: " + effective_range);
            //Debug.Log("TerrainService.update: left_top_x, left_top_z: " + left_top_x + ", " + left_top_z);
            //Debug.Log("TerrainService.update: right_bottom_x, right_bottom_z: " + right_bottom_x + ", " + right_bottom_z);

            float terrain_seed = world_config.terrain_seed;
            float perlin_noise_scale = terrain_config.perlin_noise_scale;
            bool terrain_create = false;

            for (int x = left_top_x; x <= right_bottom_x; x += 1) {
                for (int z = left_top_z; z <= right_bottom_z; z += 1) {
                    //Debug.Log("TerrainService.update: x, z: " + x + ", " + z);
                    yield return null;
                    if (terrain_collection[x, z] == null) {
                        terrain_create = true;
                        createTerrain(x, z, chunk_size, terrain_seed, perlin_noise_scale);
                    } else {
                        //Debug.Log("TerrainService.update: terrain_collection[x, z].status(): " + terrain_collection[x, z].status());
                        if (!terrain_collection[x, z].status()) {
                            terrain_collection[x, z].enable();
                        }
                    }
                }
            }

            //Debug.Log("TerrainService.update: terrain_create: " + terrain_create);

            if (terrain_create) {
                for (int x = left_top_x; x <= right_bottom_x; x += 1) {
                    for (int z = left_top_z; z <= right_bottom_z; z += 1) {
                        if (!terrain_collection[x, z].status()) {
                            setupTerrain(x, z, chunk_size);
                        }
                    }
                }
            }

            for (int x = preview_left_top_x; x <= preview_right_bottom_x; x += 1) {
                for (int z = preview_left_top_z; z <= preview_right_bottom_z; z += 1) {
                    yield return null;
                    if (left_top_x <= x && x <= right_bottom_x &&
                            left_top_z <= z && z <= right_bottom_z) {
                        continue;
                    }
                    if(terrain_collection[x, z] == null) {
                        continue;
                    }
                    //Debug.Log("TerrainService.update: disable check: x, z: " + x + ", " + z);
                    terrain_collection[x, z].disable();
                }
            }

            preview_left_top_x = left_top_x;
            preview_left_top_z = left_top_z;
            preview_right_bottom_x = right_bottom_x;
            preview_right_bottom_z = right_bottom_z;
        }

        public static void createUnityTerrains()
        {
            int chunk_size = terrain_config.chunk_size;
            float terrain_seed = world_config.terrain_seed;
            float perlin_noise_scale = terrain_config.perlin_noise_scale;
            
            int xs = terrain_collection.terrain_pos_start;
            int xe = terrain_collection.terrain_pos_end;
            int zs = terrain_collection.terrain_pos_start;
            int ze = terrain_collection.terrain_pos_end;

            for (int x = xs; x < xe; x += 1) {
                for (int z = zs; z < ze; z += 1) {
                    createTerrain(x, z, chunk_size, terrain_seed, perlin_noise_scale);
                }
            }

            preview_left_top_x = xs;
            preview_left_top_z = zs;
            preview_right_bottom_x = xe;
            preview_right_bottom_z = ze;
        }

        public static void setupTerrainCollection()
        {
/*
            Debug.Log("TerrainService.setupTerrainCollection: terrain_chunk_size: " + terrain_chunk_size);
            Debug.Log("TerrainService.setupTerrainCollection: terrain_collection: " + terrain_collection.entities.GetLength(0) + ", " + terrain_collection.entities.GetLength(1));
*/
            int chunk_size = terrain_config.chunk_size;

            for (int xx = terrain_collection.terrain_pos_start; xx < terrain_collection.terrain_pos_end; xx += 1) {
                for (int zz = terrain_collection.terrain_pos_start; zz < terrain_collection.terrain_pos_end; zz += 1) {
                    //Debug.Log("TerrainService.setupTerrainCollection: xx, zz: " + xx + ", " + zz);
                    setupTerrain(xx, zz, chunk_size);
                }
            }
        }

        private static void createTerrain(int x, int z, int chunk_size, float terrain_seed, float perlin_noise_scale)
        {
            int xs = x * chunk_size;
            int zs = z * chunk_size;
            float[,] heights = createHeightMap(xs, zs, terrain_seed, perlin_noise_scale);
            //Debug.Log("TerrainService.createUnityTerrains: x, z: " + x + ", " + z);
            //Debug.Log("TerrainService.createUnityTerrains: xs, zs: " + xs + ", " + zs);
            terrain_collection[x, z] = TerrainEntityFactory.create(x, z, heights, terrain_config, terrain_parent);
            terrain_collection[x, z].disable();
        }

        private static void setupTerrain(int x, int z, int chunk_size)
        {
            terrain_collection[x, z].setNeighbors(
                terrain_collection[x - 1, z],
                terrain_collection[x, z + 1],
                terrain_collection[x + 1, z],
                terrain_collection[x, z - 1]
            );

            terrain_collection[x, z].setTexture(texture, chunk_size);
            TerrainEntityRepository.set(terrain_collection[x, z]);
            terrain_collection[x, z].enable();
        }

        private static float[,] createHeightMap(int xs, int zs, float terrain_seed, float perlin_noise_scale)
        {
            //Debug.Log("IndexOutOfRangeException: " + xs + ", " + zs + " : " + xe + ", " + ze);
            int x_max = terrain_config.chunk_size + 1;
            int z_max = terrain_config.chunk_size + 1;
            //Debug.Log("IndexOutOfRangeException: " + x_max + ", " + z_max);
            float[,] heights = new float[z_max, x_max];

            for (int x = 0; x < x_max; x += 1) {
                for (int z = 0; z < z_max; z += 1) {
                    //Debug.Log("TerrainService.createHeightMap: " + (xs + x) + ", " + (zs + z));
                    float xx = Rand.calucurate_perlin_value(xs + x, terrain_seed, perlin_noise_scale);
                    float zz = Rand.calucurate_perlin_value(zs + z, terrain_seed, perlin_noise_scale);
                    heights[z, x] = Mathf.PerlinNoise(xx, zz);
                }
            }

            return heights;
        }

        public static float getHeight(float x, float z)
        {
            int integer_part_x = Mathf.CeilToInt(x / terrain_config.chunk_size);
            int integer_part_z = Mathf.CeilToInt(z / terrain_config.chunk_size);
            //Debug.Log("TerrainService.getHeight: integer_part_x, integer_part_z: " + integer_part_x + ", " + integer_part_z);
            float r = terrain_collection[integer_part_x, integer_part_z].getHeight(
                x - integer_part_x,
                z - integer_part_z
            );
            //Debug.Log("TerrainService.getHeight: r: " + r);
            return r;
        }

    }

}
