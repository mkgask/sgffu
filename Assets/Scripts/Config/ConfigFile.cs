using System.Collections.Generic;
using Utf8Json;
using OwrBase.Filesystem;

namespace OwrBase.Config {

    /// <summary>
    /// コンフィグファイル処理クラス
    /// </summary>
    public class ConfigFile
    {

        public static string defaultFilename = "/Configs/config.json";

        public const string terrainConfigFilename = "/Configs/terrain.json";

        public const string playerConfigFilename = "/Configs/player.json";

        public const string inputConfigFilename = "/Configs/input.json";

        private static Dictionary<string, File> configFileList = new Dictionary<string, File>();



        public static bool exist(string filename) {
            return new File().exist(filename);
        }

        private static File get_file<T>(string filename, T default_content)
        {
            if (!configFileList.ContainsKey(filename)) {
                string json_string = JsonSerializer.ToJsonString(default_content, JsonSerializer.DefaultResolver);
                configFileList.Add(filename, new File(filename, json_string));
            }

            return configFileList[filename];
        }



        /// <summary>
        /// コンフィグファイルにテキストを書き込む
        /// </summary>
        public static bool save<T>(string filename, T obj)
        {
            File config_file = ConfigFile.get_file(filename, "");
            string json_string = JsonSerializer.ToJsonString(obj, JsonSerializer.DefaultResolver);
            return config_file.write(json_string);
        }



        public static T load<T>(string filename, T default_content)
        {
            File config_file = ConfigFile.get_file(filename, default_content);
            byte[] json_bytes = config_file.readBytes();
            return JsonSerializer.Deserialize<T>(json_bytes);
        }

    }

}
