using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    //[SerializeField] float speed = 5f;


    public float MaxPullDistance = 3f;
    public float MaxSpeed = 10f;
    public LineRenderer TrajectoryLine;

    private Vector3 _startMousePos;
    private Vector3 _currentMousePos;
    private bool _isAiming = false;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private float _fuelConsumption = 5f;
    private float _fuelReserve = 100f;
    [SerializeField] private Image _fuelImage;
    [SerializeField] private TMP_Text _fuelText;

    void Start()
    {
        SetupTrajectoryLine();
        UpdateFuel();
    }

    void Update()
    {
        //transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);

        HandleInput();
        UpdateTrajectoryLine();
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
        }

        // Завершение прицеливания
        if (_isAiming && Input.GetMouseButtonUp(0))
        {
            ReleaseAndLaunch();
        }
    }
    bool IsMouseOverRocket()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        return hit.collider != null && hit.collider.gameObject == gameObject;
    }

    void StartAiming()
    {
        if (_fuelReserve < _fuelConsumption) return;

        _startMousePos = GetMouseWorldPos();
        _isAiming = true;
        TrajectoryLine.enabled = true;
        Time.timeScale = 0.01f;
    }

    void ContinueAiming()
    {
        _currentMousePos = GetMouseWorldPos();
    }

    void ReleaseAndLaunch()
    {
        Time.timeScale = 1f;
        Vector3 direction = _startMousePos - _currentMousePos;
        float distance = Mathf.Min(direction.magnitude, MaxPullDistance);

        // Нормализуем направление и устанавливаем скорость
        Vector2 launchVelocity = direction.normalized * (distance / MaxPullDistance) * MaxSpeed;
        _rb.linearVelocity = launchVelocity;

        // Сбрасываем состояние
        _isAiming = false;
        TrajectoryLine.enabled = false;
        _fuelReserve -= _fuelConsumption;
        UpdateFuel();
    }
    private void UpdateFuel()
    {
        _fuelImage.fillAmount = _fuelReserve / 100f;
        _fuelText.text = _fuelReserve.ToString("00.0");
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
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -_mainCamera.transform.position.z;
        return _mainCamera.ScreenToWorldPoint(mousePos);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _scoreManager.EndGame();
        Die();
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}