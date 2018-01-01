using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour 
{
    #region Fields

    public Text Score;
    public Text BestScore;

    #endregion // Fields

    #region Unity Methods

    public void Start()
    {
        Initialize();
    }

    public void Update()
    {

    }

    #endregion // Unity Methods
    
    #region Public Methods
    
    public void Initialize()
    {
        Score.text = PlayerPrefs.GetInt("Score").ToString();
        BestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
    }

    public void OnRetryButtonClicked()
    {
        SceneManager.LoadScene(0);


    }

    //public void OnResulationWidthPlusButtonClicked()
    //{
    //    ResulationSetting setting = GameObject.FindObjectOfType<ResulationSetting>();
    //    setting.ResulationWidth += 10;
    //    Score.text = setting.ResulationWidth + ":" + setting.ResulationHeight;
    //}

    //public void OnResulationWidthtDecresButtonClicked()
    //{
    //    ResulationSetting setting = GameObject.FindObjectOfType<ResulationSetting>();
    //    setting.ResulationWidth -= 10;
    //    Score.text = setting.ResulationWidth + ":" + setting.ResulationHeight;
    //}

    //public void OnResulationHeightPlusButtonClicked()
    //{
    //    ResulationSetting setting = GameObject.FindObjectOfType<ResulationSetting>();
    //    setting.ResulationHeight += 10;
    //    Score.text = setting.ResulationWidth + ":" + setting.ResulationHeight;
    //}

    //public void OnResulationHeightDecressButtonClicked()
    //{
    //    ResulationSetting setting = GameObject.FindObjectOfType<ResulationSetting>();
    //    setting.ResulationHeight -= 10;
    //    Score.text = setting.ResulationWidth + ":" + setting.ResulationHeight;
    //}

    #endregion // Public Methods

    #region Private Nethods

    private void TestPrivateMethods()
    {
        // to stuff
    }

    #endregion // Private Methods
}