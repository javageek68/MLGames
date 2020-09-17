using MLGames;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace TestPlatform.Games
{

    public partial class NetworkDesigner : Form
    {
        string strTableName = "NetworkDefinitionTable";
        string strActivationNone = "none";

        string strColLayerName = "LayerName";
        string strColSize = "LayerSize";
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layers"></param>
        /// <param name="activationFunctions"></param>
        public void GetNetworkStructure(ref int[] layers, ref int[] activationFunctions)
        {
            List<int> lstLayers = new List<int>();
            List<int> lstActivations = new List<int>();
            //make sure the grid data is saved to the data table
            this.SaveGridData();
            //copy structure to lists
            foreach(DataRow dataRow in this.dtNetwork.Rows)
            {
                //the last row is emty sometimes
                if (dataRow[this.strColSize].ToString().Trim().Length == 0) break;
                int intLayerSize = int.Parse( dataRow[this.strColSize].ToString());
                //parse the activation function
                //subtract 1 to account for the "none" entry at the beginning of the list in the dropdown.
                int intActivationFunction = (int.Parse(dataRow[this.strColActivation].ToString()))-1;
                //add items to lists
                lstLayers.Add(intLayerSize);
                lstActivations.Add(intActivationFunction);
            }
            //the first layer does not have an activation function
            lstActivations.RemoveAt(0);

            //convert the lists to arrays
            layers = lstLayers.ToArray();
            activationFunctions = lstActivations.ToArray();
        }

        private void CreateNetworkDataTable()
        {
            this.dtNetwork = new DataTable();
            this.dtNetwork.TableName = this.strTableName;
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
            //keep layer names updated
            this.SetLayerNames();
        }

        /// <summary>
        /// 
        /// </summary>
        private void SaveGridData()
        {
            
            this.CreateNetworkDataTable();
            foreach (DataGridViewRow row in this.dvGrid.Rows)
            {
                DataRow datarow = this.dtNetwork.NewRow();
                for(int i = 0; i < row.Cells.Count; i++)
                {
                    datarow[i] = row.Cells[i].Value;
                }
                this.dtNetwork.Rows.Add(datarow);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadGridData()
        {
            this.dvGrid.Rows.Clear();
            //loop through each row in the datatable
            foreach(DataRow datarow in this.dtNetwork.Rows)
            {
                //make a list of string
                List<string> lstRow = new List<string>();
                
                //add each field in the row to a list of strings
                for (int i = 0; i < datarow.Table.Columns.Count; i++)
                {
                    lstRow.Add(datarow[i].ToString());
                }

                //add the list of strings to the new data view grid row
                this.dvGrid.Rows.Add(lstRow.ToArray());
            }
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
            object objSize = string.Empty;
            object objAct = string.Empty;

            //store row1 in swap
            objSize = row1.Cells[this.strColSize].Value;
            objAct = row1.Cells[this.strColActivation].Value;

            //move row2 to row1
            row1.Cells[this.strColSize].Value = row2.Cells[this.strColSize].Value;
            row1.Cells[this.strColActivation].Value = row2.Cells[this.strColActivation].Value;

            //move swap to row2
            row2.Cells[this.strColSize].Value = objSize;
            row2.Cells[this.strColActivation].Value = objAct;
        }

        #region "Save and Load"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sfdFiles.Title = "Select Network Structure File - File Selection";
            this.sfdFiles.Filter = "Network Structure (*.nsf)|*.nsf|All Files (*.*)|*.*";
            if (this.sfdFiles.ShowDialog() == DialogResult.OK)
            {
                //save the grid to a datatable
                this.SaveGridData();
                //write the datatable to a file
                this.dtNetwork.WriteXml(this.sfdFiles.FileName);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ofdFiles.Title = "Select Network Structure File - File Selection";
            this.ofdFiles.Filter = "Network Structure (*.nsf)|*.nsf|All Files (*.*)|*.*";
            if (this.ofdFiles.ShowDialog() == DialogResult.OK)
            {
                this.CreateNetworkDataTable();
                this.dtNetwork.ReadXml(this.ofdFiles.FileName);
                this.LoadGridData();
                //this.dvGrid.DataSource = this.dtNetwork;
            }
        }
        #endregion

        #region"Context Menu"

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
        #endregion

        private void HandleError(string strMsg)
        {
            MessageBox.Show(strMsg);
        }


    }
}
