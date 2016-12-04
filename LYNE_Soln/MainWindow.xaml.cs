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
using LYNE_Soln.Model;
using LYNE_Soln.Exceptions;

using LYNE_Soln.Model.Shape;
using LYNE_Soln.Model.Factory;

namespace LYNE_Soln
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly BitmapImage triImg = new BitmapImage(new Uri("pack://application:,,,/Source/tri.png"));
        private readonly BitmapImage tri_s_Img = new BitmapImage(new Uri("pack://application:,,,/Source/tri_s.png"));
        private readonly BitmapImage rectImg = new BitmapImage(new Uri("pack://application:,,,/Source/rect.png"));
        private readonly BitmapImage rect_s_Img = new BitmapImage(new Uri("pack://application:,,,/Source/rect_s.png"));
        private readonly BitmapImage diaImg = new BitmapImage(new Uri("pack://application:,,,/Source/dia.png"));
        private readonly BitmapImage dia_s_Img = new BitmapImage(new Uri("pack://application:,,,/Source/dia_s.png"));
        private readonly BitmapImage polygon2Img = new BitmapImage(new Uri("pack://application:,,,/Source/polygon_2.png"));
        private readonly BitmapImage polygon3Img = new BitmapImage(new Uri("pack://application:,,,/Source/polygon_3.png"));
        private readonly BitmapImage polygon4Img = new BitmapImage(new Uri("pack://application:,,,/Source/polygon_4.png"));

        private int row = 0;
        private int col = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void setRowCol_btn_Click(object sender, RoutedEventArgs e)
        {
            row = Convert.ToInt32(row_textBox.Text);
            col = Convert.ToInt32(col_textBox.Text);
            TableGrid.ColumnDefinitions.Clear();
            TableGrid.RowDefinitions.Clear();
            TableGrid.Children.Clear();

            for (int r = 0; r < row; r++)
            {
                TableGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int c = 0; c < col; c++)
            {
                TableGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            //One row first, then next row
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    Button b = new Button()
                    {
                        DataContext = ShapeFactory.createBy(new EmptyFactory()),
                    };
                    b.Click += B_Click;
                    Grid.SetColumn(b, c);
                    Grid.SetRow(b, r);
                    TableGrid.Children.Add(b);

                    TextBlock tb = new TextBlock()
                    {
                        Text = (c + r * col + 1).ToString(),
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                    };
                    Grid.SetColumn(tb, c);
                    Grid.SetRow(tb, r);
                    TableGrid.Children.Add(tb);
                }
            }
            main_window.Height = 110 + row * 115;
            main_window.Width = 200 + col * 115;
        }

        private void B_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (empty_rd.IsChecked == true)
            {
                b.Content = null;
                b.DataContext = ShapeFactory.createBy(new EmptyFactory());
            }
            else if (polygon_rd.IsChecked == true)
            {
                if (twoHoles_rd.IsChecked == true)
                {
                    b.Content = new Image() { Source = polygon2Img };
                    b.DataContext = ShapeFactory.createBy(new Polygon2Factory());

                }
                else if (threeHoles_rd.IsChecked == true)
                {
                    b.Content = new Image() { Source = polygon3Img };
                    b.DataContext = ShapeFactory.createBy(new Polygon3Factory());

                }
                else if (fourHoles_rd.IsChecked == true)
                {
                    b.Content = new Image() { Source = polygon4Img };
                    b.DataContext = ShapeFactory.createBy(new Polygon4Factory());

                }
            }
            else
            {
                if (triangle_rd.IsChecked == true)
                {
                    if (isStartPoint_rd.IsChecked == true)
                    {
                        b.Content = new Image() { Source = tri_s_Img };
                        b.DataContext = ShapeFactory.createBy(new TriangleStartFactory());
                    }
                    else
                    {
                        b.Content = new Image() { Source = triImg };
                        b.DataContext = ShapeFactory.createBy(new TriangleFactory());
                    }
                }
                else if (rectangle_rd.IsChecked == true)
                {
                    if (isStartPoint_rd.IsChecked == true)
                    {
                        b.Content = new Image() { Source = rect_s_Img };
                        b.DataContext = ShapeFactory.createBy(new RectangleStartFactory());
                    }
                    else
                    {
                        b.Content = new Image() { Source = rectImg };
                        b.DataContext = ShapeFactory.createBy(new RectangleFactory());
                    }
                }
                else if (diamond_rd.IsChecked == true)
                {
                    if (isStartPoint_rd.IsChecked == true)
                    {
                        b.Content = new Image() { Source = dia_s_Img };
                        b.DataContext = ShapeFactory.createBy(new DiamondStartFactory());
                    }
                    else
                    {
                        b.Content = new Image() { Source = diaImg };
                        b.DataContext = ShapeFactory.createBy(new DiamondFactory());
                    }
                }

            }
        }

        private void radiobutton_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (image_sample == null) return;

            if (empty_rd.IsChecked == true)
            {
                image_sample.Source = null;
            }
            else if (triangle_rd.IsChecked == true)
            {
                if (isStartPoint_rd.IsChecked == true)
                {
                    image_sample.Source = tri_s_Img;
                }
                else
                {
                    image_sample.Source = triImg;
                }
            }
            else if (rectangle_rd.IsChecked == true)
            {
                if (isStartPoint_rd.IsChecked == true)
                {
                    image_sample.Source = rect_s_Img;
                }
                else
                {
                    image_sample.Source = rectImg;
                }
            }
            else if (diamond_rd.IsChecked == true)
            {
                if (isStartPoint_rd.IsChecked == true)
                {
                    image_sample.Source = dia_s_Img;
                }
                else
                {
                    image_sample.Source = diaImg;
                }
            }
            else if (polygon_rd.IsChecked == true)
            {
                if (twoHoles_rd.IsChecked == true)
                {
                    image_sample.Source = polygon2Img;
                }
                else if (threeHoles_rd.IsChecked == true)
                {
                    image_sample.Source = polygon3Img;
                }
                else if (fourHoles_rd.IsChecked == true)
                {
                    image_sample.Source = polygon4Img;
                }
            }
        }

        async private void SolveButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.col < 2 && this.row < 2) return;

            Button solve_b = sender as Button;
            solve_b.IsEnabled = false;
            var solve_b_content = solve_b.Content;
            solve_b.Content = "solving...";

            try
            {
                List<Shape> shapes = new List<Shape>();
                foreach (var element in TableGrid.Children)
                {
                    if (element.GetType().Equals(typeof(Button)))
                    {
                        shapes.Add((element as Button).DataContext as Shape);
                    }
                }

                Map map = new Map(this.col, this.row, shapes);
                await Task.Run(() => map.solve());

                if (map.Solutions.Count > 0)
                {
                    MessageBox.Show(map.Solutions[0]);
                }
                else
                {
                    MessageBox.Show("No Solution");
                }
            }
            catch (InvalidMapException exception)
            {
                MessageBox.Show(exception.Message);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                solve_b.IsEnabled = true;
                solve_b.Content = solve_b_content;
            }
        }

    }
}
