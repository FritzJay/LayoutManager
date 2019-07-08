using LayoutManager.Model;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LayoutManager.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new LayoutManagerContext())
            {
                Layouts.ItemsSource = db.Layouts.ToList();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new LayoutManagerContext())
            {
                var layout = new Layout { Name = NewLayoutName.Text };
                db.Layouts.Add(layout);
                db.SaveChanges();

                Layouts.ItemsSource = db.Layouts.ToList();
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Layout layout = button.DataContext as Layout;

            using (var db = new LayoutManagerContext())
            {
                db.Layouts.Remove(layout);
                db.SaveChanges();

                Layouts.ItemsSource = db.Layouts.ToList();
            }
        }
    }
}
