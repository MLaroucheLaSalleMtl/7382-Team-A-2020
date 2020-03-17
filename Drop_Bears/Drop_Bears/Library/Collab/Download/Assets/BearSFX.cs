using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSFX : MonoBehaviour
{
    // Start is called before the first frame update
    public static BearSFX instance = null;
    //Ya its alotta of arrays but i feel like its more organized this way instead of memorizing the ranges within
    //the arrays for different types of clips just call a different array 
    //Sound clip Notes just put the clips in the appropriate array and bang the bears will have it 
    //if you add arrays follow the naming convention of the bears enumcolor then type of clip 
    [Header("GreenBearClips")]
    [SerializeField] private AudioClip[] greenAttackClips;
        [SerializeField] private AudioClip[] greenIdleClips;
        [SerializeField] private AudioClip[] greenHurtClips;
    [SerializeField] private AudioClip[] greenAbility1Clips;
    [SerializeField] private AudioClip[] greenAbility2Clips;

    [Header("BlueBearClips")]
    [SerializeField] private AudioClip[] blueAttackClips;
        [SerializeField] private AudioClip[] blueIdleClips;
        [SerializeField] private AudioClip[] blueHurtClips;
    [SerializeField] private AudioClip[] blueAbility1Clips;
    [SerializeField] private AudioClip[] blueAbility2Clips;

    [Header("PinkBearClips")]
    [SerializeField] private AudioClip[] pinkAttackClips;
        [SerializeField] private AudioClip[] pinkIdleClips;
        [SerializeField] private AudioClip[] pinkHurtClips;
    [SerializeField] private AudioClip[] pinkAbility1Clips;
    [SerializeField] private AudioClip[] pinkAbility2Clips;

    [Header("BlackBearClips")]
    [SerializeField] private AudioClip[] blackAttackClips;
        [SerializeField] private AudioClip[] blackIdleClips;
        [SerializeField] private AudioClip[] blackHurtClips;
    [SerializeField] private AudioClip[] blackAbility1Clips;
    [SerializeField] private AudioClip[] blackAbility2Clips;

    [Header("RedBearClips")]
    [SerializeField] private AudioClip[] redAttackClips;
        [SerializeField] private AudioClip[] redIdleClips;
        [SerializeField] private AudioClip[] redHurtClips;
    [SerializeField] private AudioClip[] redAbility1Clips;
    [SerializeField] private AudioClip[] redAbility2Clips;


    private Dictionary<string, AudioClip[]> soundClips = new Dictionary<string, AudioClip[]>();

    public Dictionary<string, AudioClip[]> SoundClips { get => soundClips;  }
    public AudioClip[] GreenAttackClips { get => greenAttackClips;  }
    public AudioClip[] GreenIdleClips { get => greenIdleClips; }
    public AudioClip[] GreenHurtClips { get => greenHurtClips;  }
    public AudioClip[] BlueAttackClips { get => blueAttackClips;  }
    public AudioClip[] BlueIdleClips { get => blueIdleClips; }
    public AudioClip[] BlueHurtClips { get => blueHurtClips;}
    public AudioClip[] PinkAttackClips { get => pinkAttackClips; }
    public AudioClip[] PinkIdleClips { get => pinkIdleClips; }
    public AudioClip[] PinkHurtClips { get => pinkHurtClips; }
    public AudioClip[] BlackAttackClips { get => blackAttackClips;}
    public AudioClip[] BlackIdleClips { get => blackIdleClips;  }
    public AudioClip[] BlackHurtClips { get => blackHurtClips; }
    public AudioClip[] RedAttackClips { get => redAttackClips;}
    public AudioClip[] RedIdleClips { get => redIdleClips; }
    public AudioClip[] RedHurtClips { get => redHurtClips; }
    public AudioClip[] GreenAbility1Clips { get => greenAbility1Clips; set => greenAbility1Clips = value; }
    public AudioClip[] GreenAbility2Clips { get => greenAbility2Clips; set => greenAbility2Clips = value; }
    public AudioClip[] BlueAbility1Clips { get => blueAbility1Clips; set => blueAbility1Clips = value; }
    public AudioClip[] BlueAbility2Clips { get => blueAbility2Clips; set => blueAbility2Clips = value; }
    public AudioClip[] PinkAbility1Clips { get => pinkAbility1Clips; set => pinkAbility1Clips = value; }
    public AudioClip[] PinkAbility2Clips { get => pinkAbility2Clips; set => pinkAbility2Clips = value; }
    public AudioClip[] BlackAbility1Clips { get => blackAbility1Clips; set => blackAbility1Clips = value; }
    public AudioClip[] BlackAbility2Clips { get => blackAbility2Clips; set => blackAbility2Clips = value; }
    public AudioClip[] RedAbility1Clips { get => redAbility1Clips; set => redAbility1Clips = value; }
    public AudioClip[] RedAbility2Clips { get => redAbility2Clips; set => redAbility2Clips = value; }

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    

    }
  
    void Start()
    {
       
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
