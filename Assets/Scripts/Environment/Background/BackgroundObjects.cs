using UnityEngine;

#pragma warning disable 649

public class BackgroundObjects : BackgroundBlock
{
    private Transform[] objects;
    [SerializeField] private Vector2 range;

    protected override void OnStart()
    {
        objects = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            objects[i] = transform.GetChild(i);
        }
    }

    protected override void OnTranslate()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            Vector3 pos = transform.position;
            pos.x += Random.Range(-range.x, range.x);
            pos.y += Random.Range(-range.y, range.y);

            objects[i].position = pos;
        }
    }
}
