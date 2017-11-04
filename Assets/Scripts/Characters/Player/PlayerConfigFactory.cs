using sgffu.Config;

namespace sgffu.Characters.Player {

    public class PlayerConfigFactory {

        public static PlayerConfig createDefault()
        {
            return new PlayerConfig {
                player_fbx_filepath = "Characters/UnityChan/UnityChan"
            };
        }

        public static PlayerConfig loadFile(PlayerConfig player_config_default)
        {
            return ConfigFile.load<PlayerConfig>(ConfigFile.playerConfigFilename, player_config_default);
        }
    }

}