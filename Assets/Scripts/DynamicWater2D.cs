using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicWater2D : MonoBehaviour
{
	[System.Serializable]
	public struct Bound
	{
		public float top;
		public float right;
		public float bottom;
		public float left;
	}

	[Header("Water Settings")]
	public Bound bound;

	public float minHeight;
	public float maxHeight;
	public float curHeight;

	public bool waterLevelChange = false;
	public int waterLevelDir = -1;
	public float waterLevelVelo = 0.1f;

	public int quality;

	public Material waterMaterial;
	public GameObject splash;

	private Vector3[] vertices;

	private Mesh mesh;

	[Header("Physics Settings")]
	public float springconstant = 0.02f;
	public float damping = 0.1f;
	public float spread = 0.1f;
	public float collisionVelocityFactor = 0.04f;

	float[] velocities;
	float[] accelerations;
	float[] leftDeltas;
	float[] rightDeltas;

	private float timer;

	private void Start()
	{
		InitializePhysics();
		GenerateMesh();
		SetBoxCollider2D();
		curHeight = bound.top - bound.bottom;
	}

	private void InitializePhysics()
	{
		velocities = new float[quality];
		accelerations = new float[quality];
		leftDeltas = new float[quality];
		rightDeltas = new float[quality];
	}

	private void GenerateMesh()
	{
		float range = (bound.right - bound.left) / (quality - 1);
		vertices = new Vector3[quality * 2];

		// generate vertices
		// top vertices
		for (int i = 0; i < quality; i++)
		{
			vertices[i] = new Vector3(bound.left + (i * range), bound.top, 0);
		}
		// bottom vertices
		for (int i = 0; i < quality; i++)
		{
			vertices[i + quality] = new Vector3(bound.left + (i * range), bound.top - maxHeight, 0);
		}

		// generate tris. the algorithm is messed up but works. lol.
		int[] template = new int[6];
		template[0] = quality;
		template[1] = 0;
		template[2] = quality + 1;
		template[3] = 0;
		template[4] = 1;
		template[5] = quality + 1;

		int marker = 0;
		int[] tris = new int[((quality - 1) * 2) * 3];
		for (int i = 0; i < tris.Length; i++)
		{
			tris[i] = template[marker++]++;
			if (marker >= 6) marker = 0;
		}

		// generate mesh
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		if (waterMaterial) meshRenderer.sharedMaterial = waterMaterial;

		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

		mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = tris;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();

		// set up mesh
		meshFilter.mesh = mesh;
	}

	private void SetBoxCollider2D()
	{
		BoxCollider2D col = gameObject.AddComponent<BoxCollider2D>();
		col.isTrigger = true;
	}

	/*
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Plus))
			{
				waterLevelChange = true;
				waterLevelDir = 1;
				Debug.Log("water rise");
			}
			if (Input.GetKeyDown(KeyCode.Minus))
			{
				waterLevelChange = true;
				waterLevelDir = -1;
				Debug.Log("water fall");
			}
		}
	*/

	//changed to FixedUpdate
	private void FixedUpdate()
	{
		// optimization. we don't want to calculate all of this on every update.
		//if(timer <= 0) return;
		timer -= Time.deltaTime;

		// updating physics
		for (int i = 0; i < quality; i++)
		{
			float force = springconstant * (vertices[i].y - bound.top) + velocities[i] * damping;
			accelerations[i] = -force;
			vertices[i].y += velocities[i];
			velocities[i] += accelerations[i];
		}

		for (int i = 0; i < quality; i++)
		{
			if (i > 0)
			{
				leftDeltas[i] = spread * (vertices[i].y - vertices[i - 1].y);
				velocities[i - 1] += leftDeltas[i];
			}
			if (i < quality - 1)
			{
				rightDeltas[i] = spread * (vertices[i].y - vertices[i + 1].y);
				velocities[i + 1] += rightDeltas[i];
			}
		}

		if (waterLevelChange)
		{
			if ((maxHeight > curHeight && waterLevelDir > 0) || (curHeight > minHeight && waterLevelDir < 0))
			{
				for (int i = 0; i < quality; i++)
				{
					vertices[i].y += waterLevelVelo * Random.Range(.5f, 1f) * waterLevelDir;
				}
				transform.position = transform.position + new Vector3(0, waterLevelVelo * waterLevelDir, 0);
				curHeight += waterLevelVelo * waterLevelDir;
			}

			else if ((maxHeight <= curHeight && waterLevelDir > 0) || (curHeight <= minHeight && waterLevelDir < 0))
			{
				waterLevelChange = false;
			}
		}
		// updating mesh
		mesh.vertices = vertices;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			if (col.gameObject.transform.position.y < curHeight + bound.bottom + transform.position.y - 1)
			{
				Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
				Splash(col, rb.velocity.y * collisionVelocityFactor);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
		Splash(col, rb.velocity.y * collisionVelocityFactor);
	}

	public void Splash(Collider2D col, float force)
	{
		timer = 3f;
		float radius = col.bounds.max.x - col.bounds.min.x;
		Vector3 center = new Vector2(col.bounds.center.x, bound.top);

		// instantiate splash particle
		//GameObject splashGO = Instantiate(splash, new Vector3(center.x, center.y+transform.position.y, 0), Quaternion.Euler(0,0,60));
		//Destroy(splashGO, 1f);

		// applying physics
		for (int i = 0; i < quality; i++)
		{
			if (PointInsideCircle(vertices[i], center, radius))
			{
				velocities[i] = force;
			}
		}
	}

	private bool PointInsideCircle(Vector2 point, Vector2 center, float radius)
	{
		return Vector2.Distance(point, center) < radius;
	}
}
