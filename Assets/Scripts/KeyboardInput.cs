using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.InputModes
{
    [RequireComponent(typeof(SnakeController))]
    public class KeyboardInput : MonoBehaviour
    {
        public string LeftArrow { get; private set; }
        public string RightArrow { get; private set; }

        private SnakeController controller;

        private void Awake()
        {
            controller = GetComponent<SnakeController>();
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(LeftArrow))
            {
                controller.SetDirection(1);
            }

            if (UnityEngine.Input.GetKeyDown(RightArrow))
            {
                controller.SetDirection(-1);
            }
        }

        public void SetArrows(string left, string right)
        {
            LeftArrow = left;
            RightArrow = right;
        }
    }
}
