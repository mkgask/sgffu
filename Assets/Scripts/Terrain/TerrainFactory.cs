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
            //Debug.Log("World Name: " + world_config.world_name);
            //Debug.Log("seed: " + world_config.terrain_seed);

            TerrainService.reset(terrain_gameobject, terrain_config, world_config);
            TerrainService.update(0, 0, terrain_config.texture_filepath);

            //Debug.Log(StrOpe.i + "Receive: playerTerrainChunkMove: reservation");
            MessageBroker.Default.Receive<playerTerrainChunkMove>().Subscribe(x => {
                Debug.Log(StrOpe.i + "Receive: playerTerrainChunkMove: " + x.x + " , " + x.z);
                TerrainService.update(x.x, x.z, terrain_config.texture_filepath);
            });

            MessageBroker.Default.Publish(new TerrainCreated{});
        }

    }

}