using UniRx;
using sgffu.Terrain;
using sgffu.EventMessage;
using sgffu.Characters.Player;


namespace sgffu.World
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