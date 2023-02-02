using System.Windows;

namespace WpfTreeView;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    #region Constructor

    /// <summary>
    /// Default constructor
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new DirectoryStructureViewModel();
    }

    #endregion
}
