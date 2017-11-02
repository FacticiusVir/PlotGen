using Keeper.BacktraQ;
using UnityEngine;

public class Map : MonoBehaviour
{
    private VarGrid<MapTile> mapGrid;

    public Transform[] TilePrefabs;

    private static readonly int[,] mapLayout = new int[,]
    {
        { 0, 0, 0, 0, 1, 0, 4, 4, 2, 0 },
        { 0, 0, 0, 0, 1, 0, 4, 4, 4, 0 },
        { 0, 0, 0, 0, 1, 0, 2, 4, 2, 0 },
        { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
        { 0, 0, 3, 0, 0, 0, 0, 3, 0, 0 },
        { 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 }
    };

    // Use this for initialization
    void Start()
    {
        this.mapGrid = new VarGrid<MapTile>(10, 10);

        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                this.mapGrid.XYth(x, y, new MapTile() { TileIndex = mapLayout[9 - y, x] }).Succeeds();
            }
        }

        var mapTilesVar = new Var<MapTile[,]>();
        
        this.mapGrid.ToArray(mapTilesVar).Succeeds();

        var mapTiles = mapTilesVar.Value;

        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                var childTile = Instantiate(this.TilePrefabs[mapTiles[x, y].TileIndex]);

                childTile.parent = this.transform;

                childTile.localPosition = new Vector3(x, 0, y);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
