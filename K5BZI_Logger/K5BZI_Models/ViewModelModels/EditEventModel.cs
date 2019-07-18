using K5BZI_Models.Base;
using K5BZI_Models.EntityModels;
using K5BZI_Models.Enums;
using Microsoft.VisualStudio.PlatformUI;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace K5BZI_Models.ViewModelModels
{
    [AddINotifyPropertyChangedInterface]
    public class EditEventModel : BaseViewModel
    {
        #region Properties

        public Event Event { get; set; }

        public Operator EventClub { get; set; }

        public DXCC EventDxcc { get; set; }

        public ObservableCollection<Operator> Operators { get; set; }

        public ObservableCollection<Operator> Clubs { get; set; }

        public List<DXCC> DxccEntities { get; set; }

        #endregion

        #region Constructors

        public EditEventModel()
        {
            Operators = new ObservableCollection<Operator>();
            Clubs = new ObservableCollection<Operator>();

            PopulateDxcc();
        }

        #endregion

        #region Commands

        private ICommand _editEventCommand;
        public ICommand EditEventCommand
        {
            get
            {
                return _editEventCommand ??
                    (_editEventCommand = new DelegateCommand(EditEventAction, _ => { return true; }));
            }
        }
        public Action<object> EditEventAction { get; set; }

        private ICommand _updateEventCommand;
        public ICommand UpdateEventCommand
        {
            get
            {
                return _updateEventCommand ??
                    (_updateEventCommand = new DelegateCommand(UpdateEventAction, _ => { return true; }));
            }
        }
        public Action<object> UpdateEventAction { get; set; }

        #endregion

        public void PopulateDxcc()
        {
            DxccEntities =  new List<DXCC>
                {
                    new DXCC("1A", "SMO Malta", "Eu", 28, CqZone.Zone15, 1, "42N", "13E", new List<string> { "1A" }, new List<string>()),
                    new DXCC("1M8", "Minerva Reef", "Oc", 62, CqZone.Zone32, -12, "24S", "179W", new List<string>(), new List<string>()),
                    new DXCC("1S", "Spratly Is", "As", 50, CqZone.Zone26, 7, "9N", "112E", new List<string> { "1S" }, new List<string> { "9M0", "XV9" }),
                    new DXCC("3A", "Monaco", "Eu", 27, CqZone.Zone14, 1, "44N", "8E", new List<string> { "3A" }, new List<string>()),
                    new DXCC("3B6", "Agalega & St. Brandon", "Af", 53, CqZone.Zone39, 4, "10S", "57E", new List<string>(), new List<string> { "3B7" }),
                    new DXCC("3B8", "Mauritius", "Af", 53, CqZone.Zone39, 4, "20S", "58E", new List<string> { "3B" }, new List<string>()),
                    new DXCC("3B9", "Rodriguez Is.", "Af", 53, CqZone.Zone39, 4, "20S", "63E", new List<string>(), new List<string>()),
                    new DXCC("3C", "Equatorial Guinea", "Af", 47, CqZone.Zone36, -1, "4N", "9E", new List<string> { "3C" }, new List<string>()),
                    new DXCC("3C0", "Annobon Is.", "Af", 52, CqZone.Zone36, -1, "1S", "6E", new List<string>(), new List<string>()),
                    new DXCC("3D2", "Conway Reef", "Oc", 56, CqZone.Zone32, 12, "22S", "175E", new List<string>(), new List<string>()),
                    new DXCC("3D2", "Fiji", "Oc", 56, CqZone.Zone32, 12, "18S", "178E", new List<string> { "3DN-3DZ" }, new List<string>()),
                    new DXCC("3D2", "Rotuma", "Oc", 56, CqZone.Zone32, 12, "13S", "177E", new List<string>(), new List<string>()),
                    new DXCC("3DA", "Swaziland", "Af", 57, CqZone.Zone38, 2, "26S", "31E", new List<string> { "3DA-3DM" }, new List<string> { "3D6" }),
                    new DXCC("3V", "Tunisia", "Af", 37, CqZone.Zone33, 1, "37N", "10E", new List<string> { "3V", "TS" }, new List<string>()),
                    new DXCC("3W", "Vietnam", "As", 49, CqZone.Zone26, 7, "11N", "107E", new List<string> { "3W", "XV" }, new List<string>()),
                    new DXCC("3X", "Guinea", "Af", 46, CqZone.Zone35, 0, "10N", "14W", new List<string> { "3X" }, new List<string>()),
                    new DXCC("3Y", "Bouvet", "Af", 67, CqZone.Zone38, 0, "54S", "3E", new List<string>(), new List<string>()),
                    new DXCC("3Y", "Peter I", "An", 72, CqZone.Zone12, -6, "69S", "91W", new List<string>(), new List<string>()),
                    new DXCC("4J", "Azerbaijan", "As", 29, CqZone.Zone21, 4, "40N", "50E", new List<string> { "4J-4K" }, new List<string> { "UD" }),
                    new DXCC("4L", "Georgia", "As", 29, CqZone.Zone21, 4, "42N", "45E", new List<string> { "4L" }, new List<string> { "UF" }),
                    new DXCC("4O", "Montenegro", "Eu", 28, CqZone.Zone15, 1, "42N", "19E", new List<string> { "4O" }, new List<string> { "YU3, YU6" }),
                    new DXCC("4S", "Sri Lanka", "As", 41, CqZone.Zone22, 5.5, "7N", "80E", new List<string> { "4P-4S" }, new List<string>()),
                    new DXCC("4U", "ITU Geneva", "Eu", 28, CqZone.Zone14, 1, "46N", "6E", new List<string>(), new List<string>()),
                    new DXCC("4U", "UN HQ", "NA", 8, CqZone.Zone5, -5, "41N", "74W", new List<string> { "4U" }, new List<string>()),
                    new DXCC("4W", "Timor Leste", "Oc", 54, CqZone.Zone28, 8, "9S", "126E", new List<string> { "4W" }, new List<string>()),
                    new DXCC("4W*", "Yemen Arab Rep", "As", 39, CqZone.Zone21, 3, "15N", "44E", new List<string>(), new List<string>()),
                    new DXCC("4X", "Israel", "As", 39, CqZone.Zone20, 2, "32N", "35E", new List<string> { "4X", "4Z" }, new List<string>()),
                    new DXCC("5A", "Libya", "Af", 38, CqZone.Zone34, 2, "33N", "13E", new List<string> { "5A" }, new List<string>()),
                    new DXCC("5B", "Cyprus", "As", 39, CqZone.Zone20, 3, "35N", "33E", new List<string> { "5B", "C4", "H2", "P3" }, new List<string>()),
                    new DXCC("5H", "Tanzania", "Af", 53, CqZone.Zone37, 3, "7S", "39E", new List<string> { "5H-5I" }, new List<string>()),
                    new DXCC("5N", "Nigeria", "Af", 46, CqZone.Zone35, 1, "6N", "3E", new List<string> { "5N-5O" }, new List<string>()),
                    new DXCC("5R", "Madagascar", "Af", 53, CqZone.Zone39, 3, "19S", "48E", new List<string> { "5R-5S", "6X" }, new List<string>()),
                    new DXCC("5T", "Mauritania", "Af", 46, CqZone.Zone35, -1, "18N", "16W", new List<string> { "5T" }, new List<string>()),
                    new DXCC("5U", "Niger", "Af", 46, CqZone.Zone35, 1, "14N", "2W", new List<string> { "5U" }, new List<string>()),
                    new DXCC("5V", "Togo", "Af", 46, CqZone.Zone35, 0, "6N", "1E", new List<string> { "5V" }, new List<string>()),
                    new DXCC("5W", "Samoa", "Oc", 62, CqZone.Zone32, -11, "14S", "172W", new List<string> { "5W" }, new List<string>()),
                    new DXCC("5X", "Uganda", "Af", 48, CqZone.Zone37, 3, "0N", "33E", new List<string> { "5X" }, new List<string>()),
                    new DXCC("5Z", "Kenya", "Af", 48, CqZone.Zone37, 3, "2S", "37E", new List<string> { "5Y-5Z" }, new List<string>()),
                    new DXCC("6W", "Senegal", "Af", 46, CqZone.Zone35, 0, "15N", "18W", new List<string> { "6V-6W" }, new List<string>()),
                    new DXCC("6Y", "Jamaica", "NA", 11, CqZone.Zone8, -5, "18N", "77W", new List<string> { "6Y" }, new List<string>()),
                    new DXCC("7O", "Yemen", "As", 39, CqZone.Zone21, 3, "13N", "45E", new List<string> { "7O" }, new List<string>()),
                    new DXCC("7O*", "PDR Yemen", "As", 39, CqZone.Zone21, 3, "13N", "45E", new List<string>(), new List<string> { "VS9A", "VS9P", "VS9S" }),
                    new DXCC("7P", "Lesotho", "Af", 57, CqZone.Zone38, 2, "29S", "27E", new List<string> { "7P" }, new List<string>()),
                    new DXCC("7Q", "Malawi", "Af", 53, CqZone.Zone37, 2, "14S", "34E", new List<string> { "7Q" }, new List<string>()),
                    new DXCC("7X", "Algeria", "Af", 37, CqZone.Zone33, 0, "37N", "3E", new List<string> { "7R", "7T-7Y" }, new List<string>()),
                    new DXCC("8P", "Barbados", "NA", 11, CqZone.Zone8, -4, "13N", "60W", new List<string> { "8P" }, new List<string>()),
                    new DXCC("8Q", "Maldives", "As, Af", 41, CqZone.Zone22, 5, "4N", "73E", new List<string> { "8Q" }, new List<string>()),
                    new DXCC("8R", "Guyana", "SA", 12, CqZone.Zone9, -3.75, "6N", "58W", new List<string> { "8R" }, new List<string>()),
                    new DXCC("8Z4*", "S Arabia/Iraq NZ", "As", 39, CqZone.Zone21, 3, "29N", "46E", new List<string>(), new List<string>()),
                    new DXCC("8Z5*", "Kuwait/S Arabia NZ", "As", 39, CqZone.Zone21, 3, "29N", "48E", new List<string>(), new List<string> { "9K3" }),
                    new DXCC("9A", "Croatia", "Eu", 28, CqZone.Zone15, 3, "46N", "16E", new List<string> { "9A" }, new List<string>()),
                    new DXCC("9G", "Ghana", "Af", 46, CqZone.Zone35, 0, "5N", "0W", new List<string> { "9G" }, new List<string>()),
                    new DXCC("9H", "Malta", "Eu", 28, CqZone.Zone15, 1, "36N", "15E", new List<string> { "9H" }, new List<string>()),
                    new DXCC("9J", "Zambia", "Af", 53, CqZone.Zone36, 2, "15S", "28E", new List<string> { "9I-9J" }, new List<string>()),
                    new DXCC("9K", "Kuwait", "As", 39, CqZone.Zone21, 3, "29N", "48E", new List<string> { "9K" }, new List<string>()),
                };
        }
    }
}
