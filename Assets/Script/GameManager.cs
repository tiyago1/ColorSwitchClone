using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColorSwitchGame;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : ColorSwitch
{
    #region Constants

    public const float MAX_ROTATE_SPEED = 2.0f;
    public const float MIN_ROTATE_SPEED = 0.8f;

    #endregion

    #region Fields

    public event Action<ColorType> ColorChange;
    public event Action CaptureScore;
    public event Action CollisionDetected;

    public PlayerController Player;

    public GameObject CircleShapePrefab;
    public GameObject ColorSwitchPrefab;

    public Vector2 mInstantiatePosition;

    public List<ShapeController> Shapes;
    public List<ColorSwitcher> ColorSwitcher;

    //public List<Transform> ShapeTransform;
    public List<Transform> ColorSwitcherTransform;

    private List<ShapeController> mLeftRotateShapes;
    private List<ShapeController> mRightRotateShapes;

    private ColorType mPreviousColor;
    private ColorType mCurrentColor;

    public int Score;

    public int mLastTransformChangeCoin;

    public Text ScoreText;

    #endregion // Fields

    #region Unity Methods

    public void Start()
    {
        Initialize();
    }

    public void Update()
    {
        //if (Score != 0 && Score % 3 == 0)
        //{
        //    SetObjectTransform();
        //    SetObjectRotateSpeed();
        //    SetChangeShapeProperty();
        //}

        mRightRotateShapes = Shapes.Where(ite => ite.Direction == DirectionType.Right).ToList();
        mRightRotateShapes.ForEach(it => it.transform.Rotate(Vector3.forward * it.RotateSpeed));
        mLeftRotateShapes = Shapes.Where(ite => ite.Direction == DirectionType.Left).ToList();
        mLeftRotateShapes.ForEach(it => it.transform.Rotate(Vector3.forward * -1 * it.RotateSpeed));
    }

    #endregion // Unity Methods

    #region Public Methods

    private void SetObjectTransform()
    {
        if (Score != mLastTransformChangeCoin)
        {
            Shapes[0].transform.position = Shapes[6].transform.position + (Vector3.up * 5.0f);
            Shapes[1].transform.position = Shapes[0].transform.position + (Vector3.up * 5.0f);
            Shapes.ForEach(it => it.transform.GetChild(2).gameObject.SetActive(true));
            Shapes = Shapes.OrderBy(it => it.transform.position.y).ToList();

            ColorSwitcherTransform[0].position = ColorSwitcherTransform[6].position + (Vector3.up * 5.0f);
            ColorSwitcherTransform[1].position = ColorSwitcherTransform[0].position + (Vector3.up * 5.0f);
            ColorSwitcherTransform[0].gameObject.SetActive(true);
            ColorSwitcherTransform[1].gameObject.SetActive(true);
            ColorSwitcherTransform = ColorSwitcherTransform.OrderBy(it => it.position.y).ToList();
            mLastTransformChangeCoin = Score;
        }
    }

    private void SetObjectRotateSpeed()
    {
        Shapes[6].RotateSpeed = RandomRotationSpeed();
        Shapes[5].RotateSpeed = RandomRotationSpeed();
    }

    private void SetChangeShapeProperty()
    {
        Shapes[6].InitData(this, GetRandomShape(), GetAllColor(), GetRandomDirection(), (SizeType)Random.Range(0, (int)SizeType.MaxSizeTypeCount));
        Shapes[5].InitData(this, GetRandomShape(), GetAllColor(), GetRandomDirection(), (SizeType)Random.Range(0, (int)SizeType.MaxSizeTypeCount));
    }

    private ShapeType GetRandomShape()
    {
        return (ShapeType)Random.Range(0, (int)ShapeType.Triangle);
    }

    private float RandomRotationSpeed()
    {
        return Random.Range(MIN_ROTATE_SPEED, MAX_ROTATE_SPEED);
    }

    public void Initialize()
    {
        base.Initialize(this);
        CaptureScore += GameManager_CaptureScore;
        Player.Initialize();
        InitCircleShape();
        ColorSwitcher.ForEach(it => it.Initialize(this));

        for (int i = 0; i < Shapes.Count; i++)
        {
            Shapes[i].RotateSpeed = RandomRotationSpeed();

            if (i != 0)
            {
                Shapes[i].transform.position = Shapes[i - 1].transform.position + (Vector3.up * 5.0f);
            }
        }

        for (int i = 0; i < ColorSwitcherTransform.Count; i++)
        {
            if (i != 0)
            {
                ColorSwitcherTransform[i].position = ColorSwitcherTransform[i - 1].position + (Vector3.up * 5.0f);
            }
        }

        // StartCoroutine(CreaterCircleSphapes());
    }

    private void GameManager_CaptureScore()
    {
        if (Score != 0 && Score % 3 == 0)
        {
            SetObjectTransform();
            SetObjectRotateSpeed();
            SetChangeShapeProperty();
        }
    }

    private List<ColorType> GetAllColor()
    {
        List<ColorType> allColor = new List<ColorType>();

        for (int i = 0; i < (int)ColorType.MaxColorTypeCount; i++)
        {
            allColor.Add((ColorType)i);
        }

        return allColor;
    }

    private DirectionType GetRandomDirection()
    {
        return (DirectionType)Random.Range(0, 2);
    }

    public void InitCircleShape()
    {
        Shapes.ForEach(it => it.CreatedSpapeController += Shape_CreatedSpapeController);

        for (int i = 0; i < Shapes.Count; i++)
        {
            Shapes[i].InitData(this, GetRandomShape(), GetAllColor(), GetRandomDirection(), (SizeType)Random.Range(0, (int)SizeType.MaxSizeTypeCount));
        }


        //CreateColorSwitcher(mInstantiatePosition);
        //mInstantiatePosition = mInstantiatePosition + (Vector2.up * 4.4f);
    }

    private void Shape_CreatedSpapeController(Transform transform, DirectionType obj)
    {
        //switch (obj)
        //{
        //    case DirectionType.Left:
        //        mLeftRotateShapes.Add(transform);
        //        break;
        //    case DirectionType.Right:
        //        mRightRotateShapes.Add(transform);
        //        break;
        //    default:
        //        break;
        //}
        
    }

    public void CreateColorSwitcher(Vector3 position)
    {
        GameObject colorSwitcher = Instantiate(ColorSwitchPrefab, position - (Vector3.up * 2.2f ), Quaternion.identity) as GameObject;
        colorSwitcher.GetComponent<ColorSwitcher>().Initialize(this);
    }

    public void OnColorChange(ColorType color)
    {
        mPreviousColor = mCurrentColor;
        int colorRandom = -1;

        while (mPreviousColor == mCurrentColor)
        {
            colorRandom = Random.Range(0, (int)ColorType.MaxColorTypeCount);
            mCurrentColor = (ColorType)colorRandom;
        }

        if (ColorChange != null)
        {
            ColorChange((ColorType)colorRandom);
        }
    }

    public void OnCoinChange()
    {
        Score++;
        ScoreText.text = Score.ToString();
        if (CaptureScore != null)
        {
            CaptureScore();
        }

    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("Score", Score);

        if (Score > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore",Score);
        }

        SceneManager.LoadScene(1);
    }

    #endregion // Public Methods

    #region Private Nethods

    private void TestPrivateMethods()
    {
        // to stuff
    }

    #endregion // Private Methods
}