using LaunchDeck.Interfaces;
using LaunchDeck.Model;
using LaunchDeck.Services;
using LaunchDeck.ViewModel;

namespace LauchDeckTests
{
    public class NoteViewModelTests
    {
        [Fact]
        public void NoteViewModel_CreatedToday_IsCreatedDateTodayReturnsTrue()
        {
            // Arrange
            var dateStub = new DateProvider(DateOnly.FromDateTime(DateTime.Now));
            var noteStub = new Note("", dateStub.GetDate());

            // Act
            var noteViewModel = new NoteViewModel(noteStub);

            // Assert
            Assert.True(noteViewModel.IsCreatedDateToday);
        }

        [Fact]
        public void NoteViewModel_CreatedInPast_IsCreatedDateTodayReturnsFalse()
        {
            // Arrange
            var dateStub = new DateProvider(DateOnly.Parse("2023-06-23"));
            var noteStub = new Note("", dateStub.GetDate());

            // Act
            var noteViewModel = new NoteViewModel(noteStub);

            // Assert
            Assert.False(noteViewModel.IsCreatedDateToday);
        }
    }
}
