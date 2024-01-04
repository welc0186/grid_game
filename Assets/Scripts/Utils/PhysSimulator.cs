using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhysSimulator : MonoBehaviour
{
    
    public static PhysSimulator Instance;

    [SerializeField] private GameObject[] environmentObjects;
    
    void Awake()
    {
        Instance = this;
    }

    public GameObject Simulate(GameObject refObject)
    {
        var simObjects = new List<GameObject>(){ refObject };
        simObjects.AddRange(environmentObjects);
        var physSim = new PhysicsSim2D(simObjects.ToArray());
        return physSim.Simulate(1, refObject);
    }

}
