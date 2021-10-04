using FluentValidationLib;
using FluentValidationUI.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FluentValidationUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BindingList<string> errors = new BindingList<string>();
        public MainWindow()
        {
            InitializeComponent();
            ErrorListBox.ItemsSource = errors;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            errors.Clear();

            if(!decimal.TryParse(Account.Text, out decimal accountBalance))
            {
                errors.Add("Account Balance: Invalid amount ! ");
                return;
            }
            Person person = new Person()
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                Account = accountBalance,
                DateOfBirth= DateOfBirth.SelectedDate.Value
            };
            PersonValidator validator = new PersonValidator();
            FluentValidation.Results.ValidationResult results = validator.Validate(person);
            if (results.IsValid == false)
            {
                foreach (var failure in results.Errors)
                {
                    errors.Add($"{failure.ErrorMessage}");
                }
            }
            else {
                MessageBox.Show("Operation Complete !");
            }
            
        }
    }
}
