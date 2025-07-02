using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.CodeBase.Views
{
    public class DashAnimationPlayer : MonoBehaviour
    {
        private static readonly int DashAnimationId = Animator.StringToHash("Dash");

        [Required, SerializeField] private Animator _animator;

        public void PlayOnce(Vector3 position, Vector3 rotation)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            transform.position = position;
            transform.eulerAngles = rotation;

            _animator.Play(DashAnimationId);
        }

        public void OnAnimationFinished()
        {
            _animator.gameObject.SetActive(false);
        }
    }
}
