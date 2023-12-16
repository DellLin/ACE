using Google.Cloud.Firestore;
using Share.Provider.DbModels;

namespace Share.Provider;
public class FirestoreProvider
{
    private readonly FirestoreDb _fireStoreDb = null!;

    public FirestoreProvider(FirestoreDb fireStoreDb)
    {
        _fireStoreDb = fireStoreDb;
    }

    public async Task AddOrUpdate<T>(T entity, CancellationToken ct = default) where T : IFirebaseEntity
    {
        var document = _fireStoreDb.Collection(typeof(T).Name).Document(entity.Id);
        await document.SetAsync(entity, cancellationToken: ct);
    }

    public async Task<T> Get<T>(string id, CancellationToken ct = default) where T : IFirebaseEntity
    {
        var document = _fireStoreDb.Collection(typeof(T).Name).Document(id);
        var snapshot = await document.GetSnapshotAsync(ct);
        return snapshot.ConvertTo<T>();
    }

    public async Task<IReadOnlyCollection<T>> GetAll<T>(CancellationToken ct = default) where T : IFirebaseEntity
    {
        var collection = _fireStoreDb.Collection(typeof(T).Name);
        var snapshot = await collection.GetSnapshotAsync(ct);
        return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
    }

    public async Task<IReadOnlyCollection<T>> WhereEqualTo<T, VT>(string fieldPath, VT value, CancellationToken ct = default) where T : IFirebaseEntity
    {
        return await GetList<T>(_fireStoreDb.Collection(typeof(T).Name).WhereEqualTo(fieldPath, value), ct);
    }
    public async Task<IReadOnlyCollection<T>> WhereIn<T, VT>(string fieldPath, IEnumerable<VT> value, CancellationToken ct = default) where T : IFirebaseEntity
    {
        return await GetList<T>(_fireStoreDb.Collection(typeof(T).Name).WhereIn(fieldPath, value), ct);
    }
    public async Task<int> GetAllCount<T>(CancellationToken ct = default) where T : IFirebaseEntity
    {
        var collection = _fireStoreDb.Collection(typeof(T).Name);
        return (int?)(await collection.Count().GetSnapshotAsync(ct)).Count ?? 0;
    }
    private static async Task<IReadOnlyCollection<T>> GetList<T>(Query query, CancellationToken ct = default) where T : IFirebaseEntity
    {
        var snapshot = await query.GetSnapshotAsync(ct);
        return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
    }
}