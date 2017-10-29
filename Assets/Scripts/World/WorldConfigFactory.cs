using UnityEngine;

namespace OwrBase.World {

    public class WorldConfigFactory {
        
        public static WorldConfig create(string world_name, uint seed) {
            return new WorldConfig {
                world_name = world_name,
                seed = seed,
                terrain_seed = seed / Mathf.Pow(10, seed.ToString().Length)
            };
        }

        public static WorldConfig createBlank() {
            return new WorldConfig{};
        }
        
    }

}