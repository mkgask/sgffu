using UnityEngine;
using OwrBase.Filesystem;
using StrOpe = StringOperationUtil.OptimizedStringOperation;

namespace OwrBase.Utility {
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



        public static float calucurate_perlin_value(int pos, uint seed, float scale)
        {
            var n = (seed / Mathf.Pow(10, seed.ToString().Length)) + (pos * scale);
            //Log.write(StrOpe.i + pos + " : " + n);
            return n;
            //return ((uint)(pos * 50) + seed) % 30000 * scale;
        }



    }
}