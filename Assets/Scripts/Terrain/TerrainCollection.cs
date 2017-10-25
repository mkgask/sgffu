using UnityEngine;
using OwrBase.Filesystem;
using StrOpe = StringOperationUtil.OptimizedStringOperation;

namespace OwrBase.Terrain {

    public class TerrainCollection
    {
        private GameObject game_object;
        private TerrainConfig param;
        private TerrainEntity[,] entities_base_layer;


        private int preview_left_top_x = 0;
        private int preview_left_top_z = 0;
        private int preview_right_bottom_x = 0;
        private int preview_right_bottom_z = 0;



        public TerrainCollection(GameObject game_object, TerrainConfig param)
        {
            this.game_object = game_object;
            this.param = param;
            int size = param.chunk_size + param.chunk_num_offset;
            this.entities_base_layer = new TerrainEntity[size, size];

            //Debug.Log("construct: x, z: " + size + ", " + size);
        }



        public TerrainEntity this[int x, int z]
        {
            set {
                int ax = x + (this.param.chunk_num_offset / 2);
                int az = z + (this.param.chunk_num_offset / 2);

                if (ax < 0 || entities_base_layer.GetLength(0) <= ax ||
                        az < 0 || entities_base_layer.GetLength(1) <= az) {
                    return;
                }
                
                entities_base_layer[ax, az] = value;
            }
            get {
                int ax = x + (this.param.chunk_num_offset / 2);
                int az = z + (this.param.chunk_num_offset / 2);

                if (ax < 0 || entities_base_layer.GetLength(0) <= ax ||
                        az < 0 || entities_base_layer.GetLength(1) <= az) {
                    return null;
                }
                
                return entities_base_layer[ax, az];
            }
        }



        public void update(int left_top_x, int left_top_z, int right_bottom_x, int right_bottom_z, Texture2D texture, uint seed)
        {
            //Log.write("TerrainCollections.update: funchead: preview: " + preview_left_top_x + " , " + preview_left_top_z + " : " + preview_right_bottom_x + " , " + preview_right_bottom_z);
            //Log.write("TerrainCollections.update: args: " + left_top_x + " , " + left_top_z + " : " + right_bottom_x + " , " + right_bottom_z);

            for(int x = left_top_x; x <= right_bottom_x; x += 1) {
                for (int z = left_top_z; z <= right_bottom_z; z += 1) {

                    // ベースレイヤーが存在していなかったら生成
                    if (this[x, z] != null) {
                        //Log.write("TerrainCollections.update: " + x + " , " + z + " : exist");
                    } else {
                        Debug.Log(StrOpe.i + "TerrainCollections.update: " + x + " , " + z + " : create");
                        Log.write(StrOpe.i + "TerrainCollections.update: " + x + " , " + z + " : create");
                        this[x, z] = new TerrainEntity(x, z, this.game_object, this.param, seed);

                        // SetNeibors
                        this[x, z].setNeighbors(
                            this[x - 1, z],
                            this[x, z + 1],
                            this[x + 1, z],
                            this[x, z - 1]
                        );

                        // SetTextures
                        this[x, z].setTexture(texture, this.param);
                    }

                    // 有効レイヤーでなかったら有効化
                    if (!this[x, z].status()) {
                        //Log.write("TerrainCollections.update: " + x + " , " + z + " : enable");
                        this[x, z].enable();
                    }

                }
            }

            for(int x = preview_left_top_x; x <= preview_right_bottom_x; x += 1) {
                for (int z = preview_left_top_z; z <= preview_right_bottom_z; z += 1) {
                    if (left_top_x <= x && x <= right_bottom_x &&
                            left_top_z <= z && z <= right_bottom_z) {
                        continue;
                    }
                    Debug.Log(StrOpe.i + "TerrainCollections.update: " + x + " , " + z + " : disble");
                    Log.write(StrOpe.i + "TerrainCollections.update: " + x + " , " + z + " : disable");
                    this[x, z].disable();
                }
            }

            preview_left_top_x = left_top_x;
            preview_left_top_z = left_top_z;
            preview_right_bottom_x = right_bottom_x;
            preview_right_bottom_z = right_bottom_z;
        }

        public float getHeight(float x, float z)
        {
            int pos_x = Mathf.FloorToInt(x);
            int pos_z = Mathf.FloorToInt(z);
            return this[pos_x, pos_z].getHeight(x - pos_x, z - pos_z);
        }

    }

}
