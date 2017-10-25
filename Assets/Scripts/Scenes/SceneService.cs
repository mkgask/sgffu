using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OwrBase.Exception;

namespace OwrBase.Scene {
    using OwrBase.SceneTransition;

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

            transition_scene_data = next_scene;
            this_behaviour.StartCoroutine(sceneLoader(next_scene.scene_name));
            //scenes.Add(scene_name);
        }

        public static void change(SceneTransition next_scene)
        {
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

            if (first_scene_name != current_scene) {
                this_behaviour.StartCoroutine(sceneUnLoader(current_scene));
            }

            transition_scene_data = next_scene;
            this_behaviour.StartCoroutine(sceneLoader(next_scene.scene_name));
        }



        /*
            Utilities
        */

        static IEnumerator<AsyncOperation> sceneLoader(string scene_name)
        {
            SceneManager.LoadScene(loading_scene_name, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(loading_scene_name));

            yield return SceneManager.LoadSceneAsync(scene_name, LoadSceneMode.Additive);
            keepOne(scene_name);
            scenes.Add(scene_name);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene_name));

            //Debug.Log("scenes.Add(scene_name): " + scene_name);
            SceneManager.UnloadSceneAsync(loading_scene_name);
        }

        static IEnumerator<AsyncOperation> sceneUnLoader(string scene_name)
        {
            yield return SceneManager.UnloadSceneAsync(scene_name);
            scenes.Remove(scene_name);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(scenes.Last()));
            //Debug.Log("scenes.Remove(scene_name): " + scene_name);
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