using UnityEngine;
using sgffu.Filesystem;
using StrOpe = StringOperationUtil.OptimizedStringOperation;

namespace sgffu.Utility
{
    public class Rand
    {
        public static uint xorshift(int base_numeric, float base_numeric_scale = 1f)
        {
            int y = Mathf.RoundToInt(base_numeric * base_numeric_scale) + 1234567890;
            y = y ^ (y << 13);
            y = y ^ (y >> 17);
            return (uint)(y ^ (y << 5));
        }

        public static uint xorshift(string base_string, float base_numeric_scale = 1f)
        {
            int sum = 0;

            foreach (var item in base_string.ToCharArray())
            {
                sum += (int)item;
            }

            return xorshift(sum, base_numeric_scale);
        }

        public static float calucurate_perlin_value(int pos, float terrain_seed, float scale)
        {
            return terrain_seed + (pos * scale);
        }

    }
}