using UnityEngine;

[CreateAssetMenu(menuName = "Settings/GameData", fileName = "GameData")]
public class GameData : ScriptableObject
{
    [SerializeField]
    private float _startSpeed = 7f;

    [SerializeField]
    private float _speedIncrease = 3f;

    [SerializeField]
    private float _timeToIncrease = 30f;

    [SerializeField]
    private float _increaseTime = 180f;

    [SerializeField]
    private float _startEasyChance = 1f;

    [SerializeField]
    private float _easyChanceDecreaseStep = 0.025f;

    [SerializeField]
    private float _finalEasyChance = 0.2f;

    [SerializeField]
    private float _timeBetweenSkyforce = 15f;


    public float StartSpeed => _startSpeed;

    public float SpeedIncrease => _speedIncrease;

    public float TimeToIncrease => _timeToIncrease;

    public float IncreaseTime => _increaseTime;

    public float StartEasyChance => _startEasyChance;

    public float EasyChanceDecreaseStep => _easyChanceDecreaseStep;

    public float FinalEasyChance => _finalEasyChance;

    public float TimeBetweenSkyforce => _timeBetweenSkyforce;
}
