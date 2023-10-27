using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Gameplay.Core
{
    [RequireComponent(typeof(Rigidbody))]
    public class Knife : MonoBehaviour, IWeapon
    {
        [SerializeField] private KnifeSlicer _knifeSlicer;
        
        [SerializeField] private Vector2 jumpForce = new Vector2(5,10);
        [SerializeField] private float _knifeTorque;
        [SerializeField] private Vector3 _targetPosition;

        private Rigidbody _rigidbody;
        [Inject]
        private void Initialize()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        private void Update()
        {
            Debug.Log(Vector3.Dot(transform.forward, transform.position));
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                _rigidbody.isKinematic = false;

                transform
                    .DORotate(_targetPosition, _knifeTorque, RotateMode.WorldAxisAdd)
                    .SetRelative(true)
                    .SetEase(Ease.Linear);
                _rigidbody.AddForce(jumpForce * _rigidbody.mass, ForceMode.Impulse);
                _rigidbody.AddTorque(Vector3.forward * (-75f * _rigidbody.mass));
            }
#else
           if (Input.touches.Length > 0)
            {
                var firstTouch = Input.GetTouch(0);

                if (firstTouch.phase == TouchPhase.Began)
                {
                    _rigidbody.isKinematic = false;

                    transform
                        .DORotate(_targetPosition, _knifeTorque, RotateMode.WorldAxisAdd)
                        .SetRelative(true)
                        .SetEase(Ease.Linear);
                    _rigidbody.AddForce(jumpForce * _rigidbody.mass, ForceMode.Impulse);
                    _rigidbody.AddTorque(Vector3.forward * (-75f * _rigidbody.mass));
                }
            }  
#endif
        }
    }
}