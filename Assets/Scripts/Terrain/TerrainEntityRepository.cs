using UnityEngine;
using sgffu.World;
using sgffu.Config;

namespace sgffu.Terrain
{
    public class TerrainEntityRepository
    {
        const string file_path_base = "Worlds/{world_name}/Terrain/{terrain_name}.json";

        public static string file_path = "";

        private static TerrainEntityCore default_entity_core = new TerrainEntityCore();


        public static void reset(string world_name)
        {
            file_path = file_path_base.Replace("{world_name}", world_name);
        }

        public static TerrainEntity get(string terrain_name, TerrainConfig config, GameObject parent)
        {
            file_path = file_path.Replace("{terrain_name}", terrain_name);
            TerrainEntityCore core = ConfigFile.load<TerrainEntityCore>(file_path, default_entity_core);
            return TerrainEntityFactory.createFromCore(core, config, parent);
        }

        public static bool set(TerrainEntity entity)
        {
            file_path = file_path.Replace("{terrain_name}", entity.game_object.name);
            Debug.Log("TerrainEntityRepository.set: file_path: " + file_path);
            return ConfigFile.save<TerrainEntityCore>(file_path, (TerrainEntityCore)entity);
        }
    }
}