using Google.Cloud.Firestore;

namespace Share.Provider.DbModels
{
    [FirestoreData]
    public class Question : IFirebaseEntity
    {
        [FirestoreProperty("_id")]
        public string Id { get; set; }
        [FirestoreProperty]
        public int? Number { get; set; }
        [FirestoreProperty]
        public string? Type { get; set; }

        [FirestoreProperty]
        public string? Content { get; set; }
        [FirestoreProperty]
        public string? Option1 { get; set; }
        [FirestoreProperty]
        public string? Option2 { get; set; }
        [FirestoreProperty]
        public string? Option3 { get; set; }
        [FirestoreProperty]
        public string? Option4 { get; set; }
        [FirestoreProperty]
        public string? Option5 { get; set; }
        [FirestoreProperty]
        public string? Answer { get; set; }
        public string? CurrentAnswer { get; set; }
        public string[] MultiCurrentAnswer { get; set; }
        public Question()
        {
            Id = Guid.NewGuid().ToString("N");

        }
    }
}
