using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Game.CodeBase.Views
{
    public class GameplayUI : MonoBehaviour
    {
        [field: SerializeField, Required] public TextMeshProUGUI ScoreField { get; private set; }
    }
}
