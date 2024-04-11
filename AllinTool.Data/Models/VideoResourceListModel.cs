using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinTool.Data.Models
    {
    public class VideoResourceListModel
        {
        public Guid Id { get; set; }
        public Uri ResourceUri { get; set; }
        public string Title { get; set; }
        public string Container { get; set; }
        public string Resolution { get; set; }
        public string FormatTitle { get; set; }
        public string Duration { get; set; }
        public string CreationDate { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        }
    }
