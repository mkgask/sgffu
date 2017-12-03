using System.IO;

namespace sgffu.Filesystem {

    /// <summary>
    /// ディレクトリ操作クラス
    /// </summary>
    public class Dir
    {
        /// <summary>
        /// 指定したパスにディレクトリが存在しない場合、作成する
        /// （すべてのディレクトリとサブディレクトリを作成する）
        /// </summary>
        public static DirectoryInfo create(string path)
        {
            string dir = Path.GetDirectoryName(path);

            if (Directory.Exists(dir))
            {
                return new DirectoryInfo(dir);
            }
            return Directory.CreateDirectory(dir);
        }
    }

}
