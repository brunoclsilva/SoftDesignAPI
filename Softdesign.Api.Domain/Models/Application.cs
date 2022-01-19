using System;
using System.ComponentModel.DataAnnotations;

namespace Softdesign.API.Domain.Models
{
    public class Application
    {
        [Required]
        public int ApplicationId { get; set; }
        public string Url { get; set; }
        public string PathLocal { get; set; }
        public bool DebuggingMode { get; set; }
    }
}
