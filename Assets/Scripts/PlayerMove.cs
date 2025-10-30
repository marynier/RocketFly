using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    //[SerializeField] float speed = 5f;


    public float MaxPullDistance = 3f;
    public float MaxSpeed = 10f;
    public float RotationSpeed = 5f;
    public LineRenderer TrajectoryLine;
    public bool AutoLaunchOnStart = true;
    public float InitialLaunchPower = 5f;

    private Vector3 _startMousePos;
    private Vector3 _currentMousePos;
    private bool _isAiming = false;
    private bool _isLaunched = false;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Camera _mainCamera;

    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private int _fuelConsumption = 5; //Расход топлива
    [SerializeField] private FuelManager _fuelManager;


    [SerializeField] private Projection _projection;
    private Vector2 _launchVelocity = Vector2.zero;

    [SerializeField] private SpriteRenderer _explosion;
    [SerializeField] private SpriteRenderer[] _playerVisual;
    [SerializeField] private GameManager _gameManager;

    public ChunkTrigger CurrentChunk { get; set; }


    void Start()
    {
        SetupTrajectoryLine();

        // Автоматический запуск в начале игры
        if (AutoLaunchOnStart)
        {
            AutoLaunch();
        }
        _projection.gameObject.SetActive(false);
    }

    void Update()
    {
        //transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);

        HandleInput();
        UpdateTrajectoryLine();

        // Поворачиваем ракету в сторону движения, если она летит
        if (_isLaunched && _rb.linearVelocity != Vector2.zero)
        {
            RotateTowardsVelocity();
        }
    }
    void AutoLaunch()
    {
        // Генерируем случайное направление
        Vector2 randomDirection = new Vector2(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f)).normalized;

        // Создаем начальный импульс
        Vector2 initialVelocity = randomDirection * InitialLaunchPower;
        _rb.linearVelocity = initialVelocity;
        _isLaunched = true;

        // Поворачиваем ракету в направлении движения
        if (initialVelocity != Vector2.zero)
        {
            RotateTowardsVelocity(true); // Мгновенный поворот для начального запуска
        }

        Debug.Log("Автоматический запуск ракеты! Направление: " + randomDirection + ", Скорость: " + InitialLaunchPower);
    }

    void HandleInput()
    {
        // Начало прицеливания
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseOverRocket())
            {
                StartAiming();
            }
        }

        // Процесс прицеливания
        if (_isAiming && Input.GetMouseButton(0))
        {
            ContinueAiming();
            // Поворачиваем ракету в сторону прицеливания во время натяжения
            RotateTowardsAimDirection();
        }

        // Завершение прицеливания
        if (_isAiming && Input.GetMouseButtonUp(0))
        {
            ReleaseAndLaunch(_launchVelocity);
        }
    }
    bool IsMouseOverRocket()
    {
        //Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        //RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        //return hit.collider != null && hit.collider.gameObject == gameObject;
        Vector2 mouseWorldPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D col = GetComponent<Collider2D>();
        if (col == null) return false;
        return col.OverlapPoint(mouseWorldPos);


    }

    void StartAiming()
    {
        if (_fuelManager._fuelReserve < _fuelConsumption) return;

        _startMousePos = GetMouseWorldPos();
        _isAiming = true;
        TrajectoryLine.enabled = true;
        //Time.timeScale = 0.01f;

        // Останавливаем физику при прицеливании
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _rb.linearVelocity = Vector2.zero;
        _rb.angularVelocity = 0f;


    }

    void ContinueAiming()
    {
        _currentMousePos = GetMouseWorldPos();

        Vector3 direction = _startMousePos - _currentMousePos;
        float distance = Mathf.Min(direction.magnitude, MaxPullDistance);

        // Нормализуем направление и устанавливаем скорость
        _launchVelocity = direction.normalized * (distance / MaxPullDistance) * MaxSpeed;


        _projection.gameObject.SetActive(true);
        _playerVisual[1].enabled = false;
        //_projection.ResetAndLaunch(transform.position, _launchVelocity);
    }

    void ReleaseAndLaunch(Vector2 launchVelocity)
    {
        //Time.timeScale = 1f;
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.linearVelocity = launchVelocity;

        // Сразу поворачиваем в сторону движения
        if (launchVelocity != Vector2.zero)
        {
            RotateTowardsVelocity();
        }

        // Сбрасываем состояние
        _isAiming = false;
        _isLaunched = true;
        TrajectoryLine.enabled = false;
        _fuelManager.FuelBurst(_fuelConsumption);
        _projection.gameObject.SetActive(false);
        _playerVisual[1].enabled = true;
    }

    void RotateTowardsAimDirection()
    {
        Vector3 direction = _startMousePos - _currentMousePos;
        if (direction != Vector3.zero)
        {
            // Вычисляем угол поворота
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Плавно поворачиваем ракету
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
        }
    }

    void RotateTowardsVelocity(bool instant = false)
    {
        if (_rb.linearVelocity != Vector2.zero)
        {
            // Вычисляем угол на основе вектора скорости
            float angle = Mathf.Atan2(_rb.linearVelocity.y, _rb.linearVelocity.x) * Mathf.Rad2Deg;
            if (instant)
            {
                // Мгновенный поворот
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                // Плавно поворачиваем ракету
                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
            }
        }
    }

    void SetupTrajectoryLine()
    {
        TrajectoryLine.positionCount = 2;
        TrajectoryLine.enabled = false;
        TrajectoryLine.startWidth = 0.1f;
        TrajectoryLine.endWidth = 0.1f;
    }

    void UpdateTrajectoryLine()
    {
        if (!_isAiming) return;

        Vector3 direction = _startMousePos - _currentMousePos;
        float distance = Mathf.Min(direction.magnitude, MaxPullDistance);
        Vector3 endPoint = transform.position - direction.normalized * distance;

        TrajectoryLine.SetPosition(0, transform.position);
        TrajectoryLine.SetPosition(1, endPoint);

        float width = (endPoint - transform.position).magnitude;
        _projection.GetComponent<SpriteRenderer>().size = new Vector2(width * 20, 7);


    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -_mainCamera.transform.position.z;
        return _mainCamera.ScreenToWorldPoint(mousePos);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.GetComponent<FuelCharger>()) return;
        EndGame();

    }
    private void EndGame()
    {
        _gameManager.EndGame();
        _explosion.enabled = true;

        foreach (var image in _playerVisual)
            image.enabled = false;
        
        _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        //_rb.bodyType = RigidbodyType2D.Static;

        //Invoke(nameof(Die), 0.3f);
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    public void Respawn()
    {
        if (CurrentChunk != null && CurrentChunk.respawnPoint != null)
        {
            transform.position = CurrentChunk.respawnPoint.position;
            _rb.constraints = RigidbodyConstraints2D.None;
            foreach (var image in _playerVisual)
                image.enabled = true;
            _explosion.enabled = false;
            AutoLaunch();
            _fuelManager.FuelCharging(10);
        }
        else
        {
            // респавн в дефолтной точке или начало уровня
        }
    }
}