using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using SOUNDS_PACKING_PROJECT.Class;
namespace SOUNDS_PACKING_PROJECT
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        #region GUI Function
        private void Close_Buttom_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Input_Enter(object sender, EventArgs e)
        {
            if (Input.Text == "Enter the sourse of sounds Folder ...")
            {
                Input.Text = "";
            }
        }
        private void Input_Leave(object sender, EventArgs e)
        {
            if (Input.Text == "")
            {
                Input.Text = "Enter the sourse of sounds Folder ...";
            }
        }
        private void Output_Folder_Enter(object sender, EventArgs e)
        {
            if (Output_Folder.Text == "Where you would place the output ?")
            {
                Output_Folder.Text = "";
            }
        }
        private void Output_Folder_Leave(object sender, EventArgs e)
        {
            if (Output_Folder.Text == "")
            {
                Output_Folder.Text = "Where you would place the output ?";
            }
        }
        private void Show_Folder_Buttom_Click(object sender, EventArgs e)
        {
            Sound_Folder.ShowDialog();
            Input.Text = Sound_Folder.SelectedPath.ToString();
            Sound_Folder.ShowNewFolderButton = false;
        }
        private void Show_OutputFolder_Buttom_Click(object sender, EventArgs e)
        {
            Output.ShowDialog();
            Output_Folder.Text = Output.SelectedPath.ToString();
        }
        #endregion




        private void Start_Buttom_Click(object sender, EventArgs e)
        {
            
            Sound_Allocation A = new Sound_Allocation();
            A.Worst_Fit_Algo_PriorityQueue(Input.Text, Output_Folder.Text, 100);
        }

        

    }
}
