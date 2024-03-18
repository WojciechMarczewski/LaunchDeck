using LaunchDeck.Interfaces;
using LaunchDeck.Model;
using LaunchDeck.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LaunchDeck.ViewModel
{
    public class NoteViewModel : INotifyPropertyChanged
    {

        #region Model
        public Note Note { get; set; }
        #endregion

        #region Model Properties
        public string? NoteText
        {
            get { return Note.Text; }
            set
            {
                if (Note.Text != value)
                {
                    Note.Text = value;
                    OnPropertyChanged(nameof(NoteText));
                    TextChanged.Invoke(this, EventArgs.Empty);
                }
            }
        }
        //NoteDate is Listbox item label in view
        public string NoteDate { get; set; }
        //IsCreatedDateToday to check if NoteText is editable - if created today, allow edit
        public bool IsCreatedDateToday { get; set; } = false;
        #endregion

        #region Services
        //Date Provider manipulates on DateTime classes to return formatted Date for NoteDate
        private IDateProvider _dateProvider; 
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler TextChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Constructor
        public NoteViewModel(Note note)
        {
            Note = note;
            NoteText = note.Text;
            _dateProvider = new DateProvider(note.Created);
            NoteDate = $"{_dateProvider.GetDayOfWeek()} {_dateProvider.GetDateFormattedYearFirst()}";
            IsCreatedDateToday = CreatedDateVerification();
        }
        #endregion

        #region OnCreate methods
        private bool CreatedDateVerification()
        {
            DateOnly dateToday = DateOnly.FromDateTime(DateTime.Now);
            DateOnly noteDate = DateOnly.FromDateTime(_dateProvider.GetDate());
            return dateToday.ToString().Equals(noteDate.ToString()) ? true : false;
        } 
        #endregion




    }
}
