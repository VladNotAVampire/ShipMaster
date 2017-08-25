using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ColliderEnablingController
{
    private List<Collider> colliders;

    public ColliderEnablingController(GameObject obj)
        : this(obj.GetComponentsInChildren<Collider>(), false)
    { }

    public ColliderEnablingController(GameObject obj, bool off)
        : this(obj.GetComponentsInChildren<Collider>().Where(c => c.enabled), off)
    { }

    public ColliderEnablingController(IEnumerable<Collider>colliders)
        : this (colliders, false)
    { }

    public ColliderEnablingController(IEnumerable<Collider> colliders, bool off)
    {
        this.colliders = colliders.ToList();

        if (off)
            DisableColiders();

        //for(int i = 0; i < this.colliders.Count; i++)
        //{
        //    Debug.Log(this.colliders[i]);
        //}
    }

    public void DisableColiders()
    {
        foreach(var collider in colliders)
        {
            collider.enabled = false;
        }
    }

    public void EnableColliders()
    {
        foreach(var collider in colliders)
        {
            collider.enabled = true;
        }
    }

    // Static part

    public static Dictionary<object, ColliderEnablingController> controllers = new Dictionary<object, ColliderEnablingController>();

    public static ColliderEnablingController AddController(GameObject obj, bool off)
    {
        return AddController(obj.GetComponentsInChildren<Collider>().Where(c => c.enabled), obj, off);
    }

    public static ColliderEnablingController AddController(IEnumerable<Collider> colliders, object key, bool off)
    {
        var controller = new ColliderEnablingController(colliders, off);

        if (!controllers.Keys.Any(k => k == key))
            controllers.Add(key, controller);
        else
            controllers[key] = controller;
        
        return controller;
    }
}
