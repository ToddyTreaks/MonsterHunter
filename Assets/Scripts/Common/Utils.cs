using System.Collections;
using UnityEngine;

namespace Common
{
    public static class Utils
    {
        public static void RotateToTarget(Transform objectToRotate, Transform target, float rotationSpeed)
        {
            var targetRotation = Quaternion.LookRotation(target.position - objectToRotate.position);
            objectToRotate.rotation = Quaternion.Slerp(objectToRotate.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}