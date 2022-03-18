using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.UI.CustomControls;
using EasyLearn.VM.ViewModels.CustomControls;

namespace EasyLearn.VM.ViewModels.Pages
{
    public class EditWordPrepositionsListPageVM
    {
        private int currentVerbPrepositionListId;
        private readonly IVerbPrepositionsRepository verbPrepositionsRepository;
        private readonly IVerbPrepositionDictionaryRepository verbPrepositionListsRepository;

        public ObservableCollection<VerbPrepositionView> VerbPrepositions { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public EditWordPrepositionsListPageVM(IVerbPrepositionsRepository verbPrepositionsRepository, IVerbPrepositionDictionaryRepository verbPrepositionListsRepository)
        {
            this.verbPrepositionsRepository = verbPrepositionsRepository;
            this.verbPrepositionListsRepository = verbPrepositionListsRepository;
        }

        public async Task SetCurrentList(int listId)
        {
            this.currentVerbPrepositionListId = listId;
            VerbPrepositionDictionnary verbPrepositionList = await verbPrepositionListsRepository.GetVerbPrepositionDictionaryAsync(currentVerbPrepositionListId);
            this.Name = verbPrepositionList.Name;
            this.Description = verbPrepositionList.Description;
            this.VerbPrepositions = new ObservableCollection<VerbPrepositionView>(verbPrepositionList.VerbPrepositions.Select(verbPreposition => new VerbPrepositionView(new VerbPrepositionVM(verbPreposition))));
        }
    }
}
