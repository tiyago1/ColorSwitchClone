using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorSwitchGame
{
    public enum ColorType : int
    {
        Yellow,
        Purple, 
        Pink,   
        Cyan,

        MaxColorTypeCount
    }

    public enum DirectionType
    {
        Left,
        Right
    }

    public enum SizeType
    {
        Medium,
        Small,
        Big,

        MaxSizeTypeCount
    }

    public enum ShapeType
    {
        Circle,
        Rectangle,
        Triangle
    }

    public class ColorSwitch : MonoBehaviour
    {
        #region Fields

        public GameManager GameManager;

        #endregion // Fields

        #region Public Methods

        public void Initialize(GameManager gameManager)
        {
            GameManager = gameManager;
        }

        #endregion // Public Methods
    }
}