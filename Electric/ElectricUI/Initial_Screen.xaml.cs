using DBLibrary;
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

namespace ElectricUI
{
    /// <summary>
    /// Interaction logic for Initial_Screen.xaml
    /// </summary>
    public partial class Initial_Screen : Page
    {
        //electricEntities db = new electricEntities("metadata=res://*/ElectricModel.csdl|res://*/ElectricModel.ssdl|res://*/ElectricModel.msl;provider=System.Data.SqlClient;provider connection string='data source=smcaleer-hp-355;initial catalog=electric;persist security info=True;user id=electric;password=electricPassw0rd!;MultipleActiveResultSets=True;App=EntityFramework'");
        public User user = new User();
   
        public Initial_Screen()
        {
            InitializeComponent();
        }

    

    }
}
