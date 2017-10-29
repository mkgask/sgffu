using OwrBase.Terrain;
using OwrBase.Characters.Player;


namespace OwrBase.World
{

    class WorldFactory
    {

        public static void create()
        {
            TerrainFactory.create();
            PlayerFactory.create();
        }
    }

}