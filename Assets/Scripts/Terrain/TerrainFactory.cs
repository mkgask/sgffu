using UnityEngine;
using sgffu.World;
using sgffu.Scene;
using sgffu.EventMessage;
using sgffu.Filesystem;
using UniRx;
using StrOpe = StringOperationUtil.OptimizedStringOperation;

namespace sgffu.Terrain
{

    public class TerrainFactory
    {
        const string terrain_gameobject_name = "Terrain";

        public static void create()
        {
            GameObject terrain_gameobject = GameObject.FindWithTag(terrain_gameobject_name);
            TerrainConfig terrain_config = TerrainConfigRepository.get(TerrainConfigRepository.createDefault());
            WorldConfig world_config = (SceneService.transition_scene_data as AllowWorldCreate).world_config;
            //Debug.Log("terrain_seed: " + world_config.terrain_seed);

            TerrainService.reset(terrain_gameobject, terrain_config, world_config);

            Debug.Log("TerrainFactory.create: TerrainService.createUnityTerrains: ");
            TerrainService.createUnityTerrains();

            Debug.Log("TerrainFactory.create: TerrainService.setupTerrainCollection: ");
            TerrainService.setupTerrainCollection();

            Observable.FromCoroutine(x => 
                TerrainService.update(0, 0)
            ).Subscribe(x => {
                MessageBroker.Default.Publish(new TerrainCreated{});
            });

            MessageBroker.Default.Receive<playerTerrainChunkMove>().Subscribe(x => {
                Debug.Log("TerrainFactory.create: " + x.x + ", " + x.z);
                MainThreadDispatcher.StartCoroutine(TerrainService.update(x.x, x.z));
            });
        }

    }

}