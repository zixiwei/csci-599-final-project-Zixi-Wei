using Boids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{

    #region Initialization and Updates

   
    public FlockSettingScriptable Settings;

   
    private void Start()
    {
        
        DisplayVersion();

       
        DisplayGeneralSettings(true);

        
        DisplayCohesionSettings(true);

      
        DisplaySeparationSettings(true);

     
        DisplayAlignmentSettings(true);
    }

    
    private void Update()
    {
        
        UpdateGeneralSettings();
        DisplayGeneralSettings();

        
        UpdateCohesionSettings();
        DisplayCohesionSettings();

     
        UpdateSeparationSettings();
        DisplaySeparationSettings();

        
        UpdateAlignmentSettings();
        DisplayAlignmentSettings();
    }

    #endregion

    #region Version


    [SerializeField]
    [Tooltip("Text UI element displaying the app version.")]
    private Text Version;

   
    private void DisplayVersion()
    {
        Version.text = string.Format("Version: {0}", Application.version);
    }

    #endregion

    #region General Settings

    [Header("General")]

    public Text MinimumSpeedTextUI;

 
    public Slider MinimumSpeedSliderUI;

    public Text MaximumSpeedTextUI;

    public Slider MaximumSpeedSliderUI;

    public Text MaximumSteeringForceTextUI;

   
    public Slider MaximumSteeringForceSliderUI;

   
    private void DisplayGeneralSettings(bool initialize = false)
    {
        MinimumSpeedTextUI.text = string.Format("Minimum speed ({0:0.00})", Settings.MinSpeed);
        MaximumSpeedTextUI.text = string.Format("Maximum speed ({0:0.00})", Settings.MaxSpeed);
        MaximumSteeringForceTextUI.text = string.Format("Max steering force ({0:0.00})", Settings.MaxSteerForce);

        if (initialize)
        {
            MinimumSpeedSliderUI.value = Settings.MinSpeed;
            MaximumSpeedSliderUI.value = Settings.MaxSpeed;
            MaximumSteeringForceSliderUI.value = Settings.MaxSteerForce;
        }
    }

    
    private void UpdateGeneralSettings()
    {
        Settings.MinSpeed = MinimumSpeedSliderUI.value;
        Settings.MaxSpeed = MaximumSpeedSliderUI.value;
        Settings.MaxSteerForce = MaximumSteeringForceSliderUI.value;
    }

    #endregion

    #region Cohesion Settings

    [Header("Cohesion")]


    public Text CohesionForceWeightTextUI;

    
    public Slider CohesionForceWeightSliderUI;

    
    public Text CohesionRadiusTextUI;

 
    public Slider CohesionRadiusSliderUI;

  
    public Toggle CohesionUseCenterToggleUI;


    private void DisplayCohesionSettings(bool initialize = false)
    {
        CohesionForceWeightTextUI.text = string.Format("Force weight ({0:0.00})", Settings.CohesionForceWeight);
        CohesionRadiusTextUI.text = string.Format("Radius ({0:0.00})", Settings.CohesionRadiusThreshold);

        if (initialize)
        {
            CohesionForceWeightSliderUI.value = Settings.CohesionForceWeight;
            CohesionRadiusSliderUI.value = Settings.CohesionRadiusThreshold;
            CohesionUseCenterToggleUI.isOn = Settings.UseCenterForCohesion;
        }
    }

    
    private void UpdateCohesionSettings()
    {
        Settings.CohesionForceWeight = CohesionForceWeightSliderUI.value;
        Settings.CohesionRadiusThreshold = CohesionRadiusSliderUI.value;
        Settings.UseCenterForCohesion = CohesionUseCenterToggleUI.isOn;
        Settings.IsCenterVisible = CohesionUseCenterToggleUI.isOn;
    }

    #endregion

    #region Separation Settings

    [Header("Separation")]

    
    public Text SeparationForceWeightTextUI;

    
    public Slider SeparationForceWeightSliderUI;


    public Text SeparationRadiusTextUI;

    
    public Slider SeparationRadiusSliderUI;

   
    private void DisplaySeparationSettings(bool initialize = false)
    {
        SeparationForceWeightTextUI.text = string.Format("Force weight ({0:0.00})", Settings.SeperationForceWeight);
        SeparationRadiusTextUI.text = string.Format("Radius ({0:0.00})", Settings.SeperationRadiusThreshold);

        if (initialize)
        {
            SeparationForceWeightSliderUI.value = Settings.SeperationForceWeight;
            SeparationRadiusSliderUI.value = Settings.SeperationRadiusThreshold;
        }
    }


    private void UpdateSeparationSettings()
    {
        Settings.SeperationForceWeight = SeparationForceWeightSliderUI.value;
        Settings.SeperationRadiusThreshold = SeparationRadiusSliderUI.value;
    }

    #endregion

    #region Alignment Settings

    [Header("Alignment")]

    
    public Text AlignmentForceWeightTextUI;

   
    public Slider AlignmentForceWeightSliderUI;

   
    public Text AlignmentRadiusTextUI;

    
    public Slider AlignmentRadiusSliderUI;

  
    private void DisplayAlignmentSettings(bool initialize = false)
    {
        AlignmentForceWeightTextUI.text = string.Format("Force weight ({0:0.00})", Settings.AlignmentForceWeight);
        AlignmentRadiusTextUI.text = string.Format("Radius ({0:0.00})", Settings.AlignmentRadiusThreshold);

        if (initialize)
        {
            AlignmentForceWeightSliderUI.value = Settings.AlignmentForceWeight;
            AlignmentRadiusSliderUI.value = Settings.AlignmentRadiusThreshold;
        }
    }


    private void UpdateAlignmentSettings()
    {
        Settings.AlignmentForceWeight = AlignmentForceWeightSliderUI.value;
        Settings.AlignmentRadiusThreshold = AlignmentRadiusSliderUI.value;
    }

    #endregion

}
