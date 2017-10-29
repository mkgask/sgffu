using UnityEngine;
using UniRx;
using OwrBase.Config;
using OwrBase.EventMessage;
using StrOpe = StringOperationUtil.OptimizedStringOperation;

namespace OwrBase.Terrain {

    public class TerrainController : MonoBehaviour
    {

        private TerrainService terrains;

        public int chunk_effective_range = 0;  // 1 or 2

        public int chunk_size = 0;

        public float terrain_height = 0f;

        //public int base_map_resolution = 0;  // == chunk_size

        public int detail_resolution = 0;

        public int resolution_per_path = 0;

        public float perlin_noise_scale = 0f;

        public string texture_filepath;



        // Use this for initialization
        void Start()
        {
            TerrainConfig terrain_param_default = new TerrainConfig {
                chunk_effective_range = this.chunk_effective_range,
                chunk_size = this.chunk_size,
                terrain_height = this.terrain_height,
                base_map_resolution = this.chunk_size,
                detail_resolution = this.detail_resolution,
                resolution_per_path = this.resolution_per_path,
                perlin_noise_scale = this.perlin_noise_scale,
                texture_filepath = this.texture_filepath
            };

            TerrainConfig terrain_config = ConfigFile.load<TerrainConfig>(ConfigFile.terrainConfigFilename, terrain_param_default);

            //TerrainService.reset(gameObject, terrain_config);
            TerrainService.update(0, 0, terrain_config.texture_filepath);

            Debug.Log(StrOpe.i + "Receive: playerTerrainChunkMove: reservation");
            MessageBroker.Default.Receive<playerTerrainChunkMove>().Subscribe(x => {
                Debug.Log(StrOpe.i + "Receive: playerTerrainChunkMove: " + x.x + " , " + x.z);
                TerrainService.update(x.x, x.z, terrain_config.texture_filepath);
            });

            MessageBroker.Default.Publish(new TerrainCreated{});
        }

    /*
        // Update is called once per frame
        void Update () {

        }
    */

    }

}
