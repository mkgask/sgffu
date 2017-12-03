using UnityEngine;
using sgffu.Filesystem;

namespace sgffu.World
{
    public class WorldConfigFactory
    {
        public static WorldConfig create(string world_name, uint seed) {
            return new WorldConfig {
                world_name = world_name,
                world_size = 1024,
                seed = seed,
                terrain_seed = ((seed / Mathf.Pow(10, seed.ToString().Length - 1)) + 4.5f) / 2,
            };
        }

        public static WorldConfig createBlank() {
            return new WorldConfig{};
        }
        
    }

}