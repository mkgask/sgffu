using UnityEngine;
using UnityTerrain = UnityEngine.Terrain;


namespace sgffu.Terrain
{

    public class TerrainEntityFactory
    {
        public static TerrainEntity create(int x, int z, float[,] heights, TerrainConfig config, GameObject parent)
        {
            GameObject game_object = createGameObject("Terrain-" + x + "-" + z, parent);
            TerrainData tData = createTerrainData(config);
           
            tData.SetHeights(0, 0, heights);
            game_object.GetComponent<UnityTerrain>().terrainData = tData;
            game_object.GetComponent<TerrainCollider>().terrainData = tData;
            game_object.transform.position = new Vector3(x * config.chunk_size, 0f, z * config.chunk_size);
 
            TerrainEntity entity = new TerrainEntity(
                x,
                z,
                heights,
                config.terrain_height,
                game_object,
                game_object.GetComponent<UnityTerrain>()
            );

            return entity;
        }

        public static TerrainEntity createFromCore(TerrainEntityCore core, TerrainConfig config, GameObject parent)
        {
            return create(
                core.pos_x,
                core.pos_z,
                core.height_map,
                config,
                parent
            );
        }

        private static GameObject createGameObject(string name, GameObject parent)
        {
            GameObject game_object = new GameObject(name);

            game_object.AddComponent<UnityTerrain>();
            game_object.AddComponent<TerrainCollider>();
            game_object.transform.SetParent(parent.transform);

            return game_object;
        }

        private static TerrainData createTerrainData(TerrainConfig config)
        {
            TerrainData tData = new TerrainData();

            tData.size = new Vector3(config.actual_chunk_size, config.terrain_height, config.actual_chunk_size);
            tData.heightmapResolution = config.chunk_size;
            tData.baseMapResolution = config.base_map_resolution;
            tData.SetDetailResolution(config.detail_resolution, config.resolution_per_path);

            return tData;
        }

    }
}