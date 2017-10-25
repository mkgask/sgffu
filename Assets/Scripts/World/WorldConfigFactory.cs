namespace OwrBase.World {

    public class WorldConfigFactory {
        
        public static WorldConfig create(string world_name, uint seed) {
            return new WorldConfig {
                world_name = world_name,
                terrain_seed = seed
            };
        }

        public static WorldConfig createBlank() {
            return new WorldConfig{};
        }
        
    }

}