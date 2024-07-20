using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private float _speed = 3.5f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _leftEngine;
    [SerializeField] private GameObject _rightEngine;
    [SerializeField] private GameObject _shieldVisualizer;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private AudioClip _laserAduio;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private AudioSource _audioSource;
    private GameManager _gameManager;
    private Animator _anim;

    private float _offset = 1.05f;
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    private float _speedMultiplier = 2f;
    private int _lives;
    private int _score;

    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;
    public bool isPlayer1;
    public bool isPlayer2;

    public bool isTurnLeft;
    public bool isTurnRight;


    void Start()
    {
        _lives = 3;
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _anim = GetComponent<Animator>();

        if (_leftEngine != null && _rightEngine != null)
        {
            _leftEngine.SetActive(false);
            _rightEngine.SetActive(false);
        }
        if (_anim == null)
        {
            Debug.LogError("Player: Animation is Null");
        }
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }
        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL");
        }
        if (_laserAduio == null)
        {
            Debug.Log("Laser Audio is NULL");
        }
        if (_audioSource == null)
        {
            Debug.LogError("Audio sourrce is NULL");
        } else
        {
            _audioSource.clip = _laserAduio;
        }
    }
    void Update()
    {
        if (isPlayer1)
        {
            HandleMovement(KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S);
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
            {
                FireLaser();
            }
        } else if (isPlayer2)
        {
            HandleMovement(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow);
            if (Input.GetKeyDown(KeyCode.RightShift) && Time.time > _canFire)
            {
                FireLaser();
            }
        }
    }
    private void HandleMovement(KeyCode left, KeyCode right, KeyCode up, KeyCode down)
    {
        if (Input.GetKey(left))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
            _anim.SetBool("Turn_Left", true);
        }else if (Input.GetKey(right))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
            _anim.SetBool("Turn_Right", true);
        }else if (Input.GetKey(up))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }else if (Input.GetKey(down))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        } else
        {
            _anim.SetBool("Turn_Left", false);
            _anim.SetBool("Turn_Right", false);
        }

    }
    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        GameObject laserInstance;
        if (_isTripleShotActive)
        {
            laserInstance = Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, _offset, 0), Quaternion.identity);
        } else
        {
            laserInstance = Instantiate(_laserPrefab, transform.position + new Vector3(0, _offset, 0), Quaternion.identity);
            // Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, _offset, 0), Quaternion.identity);
        }
        Laser laser = laserInstance.GetComponent<Laser>();
        laser.AssignPlayerLaser(this);
        _audioSource.Play();
    }
    public void Damage()
    {
        if (_isShieldActive)
        {
            _shieldVisualizer.SetActive(false);
            _isShieldActive = false;
            return;
        }
        else
        {
            _lives--;
            if (_lives == 2)
            {
                _rightEngine.SetActive(true);
            } else if ( _lives == 1)
            {
                _leftEngine.SetActive(true);
            }
        }
        if (_lives < 1) 
        { 
            if (_spawnManager != null)
            {
                _gameManager.PlayerDeath();
            }
            
            Destroy(this.gameObject);
        }
        _uiManager.UpdateLivesImage(_lives, isPlayer1);
    }
    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostDownRoutine());
    }
    IEnumerator SpeedBoostDownRoutine()
    {
        yield return new WaitForSeconds(1);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;
    }
    public void ShieldActive()
    {
        _shieldVisualizer.SetActive(true);
        _isShieldActive = true;
        StartCoroutine(ShieldPowerDownRoutine());
    }
    IEnumerator ShieldPowerDownRoutine() 
    {
        yield return new WaitForSeconds(3);
        _isShieldActive = false;
    }
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
    public int GetLive()
    {
        return _lives;
    }
}
