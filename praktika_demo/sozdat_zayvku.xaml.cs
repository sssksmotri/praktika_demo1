using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace praktika_demo
{
    public partial class sozdat_zayvku : Window
    {
        private Entities1 db;
        int requestiD; 
        int user_id;

        public sozdat_zayvku(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;
            db = new Entities1(); 
        }
        public void sozdat_zayavka()

        {// Проверка даты
            if (!datePicker.SelectedDate.HasValue || datePicker.SelectedDate.Value > DateTime.Now)
            {
                MessageBox.Show("Пожалуйста, выберите корректную дату.");
                return;
            }
            // Устанавливаем ограничения на выбор даты
            datePicker.DisplayDateStart = new DateTime(1980, 1, 1);
            datePicker.DisplayDateEnd = new DateTime(2024, 6, 13);

            // Проверка наличия значений в полях
            if (techComboBox.SelectedItem == null || modelTextBox.Text == "" || problemDescription.Text == "" || statusComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }
            DateTime? startDate = datePicker.SelectedDate;

            string homeTechType = ((ComboBoxItem)techComboBox.SelectedItem).Content.ToString();

            
            string homeTechModel = modelTextBox.Text;

            
            string problemDescriptionText = problemDescription.Text;

            
            string requestStatus = ((ComboBoxItem)statusComboBox.SelectedItem).Content.ToString();

            var newHomeTechType = new homeTechType
            {
                homeTechType1 = homeTechType,
                homeTechModel = homeTechModel
            };

            var newStatus = new status
            {
                statusDescription = requestStatus
            };

           
            var newRequest = new request
            {
                problemDescription = problemDescriptionText,
                startDate = startDate,
            };

            // Проверяем, существует ли пользователь
            var user = db.user.SingleOrDefault(u => u.userID == this.user_id);
            if (user == null)
            {
                MessageBox.Show("Пользователь не найден.");
                return;
            }

           
            var requestClient = new request_client
            {
                clientID = this.user_id 
            };

            newRequest.homeTechType.Add(newHomeTechType);
            newRequest.status.Add(newStatus);
            newRequest.request_client.Add(requestClient);

            
            db.request.Add(newRequest);
            db.SaveChanges();

            
            MessageBox.Show("Заявка успешно создана!");
        }
        private void sozdat(object sender, RoutedEventArgs e)
        {
            try
            {
                sozdat_zayavka();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании заявки: " + ex.Message);
            }
        }

        private void redactirovat(object sender, RoutedEventArgs e)
        {
            redaktirovat_zayvki redaktirovat_Zayvki = new redaktirovat_zayvki(user_id);
            redaktirovat_Zayvki.Show();
            this.Close();
        }

        private void problemTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void sozdat_z(object sender, RoutedEventArgs e)
        {
           
        }

        private void zayavki(object sender, RoutedEventArgs e)
        {
            zakashik zakashik = new zakashik(user_id);
            zakashik.Show();
            this.Close();
        }

        private void exist(object sender, RoutedEventArgs e)
        {

            this.Close();
        }
    }
}
