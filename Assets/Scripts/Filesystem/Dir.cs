using System.IO;

namespace OwrBase.Filesystem {

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
            if (Directory.Exists(path))
            {
                return new DirectoryInfo(path);
            }
            return Directory.CreateDirectory(path);
        }
    }

}
