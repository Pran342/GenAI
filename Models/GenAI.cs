using System.ComponentModel.DataAnnotations;
using System.Net;

namespace GenAITech.Models
{
    public class GenAI
    {
        public int Id { get; set; }
       public string GenAIName { get; set; }
        public string Summary { get; set; }
        public string ImageFileName { get; set; }
        public string AnchorLink { get; set; }
        public int Like {  get; set; }
        
     
    }
}
