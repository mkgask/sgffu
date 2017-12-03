using UniRx;
using sgffu.Scene;
using sgffu.Terrain;
using sgffu.EventMessage;
using sgffu.Characters.Player;


namespace sgffu.World
{

    class WorldFactory
    {

        public static void create()
        {
            MessageBroker.Default.Receive<TerrainCreated>().Subscribe(x => {
                //MessageBroker.Default.Publish<sceneUnload>(new sceneUnload { scene_name = SceneName.worldCreating });
                PlayerFactory.create();
            });
            TerrainFactory.create();
        }
    }

}