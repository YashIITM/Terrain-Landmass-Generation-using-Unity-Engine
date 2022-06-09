public class EndlessTerrain : MonoBehaviour
{
	//Variable to denote how far the viewer can see: 'maxViewDst'
	public const float maxViewDst = 450;
	//Reference to our viewer's transform:
	public Transform viewer;

	//Vector to store the position of the viewer: static to that we can access it from other classes later on
	public static Vector2 viewerPosition;

	int chunkSize;
	int chunksVisibleInViewDst;

	//Creating our dictionary to contain the coordinates of the terrain chunks that we want to instantiate
	Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
	List<TerrainChunk> terrainChunksVisibleLastUpdate = new List<TerrainChunk>();

	//We will make a start method:
	void Start()
	{	
		//since the mapChunkSize is 241, but the actual chunkSize of the map is one less:
		chunkSize = MapGenerator.mapChunkSize - 1;
		//number of visible chunks:
		chunksVisibleInViewDst = Mathf.RoundToInt(maxViewDst / chunkSize);
	}
	//After doing the above, we will wield our game into grid with each block being 240x240
	//So single terrain chunk will fit into a single block
	//'chunksVisibleInViewDst' determines the number of terrain chunks around the viewer that we will instantiate



	
	void Update()
	{	
		//We want to update the viewer position and the visible chunks
		viewerPosition = new Vector2(viewer.position.x, viewer.position.z);
		UpdateVisibleChunks();
	}

	void UpdateVisibleChunks()
	{

		for (int i = 0; i < terrainChunksVisibleLastUpdate.Count; i++)
		{
			terrainChunksVisibleLastUpdate[i].SetVisible(false);
		}
		terrainChunksVisibleLastUpdate.Clear();

		//Lets get the coordinate of the chunk that the viwer is standing on:
		int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
		int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);

		//Now we will loop through the surrounding visible chunks:
		for (int yOffset = -chunksVisibleInViewDst; yOffset <= chunksVisibleInViewDst; yOffset++)
		{
			for (int xOffset = -chunksVisibleInViewDst; xOffset <= chunksVisibleInViewDst; xOffset++)
			{
				Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

				if (terrainChunkDictionary.ContainsKey(viewedChunkCoord))
				{
					terrainChunkDictionary[viewedChunkCoord].UpdateTerrainChunk();
					if (terrainChunkDictionary[viewedChunkCoord].IsVisible())
					{
						terrainChunksVisibleLastUpdate.Add(terrainChunkDictionary[viewedChunkCoord]);
					}
				}
				else
				{	//if terrainChunkDictionary doesn't contain the key:
					//we want to create a new terrain chunk
					terrainChunkDictionary.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord, chunkSize, transform));
				}

			}
		}
	}

	//We want to maintain a dictionary of all of the coordinates and the terrain chunks so that we can create duplicates
	public class TerrainChunk
	{

		GameObject meshObject;
		Vector2 position;
		Bounds bounds;

		
		public TerrainChunk(Vector2 coord, int size, Transform parent)
		{
			position = coord * size;
			bounds = new Bounds(position, Vector2.one * size);
			Vector3 positionV3 = new Vector3(position.x, 0, position.y);

			meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
			meshObject.transform.position = positionV3;
			meshObject.transform.localScale = Vector3.one * size / 10f;
			meshObject.transform.parent = parent;
			SetVisible(false);
		}
		//method to tell terrain chunk to update itself
		//going to find the point in its perimeter that is closest to the viewer's position
		//will find the distance between that point and the viewer
		//if that is less than the maximum view distance, make sure meshObject is enabled
		//if not disable the meshObject
		public void UpdateTerrainChunk()
		{
			float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
			bool visible = viewerDstFromNearestEdge <= maxViewDst;
			SetVisible(visible);
		}

		public void SetVisible(bool visible)
		{
			meshObject.SetActive(visible);
		}

		public bool IsVisible()
		{
			return meshObject.activeSelf;
		}

	}
}
