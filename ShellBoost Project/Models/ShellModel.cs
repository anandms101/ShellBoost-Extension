using Newtonsoft.Json;

namespace ShellBoost_Project
{
    public class ShellFileModel
    {
        [JsonProperty("FileId")]
        public long FileId { get; set; }

        [JsonProperty("Path")]
        public string Path { get; set; }

        [JsonProperty("ParentId")]
        public long ParentId { get; set; }

        [JsonProperty("FileType")]
        public string FileType { get; set; }
    }
}
