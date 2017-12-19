using PersonsInfoModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TelegramBot.Common;

namespace PersonsInfoModule.ViewModels
{
    public class PersonsInfoViewModel : ObservableModelBase, IBotModuleViewModel
    {
        public string Title => "Info";

        public ICommand SaveInfoCommand
        {
            get;
            private set;
        }

        public PersonsInfoViewModel(PersonsInfo personsInfo)
        {
            SaveInfoCommand = new RelayCommand<object>(o =>
            {
                personsInfo.Persons = EncodeInfo(o as string);
                personsInfo.SaveInfo();
            }, o =>
            {
                string info = DecodeInfo(personsInfo.Persons);
                return (o as string)?.Trim() != info.Trim();
            });
            personsInfo.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == "Persons")
                    Info = DecodeInfo((o as PersonsInfo).Persons);
            };
            personsInfo.LoadInfo();
        }

        private string _info = "";
        public string Info
        {
            get
            {
                return _info;
            }
            private set
            {
                _info = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Decodes list of persons to string to show in TextBox
        /// </summary>
        /// <param name="encodeFrom">List of persons</param>
        /// <returns></returns>
        public string DecodeInfo(List<Person> encodeFrom)
        {
            string result = "";
            foreach (Person p in encodeFrom)
            {
                result += $"{p.LastName} {p.FirstName} {p.Patronymic} {p.PhoneNumber}\r\n";
            }
            return result;
        }

        /// <summary>
        /// Encodes string from TextBox into List of Persons
        /// </summary>
        /// <param name="encodeFrom">String from TextBox</param>
        /// <returns></returns>
        public List<Person> EncodeInfo(string encodeFrom)
        {
            List<Person> persons = new List<Person>();
            string[] personsLines = encodeFrom.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in personsLines)
            {
                string[] splitted = line.Split(' ');
                persons.Add(new Person(splitted[0], splitted[1], splitted[2], splitted.Length >= 4 ? splitted[3] : ""));
            }
            return persons;
        }
    }
}
