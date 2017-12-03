using System;
using System.Linq;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using sgffu.Exception;

namespace sgffu.Scene {
    using sgffu.EventMessage;

    public class SceneService
    {



        /*
            Properties
        */

        private static List<string> scenes = new List<string>();

        private static MonoBehaviour this_behaviour;

        private const string first_scene_name = SceneName.bootStrap;

        private const string loading_scene_name = SceneName.loading;

        static string[] keep_one_object_tags;

        static Camera main_camera;

        static Light directonal_light;

        public static SceneTransition transition_scene_data;

        public static int now_loading = 0;


        /*
            Foundation
        */

        public static void init(MonoBehaviour base_behaviour, string[] tags)
        {
            this_behaviour = base_behaviour;
            keep_one_object_tags = tags;

            scenes.Clear();
            scenes.Add(first_scene_name);
        }



        /*
            Modules
        */

        public static void load(SceneTransition next_scene)
        {
            if (-1 < scenes.BinarySearch(next_scene.scene_name)) {
                throw new SceneDoubleLoadException();
            }

            //loadLoadingScene();

            string[] load_scenes = new string[next_scene.additions.Length + 1];
            load_scenes[0] = next_scene.scene_name;
            transition_scene_data = next_scene;
            Debug.Log("SceneService.load: load_scenes.Length: " + load_scenes.Length);

            if (0 < next_scene.additions.Length) {
                int i = 1;
                foreach (string scene_name in next_scene.additions) {
                    load_scenes[i] = scene_name;
                    i += 1;
                }
            }

            foreach (string scene_name in load_scenes) {
                Debug.Log("SceneService.load: string scene_name in load_scenes: scene_name: " + scene_name);
                //MainThreadDispatcher.SendStartCoroutine(sceneLoader(scene_name));
                this_behaviour.StartCoroutine(sceneLoader(scene_name));
            }

            //unloadLoadingScene();

/*
            this_behaviour.StartCoroutine(sceneLoader(next_scene.scene_name));
            //scenes.Add(scene_name);

            if (0 < next_scene.additions.Length) {
                foreach (string scene_name in next_scene.additions) {
                    this_behaviour.StartCoroutine(sceneLoader(scene_name));
                }
            }
*/
        }

        public static void change(SceneTransition next_scene)
        {
            //Debug.Log("SceneService.change(next_scene): next_scene.scene_name : " + next_scene.scene_name);
            
            if (-1 < scenes.BinarySearch(next_scene.scene_name))
            {
                /*
                foreach(string s in scenes) {
                    Debug.Log("s: " + s);
                }
                */
                throw new SceneDoubleLoadException();
            }

            string current_scene = scenes.Last();

            if (current_scene == next_scene.scene_name) {
                throw new CurrentSceneDoubleLoadException();
            }

            //loadLoadingScene();

            if (first_scene_name != current_scene) {
                //MainThreadDispatcher.SendStartCoroutine(sceneUnloader(current_scene));
                this_behaviour.StartCoroutine(sceneUnloader(current_scene));
            }

            string[] load_scenes = new string[next_scene.additions.Length + 1];
            load_scenes[0] = next_scene.scene_name;
            transition_scene_data = next_scene;
            Debug.Log("SceneService.change: load_scenes.Length: " + load_scenes.Length);

            if (0 < next_scene.additions.Length) {
                int i = 1;
                foreach (string scene_name in next_scene.additions) {
                    load_scenes[i] = scene_name;
                    i += 1;
                }
            }

            foreach (string scene_name in load_scenes) {
                Debug.Log("SceneService.change: string scene_name in load_scenes: scene_name: " + scene_name);
                //MainThreadDispatcher.SendStartCoroutine(sceneLoader(scene_name));
                this_behaviour.StartCoroutine(sceneLoader(scene_name));
            }

            //unloadLoadingScene();

        }

        public static void loadLoadingScene()
        {
            if (-1 < scenes.BinarySearch(loading_scene_name)) {
                return;
            }
            this_behaviour.StartCoroutine(sceneLoader(loading_scene_name));
            //MainThreadDispatcher.SendStartCoroutine(sceneLoader(loading_scene_name));
            //scenes.Add(loading_scene_name);
            //SceneManager.LoadScene(loading_scene_name, LoadSceneMode.Additive);
        }

        public static void unloadLoadingScene()
        {
            if (-1 < scenes.BinarySearch(loading_scene_name)) {
            this_behaviour.StartCoroutine(sceneUnloader(loading_scene_name));
                //MainThreadDispatcher.SendStartCoroutine(sceneUnloader(loading_scene_name));
                //SceneManager.UnloadSceneAsync(loading_scene_name);
                //scenes.RemoveAt(scenes.FindIndex(x => x == loading_scene_name));
            }
        }

        public static void unload(SceneTransition unload_scene)
        {
            if (scenes.BinarySearch(unload_scene.scene_name) < 0) {
                Debug.Log("SceneService.unload: throw new SceneUnloadException() : " + unload_scene.scene_name);
                throw new SceneUnloadException();
            }

            string[] load_scenes = new string[unload_scene.additions.Length + 1];
            load_scenes[0] = unload_scene.scene_name;
            transition_scene_data = unload_scene;

            if (0 < unload_scene.additions.Length) {
                int i = 1;
                foreach (string scene_name in unload_scene.additions) {
                    load_scenes[i] = scene_name;
                    i += 1;
                }
            }

            foreach (string scene_name in load_scenes) {
                this_behaviour.StartCoroutine(sceneUnloader(scene_name));
                //sceneUnloader(scene_name);
            }

/*
            transition_scene_data = unload_scene;
            this_behaviour.StartCoroutine(sceneUnloader(unload_scene.scene_name));
            //scenes.Add(scene_name);

            if (0 < unload_scene.additions.Length) {
                foreach (var scene_name in unload_scene.additions) {
                    this_behaviour.StartCoroutine(sceneUnloader(scene_name));
                }
            }
*/
        }



        /*
            Utilities
        */

        public static IEnumerator<AsyncOperation> sceneLoader(string scene_name)
        {
            Debug.Log("SceneService.sceneLoader: " + scene_name);
            yield return SceneManager.LoadSceneAsync(scene_name, LoadSceneMode.Additive);
            scenes.Add(scene_name);
            keepOne(scene_name);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene_name));
        }

        public static IEnumerator<AsyncOperation> sceneUnloader(string scene_name)
        {
            Debug.Log("SceneService.sceneUnloader: " + scene_name);
            yield return SceneManager.UnloadSceneAsync(scene_name);
            scenes.RemoveAt(scenes.FindIndex(x => x == scene_name));
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(scenes.Last()));
        }

        public static void keepOne(string scene_name)
        {
            if (scene_name.Length < 1) { scene_name = first_scene_name; }
            foreach (string tag in keep_one_object_tags) {
                GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
                bool set_active = false;
                foreach (GameObject target in targets) {
                    target.SetActive(target.scene.name == scene_name);
                    if (!set_active) { set_active = target.scene.name == scene_name; }
                }
                if (!set_active && scene_name != first_scene_name) {
                    foreach (GameObject target in targets) {
                        target.SetActive(target.scene.name == first_scene_name);
                    }
                }
            }
        }

    }

}