using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;


	private Vector3 zeroAc;
	private Vector3 curAc;
	private float sensH = 10f;
	private float sensV = 10f;
	private float smooth = 0.5f;
	private float GetAxisH = 0f;
	private float GetAxisV = 0f;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";

		ResetAxes();
    }

	void ResetAxes()
	{
		zeroAc = Input.acceleration;
		curAc = Vector3.zero;
	}

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // float moveHorizontal = Input.GetAxis("Horizontal");
        // float moveVertical = Input.GetAxis("Vertical");

		curAc = Vector3.Lerp(curAc, Input.acceleration-zeroAc, Time.deltaTime/smooth);
		float moveVertical = Mathf.Clamp(curAc.y * sensV, -1, 1);
		float moveHorizontal = Mathf.Clamp(curAc.x * sensH, -1, 1);

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText ()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winText.text = "You Win!";
        }
    }
}
