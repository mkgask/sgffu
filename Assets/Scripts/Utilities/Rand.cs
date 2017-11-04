using UnityEngine;
using sgffu.Filesystem;
using StrOpe = StringOperationUtil.OptimizedStringOperation;

namespace sgffu.Utility {
    public class Rand {

        public static uint xorshift(int base_numeric)
        {
            int y = base_numeric + 1234567890;
            y = y ^ (y << 13);
            y = y ^ (y >> 17);
            return (uint)(y ^ (y << 5));
        }

        public static uint xorshift(string base_string)
        {
            int sum = 0;

            foreach (var item in base_string.ToCharArray())
            {
                sum += (int)item;
                //Debug.Log("item: " + item);
                //Debug.Log("sum: " + sum);
            }

            return xorshift(sum);

        }



        public static float calucurate_perlin_value(int pos, float terrain_seed, float scale)
        {
            return terrain_seed + (pos * scale);
        }



    }
}