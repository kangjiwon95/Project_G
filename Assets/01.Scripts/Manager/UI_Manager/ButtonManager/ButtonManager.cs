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
        //���� ȭ�� Ŭ��
        tradingButton.onClick.AddListener(TradingOnClick);

        // �غ�ȭ�� Ŭ��
        readyButton.onClick.AddListener(StartOnClick);

        // ���� �� ��ȯ Ŭ��
        gameStartButton.onClick.AddListener(GameStartOnClick);

        // ĳ���� ȭ�� Ŭ��
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

    #region Readyȭ�� ���԰� ���
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

    #region Character ���԰� ���
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

    #region Trading ���԰� ���
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

    #region HideOut ���԰� ���
    private void HideOutOnClick()
    {

    }
    private void HideOutExitClick()
    {

    }
    #endregion

    #region GameScene ����
    private void GameStartOnClick()
    {
        SceneManager.LoadScene("GameScene");
        mainCanvers.SetActive(true);
        gameStartCanvers.SetActive(false);
    }
    #endregion

    #region OptionPanel ����
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
