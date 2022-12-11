using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Boids
{
    public class Flock : MonoBehaviour
    {

        #region Initialization

        
        private void Awake()
        {
            if (_FlockSettings == null)
                _FlockSettings = ScriptableObject.CreateInstance<FlockSettingScriptable>();

            if (_FlockSettings.NumberOfBirdsToGenerateOnAwake > 0)
                Initialize(_FlockSettings.NumberOfBirdsToGenerateOnAwake);
        }

        
        /// <param name="numberOfBirds">The number of birds to be generated in this flock.</param>
        public void Initialize(int numberOfBirds)
        {
           
            Clear();

            
            for (int i = 0; i < numberOfBirds; i++)
                CreateBird();
        }

        #endregion

        #region Fields/Properties

        
        [Tooltip("A scriptable object instance that contains the flock's settings.")]
        [SerializeField]
        private FlockSettingScriptable _FlockSettings;

        
        public FlockSettingScriptable FlockSettings { get { return _FlockSettings; } }



        [Header("Center")]

        
        [SerializeField]
        [Tooltip("The sphere representing the center of the flock.")]
        private GameObject Center;

        [SerializeField]
        [Tooltip("The current center (local position) of the flock.")]
        private Vector3 _CenterPosition;

        
        public Vector3 CenterPosition { get { return _CenterPosition; } }



        [Header("Birds")]

       
        [SerializeField]
        [Tooltip("The prefab template used to generate birds in this flock.")]
        private GameObject BirdTemplate;

        
        [SerializeField]
        [Tooltip("The parent holding all the generated birds.")]
        private GameObject BirdsParent;

       
        [SerializeField]
        [Tooltip("List of all the birds in this flock.")]
        private List<Bird> _Birds;

        
        public List<Bird> Birds { get { return _Birds; } }

        #endregion

        #region Methods

       
        private void Update()
        {
            
            float centerX = 0, centerY = 0, centerZ = 0;
            foreach (Bird bird in _Birds)
            {
                centerX += bird.transform.localPosition.x;
                centerY += bird.transform.localPosition.y;
                centerZ += bird.transform.localPosition.z;
            }
            _CenterPosition = new Vector3(centerX, centerY, centerZ) / _Birds.Count();

            
            Center.transform.localPosition = _CenterPosition;

            
            Center.gameObject.SetActive(_FlockSettings.IsCenterVisible);
        }

        private void Clear()
        {
            _Birds = new List<Bird>();
            foreach (Transform bird in BirdsParent.transform)
                GameObject.Destroy(bird.transform);
        }

        
        private void CreateBird()
        {
           
            if (_Birds == null)
                _Birds = new List<Bird>();

            
            GameObject bird = GameObject.Instantiate(BirdTemplate, BirdsParent.transform);

           
            Bird birdScript = bird.GetComponent<Bird>();
            _Birds.Add(birdScript);

            
            bird.transform.localPosition = new Vector3
            (
                Random.Range(-2f, 2f),
                Random.Range(-2f, 2f),
                Random.Range(-2f, 2f)
            );

           
            bird.transform.localEulerAngles = new Vector3
            (
                Random.Range(0f, 360f),
                Random.Range(0f, 360f),
                Random.Range(0f, 360f)
            );

            
            birdScript.Initialize(this);
        }

        #endregion

    }
}
