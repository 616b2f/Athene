using System;

namespace Athene.Inventory.Web.ViewModels
{
    public class BoardOfEducationViewModel
    {
        private BoardOfEducationViewModel() {}
        public BoardOfEducationViewModel(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name parameter is null");
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
