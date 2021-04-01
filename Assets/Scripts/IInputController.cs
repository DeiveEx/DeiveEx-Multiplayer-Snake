using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.InputModes
{
    public interface IInputController
    {
        void CopyInformation(IInputController baseInput);
    }
}