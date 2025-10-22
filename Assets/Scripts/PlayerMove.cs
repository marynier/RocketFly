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
    [SerializeField] private float _fuelConsumption = 5f; //������ �������
    private float _fuelReserve = 100f; //������� �������
    private float _maxFuel = 100f;
    [SerializeField] private Image _fuelImage;
    [SerializeField] private TMP_Text _fuelText;

    void Start()
    {
        SetupTrajectoryLine();
        UpdateFuel();

        // �������������� ������ � ������ ����
        if (AutoLaunchOnStart)
        {
            AutoLaunch();
        }
    }

    void Update()
    {
        //transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);

        HandleInput();
        UpdateTrajectoryLine();

        // ������������ ������ � ������� ��������, ���� ��� �����
        if (_isLaunched && _rb.linearVelocity != Vector2.zero)
        {
            RotateTowardsVelocity();
        }
    }
    void AutoLaunch()
    {
        // ���������� ��������� �����������
        Vector2 randomDirection = new Vector2(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f)).normalized;

        // ������� ��������� �������
        Vector2 initialVelocity = randomDirection * InitialLaunchPower;
        _rb.linearVelocity = initialVelocity;
        _isLaunched = true;

        // ������������ ������ � ����������� ��������
        if (initialVelocity != Vector2.zero)
        {
            RotateTowardsVelocity(true); // ���������� ������� ��� ���������� �������
        }

        Debug.Log("�������������� ������ ������! �����������: " + randomDirection + ", ��������: " + InitialLaunchPower);
    }

    void HandleInput()
    {
        // ������ ������������
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseOverRocket())
            {
                StartAiming();
            }
        }

        // ������� ������������
        if (_isAiming && Input.GetMouseButton(0))
        {
            ContinueAiming();
            // ������������ ������ � ������� ������������ �� ����� ���������
            RotateTowardsAimDirection();
        }

        // ���������� ������������
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

        //// ������������� ������ ��� ������������
        //_rb.linearVelocity = Vector2.zero;
        //_rb.angularVelocity = 0f;
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

        // ����������� ����������� � ������������� ��������
        Vector2 launchVelocity = direction.normalized * (distance / MaxPullDistance) * MaxSpeed;
        _rb.linearVelocity = launchVelocity;

        // ����� ������������ � ������� ��������
        if (launchVelocity != Vector2.zero)
        {
            RotateTowardsVelocity();
        }

        // ���������� ���������
        _isAiming = false;
        _isLaunched = true;
        TrajectoryLine.enabled = false;
        _fuelReserve -= _fuelConsumption;
        UpdateFuel();
    }
    private void UpdateFuel()
    {
        _fuelImage.fillAmount = _fuelReserve / 100f;
        _fuelText.text = _fuelReserve.ToString("00.0");
    }
    public void FuelCharging(float value)
    {
        if (_fuelReserve < _maxFuel)
        {
            float possibleAdding = _maxFuel - _fuelReserve;
            if (possibleAdding >= value)
                _fuelReserve += value;
            else
                _fuelReserve += possibleAdding;

            if(_fuelReserve > _maxFuel)
                _fuelReserve = _maxFuel;

            UpdateFuel();
        }

    }
    void RotateTowardsAimDirection()
    {
        Vector3 direction = _startMousePos - _currentMousePos;
        if (direction != Vector3.zero)
        {
            // ��������� ���� ��������
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // ������ ������������ ������
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
        }
    }

    void RotateTowardsVelocity(bool instant = false)
    {
        if (_rb.linearVelocity != Vector2.zero)
        {
            // ��������� ���� �� ������ ������� ��������
            float angle = Mathf.Atan2(_rb.linearVelocity.y, _rb.linearVelocity.x) * Mathf.Rad2Deg;
            if (instant)
            {
                // ���������� �������
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                // ������ ������������ ������
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