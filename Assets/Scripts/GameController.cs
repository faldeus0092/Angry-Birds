using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public SlingShooter SlingShooter;
    public List<Bird> Birds;
    public List<Enemy> Enemies;
    public TrailController TrailController;
    public BoxCollider2D TapCollider;
    private Bird _shotBird;

    [Header("GameEnded")]
    public GameObject gameEndedScreen;

    [SerializeField] private Text _winLoseInfo;
    [SerializeField] private Text _promptInfo;

    private bool _isGameEnded = false;
    private bool _isGameOver = false;

    // Start is called before the first frame update
    // start setiap burung merah mati?
    void Start()
    {
        for (int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }
        TapCollider.enabled = false;
        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
    }


    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }
        if(Birds.Count == 0 && Enemies.Count > 0)
        {
            _isGameOver = true;
            // gak bisa masuk kondisi ini mas, gak tau kenapa
            // debug log juga nggak bisa
        }
        if (Enemies.Count == 0)
        {
            _isGameEnded = true;
        }
    }

    private void gameEnded()
    {
        if (_isGameOver)
        {
            _winLoseInfo.text = "Loser";
            _promptInfo.text = "No Restart for You";
        }
        else
        {
            _winLoseInfo.text = "You Win";
            _promptInfo.text = "Press R to Restart";
        }
        gameEndedScreen.SetActive(true);
    }
    

    public void ChangeBird()
    {
        TapCollider.enabled = false;
        if (_isGameOver)
        {
            gameEnded();
            //game over
            //return;
        }
        if (_isGameEnded && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level 1"))
        {
            SceneManager.LoadScene("Level 2");
            //return;
        }
        else if (_isGameEnded && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level 2"))
        {
            gameEnded();
            // return;
            // game selesai
        }

        Birds.RemoveAt(0);

        if (Birds.Count > 0)
        {
            SlingShooter.InitiateBird(Birds[0]);
            _shotBird = Birds[0];
        }
    }

    public void AssignTrail(Bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;
    }

    void OnMouseUp()
    {
        if (_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _isGameEnded = false;
                _isGameOver = false;
                SceneManager.LoadScene("Level 1");
            }
        }
    }
}
