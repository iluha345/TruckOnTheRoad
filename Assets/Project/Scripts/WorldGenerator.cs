using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private int _enemiesCount;
    [SerializeField] private int _enemiesInOneChunk;

    [SerializeField] private int _enemiesDistansOnRoad;

    [SerializeField] private Chunk _finalGroundPrefab;
    [SerializeField] private Chunk _groundPrefab;
    [SerializeField] private Enemy _enemyPrefab;

    [SerializeField] private Transform _spawnPoint;

    private Renderer _groundRenderer;
    private Collider _enemyCollider;

    private float _groundZSize;//max size in length
    private void Start()
    {
        _groundRenderer = _groundPrefab.GetComponent<Renderer>();
        _enemyCollider = _enemyPrefab.GetComponent<Collider>();
        _groundZSize = _groundRenderer.bounds.size.z;

        GenerateLevel(_enemiesCount, _enemiesInOneChunk);
    }
    private void GenerateLevel(int enemies, int chunks)
    {
        int chunksNeed = _enemiesCount / _enemiesInOneChunk;

        for (int i = 0; i < chunksNeed; i++) 
        {
            Chunk chunk = SpawnNextChunk();
            SpawnEnemies(chunk);
            _spawnPoint.transform.position += new Vector3(0, 0, _groundZSize);
        }
        Instantiate(_finalGroundPrefab, _spawnPoint.position, Quaternion.Euler(-90, 0, 0), transform);

        Debug.Log("Chunks Spawned!");
        Debug.Log("Enemies Spawned!");
    }
    public Chunk SpawnNextChunk()
    {
        Chunk chunk = Instantiate(_groundPrefab, _spawnPoint.position, Quaternion.Euler(-90,0,0), transform);
        return chunk;
    }
    public void SpawnEnemies(Chunk chunkToSpawn)
    {

        Vector3 area = new Vector3(_enemiesDistansOnRoad, 0, _groundZSize);

        for (int i = 0; i < _enemiesInOneChunk; i++)
        {
            Vector3 spawnPos = new Vector3(chunkToSpawn.transform.position.x + Random.Range(-area.x, area.x),_enemyCollider.bounds.size.y, Random.Range(chunkToSpawn.transform.position.z - area.z / 2, chunkToSpawn.transform.position.z + area.z / 2));
            Enemy enemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.Euler(0,Random.Range(0,360),0));
            enemy.GetComponent<EnemyMovement>().SetTarget(Player.Instance.transform);
            enemy.transform.SetParent(chunkToSpawn.transform);
        }
    }
}