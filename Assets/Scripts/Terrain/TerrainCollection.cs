using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using sgffu.Filesystem;
using StrOpe = StringOperationUtil.OptimizedStringOperation;

namespace sgffu.Terrain {

    public class TerrainCollection
    {
        public Dictionary<long, Dictionary<long, TerrainEntity>> entities = new Dictionary<long, Dictionary<long, TerrainEntity>>();

        //public int[] test = new int[long.MaxValue];

        public int terrain_chunk_size = 0;

        public int terrain_chunk_offset = 0;

        public int terrain_pos_start = 0;

        public int terrain_pos_end = 0;
        
        public TerrainEntity this[long x, long z]
        {
            set {
                if (!entities.ContainsKey(x)) {
                    entities.Add(x, new Dictionary<long, TerrainEntity>());
                }
                if (!entities[x].ContainsKey(z)) {
                    entities[x].Add(z, value);
                    return;
                }
                entities[x][z] = value;
            }
            get {
                try {
                    return entities[x][z];
                } catch(KeyNotFoundException e) {
                    return null;
                }
            }
        }

    }

}
