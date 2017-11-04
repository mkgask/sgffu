using System;
using UnityEngine;
using sgffu.Filesystem;
using sgffu.Exception;

namespace sgffu.Config {

    /// <summary>
    /// 設定情報処理クラス
    /// </summary>
    public class ConfigData
    {

        public static Texture2D instantiate_texture2D(string path, int w, int h)
        {
            byte[] texture_data = (new File(path, "")).readBytes();
            Texture2D texture = new Texture2D(w, h);

            if (!texture.LoadImage(texture_data)) {
                throw new Texture2DdontLoadException();
            }

            return texture;
        }

    }

}
