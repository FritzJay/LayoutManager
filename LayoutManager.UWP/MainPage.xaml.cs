using LayoutManager.Model;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

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

        private void RemoveButton_Clicked(object sender, RoutedEventArgs e)
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

        private void Layouts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridView grid = sender as GridView;

            foreach (var item in e.AddedItems)
            {
                var container = grid.ContainerFromItem(item) as FrameworkElement;
                var button = FindControl<Button>(container, typeof(Button), "RemoveButton");
                if (button != null)
                {
                    button.Visibility = Visibility.Visible;
                }
            }

            foreach (var item in e.RemovedItems)
            {
                var container = grid.ContainerFromItem(item) as FrameworkElement;
                var button = FindControl<Button>(container, typeof(Button), "RemoveButton");
                if (button != null)
                {
                    button.Visibility = Visibility.Collapsed;
                }
            }
        }

        public static T FindControl<T>(UIElement parent, Type targetType, string ControlName) where T : FrameworkElement
        {

            if (parent == null) return null;

            if (parent.GetType() == targetType && ((T)parent).Name == ControlName)
            {
                return (T)parent;
            }
            T result = null;
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                UIElement child = (UIElement)VisualTreeHelper.GetChild(parent, i);

                if (FindControl<T>(child, targetType, ControlName) != null)
                {
                    result = FindControl<T>(child, targetType, ControlName);
                    break;
                }
            }
            return result;
        }
    }
}
