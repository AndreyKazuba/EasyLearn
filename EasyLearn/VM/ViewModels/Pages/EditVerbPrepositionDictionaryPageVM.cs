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
    public class EditVerbPrepositionDictionaryPageVM
    {
        private int currentVerbPrepositionListId;
        private readonly IVerbPrepositionRepository verbPrepositionRepository;
        private readonly IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository;

        public ObservableCollection<VerbPrepositionView> VerbPrepositions { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public EditVerbPrepositionDictionaryPageVM(IVerbPrepositionRepository verbPrepositionsRepository, IVerbPrepositionDictionaryRepository verbPrepositionListsRepository)
        {
            this.verbPrepositionRepository = verbPrepositionsRepository;
            this.verbPrepositionDictionaryRepository = verbPrepositionListsRepository;
        }

        public async Task SetAsCurrentDictionary(int dictionaryId)
        {
            this.currentVerbPrepositionListId = dictionaryId;
            VerbPrepositionDictionnary verbPrepositionList = await verbPrepositionDictionaryRepository.GetVerbPrepositionDictionaryAsync(currentVerbPrepositionListId);
            this.Name = verbPrepositionList.Name;
            this.Description = verbPrepositionList.Description;
            this.VerbPrepositions = new ObservableCollection<VerbPrepositionView>(verbPrepositionList.VerbPrepositions.Select(verbPreposition => new VerbPrepositionView(new VerbPrepositionVM(verbPreposition))));
        }
    }
}
