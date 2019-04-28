using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public float Speed;
    public Transform destination = null;

    private Animator animator;
    private Rigidbody rb;
    private float animationDuration;
    public bool standAlone;
    public bool specialityActivated = false;
    public bool animationTriggered = false;
    public bool animationFinished = false;
    public Type type;

    [SerializeField]
    private ParticleSystem tankFlare;
    private bool soundPlayed = false;

    public enum Type
    {
        Blue = 0,
        Black = 1,
        Green = 2,
        Red = 3
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.mass = Random.Range(0.15f, 0.25f);
    }

    public void ActivateSpeciality()
    {
        specialityActivated = true;
    }

    public void AssignDestinationTransform(Transform _destination)
    {
        destination = _destination;
    }

    private void Update()
    {
        if (destination)
        {
            Vector3 transformPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
            Vector3 destionationPos = new Vector3(destination.position.x, 0.0f, destination.position.z);
            if (!specialityActivated)
            {
                if (Vector3.Distance(transformPos, destionationPos) > 0.01f)
                {
                    Quaternion rotation = Quaternion.LookRotation((destionationPos - transformPos).normalized, Vector3.up);
                    transform.rotation = Quaternion.Euler(0.0f, rotation.eulerAngles.y, 0.0f);
                }
            }
            else
            {
                if (Vector3.Distance(transformPos, destionationPos) > 0.1f)
                {
                    if (type == Type.Black)
                    {
                        animator.SetTrigger(AnimationConstants.doCharge);
                        if (tankFlare != null)
                        {
                            //tankFlare.gameObject.SetActive(true);
                            tankFlare.Play();
                        }
                    }
                    Quaternion rotation = Quaternion.LookRotation((destionationPos - transformPos).normalized, Vector3.up);
                    transform.rotation = Quaternion.Euler(0.0f, rotation.eulerAngles.y, 0.0f);
                }
                else
                {
                    transform.position = new Vector3(destination.position.x, 0.25f + destination.position.y, destination.position.z);
                    
                    if(type != Type.Black && !GetComponent<AudioSource>().isPlaying && !soundPlayed)
                    {
                        GetComponent<AudioSource>().Play();
                        soundPlayed = true;
                    }

                    if (type == Type.Green)
                    {
                        if (!animationTriggered)
                        {
                            FindObjectOfType<UIBehaviour>().EggLost(Egg.Type.Green);
                            animator.SetTrigger(AnimationConstants.beTall);
                            animationTriggered = true;
                        }
                        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BeTall"))
                        {
                            animationFinished = true;
                            gameObject.layer = 0;
                            GetComponent<CapsuleCollider>().isTrigger = true;
                            GetComponent<CapsuleCollider>().radius = 1.0f;
                            Destroy(rb);
                            rb = null;
                        }
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (rb)
        {
            animator.SetFloat("Speed", new Vector2(rb.velocity.x, rb.velocity.z).sqrMagnitude);
            if (destination)
            {
                Vector3 force = (destination.position - transform.position).normalized * Speed;
                rb.AddForce(new Vector3(force.x, 0.0f, force.z), ForceMode.Force);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Egg collidedEgg = collision.gameObject.GetComponent<Egg>();
        EggArmy army = FindObjectOfType<EggArmy>();
        if (collidedEgg && army)
        {
            if (collidedEgg.standAlone)
            {
                army.AddEggie(collidedEgg);
            }
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        Egg collidedEgg = collision.gameObject.GetComponent<Egg>();
        if (collidedEgg && collidedEgg.specialityActivated && collidedEgg.type == Type.Green && collidedEgg.animationFinished)
        {
            // TODO change the movement of the egg
            float dist = Vector3.Distance(transform.position, collidedEgg.transform.position) / collidedEgg.GetComponent<CapsuleCollider>().radius;
            if (dist < 1.0f)
            {
                dist = 1 - dist;
                rb.AddForce(new Vector3(0.0f, Mathf.SmoothStep(0.0f, 20000.0f, dist), 0.0f));
            }
            //EggArmy army = FindObjectOfType<EggArmy>();
            //if (army)
            //{
            //    foreach (Egg egg in army.Eggs)
            //    {
            //        egg.rb.AddForce(new Vector3(0.0f, 10.0f, 0.0f));
            //    }
            //}
        }
    }
}
