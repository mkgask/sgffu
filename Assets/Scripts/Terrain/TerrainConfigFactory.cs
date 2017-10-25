using OwrBase.Config;

namespace OwrBase.Terrain {
    public class TerrainConfigFactory {

        public static TerrainConfig createDefault()
        {
            return new TerrainConfig {
                chunk_num_offset = 1024,
                chunk_effective_range = 2,
                chunk_size = 64,
                terrain_height = 128,
                base_map_resolution = 64,
                detail_resolution = 1024,
                resolution_per_path = 512,
                perlin_noise_scale = 0.003f,
                texture_filepath = "Terrain/Grounds/diffuse_light1.png"
            };
        }

        public static TerrainConfig loadFile(TerrainConfig terrain_config_default)
        {
            return ConfigFile.load<TerrainConfig>(ConfigFile.terrainConfigFilename, terrain_config_default);
        }
    }

}
