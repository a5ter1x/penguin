using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.CodeBase.Models
{
    public class LevelPoints : MonoBehaviour
    {
        [field: SerializeField, Required] public Transform LeftPoint { get; private set; }
        [field: SerializeField, Required] public Transform RightPoint { get; private set; }
    }
}
