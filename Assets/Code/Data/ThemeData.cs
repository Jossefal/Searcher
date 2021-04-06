using UnityEngine;

#pragma warning disable 649

[CreateAssetMenu(fileName = "NewThemeData", menuName = "ScriptableObject`s/ThemeData")]
public class ThemeData : ScriptableObject
{
    [Header("MainInformation")]
    [SerializeField] private int id;
    public int Id { get => id; }

    [SerializeField] private int price;
    public int Price { get => price; }

    [SerializeField] private ValueVariant priceCurrency;
    public ValueVariant PriceCurrency { get => priceCurrency; }


    [Space]
    [Header("Rocket")]
    [SerializeField] private Sprite rocketSprite;
    public Sprite RocketSprite { get => rocketSprite; }

    [SerializeField] private GameObject rocketTrail;
    public GameObject RocketTrail { get => rocketTrail; }

    [SerializeField] private GameObject rocketDeathEffect;
    public GameObject RocketDeathEffect { get => rocketDeathEffect; }

    [SerializeField] private Sprite rocketGunSprite;
    public Sprite RocketGunSprite { get => rocketGunSprite; }

    [SerializeField] private GameObject rocketBulletPrefab;
    public GameObject RocketBulletPrefab { get => rocketBulletPrefab; }


    [Space]
    [Header("Meteor")]
    [SerializeField] private GameObject meteorSprite;
    public GameObject MeteorSprite { get => meteorSprite; }

    [SerializeField] private GameObject meteorDeathEffect;
    public GameObject MeteorDeathEffect { get => meteorDeathEffect; }


    [Space]
    [Header("BlackHole")]
    [SerializeField] private GameObject blackHoleSprite;
    public GameObject BlackHoleSprite { get => blackHoleSprite; }


    [Space]
    [Header("Spaceman")]
    [SerializeField] private Sprite spacemanBodySprite;
    public Sprite SpacemanBodySprite { get => spacemanBodySprite; }

    [SerializeField] private Sprite spacemanHandSprite;
    public Sprite SpacemanHandSprite { get => spacemanHandSprite; }


    [Space]
    [Header("Ufo")]
    [SerializeField] private Sprite ufoSprite;
    public Sprite UfoSprite { get => ufoSprite; }

    [SerializeField] private GameObject ufoBulletPrefab;
    public GameObject UfoBulletPrefab { get => ufoBulletPrefab; }

    [SerializeField] private GameObject ufoTrail;
    public GameObject UfoTrail { get => ufoTrail; }

    [SerializeField] private GameObject ufoDeathEffect;
    public GameObject UfoDeathEffect { get => ufoDeathEffect; }


    [Space]
    [Header("Background")]
    [SerializeField] private GameObject backgroundPrefab;
    public GameObject BackgroundPrefab { get => backgroundPrefab; }
}
