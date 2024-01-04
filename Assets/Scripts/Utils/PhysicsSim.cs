using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhysicsGhost
{

    public GameObject RefObject;
    public GameObject GhostObject;

    public PhysicsGhost(GameObject refObject)
    {
        RefObject = refObject;
    }

    public void Spawn(GameObject sceneParent)
    {
        GhostObject = GameObject.Instantiate(RefObject, RefObject.transform.position, RefObject.transform.rotation, sceneParent.transform);
        GhostObject.GetComponent<Renderer>().enabled = false;
        // SceneManager.MoveGameObjectToScene(GhostObject, physicsScene);
        var refRigidBody = RefObject.GetComponent<Rigidbody2D>();
        if (refRigidBody != null && refRigidBody.bodyType == RigidbodyType2D.Dynamic)
        {
            var ghostRigidBody = GhostObject.GetComponent<Rigidbody2D>();
            ghostRigidBody.position = refRigidBody.position;
            ghostRigidBody.velocity = refRigidBody.velocity;
            ghostRigidBody.angularVelocity = refRigidBody.angularVelocity;
        }
    }
}

public class PhysicsSim2D
{
    
    private List<PhysicsGhost> _ghostObjects;
    private PhysicsScene2D _physicsScene;
    private int _sceneID;

    public PhysicsSim2D(GameObject[] sceneObjects)
    {
        
        while(SceneManager.GetSceneByName(_sceneID.ToString()).IsValid())
        {
            _sceneID++;
        }
        var spawnedScene = SceneManager.CreateScene(_sceneID.ToString(), new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        _physicsScene = spawnedScene.GetPhysicsScene2D();
        _ghostObjects = new List<PhysicsGhost>();
        var sceneParent = new GameObject("SceneParent");
        SceneManager.MoveGameObjectToScene(sceneParent, spawnedScene);
        foreach(GameObject sceneObject in sceneObjects)
        {
            var newGhost = new PhysicsGhost(sceneObject);
            _ghostObjects.Add(newGhost);
            newGhost.Spawn(sceneParent);
        }
    }

    public IEnumerator Unload()
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync("Simulation");
        yield return ao;
    }

    public GameObject Simulate(float seconds, GameObject refObject = null)
    {
        _physicsScene.Simulate(seconds);
        var ghostObject = GetPhysicsGhost(refObject);
        Debug.DrawLine(Vector3.zero, ghostObject.transform.position,Color.red,1f);
        return GetPhysicsGhost(refObject);
    }

    public GameObject GetPhysicsGhost(GameObject refObject)
    {
        if(refObject == null)
            return null;
        
        foreach(PhysicsGhost ghost in _ghostObjects)
        {
            if(ghost.RefObject == refObject)
            {
                return ghost.GhostObject;
            }
        }
        return null;
    }

}
