using UnityEngine;
using sgffu.Config;

namespace sgffu.Terrain
{
    public class TerrainConfigRepository
    {
        public static TerrainConfig createDefault()
        {
            int chunk_size = 128;

            return new TerrainConfig {
                chunk_effective_range = 2,
                chunk_size = chunk_size,
                terrain_height = 128,
                base_map_resolution = 64,
                detail_resolution = 1024,
                resolution_per_path = 512,
                perlin_noise_scale = 0.002f,
                actual_chunk_size = calcurate_actual_chunk_size(chunk_size),
                texture_filepath = "Terrain/Grounds/diffuse_light1.png"
            };
        }

        public static TerrainConfig get(TerrainConfig terrain_config_default)
        {
            TerrainConfig terrain_config = ConfigFile.load<TerrainConfig>(ConfigFile.terrainConfigFilename, terrain_config_default);
            terrain_config.actual_chunk_size = calcurate_actual_chunk_size(terrain_config.chunk_size);
            return terrain_config;
        }

        public static bool set(TerrainConfig terrain_config)
        {
            return ConfigFile.save<TerrainConfig>(ConfigFile.terrainConfigFilename, terrain_config);
        }

        public static float calcurate_actual_chunk_size(int chunk_size) {
            return chunk_size / (Mathf.Max(chunk_size / 64, 0.5f) * 2);
        }
    }

}
