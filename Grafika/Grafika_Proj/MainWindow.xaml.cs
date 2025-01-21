using Grafika_Projekat_PR38_2019.Model;
using Grafika_Projekat_PR38_2019.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Grafika_Projekat_PR38_2019
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private XML_Loader loader = new XML_Loader();
        public UIElement deletedElement { get; set; }

        // this is so I can backup canvas after clean
        Canvas BackUp { get; set; }
        bool cleared = false;
        bool gridDrawn = false;
        List<Polyline> animatedLines = new List<Polyline>();
        List<int> animatedEntitiesIndexes = new List<int>();
        int nameCounter = 0;


        // canvas size chart
        List<string> canvasSizes = new List<string>() { "300 x 200", "1125 x 750", "2250 x 1500"};


        public MainWindow()
        {
            DataStorage.DrawType = String.Empty;
            GetAllColors();
            InitializeComponent();
            canvas_size_cb.ItemsSource = canvasSizes;
            canvas_size_cb.SelectedItem = canvasSizes[1];
            customMap.Height = 750;
            customMap.Width = 1125;

            matrix_size.Content = $"{DataStorage.matrix_X} x {DataStorage.matrix_Y}";
        }

        private void load_btn_Click(object sender, RoutedEventArgs e)
        {
            loader.LoadEntities();
            MessageBox.Show("All information loaded.\n" +
                            "Number of entities: [" + DataStorage.allEntitiesDict.Count + "]\n" +
                            "Number of lines: [" + DataStorage.lines.Count + "]", "Successfully loaded Geographics.xml file.", MessageBoxButton.OK, MessageBoxImage.Information);
            load_btn.IsEnabled = false;
        }

        private void DrawEntities()
        {
            // Draw all entities
            foreach (PowerEntity entity in DataStorage.allEntitiesDict.Values)
            {
                Rectangle rect = new Rectangle();
                rect.Width = DataStorage.entityObjSize;
                rect.Height = DataStorage.entityObjSize;

                if (entity is SubstationEntity)
                {
                    rect.Fill = (Brush)(new BrushConverter().ConvertFrom("#FF800080"));
                    rect.ToolTip = "[Substation entity]\n";
                }
                else if (entity is NodeEntity)
                {
                    rect.Fill = (Brush)(new BrushConverter().ConvertFrom("#FF0000FF"));
                    rect.ToolTip = "[Node entity]\n";
                }
                else if (entity is SwitchEntity)
                { 
                    rect.Fill = (Brush)(new BrushConverter().ConvertFrom("#FF2E8B57"));
                    rect.ToolTip = "[Switch entity]\n";
                }
                rect.StrokeThickness = 0.1;

                Canvas.SetLeft(rect, entity.MatrixX * DataStorage.entitySize);
                Canvas.SetTop(rect, entity.MatrixY * DataStorage.entitySize);

                rect.ToolTip += entity.ToString();
                rect.Name = String.Format("Entity_{0}", entity.Id);

                if (FindName(rect.Name) == null)
                {
                    RegisterName(rect.Name, rect);
                }

                customMap.Children.Add(rect);
            }
        }

        private void DrawBFSLines(bool excludeNodes)            // bfs lines
        {
            Dictionary<long, List<MatrixPosition>> bfsLines = BFS_Algorithm.Search(excludeNodes);

            foreach (long id in bfsLines.Keys)
            {
                Polyline newLine = new Polyline();
                newLine.Stroke = (Brush)(new BrushConverter().ConvertFrom("#CED0E4"));
                newLine.StrokeThickness = DataStorage.entityObjMove;
                
                MatrixPosition point = bfsLines[id].First();
                // start
                newLine.Points.Add(new Point() { X = point.X * DataStorage.entitySize + DataStorage.entityObjMove, Y = point.Y * DataStorage.entitySize + DataStorage.entityObjMove });

                for (int i = 1; i < bfsLines[id].Count - 1; i++)
                {
                    point = bfsLines[id][i];
                    newLine.Points.Add(new Point() { X = point.X * DataStorage.entitySize + DataStorage.entityObjMove, Y = point.Y * DataStorage.entitySize + DataStorage.entityObjMove });
                }

                // end
                point = bfsLines[id].Last();
                newLine.Points.Add(new Point() { X = point.X * DataStorage.entitySize + DataStorage.entityObjMove, Y = point.Y * DataStorage.entitySize + DataStorage.entityObjMove });

                newLine.Name = $"Line_{id}";
                
                RegisterName(newLine.Name, newLine);
                
                newLine.ToolTip = DataStorage.lines.Find(x => x.Id == id).ToString();

                customMap.Children.Add(newLine);
            }
        }

        private void DrawBFSLinesUndergoundFirst(bool excludeNodes)            // bfs lines
        {
            Dictionary<long, List<MatrixPosition>> bfsLines = BFS_Algorithm.SearchUndergroundFirst(excludeNodes);

            foreach (long id in bfsLines.Keys)
            {
                Polyline newLine = new Polyline();
                if (DataStorage.lines.Find(x => x.Id == id).IsUnderground == false)
                {
                    newLine.Stroke = (Brush)(new BrushConverter().ConvertFrom("#CED0E4"));
                    newLine.StrokeThickness = DataStorage.entityObjMove;
                }
                else
                {
                    newLine.Stroke = (Brush)(new BrushConverter().ConvertFrom("#ff6700"));
                    newLine.StrokeThickness = DataStorage.entityObjMove;
                }

                MatrixPosition point = bfsLines[id].First();
                
                // start
                newLine.Points.Add(new Point() { X = point.X * DataStorage.entitySize + DataStorage.entityObjMove, Y = point.Y * DataStorage.entitySize + DataStorage.entityObjMove });

                for (int i = 1; i < bfsLines[id].Count - 1; i++)
                {
                    point = bfsLines[id][i];
                    newLine.Points.Add(new Point() { X = point.X * DataStorage.entitySize + DataStorage.entityObjMove, Y = point.Y * DataStorage.entitySize + DataStorage.entityObjMove });
                }

                // end
                point = bfsLines[id].Last();
                newLine.Points.Add(new Point() { X = point.X * DataStorage.entitySize + DataStorage.entityObjMove, Y = point.Y * DataStorage.entitySize + DataStorage.entityObjMove });

                newLine.Name = $"Line_{id}";
                
                RegisterName(newLine.Name, newLine);
                
                newLine.ToolTip = DataStorage.lines.Find(x => x.Id == id).ToString();

                customMap.Children.Add(newLine);
            }
        }

        private void DrawCrossSections()
        {
            foreach (MatrixPosition point in DataStorage.crossPositions)
            {
                if (FindName($"Cross_{point.X}_{point.Y}") == null)
                {

                    Ellipse cross = new Ellipse();
                    cross.Height = DataStorage.entityObjMove * 1.5;
                    cross.Width = DataStorage.entityObjMove * 1.5;
                    cross.Fill = Brushes.Black;

                    Canvas.SetLeft(cross, point.X * DataStorage.entitySize);
                    Canvas.SetTop(cross, point.Y * DataStorage.entitySize);

                    cross.ToolTip = $"Cross_{point.X}_{point.Y}";
                    cross.Name = $"Cross_{point.X}_{point.Y}";
                    
                    RegisterName(cross.Name, cross);

                    customMap.Children.Add(cross);
                }
            }
        }

        // =======================================================   Actions for button clicks and odther interactions from UI   =======================================================

        private void draw_grid_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!load_btn.IsEnabled && !gridDrawn)
            {
                bool excludeNodes = (bool)nodes_checkbox.IsChecked;
                DateTime start = DateTime.Now;
                DrawBFSLines(excludeNodes);
                DrawCrossSections();
                DrawEntities();
                DateTime end = DateTime.Now;
                draw_lines_time.Content = end.Subtract(start).ToString();
                gridDrawn = true;
                cleared = false;
            }
            else if(load_btn.IsEnabled)
                    MessageBox.Show("Please load information from XML file first!", "ERROR: Missing info", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (gridDrawn)
                    MessageBox.Show("Grid already drawn!", "ERROR: Can't draw same grid twice", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void customMap_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!String.IsNullOrEmpty(DataStorage.DrawType))               // if it is used for drawing, enter here
            {
                System.Windows.Point newPoint = new System.Windows.Point(e.GetPosition(customMap).X, e.GetPosition(customMap).Y);
                
                switch (DataStorage.DrawType)
                {
                    case "ellipse":
                        int drawingsNum = DataStorage.allDrawings.Count;
                        EllipseWindow ellipseWindow = new EllipseWindow();
                        ellipseWindow.ShowDialog();

                        if (DataStorage.allDrawings.Count > drawingsNum)        // new ellipse is added, proccess information 
                        {
                            // set ellipse at right coordinates
                            Canvas.SetLeft(DataStorage.allDrawings.Last(), newPoint.X);
                            Canvas.SetTop(DataStorage.allDrawings.Last(), newPoint.Y);

                            // now add it to canvas
                            customMap.Children.Add(DataStorage.allDrawings.Last());
                        }
                        break;
                    case "polygon":
                        // collecting all points of polygon
                        DataStorage.points.Add(newPoint);
                        break;
                    case "text":
                        TextWindow textWindow = new TextWindow();
                        textWindow.ShowDialog();

                        if (!String.IsNullOrEmpty(DataStorage.Text))           // only if text is entered
                        {
                            Label newText = new Label();
                            newText.Content = DataStorage.Text;
                            newText.FontSize = DataStorage.FontSize;
                            newText.Foreground = DataStorage.TextColor;
                            newText.Opacity = DataStorage.TextOpacity;

                            Canvas.SetLeft(newText, newPoint.X);
                            Canvas.SetTop(newText, newPoint.Y);

                            customMap.Children.Add(newText);


                            DataStorage.DrawType = String.Empty;
                        }
                        break;
                    default:
                        break;
                }
            }
            else if (e.Source is Polyline)  
            {
                if (animatedLines.Count != 0)
                {
                    foreach (var line in animatedLines)
                    {
                        line.Stroke = (Brush)(new BrushConverter().ConvertFrom("#CED0E4"));
                        line.StrokeThickness = 1;
                    }
                    if (animatedEntitiesIndexes[0] <= customMap.Children.Count && animatedEntitiesIndexes[1] <= customMap.Children.Count)
                    {
                        customMap.Children.RemoveAt(animatedEntitiesIndexes[1]);
                        customMap.Children.RemoveAt(animatedEntitiesIndexes[0]);
                    }
                    animatedEntitiesIndexes.Clear();
                }

                System.Windows.Shapes.Polyline selected = (System.Windows.Shapes.Polyline)e.OriginalSource;
                animatedLines.Add(selected);

                selected.Stroke = Brushes.Red;
                selected.StrokeThickness = 1;
                selected.ToolTip = selected.ToolTip;


                Ellipse point1 = new Ellipse()
                {
                    Name = String.Format("Elipsa1_{0}", nameCounter++),
                    Width = 4,
                    Height = 4,
                    Fill = Brushes.Yellow
                };

                Ellipse point2 = new Ellipse()
                {
                    Name = String.Format("Elipsa2_{0}", nameCounter++),
                    Width = 4,
                    Height = 4,
                    Fill = Brushes.Yellow
                };

                List<double> lista = new List<double>();
                int i = 0;



                foreach (var point in selected.Points)
                {
                    if (i == 0 || i == selected.Points.Count - 1)
                    {
                        lista.Add(point.X);
                        lista.Add(point.Y);
                    }
                    i++;
                }

                double x1 = lista[0] - 2;
                double y1 = lista[1] - 2;

                double x2 = lista[2] - 2;
                double y2 = lista[3] - 2;

                Canvas.SetLeft(point1, x1);
                Canvas.SetTop(point1, y1);

                Canvas.SetLeft(point2, x2);
                Canvas.SetTop(point2, y2);

                RegisterName(point1.Name, point1);
                RegisterName(point2.Name, point2);
                customMap.Children.Add(point1);
                customMap.Children.Add(point2);
                animatedEntitiesIndexes.Add(customMap.Children.IndexOf(point1));
                animatedEntitiesIndexes.Add(customMap.Children.IndexOf(point2));

                //animation

                ScaleTransform scalePoint1 = new ScaleTransform();
                scalePoint1.CenterX = point1.Width / 2;
                scalePoint1.CenterY = point1.Height / 2;

                ScaleTransform scalePoint2 = new ScaleTransform();
                scalePoint2.CenterX = point2.Width / 2;
                scalePoint2.CenterY = point2.Height / 2;

                DoubleAnimation doubleAnimation = new DoubleAnimation();
                doubleAnimation.From = 1;
                doubleAnimation.To = 2; 
                doubleAnimation.Duration = TimeSpan.FromSeconds(1);
                doubleAnimation.AutoReverse = true;
                doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;

                point1.RenderTransform = scalePoint1;
                point2.RenderTransform = scalePoint2;

                scalePoint1.BeginAnimation(ScaleTransform.ScaleXProperty, doubleAnimation);
                scalePoint1.BeginAnimation(ScaleTransform.ScaleYProperty, doubleAnimation);

                scalePoint2.BeginAnimation(ScaleTransform.ScaleXProperty, doubleAnimation);
                scalePoint2.BeginAnimation(ScaleTransform.ScaleYProperty, doubleAnimation);
            }
        }

        private void customMap_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DataStorage.DrawType.Equals("polygon"))
            {
                int drawingCount = DataStorage.allDrawings.Count;
                PolygonWindow polygonWindow = new PolygonWindow();
                polygonWindow.ShowDialog();

                if (DataStorage.allDrawings.Count > drawingCount)   // only if drawing is added to list
                {
                    customMap.Children.Add(DataStorage.allDrawings.Last());
                }
            }
            else
            {
                if ((e.Source is Polygon || e.Source is Ellipse || e.Source is TextBlock) && LogicalTreeHelper.GetParent(e.OriginalSource as DependencyObject) is Canvas)
                {
                    EditWindow editWindow = new EditWindow(LogicalTreeHelper.GetParent(e.OriginalSource as DependencyObject) as Canvas);
                    editWindow.ShowDialog();
                }
                else if (e.Source is Label)
                {
                    EditTextWindow editTextWindow = new EditTextWindow(e.Source as Label);
                    editTextWindow.ShowDialog();
                }
            }
        }

        private void draw_ellipse_btn_Click(object sender, RoutedEventArgs e)
        {
            DataStorage.DrawType = "ellipse";
        }

        private void draw_polygon_btn_Click(object sender, RoutedEventArgs e)
        {
            DataStorage.DrawType = "polygon";
        }

        private void add_text_btn_Click(object sender, RoutedEventArgs e)
        {
            DataStorage.DrawType = "text";
        }

        private void GetAllColors()
        {
            Type colorsType = typeof(System.Windows.Media.Colors);
            PropertyInfo[] colorsTypePropertyInfos = colorsType.GetProperties(BindingFlags.Public | BindingFlags.Static);

            foreach (PropertyInfo colorsTypePropertyInfo in colorsTypePropertyInfos)
                DataStorage.allColors.Add(colorsTypePropertyInfo.Name);
        }

        private void undo_btn_Click(object sender, RoutedEventArgs e)
        {
            if (cleared)    // after clean, return to state prior
            {
                var childrenList = BackUp.Children.Cast<UIElement>().ToArray();
                foreach (UIElement child in childrenList)
                {
                    BackUp.Children.Remove(child);
                    customMap.Children.Add(child);
                    if (child is Shape)
                        RegisterName(((Shape)child).Name, child);
                }
                cleared = false;
                
            }
            else if (customMap.Children.Count > 0)
            {
                deletedElement = customMap.Children[customMap.Children.Count - 1];
                customMap.Children.Remove(deletedElement);
            }
        }

        private void redo_btn_Click(object sender, RoutedEventArgs e)
        {
            if (deletedElement != null && !cleared)
                customMap.Children.Add(deletedElement);
            deletedElement = null;
        }

        private void clear_btn_Click(object sender, RoutedEventArgs e)
        {
            BackUp = new Canvas();
            gridDrawn = false;

            var childrenList = customMap.Children.Cast<UIElement>().ToArray();
            BackUp.Children.Clear();
            foreach (var child in childrenList)
            {
                customMap.Children.Remove(child);
                BackUp.Children.Add(child);
                if (child is Shape)
                    UnregisterName(((Shape)child).Name);
            }

            foreach (LineEntity line in DataStorage.lines)
                line.IsDrawn = false;

            DataStorage.crossPositions.Clear();

            cleared = true;
        }

        private void canvas_size_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string newSize = canvasSizes[canvas_size_cb.SelectedIndex];
            gridDrawn = false;

            // canvas size
            DataStorage.canvasWidth = Int32.Parse(newSize.Split('x')[0].Trim());    // 300, 1125, 2250
            DataStorage.canvasHeight = (int)(DataStorage.canvasWidth / 1.5);

            customMap.Height = DataStorage.canvasHeight;
            customMap.Width = DataStorage.canvasWidth;

            // matrix size
            switch (DataStorage.canvasWidth)
            {
                case 300:
                    DataStorage.matrix_X = 100;
                    break;
                case 1125:
                    DataStorage.matrix_X = 300;
                    break;
                case 2250:
                    DataStorage.matrix_X = 500;
                    break;
            }
            DataStorage.matrix_Y = (int)(Math.Floor(DataStorage.matrix_X / 1.5));
            matrix_size.Content = $"{DataStorage.matrix_X} x {DataStorage.matrix_Y}";

            // entity size
            DataStorage.entitySize = (double)DataStorage.canvasWidth / DataStorage.matrix_X;
            DataStorage.entityObjSize = DataStorage.entitySize / 2F;
            DataStorage.entityObjMove = DataStorage.entitySize / 4F;

            // matrix with all entities
            DataStorage.entities = new PowerEntity[DataStorage.matrix_X, DataStorage.matrix_Y];
            if (DataStorage.allEntitiesDict != null)
            { 
                foreach (var entity in DataStorage.allEntitiesDict.Values)
                    XML_Loader.Scale(entity);
            }

            foreach (LineEntity line in DataStorage.lines)
                line.IsDrawn = false;

            DataStorage.lineMap = new char[DataStorage.matrix_X, DataStorage.matrix_Y];
            DataStorage.InitPathMap(DataStorage.lineMap);
            DataStorage.crossPositions.Clear();

            foreach (var child in customMap.Children)
            {
                if (FindName((child as Shape).Name) != null)
                {
                    if (child is Shape)
                        UnregisterName((child as Shape).Name);
                }
            }
            customMap.Children.Clear();
        }

        private void draw_grid_UndergroundFirst_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!load_btn.IsEnabled && !gridDrawn)
            {
                bool excludeNodes = (bool)nodes_checkbox.IsChecked;
                DateTime start = DateTime.Now;
                DrawBFSLinesUndergoundFirst(excludeNodes);
                DrawCrossSections();
                DateTime end = DateTime.Now;
                draw_lines_time.Content = end.Subtract(start).ToString();
                DrawEntities();
                gridDrawn = true;
                cleared = false;
            }
            else if (load_btn.IsEnabled)
                MessageBox.Show("Please load information from XML file first!", "ERROR: Missing info", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (gridDrawn)
                MessageBox.Show("Grid already drawn!", "ERROR: Can't draw same grid twice", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
