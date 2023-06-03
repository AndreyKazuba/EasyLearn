using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EasyLearn.Data;
using EasyLearn.Data.Repositories.Implementations;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Data.Models;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Enums;
using System.Text.RegularExpressions;

#pragma warning disable
namespace EasyLearn.Autofiller
{
    public class Program
    {
        private static IServiceProvider serviceProvider;

        private static IEnumerable<EasyLearnUser>? users;
        private static EasyLearnUser? selectedUser;

        private static IEnumerable<CommonDictionary>? commonDictionaries;
        private static CommonDictionary? selectedCommonDictionary;

        private static UnitType selectedRussianUnitType;
        private static UnitType selectedEnglishUnitType;

        static void Main(string[] args)
        {
            if (args.Length == 0 || string.IsNullOrWhiteSpace(args[0]))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Необходимо открыть файл");
                Console.ResetColor();
                return;
            }


            serviceProvider = ConfigureServices();
            ProcessUsersPhase();
            ProcessDictionaryPhase();
            ProcessUnitTypesPhase();

            string filePath = args[0];
            string text = null;
            using (StreamReader reader = new StreamReader(filePath))
            {
                text = reader.ReadToEnd();
            }

            IEnumerable<Record> records = GetRecords(text.Substring(0, text.IndexOf("#end")) .Split('\n'));
            IEnumerable<ConfirmationRecord> confirmationRecords = GetConfirmationRecords(records);

            RequestConfirmation(confirmationRecords);
            CreateRelations(confirmationRecords).GetAwaiter().GetResult();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Записи успешно добавлены");
            Console.ResetColor();

            Console.ReadKey();
        }

        private static void ProcessUsersPhase()
        {
            IEasyLearnUserRepository userRepository = GetService<IEasyLearnUserRepository>();
            users = userRepository.GetAllUsers();

            bool unsuccess = true;
            while (unsuccess)
            {
                Console.WriteLine("Выберете номер пользователя...");
                foreach (EasyLearnUser user in users)
                {
                    Console.WriteLine($"\t{user.Id} - {user.Name.Normalize()}");
                }

                string? value = Console.ReadLine();
                bool parsed = Int32.TryParse(value, out int id);

                if (!parsed)
                    continue;

                if (users.Any(user => user.Id == id))
                {
                    selectedUser = users.First(user => user.Id == id);
                    unsuccess = false;
                }
            }

            Console.WriteLine();
        }

        private static void ProcessDictionaryPhase()
        {
            ICommonDictionaryRepository dictionaryRepository = GetService<ICommonDictionaryRepository>();
            commonDictionaries = dictionaryRepository.GetUsersCommonDictionaries(selectedUser.Id);

            bool unsuccess = true;
            while (unsuccess)
            {
                Console.WriteLine("Введите номер словаря...");
                foreach (CommonDictionary dictionary in commonDictionaries)
                {
                    Console.WriteLine($"\t{dictionary.Id} - {dictionary.Name.Normalize()}");
                }

                string? value = Console.ReadLine();
                bool parsed = Int32.TryParse(value, out int id);

                if (!parsed)
                    continue;

                if (commonDictionaries.Any(dictionary => dictionary.Id == id))
                {
                    selectedCommonDictionary = commonDictionaries.First(dictionary => dictionary.Id == id);
                    unsuccess = false;
                }
            }

            Console.WriteLine();
        }

        private static void ProcessUnitTypesPhase()
        {
            bool unsuccess = true;
            while (unsuccess)
            {
                Console.WriteLine("Выберите номер типа для русских слов...");
                List<UnitType> russianUnitTypes = new List<UnitType>
                {
                    UnitType.Noun,
                    UnitType.Verb,
                    UnitType.Adjective,
                    UnitType.Adverb,
                };
                foreach (UnitType type in russianUnitTypes)
                {
                    Console.WriteLine($"\t{(int)type} - {EnumHelper.GetRussianValue(type)}");
                }

                string? value = Console.ReadLine();
                bool parsed = Int32.TryParse(value, out int typeId);

                if (!parsed)
                    continue;

                if (russianUnitTypes.Any(type => (int)type == typeId))
                {
                    selectedRussianUnitType = (UnitType)typeId;
                    unsuccess = false;
                }
            }

            Console.WriteLine();

            unsuccess = true;
            while (unsuccess)
            {
                Console.WriteLine("Выберите номер типа для английских слов...");
                List<UnitType> englishUnitTypes = new List<UnitType>
                {
                    UnitType.Noun,
                    UnitType.Verb,
                    UnitType.Adjective,
                    UnitType.Adverb,
                    UnitType.PhraseVerb
                };
                foreach (UnitType type in englishUnitTypes)
                {
                    Console.WriteLine($"\t{(int)type} - {EnumHelper.GetRussianValue(type)}");
                }

                string? value = Console.ReadLine();
                bool parsed = Int32.TryParse(value, out int typeId);

                if (!parsed)
                    continue;

                if (englishUnitTypes.Any(type => (int)type == typeId))
                {
                    selectedEnglishUnitType = (UnitType)typeId;
                    unsuccess = false;
                }
            }

            Console.WriteLine();
        }

