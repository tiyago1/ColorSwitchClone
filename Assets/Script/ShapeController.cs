using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColorSwitchGame;
using System.Linq;
using System;

public class ShapeController : ColorSwitch 
{
    #region Fields

    public List<ColorType> Colors;

    public DirectionType Direction;

    public SizeType Size;

    public ShapeType ShapeType;

    public List<Shape> Shapes;

    public event Action<Transform,DirectionType> CreatedSpapeController;

    public Point Point;

    private List<GameObject> Forms;

    public float RotateSpeed;

    #endregion // Fields

    #region Unity Methods

    public void Initialize()
    {
        Forms = new List<GameObject>();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Forms.Add(this.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < Forms[(int)ShapeType].transform.childCount; i++)
        {
            Forms[(int)ShapeType].transform.GetChild(i).GetComponent<Shape>().Initialize(GameManager);
            Forms[(int)ShapeType].transform.GetChild(i).GetComponent<Shape>().Initialize(this, Colors[i]);
            Shapes.Add(Forms[(int)ShapeType].transform.GetChild(i).GetComponent<Shape>());
        }

        if (CreatedSpapeController != null)
        {
            CreatedSpapeController(this.transform, Direction);
        }

        Point.Initialize(GameManager);
    }

    #endregion // Unity Methods

    #region Public Methods

    public void InitData(GameManager manager, ShapeType type, List<ColorType> colors, DirectionType direction, SizeType size)
    {
        base.Initialize(manager);
        ShapeType = type;
        Colors = colors;
        Direction = direction;
        Size = size;
        Initialize();

        switch (size)
        {
            case SizeType.Medium:
                this.transform.localScale = new Vector3(1.3f, 1.3f, 0.2f);
                break;
            case SizeType.Small:
                this.transform.localScale = new Vector3(1.0f, 1.0f, 0.0f);
                break;
            case SizeType.Big:
                this.transform.localScale = new Vector3(1.4f, 1.4f, 0.0f);
                break;
        }

        switch (type)
        {
            case ShapeType.Circle:
                this.transform.GetChild(0).gameObject.SetActive(true);
                this.transform.GetChild(1).gameObject.SetActive(false);
                break;
            case ShapeType.Rectangle:
                this.transform.GetChild(1).gameObject.SetActive(true);
                this.transform.GetChild(0).gameObject.SetActive(false);
                break;
            case ShapeType.Triangle:
                break;
            default:
                break;
        }

        GameManager.ColorChange += GameManager_ColorChange;
    }

    private void GameManager_ColorChange(ColorType type)
    {
        Shapes.ForEach(it => it.SetTrigger(false));
        Shapes.FirstOrDefault(it => it.Color == type).SetTrigger(true);
    }

    public void OnCollisionDetect()
    {
        GameManager.GameOver();
    }

    #endregion // Public Methods
    
    #region Private Nethods
    
    private void TestPrivateMethods()
    {
        // to stuff
    }

    #endregion // Private Methods
}