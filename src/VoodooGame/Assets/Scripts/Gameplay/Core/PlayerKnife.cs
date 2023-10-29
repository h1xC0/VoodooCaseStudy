using System.Threading.Tasks;
using DG.Tweening;
using Payloads;
using Services.InputService;
using Services.LevelProgressionService;
using Services.PlayerProgression;
using Signals;
using Systems.CommandSystem;
using UnityEngine;
using Zenject;

namespace Gameplay.Core
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerKnife : MonoBehaviour, IKnife, IWeapon
    {
        public Transform Transform => transform;
        
        [SerializeField] private KnifeSlicer _knifeSlicer;
        
        [SerializeField] private Vector2 jumpForce;
        [SerializeField] private float _knifeTorque;
        [SerializeField] private float _rotationDuration;
        [SerializeField] private float _slowdownSpeed;
        [SerializeField] private Vector3 _targetPosition;

        private Rigidbody _rigidbody;
        private IInputService _inputService;
        private IPlayerProgressionService _playerProgressionService;
        private ICommandDispatcher _commandDispatcher;
        private ILevelProgressionService _levelProgressionService;
        
        [Inject]
        private void Initialize(IInputService inputService, IPlayerProgressionService playerProgressionService, ICommandDispatcher commandDispatcher, ILevelProgressionService levelProgressionService)
        {
            _rigidbody = GetComponent<Rigidbody>();
            
            _inputService = inputService;
            _playerProgressionService = playerProgressionService;
            _commandDispatcher = commandDispatcher;
            _levelProgressionService = levelProgressionService;
            
            _knifeSlicer.SliceObjectEvent += OnSliceObject;
            _knifeSlicer.KnifeReachedGroundEvent += OnReachedGround;
        }
        
        private void Update()
        {
            if (_inputService.GetClickOnScreen() && _levelProgressionService.LevelEnd == false)
            {
                MoveKnife();
            }
        }

        private void MoveKnife()
        {
            if (_rigidbody.isKinematic)
            {
                _rigidbody.isKinematic = false;
                _knifeSlicer.SetCollider(true, 1000);
            }

            transform
                .DORotate(-_targetPosition, _rotationDuration, RotateMode.WorldAxisAdd)
                .SetEase(Ease.Linear);
            _rigidbody.AddForce(jumpForce * _rigidbody.mass, ForceMode.Impulse);
        }

        private void OnSliceObject(ISliced slicedObject)
        {
            _levelProgressionService.ScoreIngredient(slicedObject.IngredientType);

            var velocity = _rigidbody.velocity;
            velocity = new Vector3(velocity.x * _slowdownSpeed, velocity.y, velocity.z);
            _rigidbody.velocity = velocity;
            
            _playerProgressionService.AddResources(slicedObject.PointValue);
        }

        private void OnReachedGround()
        {
            _rigidbody.isKinematic = true;
            _knifeSlicer.SetCollider(false);
        }

        public void Dispose()
        {
            _knifeSlicer.SliceObjectEvent -= OnSliceObject;
        }
    }
}