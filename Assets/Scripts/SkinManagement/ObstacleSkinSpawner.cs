using UnityEngine;

#pragma warning disable 649

public class ObstacleSkinSpawner : MonoBehaviour
{
    [SerializeField] private SkinsContainer skinsContainer;

    public enum ObstacleType
    {
        Meteor,
        BlackHole
    }

    [SerializeField] private ObstacleType obstacleType;

    private void Awake()
    {
        EnvironmentSkinData skinData = skinsContainer.currentEnvironemntSkin;
        GameObject sprite = null;

        switch (obstacleType)
        {
            case ObstacleType.Meteor:
                sprite = skinData.meteorSpite;
                break;
            case ObstacleType.BlackHole:
                sprite = skinData.blackHoleSprite;
                break;
        }

        Instantiate(sprite, transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }
}
