using System.IO;
using UnityEngine;

namespace OwrBase.Filesystem {
    
    /// <summary>
    /// ログファイル操作クラス
    /// </summary>
    public class Log
    {

        public static string path = "";

        /// <summary>
        /// クラスを初期化する。具体的にはログファイルのパスを設定する。
        /// </summary>
        /// <returns>初期化の成否。失敗時にはディレクトリが生成できていない</returns>
        public static bool init()
        {
            path = Application.dataPath + "/Storage/logs/logs-" + System.DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".log";
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir)) {
                DirectoryInfo di = Directory.CreateDirectory(dir);
                if (!di.Exists) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ログファイルにテキストを書き込む
        /// </summary>
        public static bool write(string text)
        {
            if (path == "") {
                if (!init()) {
                    return false;
                }
            }
            string write_text = "[" + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "." + System.DateTime.Now.Millisecond + "] " + text + "\n";
            System.IO.File.AppendAllText(path, write_text);
            return true;
        }

        public static bool writeFloat2(float[,] data)
        {
            if (path == "") {
                if (!init()) {
                    return false;
                }
            }

            int x = 0;
            int y = 0;
            int xMax = data.GetLength(0);
            int yMax = data.GetLength(1);
            string write_text = "[" + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "." + System.DateTime.Now.Millisecond +  "] xMax : " + xMax + " : yMax : " + yMax + "\n";

            for (; x < xMax; x += 1) {
                for (; y < yMax; y += 1) {
                    write_text += data[x, y];
                    write_text += ", ";
                }
                write_text += "\n";
            }

            System.IO.File.AppendAllText(path, write_text);
            return true;
        }

    }
}
