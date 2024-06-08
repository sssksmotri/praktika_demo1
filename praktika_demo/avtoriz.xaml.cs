
using System.Linq;
using System.Windows;


namespace praktika_demo
{
    public partial class avtoriz : Window
    {
        private Entities1 db;
        int user_id;

        public avtoriz()
        {
            InitializeComponent();
            db = new Entities1();
        }

        private void Registr(object sender, RoutedEventArgs e)
        {
            Registrasia registrasia = new Registrasia();
            registrasia.Show();
            this.Close();
        }
        private bool ValidateInput()
        {
            string login = loginText.Text;
            string password = passwordText.Password;

            using (Entities1 db = new Entities1())
            {
                var userAuth = db.auth.FirstOrDefault(a => a.login == login && a.password == password);

                if (userAuth == null)
                {
                    MessageBox.Show("Неверный логин или пароль.");
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(login))
            {
                MessageBox.Show("Пожалуйста, введите логин.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Пожалуйста, введите пароль.");
                return false;
            }

            return true;
        }

        public void vhod(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }
            string login = loginText.Text;
            string password = passwordText.Password;

            using (Entities1 db = new Entities1())
            {
                var userAuth = db.auth.FirstOrDefault(a => a.login == login && a.password == password);

                if (userAuth == null)
                {
                    MessageBox.Show("Неверный логин или пароль.");
                    return;
                }

                var user = userAuth.user;
                user_id = user.userID; 

                RedirectUserBasedOnRole(user.type_id.GetValueOrDefault());
            }
        }


        private void RedirectUserBasedOnRole(int typeId)
        {
            Window targetWindow = null;

            switch (typeId)
            {
                case 1: 
                    targetWindow = new oper(user_id);
                    break;
                case 2: 
                    targetWindow = new Master(user_id);
                    break;
                case 3: 
                    targetWindow = new oper(user_id);
                    break;
                case 4: 
                    targetWindow = new zakashik(user_id);
                    break;
                default:
                    MessageBox.Show("Неизвестная роль.");
                    return;
            }

            if (targetWindow != null)
            {
                targetWindow.Show();
                this.Close();
            }
        }

    }
}
