using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Player
{
    public class Helpers
    {
        public static IEnumerator LoadMovementTestScene()
        {
            var operation = SceneManager.LoadSceneAsync("MovementTest");
            while (operation.isDone == false)
                yield return null;
        }

        public static PlayerMovementController GetPlayerController()
        {
            return GameObject.FindObjectOfType<PlayerMovementController>();
        }

        public static IEnumerator LoadUIScene()
        {
            var operation = SceneManager.LoadSceneAsync("UITest", LoadSceneMode.Additive);
            while (operation.isDone == false)
                yield return null;
        }
    }


    public class movement
    {
    }
}