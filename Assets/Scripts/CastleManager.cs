using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleManager : MonoBehaviour
{
    

    public float cell_size = 2.0f;
    
    // Use this for initialization

    public int Columns;
    public int Rows;

    [Tooltip("Distancias entre celdas")]
    public float distanceX, distanceY;

    [Tooltip("Posiciones de castillos")]
    public GameObject castlePosition;//Usamos un gameobject para indicar dónde se crean los objetos

    [Tooltip("Centro del grid")]
    public Transform spawnPoint;

    [Tooltip("Cantidad de castillos completos con los que se acabaría el juego")]
    public int castleToGameover;

    [Tooltip("Cantidad de castillos completados en el momento")]
    private int completedCastles;

	public float TimeToConstruct;

	private float timeConstruct = 0;

    public int numCastlesDestroyed=0;

    public int totalShells = 0;

    void Start()
    {
        //CreateGrid();


    }

    // Update is called once per frame
    void Update()
    {

		//Cada x 
		timeConstruct += Time.deltaTime;

		if (timeConstruct > TimeToConstruct)//Cuando pasa X tiempo se llama a un niño
		{

			Vector2 pos = GetRandomPosition();
            //Si esta libre la posicion lanzamos al niño, cuando el collider del niño entre en el del castillo se empieza a construir

            GameObject newEmptyGameObject = GameObject.FindGameObjectWithTag("Castillos");
			CastilloArena[] castillos = newEmptyGameObject.GetComponentsInChildren<CastilloArena>();

			foreach (CastilloArena c in castillos)
			{
				if (c.posInGrid == pos && !c.EnConstruccion && !c.volviendo)
				{
					c.EnConstruccion = true;
					c.GetComponentInChildren<Kids>().mover = true;
					//c.GetComponentInChildren<SpriteRenderer>().enabled = true;
				}
			}
			timeConstruct = 0;
		}
	}

    /// <summary>
    /// Crea la matriz de posiciones para los niños
    /// </summary>
    public void CreateGrid()
    {
        Destroy(GameObject.Find("Grid"));
        GameObject newEmptyGameObject = new GameObject("Grid");
        // following line is probably not neccessary
        newEmptyGameObject.transform.position = Vector3.zero;
		newEmptyGameObject.gameObject.tag = "Castillos";

		// some math to find the most left and bottom offset
		float offsetLeft = (-Columns / 2f) * distanceX + distanceX / 2f;
        float offsetBottom = (-Rows / 2f) * distanceY + distanceY / 2f;
        // set it as first spawn castlePosition (z=1 because you had it in your script)
        Vector3 nextPosition = new Vector3(offsetLeft, offsetBottom, 1f);

        for (int y = 0; y < Rows; y++)
        {
            for (int x = 0; x < Columns; x++)
            {
                GameObject clone = Instantiate(castlePosition, nextPosition, Quaternion.identity) as GameObject;
                clone.transform.parent = newEmptyGameObject.transform;
                // add x distance
                nextPosition.x += distanceX;
				if (clone.GetComponent<CastilloArena>() != null)
				{
					clone.GetComponent<CastilloArena>().SetPositionInGrid(x, y);
					clone.GetComponent<CastilloArena>().EnConstruccion = false;
				}

			}
            // reset x castlePosition and add y distance
            nextPosition.x = offsetLeft;
            nextPosition.y += distanceY;
        }
        // move the whole grid to the spawnPoint, if there is one
        if (spawnPoint != null)
            newEmptyGameObject.transform.position = spawnPoint.position;
    }


	public Vector2 GetRandomPosition()
	{
		Vector2 position = new Vector2(Random.Range(0, Columns), Random.Range(0, Rows));

		return position;
	}
    
}
