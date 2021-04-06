using UnityEngine;

public class RandomObjectsArray<T>
{
    private T[] objects;
    private int lastIndex = -1;

    public RandomObjectsArray(T[] objects)
    {
        this.objects = objects;
    }

    public T Next()
    {
        if (objects.Length == 0)
            return default(T);
        else if (objects.Length == 1)
            return objects[0];

        int rndIndex = lastIndex;

        while (rndIndex == lastIndex)
            rndIndex = Random.Range(0, objects.Length);

        lastIndex = rndIndex;

        return objects[rndIndex];
    }
}
