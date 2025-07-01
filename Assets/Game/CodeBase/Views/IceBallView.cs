using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.CodeBase.Views
{
    public class IceBallView : MonoBehaviour
    {
        [Required, SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }
    }
}
