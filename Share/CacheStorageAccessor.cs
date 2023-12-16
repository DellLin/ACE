using Microsoft.JSInterop;
using Share.Provider.DbModels;

namespace Share
{
    public class CacheStorageAccessor : IAsyncDisposable
    {
        private Lazy<IJSObjectReference> _accessorJsRef = new();
        private readonly IJSRuntime _jsRuntime;

        public CacheStorageAccessor(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        private async Task WaitForReference()
        {
            if (_accessorJsRef.IsValueCreated is false)
            {
                _accessorJsRef = new(await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "/js/CacheStorageAccessor.js"));
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_accessorJsRef.IsValueCreated)
            {
                await _accessorJsRef.Value.DisposeAsync();
            }
        }
        public async Task StoreAsync(IReadOnlyCollection<Question> questions)
        {
            await WaitForReference();

            await _accessorJsRef.Value.InvokeVoidAsync("store", questions);
        }

        public async Task<IReadOnlyCollection<Question>> GetAsync()
        {
            await WaitForReference();
            var result = await _accessorJsRef.Value.InvokeAsync<IReadOnlyCollection<Question>>("get");

            return result;
        }

        public async Task RemoveAllAsync()
        {
            await WaitForReference();
            await _accessorJsRef.Value.InvokeVoidAsync("removeAll");
        }

    }
}
