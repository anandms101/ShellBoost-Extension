using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ShellBoost_Project
{
    public class APICalls
    {
        public  int maxItemsToDisplay { set; get; }

        public  ShellFileModel[] childItemsDetails;
        public  long currentFileId { set; get; }
        public long currentUpdateFileId { set; get; }
        public async Task RunAsyncGetAllDrives()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                try
                {
                    var response = await client.GetAsync("api/filemodel/drives");
                    if (response.IsSuccessStatusCode)
                    {
                        string filesJson = await response.Content.ReadAsStringAsync(); //Getting response  

                        if (filesJson != " ")
                        {
                            childItemsDetails = new JavaScriptSerializer().Deserialize<ShellFileModel[]>(filesJson);
                            maxItemsToDisplay = childItemsDetails.Length;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
        public async Task RunAsyncGetFileIdByFilePath(string path)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                try
                {
                    var response = await client.GetAsync("api/filemodel/getfilebypath/" + path.Replace(@"\", ""));
                    if (response.IsSuccessStatusCode)
                    {
                        string filesJson = await response.Content.ReadAsStringAsync(); //Getting response  

                        if (filesJson != " ")
                        {
                            currentFileId = new JavaScriptSerializer().Deserialize<ShellFileModel[]>(filesJson)[0].FileId;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
        public async Task RunAsyncGetFirstChildById(long id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                try
                {
                    var response = await client.GetAsync("api/filemodel/getfirstlevelchilds/" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        string filesJson = await response.Content.ReadAsStringAsync(); //Getting response  

                        if (filesJson != " ")
                        {
                            childItemsDetails = new JavaScriptSerializer().Deserialize<ShellFileModel[]>(filesJson);
                            maxItemsToDisplay = childItemsDetails.Length;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
        public async Task RunAsyncDeleteByFileId(long id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                try
                {
                    var response = await client.DeleteAsync("api/filemodel/" + id);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
        public async Task RunAsyncRenameByFileId(long id, string name)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                try
                {
                    ShellFileModel putObj = new ShellFileModel();
                    putObj.Path = name;
                    var response = await client.PutAsJsonAsync("api/filemodel/" + id, putObj);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
    }
}
