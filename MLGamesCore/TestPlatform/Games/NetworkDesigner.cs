using MLGames;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPlatform.Games
{
    public partial class NetworkDesigner : Form
    {
        //NeuralNetwork nn = new NeuralNetwork()
        public NetworkDesigner()
        {
            InitializeComponent();
            this.Load += NetworkDesigner_Load;
            
        }

        private void NetworkDesigner_Load(object sender, EventArgs e)
        {
            this.PopulationActivationDDL();
        }

        private void PopulationActivationDDL()
        {
            string strName = "Name";
            string strValue = "Value";
            this.Activation.ValueMember = strValue;
            this.Activation.DisplayMember = strName;

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(strName);
            dataTable.Columns.Add(strValue);
            int intRowNo = 0;
            foreach (var strFunction in Enum.GetValues(typeof(NNSettings.ActivationFunctions)))
            {
                DataRow row = dataTable.NewRow();
                row[strName] = strFunction.ToString();
                row[strValue] = intRowNo;
                dataTable.Rows.Add(row);
                intRowNo++;
            }

            this.Activation.DataSource = dataTable;
        }
    }
}
