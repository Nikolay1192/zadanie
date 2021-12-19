using System;
using System.Windows;
using System.IO;


namespace TimeConv
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Cleaning(object sender, RoutedEventArgs e) => text1.Text = ""; //Очистка поля ввода/вывода
        private bool i = true; //Изначальное состояние выбора конвертации = Йены в доллары
        private void RadioButton_Checked1(object sender, RoutedEventArgs e) => i = true; //Состояние выбора конвертации = Йены в доллары
        private void RadioButton_Checked2(object sender, RoutedEventArgs e) => i = false; //Состояние выбора конвертации = Доллары в йены
        private void Button_Convert(object sender, RoutedEventArgs e) //Конвертация
        {
            try
            {
                if (Convert.ToDouble(text1.Text) < 0) throw new Exception("Значение не может быть меньше 0");
                if (i) //Если состояние выбора конвертации = Йены в Доллары
                {
                    if (Convert.ToDouble(text1.Text) > 2147483520) throw new Exception("Максимальное значение Йен 2147483520");
                    Log(text1.Text, $"{Convert.ToDouble(text1.Text) / 111.3350182} Долларов");
                    text1.Text = $"{Convert.ToDouble(text1.Text)} Йен это:\n {Convert.ToDouble(text1.Text) / 111.3350182} долларов";
                }
                if (!i) //Если  состояние выбора конвертации = Доллары в йены
                {
                    if (Convert.ToDouble(text1.Text) > 1491308) throw new Exception("Максимальное значение долларов 1491308");
                    Log(text1.Text, $"{Convert.ToDouble(text1.Text) * 111.3350182} йен");
                    text1.Text = $"{Convert.ToDouble(text1.Text)} Долларов это:\n {Convert.ToDouble(text1.Text) * 111.3350182} йен";
                }
            }
            catch (FormatException)
            {
                MessageBox.Show($"Некорректный ввод");
                Log(text1.Text, "ошибка(Некорректный ввод)");
            }
            catch (OverflowException)
            {
                if (i) MessageBox.Show("Максимальное значение йен 2147483520");
                if (!i) MessageBox.Show("Максимальное значение долларов 1491308");
                Log(text1.Text, "ошибка(переполнение int)");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log(text1.Text, $"ошибка({ex.Message})");
            }
        }
        private void Log(string text, string text1) //Запись действий пользователя
        {
            DateTime Time = DateTime.Now;
            using (FileStream fstream = new FileStream($"Logi.txt", FileMode.Append))
            {
                string RB = i ? "Йены в доллары" : "Доллары в йены";
                byte[] array = System.Text.Encoding.Default.GetBytes($"\n{new string('-', 40)}\n{Time:G} \nВвод:{text}({RB})\nВывод:{text1}\n{new string('-', 40)}");
                fstream.Write(array, 0, array.Length);
            }
        }
    }
}