        private static IEnumerable<Record> GetRecords(string[] rows)
        {
            return rows.Select(row => TryParseRecord(row)).Where(record => record != null);
        }

        private static Record? TryParseRecord(string row)
        {
            int separatorIndex = row.IndexOf('=');

            if (separatorIndex == -1)
                return null;

            string russianWord = row.Substring(0, separatorIndex);
            string englishWord = row.Substring(separatorIndex + 1);

            if (string.IsNullOrWhiteSpace(englishWord) || string.IsNullOrWhiteSpace(russianWord))
                return null;

            return new Record
            {
                EnglishWord = englishWord.Prepare(),
                RussianWord = russianWord.Prepare(),
            };
        }

        private static IEnumerable<ConfirmationRecord> GetConfirmationRecords(IEnumerable<Record> records)
        {
            List<ConfirmationRecord> confirmationRecords = new List<ConfirmationRecord>();
            foreach (Record record in records)
            {
                ICommonRelationRepository commonRelationRepository = GetService<ICommonRelationRepository>();
                bool exist = commonRelationRepository.IsCommonRelationExist(record.RussianWord, selectedRussianUnitType, record.EnglishWord, selectedEnglishUnitType, selectedCommonDictionary.Id);
                confirmationRecords.Add(new ConfirmationRecord
                {
                    RussianWord = record.RussianWord,
                    EnglishWord = record.EnglishWord,
                    AlreadyExist = exist,
                    WillBeAdded = !exist,
                });
            }

            return confirmationRecords;
        }

        private static void RequestConfirmation(IEnumerable<ConfirmationRecord> confirmationRecords)
        {
            Console.WriteLine("Ожидаемый результат:");
            foreach (ConfirmationRecord confirmationRecord in confirmationRecords)
            {
                string decision = confirmationRecord.WillBeAdded ? "будет добавлено" : "не будет додавлено: уже существует";
                Console.ForegroundColor = confirmationRecord.AlreadyExist ? ConsoleColor.Red : ConsoleColor.Green;
                Console.WriteLine($"\t{confirmationRecord.RussianWord.Normalize()} - {confirmationRecord.EnglishWord.Normalize()} ({decision})");
                Console.ResetColor();
            }
            Console.WriteLine();
            Console.WriteLine("Нажмите любую кнопку для подтверждения");
            Console.ReadKey();
            Console.WriteLine();
        }

        private static async Task CreateRelations(IEnumerable<ConfirmationRecord> confirmationRecords)
        {
            ICommonRelationRepository commonRelationRepository = GetService<ICommonRelationRepository>();
            foreach (ConfirmationRecord record in confirmationRecords.Where(currentRecord => currentRecord.WillBeAdded))
            {
                await commonRelationRepository.CreateCommonRelation(record.RussianWord, selectedRussianUnitType, record.EnglishWord, selectedEnglishUnitType, selectedCommonDictionary.Id, null, null, null, null, null);
            }
        }

        private class Record
        {
            public string RussianWord { get; set; }
            public string EnglishWord { get; set; }
        }

        private class ConfirmationRecord
        {
            public string RussianWord { get; set; }
            public string EnglishWord { get; set; }
            public bool WillBeAdded { get; set; }
            public bool AlreadyExist { get; set; }
        }

        #region Services
        public static TService GetService<TService>()
        {
            TService? service = serviceProvider.GetService<TService>();
            if (service is not null)
                return service;
            else
                throw new Exception("There is no such service");
        }

        private static IServiceProvider ConfigureServices()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddDbContext<EasyLearnContext>(options =>
            {
                options.UseSqlServer(Config.ConnectionString);
            });

            services.AddTransient<IRussianUnitRepository, RussianUnitRepository>();
            services.AddTransient<IEnglishUnitRepository, EnglishUnitRepository>();

            services.AddTransient<ICommonRelationRepository, CommonRelationsRepository>();
            services.AddTransient<IVerbPrepositionRepository, VerbPrepositionsRepository>();
            services.AddTransient<IIrregularVerbRepository, IrregularVerbsRepository>();

            services.AddTransient<ICommonDictionaryRepository, CommonDictionaryRepository>();
            services.AddTransient<IVerbPrepositionDictionaryRepository, VerbPrepositionDictionaryRepository>();

            services.AddTransient<IEasyLearnUserRepository, EasyLearnUsersRerository>();

            return services.BuildServiceProvider();
        }
        #endregion
    }
}