using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using OwrBase.Config;
using OwrBase.World;
using OwrBase.EventMessage;
using StrOpe = StringOperationUtil.OptimizedStringOperation;

namespace OwrBase.Terrain {

    public class TerrainService
    {

        public static TerrainConfig terrain_config;

        private static TerrainCollection terrain_collection;

        private static WorldConfig world_config;

        public static int chunk_effective_range = 0;  // 1 or 2

        public static void reset(GameObject game_object, TerrainConfig terrain_config, WorldConfig world_config)
        {
            TerrainService.terrain_config = terrain_config;
            TerrainService.world_config = world_config;
            TerrainService.chunk_effective_range = terrain_config.chunk_effective_range;
            TerrainService.terrain_collection = new TerrainCollection(game_object, terrain_config);
        }

        public static void first_create(int center_x, int center_z, string texture_filepath)
        {
            Texture2D texture = ConfigData.instantiate_texture2D(
                StrOpe.i + "/Resources/" + texture_filepath,
                TerrainService.terrain_config.detail_resolution,
                TerrainService.terrain_config.detail_resolution
            );

            Observable.FromCoroutine(() => 
                TerrainService.terrain_collection.update(
                    center_x - TerrainService.chunk_effective_range,
                    center_z - TerrainService.chunk_effective_range,
                    center_x + TerrainService.chunk_effective_range,
                    center_z + TerrainService.chunk_effective_range,
                    texture,
                    TerrainService.world_config.terrain_seed
                )
            ).Subscribe(x =>
                MessageBroker.Default.Publish(new TerrainCreated{})
            );
        }

        public static void update(int center_x, int center_z, string texture_filepath)
        {
            Texture2D texture = ConfigData.instantiate_texture2D(
                StrOpe.i + "/Resources/" + texture_filepath,
                TerrainService.terrain_config.detail_resolution,
                TerrainService.terrain_config.detail_resolution
            );

            MainThreadDispatcher.StartUpdateMicroCoroutine(
                TerrainService.terrain_collection.update(
                    center_x - TerrainService.chunk_effective_range,
                    center_z - TerrainService.chunk_effective_range,
                    center_x + TerrainService.chunk_effective_range,
                    center_z + TerrainService.chunk_effective_range,
                    texture,
                    TerrainService.world_config.terrain_seed
                )
            );
        }

        public static float getHeight(float x, float z)
        {
            return TerrainService.terrain_collection.getHeight(x, z);
        }

    }

}
