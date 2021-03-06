﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitlescreenController : MonoBehaviour {
    public Button singleplayerButton;
    public Button multiplayerButton;
    public Button crebitosButton;
    public Button sairButton;
    public string singleplayerScene;
    public string multiplayerScene;
	public string tutorialScene;
    public GameObject gameObjectTitlescreen;
    public GameObject gameObjectCrebitos;
	public LevelInfo gameLevelInfo;
    public ScrollText crebitosScrollText;


    private MenuLocker titlescreenLocker;
    private MenuLocker crebitosLocker;

    // Use this for initialization
    void Start () {

		/*
		 * O sistema de Listener de cliques é igual ao de touch.
		 * Tentem entender o código atual, onde será executadas ações para cada evento de clique.
		 * Após a compreensão, devemos substituir nos outros arquivos.
		 * Devemos substituir os comandos dos arquivos TitleScreenPressButton.cs e HUDController.cs por enquanto.
		 * Se tudo der certo, devemos arrumar a RemakeController.cs
		 * RECOMENDO COMEÇAR PELA TitleScreenPressButton.cs pois é bem mais simples.
         * TitleScreenPressButton.cs -> Já foi implementado, devemos testar em um mobile.
         * TitlesceenController.cs -> Testar no mobile se as funções do menu inicial já funcionam no touch.
         * HUDController.cs -> Troca tudo hahahahaha.... =(
		 * RemakeController.cs -> Poucas coisas para substituir, mas só faremos isso depois do HUDController.cs
		*/


		singleplayerButton.onClick.AddListener(TaskOnClickSingleplayer);
        multiplayerButton.onClick.AddListener(TaskOnClickMultiplayer);
        crebitosButton.onClick.AddListener(TaskOnClickCrebitos);
        sairButton.onClick.AddListener(TaskOnClickSair);
        titlescreenLocker = GetComponent<MenuLocker>();
		crebitosLocker = gameObjectCrebitos.GetComponent<MenuLocker>();
		gameLevelInfo = GameObject.Find("gameLevelInfo").GetComponent<LevelInfo>() ;
    }


    void TaskOnClickSingleplayer ()
    {
        GlobalParameters.gameMode = 1;
        Debug.Log("Clicou no botao Singleplayer");
		gameLevelInfo.gameMode = LevelInfo.GAME_MODE.SINGLE;
		gameLevelInfo.lvl = 1;
		SceneManager.LoadScene(multiplayerScene);
    }

    void TaskOnClickMultiplayer()
    {
        GlobalParameters.gameMode = 2;
        Debug.Log("Clicou no botao Multiplayer");
		gameLevelInfo.gameMode = LevelInfo.GAME_MODE.MULTI;
		gameLevelInfo.lvl = 0;
        SceneManager.LoadScene(multiplayerScene);
    }

    void TaskOnClickCrebitos()
    {
        if (!titlescreenLocker.isLocked() && !crebitosLocker.isLocked()) {
            Debug.Log("Clicou no botao Crebitos");
            
            titlescreenLocker.lockMenu();
            StartCoroutine(routine: fadeOutGameObject(gameObjectTitlescreen));
            crebitosLocker.lockMenu();
            StartCoroutine(routine: fadeInGameObject(gameObjectCrebitos));

            crebitosScrollText.Scroll();
        }
    }
	public void TaskOnClickTutorial(){
		SceneManager.LoadScene(tutorialScene);
	}
    void TaskOnClickSair()
    {
        Debug.Log("Clicou no botao Sair");
        Application.Quit();
    }

    IEnumerator fadeOutGameObject(GameObject go)
    {
        // While Crebitos screen is on, titlescreen can not be used
        go.GetComponent<CanvasGroup>().interactable = false;

        CanvasGroup cg = go.GetComponent<CanvasGroup>();
        float time = 0.5f;
        while (cg.alpha > 0.0f)
        {
            cg.alpha -= Time.deltaTime / time;
            yield return null;
        }
        cg.alpha = 0.0f;
        titlescreenLocker.unlockMenu();
    }

    IEnumerator fadeInGameObject(GameObject go)
    {
        CanvasGroup cg = go.GetComponent<CanvasGroup>();
        float time = 0.5f;
        while (cg.alpha < 1.0f)
        {
            cg.alpha += Time.deltaTime / time;
            yield return null;
        }
        cg.alpha = 1.0f;
        crebitosLocker.unlockMenu();
    }
}
