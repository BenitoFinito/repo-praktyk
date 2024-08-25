using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    public Transform FLWheel, FRWheel;
    public Transform RLWheel, RRWheel;
    public float wheelRadius;
    private float m_steeringAngle;

    private float FLWheelRotation = 0f;
    private float FRWheelRotation = 0f;
    private float RLWheelRotation = 0f;
    private float RRWheelRotation = 0f;

    public void UpdateWheelRotation(float speed, float turnInput, float maxSteerAngle)
    {
        float rotationAngle = CalculateRotationAngle(speed);
        m_steeringAngle = maxSteerAngle * turnInput;

        FLWheelRotation += rotationAngle;
        FRWheelRotation += rotationAngle;
        RLWheelRotation += rotationAngle;
        RRWheelRotation += rotationAngle;

        FLWheel.localRotation = Quaternion.Euler(FLWheelRotation, m_steeringAngle, 90);
        FRWheel.localRotation = Quaternion.Euler(FRWheelRotation, m_steeringAngle, 90);
        RLWheel.localRotation = Quaternion.Euler(RLWheelRotation, 0, 90);
        RRWheel.localRotation = Quaternion.Euler(RRWheelRotation, 0, 90);
    }

    private float CalculateRotationAngle(float speed)
    {
        float circumference = 2 * Mathf.PI * wheelRadius;
        float rotationsPerSecond = speed / circumference;

        float rotationAngle = rotationsPerSecond * 360 * Time.fixedDeltaTime;

        return rotationAngle;
    }
}
