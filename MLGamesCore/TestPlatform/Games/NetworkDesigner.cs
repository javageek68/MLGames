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

        string strColLayerName = "LayerName";
        string strColSize = "Size";
        string strColActivation = "Activation";
        DataTable dtNetwork = null;

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
            this.CreateNetworkDataTable();
        }

        private void CreateNetworkDataTable()
        {
            this.dtNetwork = new DataTable();
            this.dtNetwork.Columns.Add(strColLayerName);
            this.dtNetwork.Columns.Add(strColSize);
            this.dtNetwork.Columns.Add(strColActivation);
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
            this.SetLayerNames();
            this.CreateNetworkDataTable();
            int intLayerNo = 0;
            List<int> lstLayers = new List<int>();
            List<NNSettings.ActivationFunctions> lstAct = new List<NNSettings.ActivationFunctions>();
            foreach(DataGridViewRow row in this.dvGrid.Rows)
            {
                if (row.Cells[this.strColSize].Value == null || row.Cells[this.strColActivation].Value == null) return;
                DataRow datarow = this.dtNetwork.NewRow();

                //get layer size from grid
                string strSize = row.Cells[this.strColSize].Value.ToString();
                //add layer size to the list
                lstLayers.Add(int.Parse(strSize));


                //get the index of the activation from the grid
                string strActIndex = row.Cells[this.strColActivation].Value.ToString();
                int intActIndex = int.Parse(strActIndex);

                //now that we have parsed all of the columns, we can populate the data table row
                datarow[strColLayerName] = string.Format("Layer_{0}", intLayerNo);
                datarow[strColSize] = strSize;
                datarow[strColActivation] = strActIndex;

                //add the new row
                this.dtNetwork.Rows.Add(datarow);

                ////convert to in and subject 1 to account for the "none" in the first position
                ////note that the user should select "none" for the input layer
                //intActIndex--;
                ////if the user did not select "none", then add it to the list
                //if (intActIndex > -1)
                //{
                //    NNSettings.ActivationFunctions activationFunction = (NNSettings.ActivationFunctions)intActIndex;
                //    lstAct.Add(activationFunction);
                //}
                //intLayerNo++;
             
            }

            
            //there should be 1 less activation functions than layers 
            //since the first layer doesn't have an activation function.
            //so, if they are equal, then we need to remove the first 
            //activation function.
            //if (lstAct.Count == lstLayers.Count) lstAct.RemoveAt(0);

            //int[] layers = lstLayers.ToArray();
            //NNSettings.ActivationFunctions[] actFuncts = lstAct.ToArray();
            //this.dvGrid.DataSource = this.dtNetwork; 

        }

        /// <summary>
        /// 
        /// </summary>
        private void SetLayerNames()
        {
            //get the index position of the output layer
            int intOutputLayerIdx = this.GetOutputLayerIdx();

            for (int idx = 0; idx <= intOutputLayerIdx; idx++)
            {
                string strLayerName = string.Empty;
                if (idx == 0) strLayerName = "Input Layer";
                else if (idx == intOutputLayerIdx) strLayerName = "Output Layer";
                else strLayerName = string.Format("Hidden Layer {0}",idx);
                //set the layer name
                this.dvGrid.Rows[idx].Cells[strColLayerName].Value = strLayerName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetOutputLayerIdx()
        {
            int intOutputLayerIdx = 0;
            //The grid view creates a new row as soon as the user starts editing the one before it
            //So, the last row in the grid may not be intended to be the output layer.
            //We will consider the last complete row to be the output layer
            for (int index = this.dvGrid.Rows.Count-1; index >= 0; index--)
            {
                if (this.RowComplete(index))
                {
                    intOutputLayerIdx = index;
                    break;
                }
            }
            return intOutputLayerIdx;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intRow"></param>
        /// <returns></returns>
        private bool RowComplete(int intRow)
        {
            DataGridViewRow row = this.dvGrid.Rows[intRow];
            string strSize = string.Empty;
            string strAct = string.Empty;

            if (row.Cells[this.strColSize].Value != null) strSize = row.Cells[this.strColSize].Value.ToString().Trim();
            if (row.Cells[this.strColActivation].Value != null) strAct = row.Cells[this.strColActivation].Value.ToString().Trim();

            bool blnSizeSelected = strSize.Length > 0;
            bool blnActSelected = strAct.Length > 0;

            return (blnSizeSelected && blnActSelected);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx1"></param>
        /// <param name="idx2"></param>
        private void Swap(int idx1, int idx2)
        {
            DataGridViewRow row1 = this.dvGrid.Rows[idx1];
            DataGridViewRow row2 = this.dvGrid.Rows[idx2];

            //make swap space
            object strSize = string.Empty;
            object strAct = string.Empty;

            //store row1 in swap
            strSize = row1.Cells[this.strColSize].Value;
            strAct = row1.Cells[this.strColActivation].Value;

            //move row2 to row1
            row1.Cells[this.strColSize].Value = row2.Cells[this.strColSize].Value;
            row1.Cells[this.strColActivation].Value = row2.Cells[this.strColActivation].Value;

            //move swap to row2
            row2.Cells[this.strColSize].Value = strSize;
            row2.Cells[this.strColActivation].Value = strAct;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuMoveLayerUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dvGrid.SelectedRows != null && this.dvGrid.SelectedRows.Count > 0)
                {
                    int idx = this.dvGrid.SelectedRows[0].Index;
                    if (idx > 0)
                    {
                        int intOtherIdx = idx - 1;
                        this.Swap(idx, intOtherIdx);
                    }
                }
            }
            catch (Exception ex)
            {
                this.HandleError(ex.ToString());
            }
  
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuMoveLayerDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dvGrid.SelectedRows != null && this.dvGrid.SelectedRows.Count > 0)
                {
                    int idx = this.dvGrid.SelectedRows[0].Index;
                    int intOutputLayerIdx = this.GetOutputLayerIdx();
                    if (idx < intOutputLayerIdx)
                    {
                        int intOtherIdx = idx + 1;
                        this.Swap(idx, intOtherIdx);
                    }
                }
            }
            catch (Exception ex)
            {
                this.HandleError(ex.ToString());
            }
    
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuDeleteLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dvGrid.SelectedRows != null && this.dvGrid.SelectedRows.Count > 0 )
            {
                DataGridViewRow row = this.dvGrid.SelectedRows[0];
                this.dvGrid.Rows.Remove(row);
            }
        }

        private void HandleError(string strMsg)
        {
            MessageBox.Show(strMsg);
        }
    }
}
