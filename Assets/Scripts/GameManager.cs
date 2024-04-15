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
    public Transform player;
    public CameraMovement cameraMovement;
    public float checkPointSpawnedDistance = 10;
    private bool checkPointReached = false;
    private bool waveDefeated = false;
    private bool isSceneLoaded = false;
    private bool isDialogueFinished = false;

    #region WaveData
    public WaveData waveData;
    #endregion

    #region dialogue
    public DialogueScene dialogueOne;
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
        player = FindObjectOfType<PlayerController>()?.transform;
        //DebugUtils.HandleErrorIfNullGetComponent(player, this);
        cameraMovement = FindObjectOfType<CameraMovement>();
        //DebugUtils.HandleErrorIfNullGetComponent(cameraMovement, this);
    }
    #endregion

    #region CHeckpoint
    private void CreateNewCheckPoint(float distance)
    {
        Instantiate(checkPoint, player.position + (Vector3.right * distance), Quaternion.identity);
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


        yield return null;
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

        yield return null;
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
        yield return WaitForSceneToLoad();
        switch (SceneManager.GetActiveScene().name)
        {
            case "Main Menu":
                break;
            case "Level1":
                yield return FirstLevel();
                break;
            case "":
                break;
        }
    }

    private IEnumerator FirstLevel()
    {
        

        yield return CreateAndWaitForCheckPoint(checkPointSpawnedDistance);
        yield return FightSequence(waveData);
        //DialogueManager.Play(DIALOGUE_DATA)

        //While(DialogueManager.IsPlaying)
        {
            yield return null;
        }


        // SpawnManager.Spawn(WAVE_DATA)

        //While(SpawnManager.EnemiesBeingSpawnedOrAlive)
        {
            yield return null;
        }

        //DialogueManager.Play(BOSS_DIALOGUE_DATA)

        //While(DialogueManager.IsPlaying)
        {
            yield return null;
        }

        // SpawnManager.Spawn(WAVE_DATA)

        //While(DialogueManager.IsPlaying)
        {
            yield return null;
        }

    }
}
