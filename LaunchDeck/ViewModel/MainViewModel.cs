using LaunchDeck.Commands;
using LaunchDeck.Interfaces;
using LaunchDeck.Model;
using LaunchDeck.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace LaunchDeck.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {


        #region ViewModels Lists
        private ObservableCollection<NoteViewModel> _noteViewModelsList;
        public ObservableCollection<NoteViewModel> NoteViewModelsList
        {
            get
            {
                return _noteViewModelsList;
            }
            set
            {
                if (_noteViewModelsList != value)
                {
                    _noteViewModelsList = value;
                    OnPropertyChanged(nameof(NoteViewModelsList));
                }
            }
        }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            NoteViewModelsList = new ObservableCollection<NoteViewModel>();
            _dataService = new DataService<Note>(new DbContexts.LaunchDeckContext());
            PopulateViewModelsList();
            DeleteNoteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
        }
        #endregion

        #region Commands
        public ICommand DeleteNoteCommand { get; set; }
        private async void ExecuteDeleteCommand(object value)
        {
            if (value is NoteViewModel noteVM)
            {
                try
                {
                    await _dataService.DeleteAsync(noteVM.Note);
                    NoteViewModelsList.Remove(noteVM);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error deleting note: {ex.Message}");
                }
            }
        }
        private bool CanExecuteDeleteCommand(object value)
        {
            if (value is NoteViewModel noteVm) return !noteVm.IsCreatedDateToday;
            return false;
        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion

        #region OnCreate functions
        private void PopulateViewModelsList()
        {
            DateOnly todayDate = DateOnly.FromDateTime(DateTime.Now);
            var entities = _dataService.GetAllAsync().Result.ToList();
            //Check if today's note exists
            bool b = entities.Any(x => DateOnly.FromDateTime(x.Created) == todayDate);
            if (!b)
            {
                IDateProvider date = new DateProvider();
                Note todayNote = new Note("", date.GetDate());
                NoteViewModel noteViewModel = new NoteViewModel(todayNote);
                noteViewModel.TextChanged += SaveToDB;
                NoteViewModelsList.Add(noteViewModel);
                Task.Run(async () => await _dataService.AddAsync(todayNote));
            }
            RemoveEmptyNotes(entities);
            //iterate from the last position to sort notes by newest date
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                NoteViewModel noteViewModel = new NoteViewModel(entities[i]);
                noteViewModel.TextChanged += SaveToDB;
                NoteViewModelsList.Add(noteViewModel);
            }

        } 
        #endregion

        #region Database operations
        private DataService<Note> _dataService;
        private void SaveToDB(object sender, EventArgs e)
        {
            var noteViewModel = (NoteViewModel)sender;
            Task.Run(async () => await _dataService.UpdateAsync(noteViewModel.Note));
        }
        private void DeleteFromDB(List<Note> notes, Note note)
        {
            Task.Run(async () => await _dataService.DeleteAsync(note));
            notes.Remove(note);
        }
        private void RemoveEmptyNotes(List<Note> notes)
        {
            DateOnly todayDate = DateOnly.FromDateTime(DateTime.Now);
            foreach (var note in notes)
            {

                DateOnly noteDate = DateOnly.FromDateTime(note.Created);
                if (todayDate != noteDate)
                {
                    if (note.Text == "" || note.Text == null)
                    {
                        DeleteFromDB(notes, note);
                    }
                }
            }
        } 
        #endregion


    }
}
