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

namespace Trader.WPF.Views
{
    /// <summary>
    /// Logika interakcji dla klasy SellView.xaml
    /// </summary>
    public partial class SellView : UserControl
    {



        public ICommand SelectedAssetChangedCommand
        {
            get { return (ICommand)GetValue(SelectedAssetChangedProperty); }
            set { SetValue(SelectedAssetChangedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedAssetChangedProperty =
            DependencyProperty.Register("SelectedAssetChangedCommand", typeof(ICommand), typeof(SellView), new PropertyMetadata(null));


        public SellView()
        {
            InitializeComponent();
        }

        private void cbAssets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbAssets.SelectedItem != null)
            {
                SelectedAssetChangedCommand?.Execute(null);
            }
            
        }
    }
}
