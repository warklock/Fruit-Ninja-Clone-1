using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Spawner mainSpawner;
    public SideSpawner sideSpawnerLeft;
    public SideSpawner sideSpawnerRight;
    public GameObject replay_pomegrenate;
    public GameObject replayCircle;
    public MouseSelection mouseSelection;

    public enum Mouse
    {
        Mouse0,
        Mouse1,
        Mouse2, 
        Mouse3, 
        Mouse4
    };
    public Mouse mouse = Mouse.Mouse1;

    [Header("Fruit List")]
    public List<Fruits> fruitsList = new List<Fruits>();
    public List<Fruits> initialFruitsList = new List<Fruits>();
    public Fruits bomb;
    public Fruits pomegranete;
    public Fruits specialBanana;
    public Fruits specialWaterMelon;

    [Header("COMBO")]
    public RectTransform combo_Display;
    public Image freezeScreen;
    public bool startComboCounting = false;
    public int comboCount;
    public float comboDuration;
    public Vector3 fruitEndOfComboPosition;
    public GameObject combo_Text;
    private Coroutine comboCourotine;
    Queue<GameObject> TextQueue = new Queue<GameObject>();

    [Header("UI")]

    public UIManager UIManager;
    public CanvasGroup PreGameUI;
    public CanvasGroup EndGameUI;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public float indicatorInitial_x;
    public float indicatorInitial_y;


    [Header("Game stats")]

    public int MaximumFruitPerTurn;
    public float speed;
    public float maxSpeed;
    public float acceleration;
    public int pome_turn_cooldown;
    public int pome_turn_cooldown_leap;
    public float spawnCooldown;
    public float spawnCooldownDeduction;
    public float bombSpawnChance;
    public float specialSpawnChance;
    public float specialSpawnChanceWatermelon;
    public bool endGameState = false;

    private float intitialSpeed;
    private float initialAcceleration;
    private float initialSpawnCooldown;
    private int initialPome_turn_cooldown;
    private int initialMaximumFruitPerTurn;

    [SerializeField] private int scoreMark;
    private int initialScoreMark;

    [Header("Player stats")]

    public float lives;
    public int score;
    public float timeLimitModeDuration;
    private float initialTimeLimitModeDuration;
    [SerializeField] private float frenzyDuration;
    [SerializeField] private float freezeDuration;
    [SerializeField] private float speedWhenFreeze;
    public float timeBonusMult;
    private Coroutine frenzyCourotine;
    private Coroutine freezeCourotine;
    public bool frezzing= false;

    [Header("Game Modes")]

    public int gameMode;

    [Header("Audio")]

    public AudioSource sfx;
    public AudioClip[] otherClip;

    private void Awake()
    {
        Create();

        switch (gameMode)
        {
            case 1:
                intitialSpeed = speed;
                initialPome_turn_cooldown = pome_turn_cooldown;
                initialSpawnCooldown = spawnCooldown;
                initialScoreMark = scoreMark;
                initialMaximumFruitPerTurn = MaximumFruitPerTurn;
                livesText.text = lives.ToString();
                break;

            case 2:
                intitialSpeed = speed;
                initialPome_turn_cooldown = pome_turn_cooldown;
                initialSpawnCooldown = spawnCooldown;
                initialTimeLimitModeDuration = timeLimitModeDuration;
                initialScoreMark = scoreMark;
                initialMaximumFruitPerTurn = MaximumFruitPerTurn;
                break;

            case 3:
                intitialSpeed = speed;
                initialPome_turn_cooldown = pome_turn_cooldown;
                initialSpawnCooldown = spawnCooldown;
                initialTimeLimitModeDuration = timeLimitModeDuration;
                initialScoreMark = scoreMark;
                initialMaximumFruitPerTurn = MaximumFruitPerTurn;
                break;
        }
    }

    private void FixedUpdate()
    {
        if(speed > maxSpeed) { maxSpeed = speed; }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        UIManager.canvasGroup[2].gameObject.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        UIManager.canvasGroup[2].gameObject.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        UIManager.canvasGroup[2].gameObject.SetActive(false);
        for (int i = 0; i < fruitsList.Count; i++)
        {
            if (fruitsList[i] != null)
                Destroy(fruitsList[i].gameObject);
        }
        fruitsList.Clear();
        freezeScreen.gameObject.SetActive(false);
        frezzing = false;
        Pomegranate.zooming = false;
        Pomegranate.startShake = false;
        ReloadScene();
    }

    void Create()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void ScoreIncrease()
    {
        score++;
        scoreText.text = score.ToString();

        switch (gameMode)
        {
            case 1:
                if (score - scoreMark >= 0)
                {
                    scoreMark += scoreMark;
                    MaximumFruitPerTurn++;
                }
                break;
            case 2:
                if (score - scoreMark >= 0)
                {
                    scoreMark += scoreMark;
                    MaximumFruitPerTurn++;
                }
                break;
            
            case 3:
                if (sideSpawnerLeft && sideSpawnerRight && endGameState == false && Pomegranate.zooming == false && score - scoreMark >= 0)
                {
                    scoreMark += scoreMark;
                    MaximumFruitPerTurn++;
                    sideSpawnerLeft.on = true;
                    sideSpawnerRight.on = true;
                    if (frenzyCourotine != null)
                    {
                        StopCoroutine(frenzyCourotine);
                    }
                    frenzyCourotine = StartCoroutine(EndFrenzyTime());
                }
                break;
        }
    }

    IEnumerator EndFrenzyTime()
    {
        yield return new WaitForSeconds(frenzyDuration);
        sideSpawnerLeft.on = false;
        sideSpawnerRight.on = false;
    }

    public void Freeze() 
    {
        frezzing = true;
        freezeScreen.gameObject.SetActive(true);
        if (freezeCourotine != null)
        {
            StopCoroutine(freezeCourotine);
        }
        freezeCourotine = StartCoroutine(EndFreezeTime());
    }

    IEnumerator EndFreezeTime()
    {
        speed = speedWhenFreeze;
        foreach( Fruits fruit in fruitsList )
        {
            fruit.speed = speedWhenFreeze;
        }
        freezeScreen.DOFade(1, 1.5f);
        yield return new WaitForSeconds(freezeDuration);
        freezeScreen.DOFade(0,1.5f);
        freezeScreen.gameObject.SetActive(false);
        foreach (Fruits fruit in fruitsList)
        {
            fruit.speed = maxSpeed;
        }
        speed = maxSpeed;
        frezzing = false;
    }

    public void TimeIncrease()
    {
        timeLimitModeDuration += timeBonusMult;
    }


    public void PomegraneteSpawn()
    {
        mainSpawner.pome_Cooldown--;
        if (mainSpawner.pome_Cooldown == 0)
        {
            mainSpawner.spawnPome();
            pome_turn_cooldown_leap += pome_turn_cooldown_leap * 2;
            pome_turn_cooldown += pome_turn_cooldown_leap;
            mainSpawner.pome_Cooldown = pome_turn_cooldown;
        }
    }

    public void LivesDescrease()
    {
        lives--;
        livesText.text = lives.ToString();
    }

    public void ComboCount()
    {
        if (comboCourotine != null)
        {
            StopCoroutine(comboCourotine);
        }
        comboCourotine = StartCoroutine(ComboEnd());
    }

    IEnumerator ComboEnd()
    {
        yield return new WaitForSeconds(comboDuration);
        startComboCounting = false;
        if (comboCount >= 3 && startComboCounting == false)
        {
            TextDisPlay(comboCount.ToString() + " hits" + Environment.NewLine + "+" + comboCount.ToString() + " points", fruitEndOfComboPosition);
        }
        score += comboCount - 1;
        ScoreIncrease();
        comboCount = 0;
    }

    public void TextDisPlay(string textToDisplay, Vector3 position)
    {
        GameObject oldText = null;
        if (TextQueue.Count != 0)
        {
            Destroy(TextQueue.Dequeue());
        }
        Vector3 newPos = Camera.main.WorldToViewportPoint(position);
        newPos = new Vector3(combo_Display.rect.width * newPos.x - (combo_Display.rect.width / 2),
            combo_Display.rect.height * newPos.y - (combo_Display.rect.height / 2), 0);
        oldText = Instantiate(combo_Text, combo_Display);
        TextQueue.Enqueue(oldText);
        TextMeshProUGUI text = oldText.GetComponent<TextMeshProUGUI>();
        text.SetText(textToDisplay);
        text.rectTransform.anchoredPosition = newPos + new Vector3(0f, 100f, 0f);
    }

    public void EndGameMethod()
    {
        StartCoroutine(EndGameCoroutine());
    }

    IEnumerator EndGameCoroutine()
    {
        endGameState = true;
        yield return new WaitUntil(() => Pomegranate.zooming == false);
        for(int i =0; i < fruitsList.Count; i++)
        {
            if (fruitsList[i] != null)
                Destroy(fruitsList[i].gameObject);
        }
        fruitsList.Clear();
        yield return new WaitForSeconds(.5f);
        EndGameUI.gameObject.SetActive(true);
        UIManager.PanelFadeIn(1);
        yield return new WaitForSeconds(5f);
        replayCircle.gameObject.SetActive(true);
        replay_pomegrenate.gameObject.SetActive(true);
    }

    public void RePlayButton()
    {
        StartCoroutine(ReplayCoroutine());
    }

    IEnumerator ReplayCoroutine()
    {
        yield return new WaitForSeconds(1f);
        EndGameUI.gameObject.SetActive(false);
    }

    public void GameProcess()
    {
        switch (gameMode)
        {
            case 1:
                LivesDescrease();
                if (lives <= 0)
                {
                    EndGameMethod();
                }
                break;

            case 2:
                timeLimitModeDuration -= 1f;
                break; 

            case 3:
                timeLimitModeDuration -= 1f;
                break;

        }
    }
    public void ReloadScene()
    {
        switch (gameMode)
        {
            case 1:
                speed = intitialSpeed;
                pome_turn_cooldown = initialPome_turn_cooldown;
                spawnCooldown = initialSpawnCooldown;
                scoreMark = initialScoreMark;
                MaximumFruitPerTurn = initialMaximumFruitPerTurn;

                score = 0;
                scoreText.text = score.ToString();
                lives = 7;
                livesText.text = lives.ToString();
                mainSpawner.gameObject.SetActive(true);
                endGameState = false;
                break;

            case 2:
                speed = intitialSpeed;
                pome_turn_cooldown = initialPome_turn_cooldown;
                spawnCooldown = initialSpawnCooldown;
                scoreMark = initialScoreMark;
                MaximumFruitPerTurn = initialMaximumFruitPerTurn;
                

                score = 0;
                scoreText.text = score.ToString();

                timeLimitModeDuration = initialTimeLimitModeDuration;
                mainSpawner.gameObject.SetActive(true);
                endGameState = false;
                break;
            case 3:
                speed = intitialSpeed;
                pome_turn_cooldown = initialPome_turn_cooldown;
                spawnCooldown = initialSpawnCooldown;
                scoreMark = initialScoreMark;
                MaximumFruitPerTurn = initialMaximumFruitPerTurn;

                score = 0;
                scoreText.text = score.ToString();

                timeLimitModeDuration = initialTimeLimitModeDuration;
                mainSpawner.gameObject.SetActive(true);
                sideSpawnerLeft.on = false;
                sideSpawnerRight.on = false;
                freezeScreen.gameObject.SetActive(false);
                frezzing = false;
                endGameState = false;
                break;
        }
    }

    public void SceneLoader(int i)
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(i);
        Application.backgroundLoadingPriority = ThreadPriority.Low;
    }
}