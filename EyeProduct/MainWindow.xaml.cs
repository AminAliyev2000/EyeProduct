using System;
using System.Collections.Generic;
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

namespace EyeProduct
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Uri _productimagepath;
        private Image _prodimage = new Image();
        private TextBlock _prodtextblock = new TextBlock();
        private bool _isprodchanged = false;
        public List<Product> Products { get; set; } = ProdDetails.GetProducts();
        public MainWindow()
        {
            InitializeComponent();
            Products.ForEach(p => ProductsListBox.Items.Add(p));

            _productimagepath = new Uri(ProductImage.Source.ToString());
        }


        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".jpg",
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
            };

            Nullable<bool> result = openFileDialog.ShowDialog();

            if (result == true)
            {
                ProductImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                TxtBlckFilePath.Text = openFileDialog.FileName;
            }

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            Grid secondRowBaseGrid = button.Parent as Grid;

            TextBox secondRowBaseGridSecondChildren = (secondRowBaseGrid.Children[0] as ScrollViewer).Content as TextBox;
            Grid baseGrid = secondRowBaseGrid.Parent as Grid;

            Grid firstRowBaseGrid = baseGrid.Children[0] as Grid;
            Image firstRowBaseGridFirstChildren = firstRowBaseGrid.Children[0] as Image;

            TextBlock firstRowSecondChildren = ((firstRowBaseGrid.Children[1] as Grid).Children[0] as ScrollViewer).Content as TextBlock;
            if (ProductImage.Source.ToString() == _productimagepath.ToString() || string.IsNullOrWhiteSpace(secondRowBaseGridSecondChildren.Text))
            {
                MessageBox.Show("You must upload image and write about details of new product.");
                return;
            }



            if (_isprodchanged == true)
            {
                _prodtextblock.Text = secondRowBaseGridSecondChildren.Text;
                _prodimage.Source = firstRowBaseGridFirstChildren.Source;
                _isprodchanged = false;
            }
            else
            {
                Product product = new Product
                {
                    Name = secondRowBaseGridSecondChildren.Text,
                    ProductImage = firstRowBaseGridFirstChildren.Source.ToString()
                };

                ProductsListBox.Items.Add(product);

                ProductsListBox.ScrollIntoView(ProductsListBox.Items[ProductsListBox.Items.Count - 1]);

            }


            firstRowBaseGridFirstChildren.Source = new BitmapImage(_productimagepath);
            secondRowBaseGridSecondChildren.Text = default;
            firstRowSecondChildren.Text = "Upload product image.";
            MessageBox.Show("New Product added successfully");

        }
    

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            const int firstChildrenIndex = 0;

            Button btn = sender as Button;
            Grid grid = btn.Parent as Grid;

            ScrollViewer scl = grid.Children[firstChildrenIndex] as ScrollViewer;

            _prodtextblock = scl.Content as TextBlock;

            Border border1 = grid.Parent as Border;

            Grid newGrid = border1.Parent as Grid;

            Border border = newGrid.Children[firstChildrenIndex] as Border;

            _prodimage = border.Child as Image;


            ProductImage.Source = _prodimage.Source;

            TxtBlckFilePath.Text = _prodimage.Source.ToString();
            TxtBxInfoAboutProduct.Text = _prodtextblock.Text;

            _isprodchanged = true;

            TxtBxInfoAboutProduct.Focus();
            TxtBxInfoAboutProduct.SelectionStart = TxtBxInfoAboutProduct.Text.Length;
        }

     
    }
}
