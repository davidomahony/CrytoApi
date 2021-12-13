
namespace CrytpoInfo.Models
{
    public class TwitterUserGetAccountIdResponse
    {
        public DataObject Data { get; set; }

        public class DataObject
        {

            public string Id { get; set; }

            public string Name { get; set; }

            public string Username { get; set; }
        }
    }
}
