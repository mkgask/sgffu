using UnityEngine;
using System;
using System.Linq;
using System.Collections;
//using System.Collections.Generic;
using OwrBase.Filesystem;
using StrOpe = StringOperationUtil.OptimizedStringOperation;

namespace OwrBase.Terrain {

    public class TerrainCollection
    {
        private GameObject game_object;
        private TerrainConfig param;

        private TerrainEntity[,] entities;

        //private List<TerrainEntity> entities = new List<TerrainEntity>();

        //private const int terrain_chunk_offset = Int32.MaxValue / 2;

        private const int terrain_chunk_max = 6180;

        private const int terrain_chunk_offset = terrain_chunk_max / 2;

        private int preview_left_top_x = 0;

        private int preview_left_top_z = 0;

        private int preview_right_bottom_x = 0;

        private int preview_right_bottom_z = 0;



        public TerrainCollection(GameObject game_object, TerrainConfig param)
        {
            this.game_object = game_object;
            this.param = param;
            this.entities = new TerrainEntity[terrain_chunk_max,terrain_chunk_max];
        }



        public TerrainEntity this[int x, int z]
        {
            set {
                int ax = x + terrain_chunk_offset;
                int az = z + terrain_chunk_offset;

                if (ax < 0 || terrain_chunk_max < ax ||
                        az < 0 || terrain_chunk_max < az) {
                    return;
                }

                entities[ax, az] = value;
/*
                int index = entities.FindIndex(entity => entity.position_x == x && entity.position_z == z);
                
                if (-1 < index) {
                    entities[index] = value;
                } else {
                    entities.Add(value);
                }
*/
            }
            get {
                int ax = x + terrain_chunk_offset;
                int az = z + terrain_chunk_offset;

                if (ax < 0 || terrain_chunk_max < ax ||
                        az < 0 || terrain_chunk_max < az) {
                    return null;
                }

                return entities[ax, az];
                //return entities.FirstOrDefault(entity => entity.position_x == x && entity.position_z == z);
            }
        }



        public IEnumerator update(int left_top_x, int left_top_z, int right_bottom_x, int right_bottom_z, Texture2D texture, float terrain_seed)
        {
            for(int x = left_top_x; x <= right_bottom_x; x += 1) {
                for (int z = left_top_z; z <= right_bottom_z; z += 1) {

                    yield return new WaitForSeconds(0);

                    // ベースレイヤーが存在していなかったら生成
                    if (this[x, z] == null) {
                        this[x, z] = new TerrainEntity(x, z, this.game_object, this.param, terrain_seed);

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
                    //Debug.Log(StrOpe.i + "TerrainCollections.update: " + x + " , " + z + " : disble");
                    //Log.write(StrOpe.i + "TerrainCollections.update: " + x + " , " + z + " : disable");
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
