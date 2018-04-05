using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public List<Boid> boidParticles;

    public int boidsToSpawn = 10;
    public float maxSpeed = 5f, 
        oceanSizeMinX = -35f,
        oceanSizeMaxX = 35f,
        oceanSizeMinZ = -35f,
        oceanSizeMaxZ = 35f,

        islandSize = 7f;
    public float oceanHeightOffsetMin = -1f, oceanHeightOffsetMax = -4f;
    private Transform fishOwner;

    public float[] ruleEffects;

    public void Start()
    {
        boidParticles = new List<Boid>();

        fishOwner = new GameObject("Fish Owner").transform;
        fishOwner.position = Vector3.zero;
        fishOwner.rotation = Quaternion.identity;
        fishOwner.parent = transform;

        //Spawn Boid
        int count = 0;
        while(count < boidsToSpawn)
        {
            Vector3 position = new Vector3(Random.Range(oceanSizeMinX, oceanSizeMaxX), Random.Range(oceanHeightOffsetMin, oceanHeightOffsetMax), Random.Range(oceanSizeMinZ, oceanSizeMaxZ));
            Quaternion rotation = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up);
            Vector3 velocity = rotation * Vector3.forward;
            float size = Random.Range(0.1f, 1f);
            Vector3 randomSize = Vector3.one * size;

            GameObject boid = Instantiate(Resources.Load<GameObject>("Prefabs/Fish"), position, rotation, fishOwner);
            boid.transform.localScale = randomSize;

            boidParticles.Add(new Boid(boid, position, velocity, size));
            count++;
        }
    }

    public void Update()
    {
        //Update Boids
        foreach(Boid boid in boidParticles)
        {
            //Do Position
            Vector3 v1 = RuleOne(boid) * ruleEffects[0];
            Vector3 v2 = RuleTwo(boid) * ruleEffects[1];
            Vector3 v3 = RuleThree(boid) * ruleEffects[2];
            Vector3 v4 = PushRoundEdge(boid) * ruleEffects[3];
            Vector3 v5 = PullToEdge(boid) * ruleEffects[4];

            if(Mathf.Abs(boid.position.y - boid.targetY) < 0.2f)
            {
                boid.targetY = Random.Range(oceanHeightOffsetMin, oceanHeightOffsetMax);
            }
            else
            {
                boid.velocity.y += Mathf.Sign(boid.targetY - boid.position.y) * ruleEffects[5];
            }

            //v =u + at

            boid.velocity += v1 + v2 + v3 + v4 + v5;
            boid.velocity = Vector3.ClampMagnitude(boid.velocity, maxSpeed);

            StayInsideOcean(boid);

            boid.position += boid.velocity * Time.deltaTime;

            boid.boidObject.transform.localPosition = boid.position;
            boid.boidObject.transform.rotation = Quaternion.Lerp(boid.boidObject.transform.rotation, Quaternion.LookRotation(boid.velocity, Vector3.up), Time.deltaTime * 2f);
        }
    }

    //Rule 1: Boids try to fly towards the centre of mass of neighbouring boids.
    public Vector3 RuleOne(Boid _boid)
    {
        Vector3 perceivedPosition = Vector3.zero;
        foreach (Boid otherBoid in boidParticles)
        {
            if (otherBoid != _boid && Vector3.Distance(otherBoid.position, _boid.position) < 5f)
            {
                perceivedPosition += otherBoid.position;
            }
        }
        perceivedPosition /= boidsToSpawn - 1;

        return (perceivedPosition - _boid.position) / 100f;
    }

    //Rule 2: Boids try to keep a small distance away from other objects(including other boids).
    public Vector3 RuleTwo(Boid _boid)
    {
        Vector3 perceivedPosition = Vector3.zero;
        foreach (Boid otherBoid in boidParticles)
        {
            if (otherBoid != _boid && Vector3.Distance(_boid.position, otherBoid.position) < (_boid.size/2f) + 0.1f)
            {
                perceivedPosition -= (otherBoid.position - _boid.position);
            }
        }

        return perceivedPosition;
    }

    //Rule 3: Boids try to match velocity with near boids.
    public Vector3 RuleThree(Boid _boid)
    {
        Vector3 perceivedVelocity = Vector3.zero;
        foreach (Boid otherBoid in boidParticles)
        {
            if (otherBoid != _boid && Vector3.Distance(otherBoid.position, _boid.position) < 5f)
            {
                perceivedVelocity += otherBoid.velocity;
            }
        }
        perceivedVelocity /= boidsToSpawn - 1;

        return (perceivedVelocity - _boid.velocity) / 8.0f;
    }

    public Vector3 PushRoundEdge(Boid _boid)
    {
        return Quaternion.AngleAxis(_boid.swimClockwise ? 90f : -90f, Vector3.up) * Vector3.Scale(_boid.position.normalized, new Vector3(1f,0f,1f));
    }

    public Vector3 PullToEdge(Boid _boid)
    {
        int closestEdge = 0;
        float dist = Mathf.Abs(oceanSizeMinX - _boid.position.x);

        float xMaxDist = Mathf.Abs(oceanSizeMaxX - _boid.position.x);
        if(xMaxDist < dist)
        {
            dist = xMaxDist;
            closestEdge = 1;
        }

        float zMinDist = Mathf.Abs(oceanSizeMinZ - _boid.position.z);
        if (zMinDist < dist)
        {
            dist = zMinDist;
            closestEdge = 2;
        }

        float zMaxDist = Mathf.Abs(oceanSizeMaxZ - _boid.position.z);
        if (zMaxDist < dist)
        {
            dist = zMaxDist;
            closestEdge = 3;
        }

        if(closestEdge == 0)
        {
            return new Vector3(-1f, 0f, 0f) * Mathf.Lerp(1f, 0f, (_boid.position.magnitude - islandSize) / 0.5f);
        }
        else if (closestEdge == 1)
        {
            return new Vector3(1f, 0f, 0f) * Mathf.Lerp(1f, 0f, (_boid.position.magnitude - islandSize) / (oceanSizeMaxX-islandSize));
        }
        else if(closestEdge == 2)
        {
            return new Vector3(0f, 0f, -1f) * Mathf.Lerp(1f, 0f, (_boid.position.magnitude - islandSize) / (oceanSizeMinZ - islandSize));
        }
        else
        {
            return new Vector3(0f, 0f, 1f) * Mathf.Lerp(1f, 0f, (_boid.position.magnitude - islandSize) / (oceanSizeMaxZ - islandSize));
        }
    }

    public void StayInsideOcean(Boid _boid)
    {
        _boid.position.x = Mathf.Clamp(_boid.position.x, oceanSizeMinX + _boid.size, oceanSizeMaxX - _boid.size);
        _boid.position.y = Mathf.Clamp(_boid.position.y, oceanHeightOffsetMax, oceanHeightOffsetMin);
        _boid.position.z = Mathf.Clamp(_boid.position.z, oceanSizeMinZ + _boid.size, oceanSizeMaxZ - _boid.size); 
        
        if(_boid.position.magnitude <= islandSize)
        {
            _boid.position = _boid.position.normalized * islandSize;
        }
    }

    public class Boid
    {
        public GameObject boidObject;
        public Vector3 position, velocity;
        public float size;
        public bool swimClockwise;

        public float targetY;

        public Boid(GameObject _gameObject, Vector3 _position, Vector3 _velocity, float _size)
        {
            boidObject = _gameObject;
            position = _position;
            targetY = _position.y;
            velocity = _velocity;
            size = _size;
            swimClockwise = Random.Range(0, 100) < 50;
        }

    }
}
