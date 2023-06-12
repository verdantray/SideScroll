using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool<T> : IDisposable where T : Component
{
    private string InstanceName => nameof(T);
    private bool InstanceExist => instanceQueue != null && instanceQueue.Any();

    private Transform parentObject = null;
    private Transform containerObject = null;
    private Queue<T> instanceQueue = null;

    public Pool(Transform parent, int capacity)
    {
        parentObject = parent;
        containerObject = new GameObject($"{nameof(T)} Pool").transform;
        containerObject.SetParent(parentObject);
        
        instanceQueue = new Queue<T>();

        for (int i = 0; i < capacity; i++)
        {
            Return(Create());
        }
    }

    // inherit of IDisposable
    public void Dispose()
    {
        while (InstanceExist)
        {
            GameObject.Destroy(instanceQueue.Dequeue());
        }

        instanceQueue.Clear();
        instanceQueue = null;

        GameObject.Destroy(containerObject);
        containerObject = null;

        parentObject = null;
    }

    public T Get()
    {
        T instance = InstanceExist
            ? instanceQueue.Dequeue()
            : Create();

        instance.transform.SetParent(parentObject);
        instance.gameObject.SetActive(true);

        return instance;
    }

    public void Return(T toReturn)
    {
        toReturn.gameObject.SetActive(false);
        toReturn.transform.SetParent(containerObject);
        
        instanceQueue.Enqueue(toReturn);
    }

    private T Create()
    {
        GameObject createdObject = new GameObject(InstanceName);
        return createdObject.AddComponent<T>();
    }
}
