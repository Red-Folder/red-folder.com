using Newtonsoft.Json;
using System.Collections.Generic;

namespace Red_Folder.com.Models.Activity
{
    public class BookActivity: IActivity
    {
        [JsonProperty("books")]
        public List<Book> Books;
    }
}
