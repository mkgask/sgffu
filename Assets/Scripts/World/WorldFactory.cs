using UniRx;
using OwrBase.Terrain;
using OwrBase.EventMessage;
using OwrBase.Characters.Player;


namespace OwrBase.World
{

    class WorldFactory
    {

        public static void create()
        {
            TerrainFactory.create();
            MessageBroker.Default.Receive<TerrainCreated>().Subscribe(x => PlayerFactory.create());
        }
    }

}