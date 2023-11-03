using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using K5BZI_Models;
using K5BZI_Models.ViewModelModels;
using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using MessageBox = System.Windows.Forms.MessageBox;

namespace K5BZI_ViewModels
{
    public class EventViewModel : IEventViewModel
    {
        #region Properties

        public EventModel EventModel { get; private set; }

        private readonly IEventService _eventService;
        private readonly IExcelFileService _excelFileService;
        private readonly ILogViewModel _logViewModel;
        private readonly IOperatorViewModel _operatorsViewModel;

        #endregion

        #region Constructors

        public EventViewModel(
            IEventService eventService,
            IExcelFileService excelFileService,
            ILogViewModel logViewModel,
            IOperatorViewModel operatorsViewModel)
        {
            _eventService = eventService;
            _excelFileService = excelFileService;
            _logViewModel = logViewModel;
            _operatorsViewModel = operatorsViewModel;

            Initialize();
            GetExistingEventsAsync();
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            EventModel = new EventModel
            {
                DXCCValues = _excelFileService.ReadDxccExcelData(),
                ViewFileStoreAction = (_) => _eventService.OpenEventDirectory(),
                SelectEventAction = (_) => SelectEvent(),
                ChangeEventAction = (_) => ChangeEvent(),
                EditEventAction = (_) => EditEvent(),
                DeleteEventAction = async (_) => await DeleteEventAsync((Guid)_),
                CreateNewEventAction = async (_) => await CreateNewEventAsync(),
                EditEventsAction = (_) => EditEvent(true),
                UpdateEventAction = async (_) => await UpdateEventAsync()
            };
        }

        private async Task GetExistingEventsAsync()
        {
            EventModel.Events.Clear();

            (await _eventService
                .GetEventsAsync())?
                .OrderByDescending(_ => _.CreatedDate)
                .ToList()
                .ForEach(eventObj =>
                {
                    if (!eventObj.IsDeleted)
                    {
                        EventModel.Events.Add(eventObj);
                    }
                });

            _operatorsViewModel.OperatorModel.Events = EventModel.Events.ToList();
        }

        private async Task CreateNewEventAsync()
        {
            EventModel.IsOpen = false;

            var currentEvent = EventModel.Events.FirstOrDefault(_ => _.IsActive);

            if (currentEvent != null)
                currentEvent.IsActive = false;

            var newEvent = new Event
            {
                Id = Guid.NewGuid(),
                IsActive = true,
                EventName = EventModel.NewEventName,
                CreatedDate = DateTime.Now,
                LogFileName = $"{EventModel.NewEventName.Replace(" ", "_")}_{DateTime.UtcNow.ToString("yyyy'-'MM'-'dd")}",
            };

            EventModel.Events.Add(newEvent);
            EventModel.ActiveEvent = currentEvent; //Trigger update

            _operatorsViewModel.OperatorModel.Events = EventModel.Events.ToList();

            if (!_operatorsViewModel.OperatorModel.Operators.Any())
            {
                _operatorsViewModel.OperatorModel.IsOpen = true;
                _operatorsViewModel.OperatorModel.AddOperatorCommand.Execute(null);
            }

            while (_operatorsViewModel.OperatorModel.IsOpen) { await Task.Delay(25); }

            _logViewModel.CreateNewLog(EventModel.ActiveEvent);

            EditEvent();
        }

        private void SelectEvent()
        {
            _logViewModel.GetLog(EventModel.ActiveEvent);

            EventModel.IsOpen = false;
        }

        private void ChangeEvent()
        {
            EventModel.ShowCloseButton = true;
            EventModel.IsOpen = true;
        }

        private async Task DeleteEventAsync(Guid Id)
        {
            var result = MessageBox.Show("Are you sure you want to delete this event?", "", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                EventModel.Events.First(_ => _.Id == Id).IsDeleted = true;

                await _eventService.SaveEventsAsync(EventModel.Events.ToList());
            }
        }

        private async Task UpdateEventAsync()
        {
            if (!EventModel.ActiveEvent.Operators.Any())
            {
                MessageBox.Show("Please add at least one operator to the event.", "Invalid input");
                return;
            }

            if (EventModel.ActiveEvent.ItuZone < 0 || EventModel.ActiveEvent.ItuZone > 90)
            {
                MessageBox.Show("Please enter an ITU one value between 0 and 90", "Invalid input");
                return;
            }

            EventModel.EditEventIsOpen = false;

            await _eventService.SaveEventsAsync(EventModel.Events.ToList());
        }

        private void EditEvent(bool allEvents = false)
        {
            EventModel.ShowCloseButton = true;
            EventModel.EditEventIsOpen = true;
        }

        #endregion
    }
}
