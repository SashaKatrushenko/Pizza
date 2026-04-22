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
using System.Xml.Linq;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Order currentOrder;
        List<Ingridient> currentPizza = new List<Ingridient>();
        public MainWindow()
        {
            InitializeComponent();
            currentOrder = GenerateOrder();
            ShowOrder();
            List<Ingridient> currentPizza = new List<Ingridient>();

        }
        Order GenerateOrder()
        {
            Random rnd = new Random();
            Order order = new Order();
            order.Ingridients.Add(new Ingridient { Name = "Ketchup ", Count = rnd.Next(1, 2) });
            order.Ingridients.Add(new Ingridient { Name = "Cheese ", Count = rnd.Next(1, 3) });
            order.Ingridients.Add(new Ingridient { Name = "Peperoni ", Count = rnd.Next(1, 5) });
            order.Ingridients.Add(new Ingridient { Name = "Paprika ", Count = rnd.Next(1, 4) });
            order.Ingridients.Add(new Ingridient { Name = "Mushroom ", Count = rnd.Next(1, 3) });
            return order;
        }

        void ShowOrder()
        {
            OrderList.Items.Clear();
            foreach (var item in currentOrder.Ingridients)
            {
                OrderList.Items.Add($"{ item.Name}x{item.Count}");
            }
        }
        void AddIngredient(string name)
        {
            var ingridient = currentPizza.FirstOrDefault(i => i.Name == name);
            if (ingridient != null)
                ingridient.Count++;
            else
                currentPizza.Add(new Ingridient { Name = name, Count = 1 });
        }

        private void Cheese_Click(object sender, RoutedEventArgs e)
        {
            AddIngredient("Cheese");
        }
        bool CheckOrder()
        {
            foreach (var item in currentOrder.Ingridients)
            {
                var pizzaItem = currentPizza.FirstOrDefault(i => i.Name == item.Name);
                if (pizzaItem == null)
                    return false;
                if (pizzaItem.Count != item.Count)
                    return false;
            }
            return true;
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            if (CheckOrder())
            {
                MessageBox.Show("Cool");
                int xp = 0;
                xp += 10;
                XPBar.Value = xp;
            }
            else
            {
                MessageBox.Show("Mistake");
            }

            currentPizza.Clear();
            currentOrder = GenerateOrder();
            ShowOrder();
        }

    }
}