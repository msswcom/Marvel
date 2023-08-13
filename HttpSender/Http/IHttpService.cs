
namespace HttpSender
{
    public interface IHttpService
    {
        public Task<string> GetAsync(string url,
            Dictionary<string, string>? headers = null,
            Dictionary<string, string>? cookies = null);
        public Task<string> PostAsync<Input>(string url, Input? input,
            Dictionary<string, string>? headers = null,
            Dictionary<string, string>? cookies = null);
        public Task<string> PutAsync<Input>(string url, Input? input,
            Dictionary<string, string>? headers = null,
            Dictionary<string, string>? cookies = null);
        public Task<string> DeleteAsync(string url,
            Dictionary<string, string>? headers = null,
            Dictionary<string, string>? cookies = null);
    }
}
