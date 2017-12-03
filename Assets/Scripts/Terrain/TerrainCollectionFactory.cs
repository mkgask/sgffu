using UnityEngine;
using sgffu.World;

namespace sgffu.Terrain
{
    public class TerrainCollectionFactory
    {
        public static TerrainCollection create(TerrainConfig terrain_config, int world_size)
        {
            int terrain_size = world_size / terrain_config.chunk_size;
            //Debug.Log("TerrainCollectionFactory.create: terrain_size: " + terrain_size);

            TerrainCollection terrain_collection = new TerrainCollection();
            terrain_collection.terrain_chunk_size = terrain_size;
            terrain_collection.terrain_chunk_offset = Mathf.FloorToInt(terrain_size / 2);
            terrain_collection.terrain_pos_start = terrain_collection.terrain_chunk_offset * -1;
            terrain_collection.terrain_pos_end = terrain_collection.terrain_chunk_offset;
            return terrain_collection;
        }
    }
}