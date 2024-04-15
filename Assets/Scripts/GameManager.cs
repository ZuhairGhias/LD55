using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public GameObject checkPoint;
    public PlayerController player;
    public CameraMovement cameraMovement;
    public float checkPointSpawnedDistance = 10;
    private bool checkPointReached = false;
    private bool waveDefeated = false;
    private bool isSceneLoaded = false;
    private bool isDialogueFinished = false;
    private bool isGameStarted = false;

    [Header("Waves")]
    #region WaveData
    //Level 1
    public WaveData wave1Data;
    public WaveData wave2Data;
    public WaveData Boss1Data;
    //Level 2
    public WaveData wave3Data;
    public WaveData wave4Data;
    public WaveData Boss2Data;
    //Level 3
    public WaveData wave5Data;
    public WaveData wave6Data;
    public WaveData Boss3Data;
    #endregion

    [Header("Dialogues")]
    #region dialogue
    public DialogueScene dialogueOne;
    public DialogueScene dialogueBossPigeon;
    public DialogueScene dialogueBossPigeonDefeated;
    public DialogueScene dialogueBossMouse;
    public DialogueScene dialogueBossMouseDefeated;
    public DialogueScene dialogueBossMacaroon;
    public DialogueScene dialogueBossMacaroonDefeated;
    #endregion

    #region Monobehavior

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        PlayerCheckpoint.CheckpointReached += OnCheckpointReached;
        WaveSpawner.WaveDefeated += OnWaveDefeated;
        SceneManager.sceneLoaded += SceneLoaded;
        DialogueManager.DialogueFinished += OnDialogueFinished;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        DebugUtils.HandleErrorIfNullGetComponent(checkPoint, this);
        
        StartGameLoop();
    }

    private void OnDestroy()
    {
        PlayerCheckpoint.CheckpointReached -= OnCheckpointReached;
        WaveSpawner.WaveDefeated += OnWaveDefeated;
    }

    #endregion


    #region Scene
    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isSceneLoaded = true;
        GetPlayerandCamera();
    }

    private IEnumerator WaitForSceneToLoad()
    {
        while (!isSceneLoaded)
        {
            yield return null;
        }
    }

    private void GetPlayerandCamera()
    {
        player = FindObjectOfType<PlayerController>();
        //DebugUtils.HandleErrorIfNullGetComponent(player, this);
        cameraMovement = FindObjectOfType<CameraMovement>();
        //DebugUtils.HandleErrorIfNullGetComponent(cameraMovement, this);
    }
    #endregion

    #region CHeckpoint
    private void CreateNewCheckPoint(float distance)
    {
        Instantiate(checkPoint, player.transform.position + (Vector3.right * distance), Quaternion.identity);
        checkPointReached = false;

    }
    private IEnumerator WaitForCheckPoint()
    {
        while (!checkPointReached)
        {
            yield return null;
        }
    }

    private void OnCheckpointReached()
    {
        checkPointReached = true;
    }

    private IEnumerator CreateAndWaitForCheckPoint(float distance)
    {
        CreateNewCheckPoint(distance);
        yield return WaitForCheckPoint();
    }
    #endregion

    #region Fight
    private IEnumerator FightSequence(WaveData waveData)
    {
        cameraMovement.cameraState = CameraMovement.CameraStates.Stop;
        
        waveDefeated = false;
        WaveSpawner.StartWave(waveData, new(cameraMovement.transform.position.x, 0));
        yield return WaitForWaveToBeDefeated();

        cameraMovement.cameraState = CameraMovement.CameraStates.Active;
    }

    private void OnWaveDefeated()
    {
        waveDefeated = true;
    }

    private IEnumerator WaitForWaveToBeDefeated()
    {
        while (!waveDefeated)
        {
            yield return null;
        }
    }

    #endregion

    #region Dialogue
    private IEnumerator DialogueSequence(DialogueScene dialogue)
    {
        player.CurrentState = PlayerController.PlayerState.WAIT;
        isDialogueFinished = false;
        DialogueManager.PlayDialogue(dialogue);
        yield return WaitForDialogueToFinish();
        player.CurrentState = PlayerController.PlayerState.READY;
    }

    private IEnumerator WaitForDialogueToFinish()
    {
        while (!isDialogueFinished)
        {
            yield return null;
        }
    }

    public void OnDialogueFinished()
    {
        isDialogueFinished = true;
    }
    #endregion

    /// <summary>
    /// Called to start game statically
    /// </summary>
    public static void StartGame()
    {
        instance.StartGameLoop();
    }

    /// <summary>
    /// Singletin game loop
    /// </summary>
    private void StartGameLoop()
    {
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        while (true)
        {
            yield return WaitForSceneToLoad();
            switch (SceneManager.GetActiveScene().name)
            {
                case "Main Menu":
                    yield return WaitForGameStart();
                    SceneManager.LoadScene("Level1");
                    break;
                case "Level1":
                    yield return FirstLevel();
                    SceneManager.LoadScene("Level2");
                    break;
                case "Level2":
                    yield return SecondLevel();
                    SceneManager.LoadScene("Level3");
                    break;
                case "Level3":
                    yield return FinalLevel();
                    SceneManager.LoadScene("Credits");
                    break;
                case "Credits":
                    //yield return WaitForCreditsEnd();
                    SceneManager.LoadScene("Main Menu");
                    break;
            }
        }
    }

    private IEnumerator WaitForGameStart()
    {
        isGameStarted = false;
        while(!isGameStarted)
        {
            yield return null;
        }
    }

    private IEnumerator FirstLevel()
    {

        yield return DialogueSequence(dialogueOne);
        
        //First Checkpoint
        yield return CreateAndWaitForCheckPoint(checkPointSpawnedDistance);
        yield return FightSequence(wave1Data);


        //Second Checkpoint
        yield return CreateAndWaitForCheckPoint(checkPointSpawnedDistance);
        yield return FightSequence(wave1Data);

        //BossFight
        yield return CreateAndWaitForCheckPoint(checkPointSpawnedDistance);
        yield return DialogueSequence(dialogueBossPigeon);
        yield return FightSequence(wave1Data);

        yield return DialogueSequence(dialogueBossPigeonDefeated);
        yield return new WaitForSeconds(2);
    }

    private IEnumerator SecondLevel()
    {

        //First Checkpoint
        yield return CreateAndWaitForCheckPoint(checkPointSpawnedDistance);
        yield return FightSequence(wave1Data);


        //Second Checkpoint
        yield return CreateAndWaitForCheckPoint(checkPointSpawnedDistance);
        yield return FightSequence(wave1Data);

        //BossFight
        yield return CreateAndWaitForCheckPoint(checkPointSpawnedDistance);
        yield return DialogueSequence(dialogueBossMouse);
        yield return FightSequence(wave1Data);

        yield return DialogueSequence(dialogueBossMouseDefeated);
        yield return new WaitForSeconds(2);

    }

    private IEnumerator FinalLevel()
    {





        //Second Checkpoint
        yield return FightSequence(wave1Data);

        //BossFight
        yield return CreateAndWaitForCheckPoint(checkPointSpawnedDistance);
        yield return DialogueSequence(dialogueBossMacaroon);
        yield return FightSequence(wave1Data);

        yield return DialogueSequence(dialogueBossMacaroonDefeated);
        yield return new WaitForSeconds(2);

    }
}
