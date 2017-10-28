using UnityEngine;
using OwrBase.Utility;
using OwrBase.Filesystem;
using UnityTerrain = UnityEngine.Terrain;
using StrOpe = StringOperationUtil.OptimizedStringOperation;

namespace OwrBase.Terrain {

    public class TerrainEntity {

        public bool enabled = false;

        private GameObject game_object = null;

        private UnityTerrain terrain = null;

        private int position_x;

        private int position_z;

        private float[,] height_map;

        private float terrain_height;


        public TerrainEntity(int x, int z, GameObject parent, TerrainConfig param, uint seed)
        {
            this.position_x = x;
            this.position_z = z;
            this.game_object = this.create_game_object(x, z, parent);

            int chunk_size = param.chunk_size;
            this.terrain_height = param.terrain_height;

            TerrainData tData = new TerrainData();
            float actual_chunk_size = chunk_size / (Mathf.Max(chunk_size / 64, 0.5f) * 2);

            //Debug.Log("chunk_size: " + chunk_size);
            //Debug.Log("actual_chunk_size: " + actual_chunk_size);

            tData.size = new Vector3(actual_chunk_size, this.terrain_height, actual_chunk_size);
            tData.heightmapResolution = chunk_size;
            tData.baseMapResolution = param.base_map_resolution;
            tData.SetDetailResolution(param.detail_resolution, param.resolution_per_path);

            float[,] heights = tData.GetHeights(0, 0, tData.heightmapWidth, tData.heightmapHeight);
            heights = this.fillHeights(
                x * chunk_size,
                z * chunk_size,
                tData.heightmapWidth,
                tData.heightmapHeight,
                param.perlin_noise_scale,
                seed
            );

            this.height_map = heights;
            tData.SetHeights(0, 0, heights);

            TerrainCollider tCollider = game_object.GetComponent<TerrainCollider>();
            tCollider.terrainData = tData;
            terrain.terrainData = tData;

            this.game_object.transform.position = new Vector3(x * chunk_size, 0f, z * chunk_size);

/*
            terrain.terrainData.wavingGrassAmount = 5f;
            terrain.terrainData.wavingGrassSpeed = 5f;
            terrain.terrainData.wavingGrassStrength = 5f;
            terrain.terrainData.wavingGrassTint = new Color(0.5f, 1.0f, 0.5f, 0.5f);
*/
        }



        public GameObject create_game_object(int x, int z, GameObject parent)
        {
            if (this.game_object) { return this.game_object; }

            GameObject go = new GameObject("Terrain-" + x + "-" + z);
            go.AddComponent<UnityTerrain>();
            go.AddComponent<TerrainCollider>();
            terrain = go.GetComponent<UnityTerrain>();
            go.transform.SetParent(parent.transform);
            return go;
        }



        public float[,] fillHeights(int base_x, int base_z, int height_map_width, int height_map_height, float perlin_noise_scale, uint seed)
        {
            int x = 0;
            int z = 0;

            float [,] heights = new float[height_map_width, height_map_height];

            for (x = 0; x < height_map_width; x += 1) {
                for (z = 0; z < height_map_height; z += 1) {
                    //float xx = Rand.calucurate_perlin_value((base_x + x), seed, perlin_noise_scale);
                    //float zz = Rand.calucurate_perlin_value((base_z + z), seed, perlin_noise_scale);
                    heights[z, x] = Mathf.PerlinNoise(
                        Rand.calucurate_perlin_value((base_x + x), seed, perlin_noise_scale),
                        Rand.calucurate_perlin_value((base_z + z), seed, perlin_noise_scale)
                        //xx, zz
                    );
                    //Log.write("heights: " + x + ", " + z + " : " + heights[z, x]);

                }
            }

            //Log.writeFloat2(heights);
            return heights;
        }



        public void enable()
        {
            this.game_object.SetActive(true);
            this.enabled = true;
        }

        public void disable()
        {
            this.game_object.SetActive(false);
            this.enabled = false;
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

        public void setTexture(Texture2D texture, TerrainConfig param)
        {
            TerrainData tData = this.terrain.terrainData;
            Vector3 tDataSize = tData.size;
            tData.alphamapResolution = param.chunk_size;

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
            TerrainData tData = terrain.terrainData;
            int max_w = tData.heightmapWidth;
            int max_h = tData.heightmapHeight;
            int x = Mathf.FloorToInt(max_w * offset_x);
            int z = Mathf.FloorToInt(max_h * offset_z);
/*
            Debug.Log("TerrainEntity.get_height: offset_x: " + offset_x);
            Debug.Log("TerrainEntity.get_height: offset_z: " + offset_z);
            Debug.Log("TerrainEntity.get_height: x: " + x);
            Debug.Log("TerrainEntity.get_height: z: " + z);
            Debug.Log("TerrainEntity.get_height: this.height_map[x, z]: " + this.height_map[x, z]);
*/
            return this.height_map[x, z] * this.terrain_height;
        }

    }

}
