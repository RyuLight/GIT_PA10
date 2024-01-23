using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Animation thisAnimation;
    private Rigidbody rb;

    public GameManager GM;

    private float yMin = -5.5f, yMax = 3.5f;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = this.GetComponent<Rigidbody>();
        thisAnimation = GetComponent<Animation>();
        thisAnimation["Flap_Legacy"].speed = 3;
    }

    void Update()
    {
        float tempRotation = this.transform.rotation.z;
        transform.position = new Vector3(0, Mathf.Clamp(transform.position.y, yMin, yMax), 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(0, 5, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            thisAnimation.Play();
        }

        if (this.transform.position.y <= -3.6f)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Score"))
        {
            GM.UpdateScore(1);
        }
    }
}
