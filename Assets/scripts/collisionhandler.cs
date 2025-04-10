using UnityEngine;
using UnityEngine.SceneManagement;

public class collisionhandler : MonoBehaviour
{
    [SerializeField]float deltime=1f;
     [SerializeField] AudioClip blastsound;
    [SerializeField] AudioClip winsound;
     [SerializeField] ParticleSystem successparticle;
      [SerializeField] ParticleSystem colllisionparticles;
     AudioSource audioSource;
      bool iscontrollable=true;

    private void Start() 
    {
      audioSource=GetComponent<AudioSource>();
    
    }
 
   private void OnCollisionEnter(Collision other) 
   {
    if(!iscontrollable)
    return;

     switch(other.gameObject.tag)
     {
        case "Friendly":
        Debug.Log("everything is looking good");
        break;
        case "Finish":
        startnextlevel();
        break;
        default:
        strartCrashingSequence();
        break;

     }
     }
     void startnextlevel()
     { iscontrollable =false;
     audioSource.Stop();
      GetComponent<movement>().enabled=false;
       audioSource.PlayOneShot(winsound);
       successparticle.Play();
      Invoke("LoadNext",deltime);
     }
     void strartCrashingSequence()
     {
       iscontrollable=false;
     audioSource.Stop();
       GetComponent<movement>().enabled=false;
     //movement is the sccript name here  and it is calling the On  enable and making the value as false it can be done because they are on the same game object
        audioSource.PlayOneShot(blastsound);
        colllisionparticles.Play();
       Invoke("reloadlevel",2f);
     }
     void reloadlevel()
     { int CurrScene=SceneManager.GetActiveScene().buildIndex;
           SceneManager.LoadScene(CurrScene);
     }
     void LoadNext()
     {
      int CurrScene=SceneManager.GetActiveScene().buildIndex;
      int nextScene=CurrScene + 1;
           if(nextScene == SceneManager.sceneCountInBuildSettings)
           {
              nextScene=0;
           }
               SceneManager.LoadScene(nextScene);

     }
   }

