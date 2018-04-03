using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public List<Boid> boidParticles;

    public int boidsToSpawn = 10;
    public float maxSpeed = 5f, oceanSize = 35f, oceanHeightOffset = -1f, islandSize = 7f;

    public void Start()
    {
        boidParticles = new List<Boid>();

        //Spawn Boid
        int count = 0;
        while(count < boidsToSpawn)
        {
            Vector3 position = new Vector3(Random.Range(-oceanSize, oceanSize), oceanHeightOffset, Random.Range(-oceanSize, oceanSize));
            Quaternion rotation = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up);
            Vector3 velocity = rotation * Vector3.forward;
            float size = Random.Range(0.3f, 1.25f);
            Vector3 randomSize = Vector3.one * size;

            GameObject boid = Instantiate(Resources.Load<GameObject>("Prefabs/Fish"), position, rotation);
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
            Vector3 v1 = RuleOne(boid);
            Vector3 v2 = RuleTwo(boid);
            Vector3 v3 = RuleTwo(boid);

            boid.velocity += v1 + v2 + v3;
            boid.velocity = Vector3.ClampMagnitude(boid.velocity, maxSpeed);

            StayInsideOcean(boid);

            boid.position += boid.velocity * Time.deltaTime;

            boid.boidObject.transform.position = boid.position;
            boid.boidObject.transform.rotation = Quaternion.Lerp(boid.boidObject.transform.rotation, Quaternion.LookRotation(boid.velocity, Vector3.up), Time.deltaTime * 2f);
        }
    }

    //Rule 1: Boids try to fly towards the centre of mass of neighbouring boids.
    public Vector3 RuleOne(Boid _boid)
    {
        Vector3 perceivedPosition = Vector3.zero;
        foreach (Boid otherBoid in boidParticles)
        {
            if (otherBoid != _boid)
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
            if (otherBoid != _boid && Vector3.Distance(_boid.position, otherBoid.position) < _boid.size + 0.25f)
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
            if (otherBoid != _boid)
            {
                perceivedVelocity += otherBoid.velocity;
            }
        }
        perceivedVelocity /= boidsToSpawn - 1;

        return (perceivedVelocity - _boid.velocity) / 8.0f;
    }

    public void StayInsideOcean(Boid _boid)
    {
        if(_boid.position.x < -oceanSize + 1f)
        {
            _boid.velocity.x += maxSpeed * 2f;
        }
        if (_boid.position.x > oceanSize - 1f)
        {
            _boid.velocity.x -= maxSpeed * 2f;
        }

        if (_boid.position.z < -oceanSize + 1f)
        {
            _boid.velocity.z += maxSpeed * 2f;
        }
        if (_boid.position.z > oceanSize - 1f)
        {
            _boid.velocity.z -= maxSpeed * 2f;
        }

        if (_boid.position.magnitude < islandSize)
        {
            _boid.velocity = Vector3.Scale(_boid.position, new Vector3(1f,0f,1f));
        }

    }

    public class Boid
    {
        public GameObject boidObject;
        public Vector3 position, velocity;
        public float size;

        public Boid(GameObject _gameObject, Vector3 _position, Vector3 _velocity, float _size)
        {
            boidObject = _gameObject;
            position = _position;
            velocity = _velocity;
            size = _size;
        }

    }
}
