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
    public WaveData waveData;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        DebugUtils.HandleErrorIfNullGetComponent(checkPoint, this);
        PlayerCheckpoint.CheckpointReached += OnCheckpointReached;
        WaveSpawner.WaveDefeated += OnWaveDefeated;
        SceneManager.sceneLoaded += SceneLoaded;
        StartGameLoop();
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isSceneLoaded = true;
        GetPlayerandCamera();
    }

    private void OnDestroy()
    {
        PlayerCheckpoint.CheckpointReached -= OnCheckpointReached;
        WaveSpawner.WaveDefeated += OnWaveDefeated;
    }

    private void GetPlayerandCamera()
    {
        player = FindObjectOfType<PlayerController>().transform;
        DebugUtils.HandleErrorIfNullGetComponent(player, this);
        cameraMovement = FindObjectOfType<CameraMovement>();
        DebugUtils.HandleErrorIfNullGetComponent(cameraMovement, this);
    }

    private void OnCheckpointReached()
    {
        checkPointReached = true;
    }

    private void OnWaveDefeated()
    {
        waveDefeated = true;
    }

    private IEnumerator WaitForCheckPoint()
    {
        while(!checkPointReached)
        {
            yield return null;
        }
    }

    private IEnumerator WaitForSceneToLoad()
    {
        while (!isSceneLoaded)
        {
            yield return null;
        }
    }

    private void CreateNewCheckPoint(float distance)
    {
        Instantiate(checkPoint, player.position + (Vector3.right * distance), Quaternion.identity);
        checkPointReached = false;

    }

    private IEnumerator CreateAndWaitForCheckPoint(float distance)
    {
        CreateNewCheckPoint(distance);
        yield return WaitForCheckPoint();
    }

    private IEnumerator FightSequence(WaveData waveData)
    {
        cameraMovement.cameraState = CameraMovement.CameraStates.Stop;

        WaveSpawner.StartWave(waveData, new(cameraMovement.transform.position.x, cameraMovement.transform.position.y));
        waveDefeated = false;
        yield return WaitForWaveToBeDefeated();

        cameraMovement.cameraState = CameraMovement.CameraStates.Active;


        yield return null;
    }

    private IEnumerator WaitForWaveToBeDefeated()
    {
        while (!waveDefeated)
        {
            yield return null;
        }
    }

    public static void StartGame()
    {
        instance.StartGameLoop();
    }

    private void StartGameLoop()
    {
        StartCoroutine(GameLoop());
        SceneManager.LoadScene(0);
        isSceneLoaded = false;
    }

    private IEnumerator GameLoop()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                yield return FirstLevel();
                break;
            case 1:
                break;
            case 2:
                break;
        }
        Debug.Log("You Won!");
    }

    private IEnumerator FirstLevel()
    {
        
        yield return WaitForSceneToLoad();

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
