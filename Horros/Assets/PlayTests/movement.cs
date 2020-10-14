using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEditor;
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
        [UnityTest]
        public IEnumerator moves_forward()
        {
            yield return Helpers.LoadMovementTestScene();
            var player = Helpers.GetPlayerController();

            var startingPosition = player.transform.position;
            PlayerInput.Instance = Substitute.For<IPlayerInput>();

            PlayerInput.Instance.Vertical.Returns(1f);
            yield return new WaitForSeconds(0.2f);

            var endingPosition = player.transform.position;
            Assert.Greater(endingPosition.z, startingPosition.z);
        }

        [UnityTest]
        public IEnumerator moves_backward()
        {
            yield return Helpers.LoadMovementTestScene();
            var player = Helpers.GetPlayerController();

            var startingPosition = player.transform.position;
            PlayerInput.Instance = Substitute.For<IPlayerInput>();

            PlayerInput.Instance.Vertical.Returns(-1f);
            yield return new WaitForSeconds(0.2f);

            var endingPosition = player.transform.position;
            Assert.Less(endingPosition.z, startingPosition.z);
        }
        
        
        [UnityTest]
        public IEnumerator moves_right()
        {
            yield return Helpers.LoadMovementTestScene();
            var player = Helpers.GetPlayerController();

            var startingPosition = player.transform.position;
            PlayerInput.Instance = Substitute.For<IPlayerInput>();
            
            PlayerInput.Instance.Horizontal.Returns(1f);
            yield return new WaitForSeconds(0.2f);

            var endingPosition = player.transform.position;
            Assert.Greater(endingPosition.x, startingPosition.x);
        }
        
        [UnityTest]
        public IEnumerator moves_left()
        {
            yield return Helpers.LoadMovementTestScene();
            var player = Helpers.GetPlayerController();

            var startingPosition = player.transform.position;
            PlayerInput.Instance = Substitute.For<IPlayerInput>();
            
            PlayerInput.Instance.Horizontal.Returns(-1f);
            yield return new WaitForSeconds(0.2f);

            var endingPosition = player.transform.position;
            Assert.Less(endingPosition.x, startingPosition.x);
        }
        
        [UnityTest]
        public IEnumerator rotates_right()
        {
            yield return Helpers.LoadMovementTestScene();
            var player = Helpers.GetPlayerController();

            var startingRotation = player.transform.rotation; 
            PlayerInput.Instance = Substitute.For<IPlayerInput>();
            
            PlayerInput.Instance.Horizontal.Returns(1f);
            yield return new WaitForSeconds(0.2f);

            var endingRotation = player.transform.rotation;
            Assert.Greater(endingRotation.y, startingRotation.y);
        }
        
        [UnityTest]
        public IEnumerator rotates_left()
        {
            yield return Helpers.LoadMovementTestScene();
            var player = Helpers.GetPlayerController();

            var startingRotation = player.transform.rotation; 
            PlayerInput.Instance = Substitute.For<IPlayerInput>();
            
            PlayerInput.Instance.Horizontal.Returns(-1f);
            yield return new WaitForSeconds(0.2f);

            var endingRotation = player.transform.rotation;
            Assert.Greater(endingRotation.y, startingRotation.y);
        }
    }
}