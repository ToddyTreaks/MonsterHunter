using System.Collections;
using UnityEngine;

public class PlayerControlle : MonoBehaviour
{
    [SerializeField] private GameObject rotateCamera;
    [SerializeField] private PlayerData playerData;

    #region ScriptableVariable
    private float speed = 5f;
    private float gravityValue = 20f;
    private int mouseSensitivity = 1;
    private int maxLookAngle = 35;
    private int minLookAngle = -30;
    private float jumpForce = 6f;
    private float jumpTimer = 0.5f;
    private float slopeLimit = 75f;
    private float airSpeed = 0.3f; //between [0,1]
    private float jumpHeight = 4f;
    private int nbJump = 1;
    private float velocityConvertJump = 0.3f;
    private float smoothDirection = 1f;
    private float dashDistance = 5f;
    private float dashTime = 0.2f;
    private float waitDash = 0.5f;
    #endregion


    //for check ground method
    private float _groundCheckDistance = 0.7f;

    //for movement method
    private Vector3 input = Vector3.zero;
    internal Vector3 moveDirection;  
    internal bool stopMove = false;
    
    // for jump method
    private static bool _isJumping = false;
    private float _jumpCounter = 0f;
    private static bool _isGrounded = true;
    private float _mass;

    //for dash method
    private bool isDashing = false;
    private bool canDash = true;
    private bool waitForDash = false; //true if the Coroutine WaitForDash is undergoing
    private float dashSpeed;


    //for physic material
    private Rigidbody _rigidbody;
    internal PhysicMaterial frictionPhysics, maxFrictionPhysics, slippyPhysics;
    internal CapsuleCollider _capsuleCollider;
    internal RaycastHit _groundHit;

    #region Init

    void InitPlayerData()
    {
        speed = playerData.Speed;
        gravityValue = playerData.ForceGravity;
        mouseSensitivity = playerData.MouseSensitivity;
        maxLookAngle = playerData.MaxLookAngle;
        minLookAngle = playerData.MinLookAngle;
        jumpForce = playerData.JumpForce;
        jumpTimer = playerData.JumpTimer;
        jumpHeight = playerData.JumpHeight;
        nbJump = playerData.nbJump;
        velocityConvertJump = playerData.velocityConvertJump;
        airSpeed = playerData.airSpeed;
        slopeLimit = playerData.slopeLimit;
        smoothDirection = playerData.smoothDirection;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitPlayerData();
        InitPhysicMaterials();

        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null) Debug.LogError("no rigidBody on player");

        _capsuleCollider = GetComponent<CapsuleCollider>();
        if (_capsuleCollider == null) Debug.LogError("no capsuleCollider on player");

        airSpeed = Mathf.Clamp(airSpeed, 0f, 1f);
        airSpeed = speed * airSpeed; //prevent airspeed to be superior as speed

        _mass = _rigidbody.mass;

