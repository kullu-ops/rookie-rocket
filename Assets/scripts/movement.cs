using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotater;
    [SerializeField] AudioClip mainthrust;
   
      [ SerializeField] float thruststrength=10;
   [ SerializeField] float rotationstrength=10;
    [ SerializeField]  ParticleSystem leftboost;
     [ SerializeField]  ParticleSystem rightboost;
      [ SerializeField]  ParticleSystem  thrustParticles;
      Rigidbody rb;
    AudioSource audioSource;
    private void Start()
     {
        rb = GetComponent<Rigidbody>();
        audioSource=GetComponent<AudioSource>();
        
    }
    private void OnEnable() 
    {
        thrust.Enable();
        rotater.Enable();
    }
   private void FixedUpdate()
     {
        
     thruster();
     rotation();
        
     }
     private void thruster()
     {
        if(thrust.IsPressed())
        {
      rb.AddRelativeForce(Time.fixedDeltaTime*thruststrength*Vector3.up);
      if(!audioSource.isPlaying)
      {
      audioSource.PlayOneShot(mainthrust);
      
      }
      if(!thrustParticles.isPlaying)
      {
        thrustParticles.Play();
     }
        }
     else
     {
     audioSource.Stop();
     thrustParticles.Stop();
     }
     }
     public void rotation()
     {
      float rotationinput=rotater.ReadValue<float>();
      Debug.Log("value"+rotationinput);
      rotationinput=-rotationinput;
      if(rotationinput > 0)
      {
        applyrotation(rotationstrength);
         if(!rightboost.isPlaying)
      {
        leftboost.Stop();
        rightboost.Play();
     }
      
      }
      else if(rotationinput < 0)
      {
       applyrotation(-rotationstrength);
        if(!leftboost.isPlaying)
      {
        rightboost.Stop();
        leftboost.Play();
     }
       }
       else{
        leftboost.Stop();
        rightboost.Stop();
       }
       }
     void applyrotation(float rotateperframe)
     {
       rb.freezeRotation=true;
       transform.Rotate(Vector3.forward*Time.fixedDeltaTime*rotateperframe);
       rb.freezeRotation=false;
     }
}
