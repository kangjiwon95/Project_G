using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [Header("Canvers")]
    public GameObject mainCanvers;
    public GameObject gameStartCanvers;
    public GameObject charCanvers;
    public GameObject tradingCanvers;
    public GameObject hideOutCanvers;

    [Space (20)]
    [Header("Button")]
    public Button readyButton;
    public Button charButton;
    public Button tradingButton;
    public Button hiedoutButton;
    public Button gameStartButton;
    public Button optionButton;

    [Space(20)]
    [Header("Canvers")]
    public GameObject optionPanel;

    private void Start()
    {
        //상점 화면 클릭
        tradingButton.onClick.AddListener(TradingOnClick);

        // 준비화면 클릭
        readyButton.onClick.AddListener(StartOnClick);

        // 게임 씬 전환 클릭
        gameStartButton.onClick.AddListener(GameStartOnClick);

        // 캐릭터 화면 클릭
        charButton.onClick.AddListener(CharacterOnClick);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if(tradingCanvers.activeSelf)
            {
                TradingExitClick();
            }
            else if(gameStartCanvers.activeSelf)
            {
                StartExitClick();
            }
            else if(charCanvers.activeSelf)
            {
                CharacterExitClick();
            }
            else if(optionPanel.activeSelf)
            {
                OptionpanelExitClick();
            }
        }
    }

    #region Ready화면 진입과 취소
    private void StartOnClick()
    {
        gameStartCanvers.SetActive(true);
        mainCanvers.SetActive(false);
    }
    private void StartExitClick()
    {
        gameStartCanvers.SetActive(false);
        mainCanvers.SetActive(true);
    }
    #endregion

    #region Character 진입과 취소
    private void CharacterOnClick()
    {
        charCanvers.SetActive(true);
        mainCanvers.SetActive(false);
    }
    private void CharacterExitClick()
    {
        charCanvers.SetActive(false);
        mainCanvers.SetActive(true);
    }
    #endregion

    #region Trading 진입과 취소
    private void TradingOnClick()
    {
        tradingCanvers.SetActive(true);
        mainCanvers.SetActive(false);
    }
    private void TradingExitClick()
    {
        tradingCanvers.SetActive(false);
        mainCanvers.SetActive(true);
    }
    #endregion

    #region HideOut 진입과 취소
    private void HideOutOnClick()
    {

    }
    private void HideOutExitClick()
    {

    }
    #endregion

    #region GameScene 진입
    private void GameStartOnClick()
    {
        SceneManager.LoadScene("GameScene");
        mainCanvers.SetActive(true);
        gameStartCanvers.SetActive(false);
    }
    #endregion

    #region OptionPanel 진입
    private void OptionpanelOnClick()
    {
        optionPanel.SetActive(true);
    }
    private void OptionpanelExitClick()
    {
        optionPanel.SetActive(false);
    }
    #endregion
}