        dashSpeed = dashDistance / dashTime;
    }

    #endregion

    #region Update
    private void UpdateMotor()
    {
        ApplyGravity();
        IsGrounded();
        ControlMaterialPhysics();
        CanMove();
        CanDash();
        ControlJumpBehaviour();
        AirControl();
    }
    void FixedUpdate()
    {
        UpdateMotor();
        Move();
    }

    void Update()
    {
        UpDateInput();
        Rotate();
    }

    #endregion

    #region Input
    private void UpDateInput()
    {
        InputMove();
        InputJump();
        InputDash();
    }

    private void InputMove()
    {
        var dirVertical = Input.GetAxisRaw("Vertical") * transform.forward;
        var dirHorizontal = Input.GetAxisRaw("Horizontal") * transform.right;
        input = (dirHorizontal + dirVertical).normalized;

        moveDirection = (input.magnitude < 0.01)
            ? Vector3.Lerp(input, moveDirection, smoothDirection * Time.deltaTime)
            : input;
    }

    private void InputJump()
    {
        if (Input.GetButtonDown("Jump")) Jump();
        if (Input.GetButtonUp("Jump")) _isJumping = false;
    }

    private void InputDash()
    {
        if (Input.GetButtonDown("Dash")) Dash();
    }
    #endregion

    #region Movement
    private void Move()
    {

        if (!_isGrounded || _isJumping || stopMove || isDashing) return;
        var vitesse = speed * moveDirection;
        _rigidbody.MovePosition(vitesse * Time.deltaTime + transform.position);
        /*        _rigidbody.velocity = new Vector3(vitesse.x, _rigidbody.velocity.y, vitesse.z);*/
    }
    public void CanMove()
    {
        stopMove = GroundAngle() >= slopeLimit;
    }
    #endregion

    #region Jump
    private void Jump() 
    {

        if (_isJumping || !_isGrounded || stopMove || isDashing) return;
            
        _isJumping = true;
        _jumpCounter = jumpTimer;
        var vitesse = speed * moveDirection;
        var forceTotal = jumpForce * Vector3.up + velocityConvertJump * vitesse;
        _rigidbody.AddForce(forceTotal*_mass, ForceMode.Impulse);

        //teste si la vitesse n'est pas supérieure à la vitesse maximum du perso si oui le clamp
        var velProj = Vector3.ProjectOnPlane(_rigidbody.velocity, Vector3.up);
        if (velProj.magnitude > speed)
        {
            var vel = _rigidbody.velocity;
            var dir = speed * moveDirection;
            vel.z = dir.z;
            vel.x = dir.x;
            _rigidbody.velocity = vel;
        }
    }

    private void ControlJumpBehaviour()
    {
        if (!_isJumping) return;

        _jumpCounter -= Time.deltaTime;
        if (_jumpCounter <= 0)
        {
            _jumpCounter = 0;
            _isJumping = false;
        }
           
        var vel = _rigidbody.velocity;
        vel.y = jumpHeight;
        _rigidbody.velocity = vel;
    }

    public void AirControl()
    {
        if ((_isGrounded && !_isJumping)) return;

        var vitesse = airSpeed * moveDirection;
        _rigidbody.MovePosition(vitesse * Time.deltaTime + transform.position);

        /*        moveDirection.y = 0;
                moveDirection.x = Mathf.Clamp(moveDirection.x, -1f, 1f);
                moveDirection.z = Mathf.Clamp(moveDirection.z, -1f, 1f);

                Vector3 targetPosition = _rigidbody.position + (moveDirection * airSpeed) * Time.deltaTime;
                Vector3 targetVelocity = (targetPosition - transform.position) / Time.deltaTime;

                targetVelocity.y = _rigidbody.velocity.y;
                _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, targetVelocity, airSpeed * Time.deltaTime);*/
    }
    #endregion

    #region Dash

    void Dash()
    {
        if (!canDash || isDashing) return;
        canDash = false;
        isDashing = true;
        _rigidbody.velocity = Vector3.zero;
        if (moveDirection.magnitude>0.01) _rigidbody.AddForce(dashSpeed*moveDirection,ForceMode.VelocityChange);
        else _rigidbody.AddForce(dashSpeed * transform.forward, ForceMode.VelocityChange);
        StartCoroutine(DashCoroutine());
    }

    IEnumerator DashCoroutine()
    {
        yield return new WaitForSeconds(dashTime);
        _rigidbody.velocity = Vector3.zero;
        isDashing = false;
        yield return null;
    }

    IEnumerator WaitForDash()
    {
        waitForDash = true;
        yield return new WaitForSeconds(waitDash);
        waitForDash = false;
        canDash = true;
        yield return null;
    }

    public void CanDash()
    {
        if (!canDash && _isGrounded && !waitForDash)
        {
            StartCoroutine(WaitForDash());
        }
    }
    #endregion

    #region Rotation
    private void Rotate()
    {
        var yaw = mouseSensitivity * Input.GetAxis("Mouse X");

        var pitch = -mouseSensitivity * Input.GetAxis("Mouse Y");

        transform.localEulerAngles += new Vector3(0, yaw, 0);

        var rotation = rotateCamera.transform.localEulerAngles + new Vector3(pitch, 0, 0);

        rotation.x = ClampRotation(rotation.x, minLookAngle, maxLookAngle);

        rotateCamera.transform.localEulerAngles = rotation;
    }
    private static float ClampRotation(float angle, float min, float max)
    {
        if (angle < 0) angle += 360;
        return angle > 180f ? Mathf.Max(angle, 360 + min) : Mathf.Min(angle, max);
    }
    #endregion

    #region ControlMaterial

    void InitPhysicMaterials()
    {
        // slides the character through walls and edges
        frictionPhysics = new PhysicMaterial();
        frictionPhysics.name = "frictionPhysics";
        frictionPhysics.staticFriction = .25f;
        frictionPhysics.dynamicFriction = .25f;
        frictionPhysics.frictionCombine = PhysicMaterialCombine.Multiply;

        // prevents the collider from slipping on ramps
        maxFrictionPhysics = new PhysicMaterial();
        maxFrictionPhysics.name = "maxFrictionPhysics";
        maxFrictionPhysics.staticFriction = 1f;
        maxFrictionPhysics.dynamicFriction = 1f;
        maxFrictionPhysics.frictionCombine = PhysicMaterialCombine.Maximum;

        // air physics 
        slippyPhysics = new PhysicMaterial();
        slippyPhysics.name = "slippyPhysics";
        slippyPhysics.staticFriction = 0f;
        slippyPhysics.dynamicFriction = 0f;
        slippyPhysics.frictionCombine = PhysicMaterialCombine.Minimum;
    }
    protected void ControlMaterialPhysics()
    {
        // change the physics material to very slip when not grounded
        _capsuleCollider.material = (_isGrounded && GroundAngle() < slopeLimit) ? frictionPhysics : slippyPhysics;

        if (_isGrounded && input == Vector3.zero)
            _capsuleCollider.material = maxFrictionPhysics;
        else if (_isGrounded && input != Vector3.zero)
            _capsuleCollider.material = frictionPhysics;
        else
            _capsuleCollider.material = slippyPhysics;
    }

    #endregion

    #region Gravity
    private void ApplyGravity()
    {
        _rigidbody.AddForce(gravityValue * Vector3.down, ForceMode.Acceleration);
    }
    #endregion

    #region DetectionGround
    public void IsGrounded()
    {
        var position = transform.position;
        var origin = new Vector3(position.x, position.y + 0.5f, position.z + 0.5f);
        var direction = transform.TransformDirection(Vector3.down);

        Debug.DrawLine(origin, origin+direction * _groundCheckDistance);
        _isGrounded = Physics.Raycast(origin, direction, out _groundHit, _groundCheckDistance);
    }

    public float GroundAngle()
    {
        var groundAngle = Vector3.Angle(_groundHit.normal, Vector3.up);
        return groundAngle;
    }
    #endregion
}
