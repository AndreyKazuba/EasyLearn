using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLearn.VM.Core;
using EasyLearn.Data.Repositories.Interfaces;

namespace EasyLearn.VM.ViewModels.Pages
{
    public class ListsPageVM : ViewModel
    {
        private readonly IEasyLearnUsersRerository usersRerository;
        private readonly ICommonWordListsRepository commonWordListsRepository;
        private readonly IVerbPrepositionListsRepository verbPrepositionListsRepository;
        private int currentUserId;

        public ListsPageVM
            (IEasyLearnUsersRerository usersRerository,
            ICommonWordListsRepository commonWordListsRepository,
            IVerbPrepositionListsRepository verbPrepositionListsRepository)
        {
            this.usersRerository = usersRerository;
            this.commonWordListsRepository = commonWordListsRepository;
            this.verbPrepositionListsRepository = verbPrepositionListsRepository;
            UpdateCurrentUserId();
        }

        private void UpdateCurrentUserId()
        {
            this.currentUserId = usersRerository.GetCurrentUser().Id;
        }
    }
}
