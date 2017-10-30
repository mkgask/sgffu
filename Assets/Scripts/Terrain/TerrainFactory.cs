using UnityEngine;
using OwrBase.World;
using OwrBase.Scene;
using OwrBase.EventMessage;
using UniRx;
using StrOpe = StringOperationUtil.OptimizedStringOperation;

namespace OwrBase.Terrain
{

    public class TerrainFactory
    {

        const string terrain_gameobject_name = "Terrain";

        public static void create()
        {
            GameObject terrain_gameobject = GameObject.Find(terrain_gameobject_name);
            TerrainConfig terrain_config = TerrainConfigFactory.loadFile(TerrainConfigFactory.createDefault());
            WorldConfig world_config = (SceneService.transition_scene_data as WorldCreated).world_config;

            TerrainService.reset(terrain_gameobject, terrain_config, world_config);
            TerrainService.first_create(0, 0, terrain_config.texture_filepath);

            MessageBroker.Default.Receive<playerTerrainChunkMove>().Subscribe(x => {
                TerrainService.update(x.x, x.z, terrain_config.texture_filepath);
            });
        }

    }

}