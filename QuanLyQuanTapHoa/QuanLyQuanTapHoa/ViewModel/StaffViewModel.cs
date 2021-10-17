using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuanLyQuanTapHoa.ViewModel;
using QuanLyQuanTapHoa.UserControls;
using System.Windows;

namespace QuanLyQuanTapHoa.ViewModel
{

    class StaffViewModel : BaseViewModel
    {
        public List<Staff> listStaff { get; set; }


        #region commands
        public ICommand AddStaff { get; set; }
        public ICommand EditStaff { get; set; }
        public ICommand DelStaff { get; set; }
        #endregion


        public StaffViewModel()
        {
            listStaff = new List<Staff>();
            listStaff.Add(new Staff() { Id = 1, DisplayName = "Nguyen Van A", Sex = "Nam", Age = "21", Phone = "09834561", Role = "Staff", Salary = 200000 });
            listStaff.Add(new Staff() { Id = 3, DisplayName = "Nguyen Thi C", Sex = "Nu", Age = "18", Phone = "1654894", Role = "Staff", Salary = 200000 });
            listStaff.Add(new Staff() { Id = 2, DisplayName = "Nguyen Van G", Sex = "Nam", Age = "32", Phone = "4841841", Role = "Manager", Salary = 500000 });
            listStaff.Add(new Staff() { Id = 4, DisplayName = "Nguyen Thi H", Sex = "Nu", Age = "25", Phone = "1111111", Role = "Staff", Salary = 200000 });
            listStaff.Add(new Staff() { Id = 5, DisplayName = "Nguyen Van F", Sex = "Nam", Age = "19", Phone = "2222222", Role = "Staff", Salary = 200000 });


            AddStaff = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                AddStaffWindow add = new AddStaffWindow();
                add.ShowDialog();
            });
            EditStaff = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                EditStaffWindow edit = new EditStaffWindow();
                edit.ShowDialog();
            });
        }



    }

    class Staff : BaseViewModel
    {
        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private string _Sex;
        public string Sex { get => _Sex; set { _Sex = value; OnPropertyChanged(); } }

        private string _Age;
        public string Age { get => _Age; set { _Age = value; OnPropertyChanged(); } }

        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }

        private string _Role;
        public string Role { get => _Role; set { _Role = value; OnPropertyChanged(); } }

        private int _Salary;
        public int Salary { get => _Salary; set { _Salary = value; OnPropertyChanged(); } }

    }



}
