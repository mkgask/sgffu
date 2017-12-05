using UnityEngine;
using sgffu.Utility;
using sgffu.Filesystem;
using UnityTerrain = UnityEngine.Terrain;
using StrOpe = StringOperationUtil.OptimizedStringOperation;

namespace sgffu.Terrain
{
    public class TerrainEntityCore
    {
        public int pos_x = int.MinValue;

        public int pos_z = int.MinValue;

        public float[,] height_map = new float[0,0];

        public float terrain_height = float.MinValue;
    }

    public class TerrainEntity : TerrainEntityCore
    {

        public GameObject game_object = null;

        public UnityTerrain terrain = null;

        public TerrainEntity(int x, int z, float[,] height_map, float terrain_height,
            GameObject game_object, UnityTerrain terrain
        ) {
            this.pos_x = x;
            this.pos_z = z;
            this.height_map = height_map;
            this.terrain_height = terrain_height;
            this.game_object = game_object;
            this.terrain = terrain;
        }

        public void enable()
        {
            this.game_object.SetActive(true);
        }

        public void disable()
        {
            this.game_object.SetActive(false);
        }

        public bool status()
        {
            return this.game_object.activeInHierarchy;
        }



        public void setNeighbors(TerrainEntity left, TerrainEntity top, TerrainEntity right, TerrainEntity bottom)
        {
            UnityTerrain left_terrain = null;
            UnityTerrain top_terrain = null;
            UnityTerrain right_terrain = null;
            UnityTerrain bottom_terrain = null;

            if(left != null) { left_terrain = left.terrain; }
            if(top != null) { top_terrain = top.terrain; }
            if(right != null) { right_terrain = right.terrain; }
            if(bottom != null) { bottom_terrain = bottom.terrain; }

            this.terrain.SetNeighbors(left_terrain, top_terrain, right_terrain, bottom_terrain);
        }

        public void setTexture(Texture2D texture, int chunk_size)
        {
            Debug.Assert(0 < chunk_size);

            TerrainData tData = this.terrain.terrainData;
            Vector3 tDataSize = tData.size;
            tData.alphamapResolution = chunk_size;

            SplatPrototype[] splatprototype = new SplatPrototype[1];
            splatprototype[0] = new SplatPrototype();
            splatprototype[0].texture = texture;
            splatprototype[0].tileSize = new Vector2(tDataSize.x, tDataSize.z);

            tData.splatPrototypes = splatprototype;

            int al_w = tData.alphamapWidth;
            int al_h = tData.alphamapHeight;
            float[,,] map = new float[al_w, al_h, 1];

            for (int x = 0; x < al_w; x += 1) {
                for (int z = 0; z < al_h; z += 1) {
                    map[x, z, 0] = 1f;
                }
            }

            tData.SetAlphamaps(0, 0, map);
            this.terrain.terrainData = tData;
        }

        public float getHeight(float offset_x, float offset_z)
        {
            Debug.Assert(offset_x < 1f);
            Debug.Assert(offset_z < 1f);
            
            TerrainData tData = terrain.terrainData;
            int max_w = tData.heightmapWidth;
            int max_h = tData.heightmapHeight;
            int x = Mathf.FloorToInt(max_w * offset_x);
            int z = Mathf.FloorToInt(max_h * offset_z);
            //Debug.Log("TerrainService.getHeight: this.height_map[x, z]: " + this.height_map[x, z]);
            //Debug.Log("TerrainService.getHeight: this.terrain_height: " + this.terrain_height);
            float r = this.height_map[x, z] * this.terrain_height;
            //Debug.Log("TerrainService.getHeight: r: " + r);
            return r;
        }

    }

}
