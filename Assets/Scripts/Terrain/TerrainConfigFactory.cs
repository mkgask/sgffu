using UnityEngine;
using OwrBase.Config;

namespace OwrBase.Terrain {
    public class TerrainConfigFactory {

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
                perlin_noise_scale = 0.0035f,
                actual_chunk_size = calcurate_actual_chunk_size(chunk_size),
                texture_filepath = "Terrain/Grounds/diffuse_light1.png"
            };
        }

        public static TerrainConfig loadFile(TerrainConfig terrain_config_default)
        {
            TerrainConfig terrain_config = ConfigFile.load<TerrainConfig>(ConfigFile.terrainConfigFilename, terrain_config_default);
            terrain_config.actual_chunk_size = calcurate_actual_chunk_size(terrain_config.chunk_size);
            return terrain_config;
        }

        public static float calcurate_actual_chunk_size(int chunk_size) {
            return chunk_size / (Mathf.Max(chunk_size / 64, 0.5f) * 2);
        }
    }

}
