using EasyLearn.VM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class VerbPrepositionListVM : ViewModel
    {
        private int listId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public VerbPrepositionListVM(string name, string description, int listId)
        {
            this.Name = name;
            this.Description = description;
            this.listId = listId;
        }
    }
}
