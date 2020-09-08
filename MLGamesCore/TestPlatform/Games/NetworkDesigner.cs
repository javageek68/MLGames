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
        string strActivationNone = "none";

        string strColSize = "Size";
        string strColActivation = "Activation";

        //NeuralNetwork nn = new NeuralNetwork()
        public NetworkDesigner()
        {
            InitializeComponent();
            this.Load += NetworkDesigner_Load;
            this.dvGrid.CellValueChanged += DvGrid_CellValueChanged;
            
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
            DataRow firstrow = dataTable.NewRow();
            firstrow[strName] = strActivationNone;
            firstrow[strValue] = 0;
            dataTable.Rows.Add(firstrow);

            int intRowNo = 1;
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

        private void DvGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (this.dvGrid.CurrentRow != null)
            //{
            //    DataGridViewRow dgRow = this.dvGrid.CurrentRow;
            //    string strSize = dgRow.Cells["Size"].Value.ToString();
            //    string strActivation = dgRow.Cells["Activation"].Value.ToString();
            //}
            this.SaveGridData();
        }

        private void SaveGridData()
        {
            
            List<int> lstLayers = new List<int>();
            List<NNSettings.ActivationFunctions> lstAct = new List<NNSettings.ActivationFunctions>();
            foreach(DataGridViewRow row in this.dvGrid.Rows)
            {
                if (row.Cells[this.strColSize].Value == null || row.Cells[this.strColActivation].Value == null) return;

                //get layer size from grid
                string strSize = row.Cells[this.strColSize].Value.ToString();
                //add layer size to the list
                lstLayers.Add(int.Parse(strSize));

                //get the index of the activation from the grid
                string strActIndex = row.Cells[this.strColActivation].Value.ToString();
                //convert to in and subject 1 to account for the "none" in the first position
                //note that the user should select "none" for the input layer
                int intActIndex = int.Parse(strActIndex) - 1;
                //if the user did not select "none", then add it to the list
                if (intActIndex > -1)
                {
                    NNSettings.ActivationFunctions activationFunction = (NNSettings.ActivationFunctions)intActIndex;
                    lstAct.Add(activationFunction);
                }
             
            }
            int[] layers = lstLayers.ToArray();
            NNSettings.ActivationFunctions[] actFuncts = lstAct.ToArray();

        }
    }
}
