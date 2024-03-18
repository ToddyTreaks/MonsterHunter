using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [Range(1, 100)]
    [Tooltip("Speed")]
    public int Speed = 5;

    [Range(1, 20)]
    [Tooltip("rotation Speed")]
    public int rotationSpeed = 7;

    [Range(1, 100)]
    [Tooltip("ExtraForceGravity")]
    public int ForceGravity = 20;

    [Range(20, 1000)]
    [Tooltip("mouseSensitivity")]
    public int MouseSensitivity = 1;

    [Range(20, 45)]
    [Tooltip("maxAngleCamera")]
    public int MaxLookAngle = 35;

    [Range(-45, 0)]
    [Tooltip("minAngleCamera")]
    public int MinLookAngle = -30;

    [Range(1, 20)]
    [Tooltip("jumpForce")]
    public float JumpForce = 5;

    [Range(0f,10f)]
    [Tooltip("jumpTimer")]
    public float JumpTimer = 0.3f;

    [Range(0f, 10f)]
    [Tooltip("jumpHeight")]
    public float JumpHeight = 4f;

    [Range(0, 10)]
    [Tooltip("number of Jump")]
    public int nbJump = 1;

    [Range(0, 10)]
    [Tooltip("percent of velocity convert for jump ")]
    public float velocityConvertJump = 0.3f;

    [Range(0, 1)]
    [Tooltip("AirControl")]
    public float airSpeed = 0.3f;

    [Range(0, 90)]
    [Tooltip("SlopeLimit")]
    public float slopeLimit = 75f;

    [Range(0, 90)]
    [Tooltip("smoothDirection")]
    public float smoothDirection = 1f;

    [Range(0, 10)]
    [Tooltip("distance Dash")]
    public float dashDistance = 5f;

    [Range(0, 1)]
    [Tooltip("dash Time")]
    public float dashTime = 0.2f;

    [Range(0, 1000)] [Tooltip("Point life")]
    public int maxLife = 200;
}
