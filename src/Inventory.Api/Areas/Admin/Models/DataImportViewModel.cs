using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Athene.Inventory.Web.Areas.Admin.Models
{
    public class DataImportDto
    {
        [Required]
        public string SourceType { get; set; }
        [Required]
        public string DataType { get; set; }
        [Required]
        public IFormFile UploadFile { get; set; }
        public SelectList DataTypes { get; set; }
        public SelectList SourceTypes { get; set; }
    }
}