using Npgsql;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestDb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string sql = "Server=localhost;Port=5432;Database=my_login_db; User Id = postgres; Password=admin";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadDataButtonClick(object sender, RoutedEventArgs e)
        {   
            SqlConnectionReader();
        }

        private void SqlConnectionReader()
        {
            NpgsqlConnection sqlConnection = new NpgsqlConnection(sql);
            sqlConnection.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM public.users";
            NpgsqlDataReader dataReader = command.ExecuteReader();
           // command.ExecuteNonQuery();

            while(dataReader.Read())
            {
                User user = new User();

                user.Id = dataReader.GetInt32(0);
                user.Name = dataReader.GetString(1);
                user.Login = dataReader.GetString(2);
                user.Password = dataReader.GetString(3);
                UsersLabel.Content += user.Id + " " 
                    + user.Name 
                    + user.Login
                    + user.Password + "\n";
            }
            
            command.Dispose();
            sqlConnection.Close();  
        }
    }
}