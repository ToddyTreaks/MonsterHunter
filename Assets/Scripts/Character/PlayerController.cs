using System;
using System.Collections;
using Assets.Scripts.Character.Objet;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Transform _cameratTransform;

    private PlayerAnimatorControl _anim;
    private Animator _animator;

    #region ScriptableVariable

    internal float speed;
    private float rotationSpeed;
    private float gravityValue;
    private float jumpForce;
    private float jumpTimer;
    private float slopeLimit;
    private float airSpeed; //between [0,1]
    private float jumpHeight;
    private int nbJump;
    private float velocityConvertJump;
    private float smoothDirection;
    private float dashDistance;
    private float dashTime;
    private float waitDash;
    #endregion

    #region Variable
    //Static variable
    public static bool StopPlayer = false;

    //Variable for item to use items
    public static bool isUseItem;
    
    //For interact method
    public static bool isInteract = false;

    //for check ground method
    internal float _groundCheckDistance = 1.8f;
    internal float distanceToGround;
    private int layerMask;

    //for movement method
    internal Vector3 input = Vector3.zero;
    internal Vector3 moveDirection;
    internal bool stopMove = false;
    internal Vector3 vitesse;

    // for jump method
    internal static bool _isJumping = false;
    internal float _jumpCounter = 0f;
    internal bool _isGrounded = true;
    internal float _mass;

    //for dash method
    internal bool isDashing = false;
    internal bool canDash = true;
    internal bool waitForDash = false; //true if the Coroutine WaitForDash is undergoing
    internal float dashSpeed;

    //for physic material
    private Rigidbody _rigidbody;
    internal PhysicMaterial frictionPhysics, maxFrictionPhysics, slippyPhysics;
    internal CapsuleCollider _capsuleCollider;
    internal RaycastHit _groundHit;
    #endregion

    #region Init

    void InitPlayerData()
    {
        speed = playerData.Speed;
        rotationSpeed = playerData.rotationSpeed;
        gravityValue = playerData.ForceGravity;
        jumpForce = playerData.JumpForce;
        jumpTimer = playerData.JumpTimer;
        jumpHeight = playerData.JumpHeight;
        nbJump = playerData.nbJump;
        velocityConvertJump = playerData.velocityConvertJump;
        airSpeed = playerData.airSpeed;
        slopeLimit = playerData.slopeLimit;
        smoothDirection = playerData.smoothDirection;
        dashDistance = playerData.dashDistance;
        dashTime = playerData.dashTime;
    }
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
        maxFrictionPhysics.staticFriction = 0.9f;
        maxFrictionPhysics.dynamicFriction = 0.9f;
        maxFrictionPhysics.frictionCombine = PhysicMaterialCombine.Maximum;

        // air physics 
        slippyPhysics = new PhysicMaterial();
        slippyPhysics.name = "slippyPhysics";
        slippyPhysics.staticFriction = 0f;
        slippyPhysics.dynamicFriction = 0f;
        slippyPhysics.frictionCombine = PhysicMaterialCombine.Minimum;
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

        _anim = GetComponent<PlayerAnimatorControl>();
        if (_anim == null) Debug.LogError("no PlayerAnimatorControl");

        _animator = GetComponent<Animator>();

        airSpeed = Mathf.Clamp(airSpeed, 0f, 1f);
        airSpeed = speed * airSpeed; //prevent airspeed to be greater than speed

        _mass = _rigidbody.mass;

        dashSpeed = dashDistance / dashTime;

        Cursor.lockState = CursorLockMode.Locked;

        layerMask |= 1<<0;
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
        if (StopPlayer) return;
        Move();
    }

    void Update()
    {
        _anim.UpdateAnimation();
        UpDateInput();
        if (StopPlayer) return;
        Rotate();
    }

    #endregion

    #region Input
    private void UpDateInput()
    {
        InputMove();
        InputJump();
        InputDash();
        InputInteract();
    }

    private void InputMove()
    {
        var forward = Vector3.ProjectOnPlane(_cameratTransform.forward, Vector3.up);
        var right = Vector3.ProjectOnPlane(_cameratTransform.right, Vector3.up); 
        
        var dirVertical = Input.GetAxisRaw("Vertical") * forward;
        var dirHorizontal = Input.GetAxisRaw("Horizontal") * right;
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

    private static void InputInteract()
    {
        if (Input.GetButtonDown("Interact")) isInteract = true;
    }
    #endregion

    #region Movement
    private void Move()
    {

        if (!_isGrounded || _isJumping || isDashing) return;

        vitesse = (stopMove) ? Vector3.zero : speed * moveDirection;
        _rigidbody.MovePosition(vitesse * Time.deltaTime + transform.position);
        /*        _rigidbody.velocity = new Vector3(vitesse.x, _rigidbody.velocity.y, vitesse.z);*/
    }
    private void CanMove()
    {
        stopMove = ((GroundAngle() > slopeLimit) || StopPlayer);
    }

    private bool CheckAngleToMove(Vector3 moveDirection) //if true player can't move
    {
        return Vector3.Dot(moveDirection, _groundHit.normal) > 0;
    }


    #endregion

    #region Jump
    private void Jump() 
    {

        if (_isJumping || !_isGrounded || isDashing || stopMove) return;
            
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

        //animation
        _animator.CrossFade("jump",0.1f);
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
        if (!canDash || isDashing || stopMove) return;
        canDash = false;
        isDashing = true;
        _rigidbody.velocity = Vector3.zero;
        if (moveDirection.magnitude>0.01) _rigidbody.AddForce(dashSpeed*moveDirection,ForceMode.VelocityChange);
        else _rigidbody.AddForce(-dashSpeed * transform.forward, ForceMode.VelocityChange);
        _animator.CrossFade("Dash", 0.1f);
        StartCoroutine(DashCoroutine());
    }

    IEnumerator DashCoroutine()
    {
        HealthPlayer.SetCanHit(false);
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
        var direction = moveDirection;
        direction.y = 0f;
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, direction.normalized, rotationSpeed * Time.deltaTime, .1f);
        Quaternion _newRotation = Quaternion.LookRotation(desiredForward);
        transform.rotation = _newRotation;
    }
    public static float ClampRotation(float angle, float min, float max)
    {
        if (angle < 0) angle += 360;
        return angle > 180f ? Mathf.Max(angle, 360 + min) : Mathf.Min(angle, max);
    }
    #endregion

    #region ControlMaterial
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
        var origin = new Vector3(position.x, position.y + _capsuleCollider.height + 2 * _capsuleCollider.radius, position.z);
        
        var direction = Vector3.down;


        
        _isGrounded = Physics.SphereCast(origin, _capsuleCollider.radius, direction, out var tt, _groundCheckDistance, layerMask);

        origin += moveDirection/2;

        Physics.Raycast(origin, direction, out _groundHit, _groundCheckDistance, layerMask);
        Debug.DrawLine(origin, origin + direction * _groundCheckDistance);

        Physics.Raycast(position, direction, out var hitInfo, Single.PositiveInfinity, layerMask);
        distanceToGround = hitInfo.distance;
    }

    public float GroundAngle()
    {
        var groundAngle = Vector3.Angle(_groundHit.normal, Vector3.up);
        return groundAngle;
    }
    #endregion
}
