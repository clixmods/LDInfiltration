using UnityEngine;


    [RequireComponent(typeof(Rigidbody))]
    [SelectionBase]
    public class Character : MonoBehaviour
    {
        private Vector3 _initialPosition;
        protected Vector3 InitialPosition => _initialPosition;
        protected Rigidbody _rb;
        protected CapsuleCollider _collider;
        [SerializeField] protected float _speed = 1;
        // Start is called before the first frame update
        public virtual void Start()
        {
            _initialPosition = transform.position;
            _collider = GetComponentInChildren<CapsuleCollider>();
            if (_collider == null)
                _collider = gameObject.AddComponent<CapsuleCollider>();
            _collider.radius = 0.28f;
            _collider.center = Vector3.zero;
        
            _rb = GetComponent<Rigidbody>();
            _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

