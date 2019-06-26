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
using LiveCharts;
using LiveCharts.Wpf;
using SOUNDS_PACKING_PROJECT.Class;
namespace SOUNDS_PACKING_PROJECT
{
    public partial class Main : Form
    {
        public Main()
        {

            InitializeComponent();
       
        }

        // Start Buttom Event
        //
        private void Start_Buttom_Click(object sender, EventArgs e)
        {
            bool Text_Only = true;

            if (Text_File_Only.Checked == true)
            {
                Text_Only = false;
            }

            Sound_Allocation File = new Sound_Allocation();

            Stopwatch Timer = new Stopwatch();

            string Algo_Time = "";

            if (Strategies.Text == "All (Liner Search)")
            {

                Timer = File.Worst_Fit_Algo_LinerSearch(Input.Text, Output_Folder.Text, int.Parse(Time.Text), File_Type.Text, Text_Only); 
                File.Split_Timer(Timer, ref Algo_Time);
                Worst_Liner_Time.Text = Algo_Time;

                Timer = File.Worst_Fit_Dec_Algo_LinerSearch(Input.Text, Output_Folder.Text, int.Parse(Time.Text), File_Type.Text, Text_Only);
                File.Split_Timer(Timer, ref Algo_Time);
                Worst_Decreasing_Liner_Time.Text = Algo_Time;

                Timer = File.First_Fit_Dec_Algo_LinerSearch(Input.Text, Output_Folder.Text, int.Parse(Time.Text), File_Type.Text, Text_Only);
                File.Split_Timer(Timer, ref Algo_Time);
                First_Decreasing_Liner_Time.Text = Algo_Time;

                Timer = File.Best_Fit_Algo_LinerSearch(Input.Text, Output_Folder.Text, int.Parse(Time.Text), File_Type.Text, Text_Only);
                File.Split_Timer(Timer, ref Algo_Time);
                Best_Liner_Time.Text = Algo_Time;

                Timer = File.Best_Fit_Dec_Algo_LinerSearch(Input.Text, Output_Folder.Text, int.Parse(Time.Text), File_Type.Text, Text_Only);
                File.Split_Timer(Timer, ref Algo_Time);
                Best_Decreasing_Liner_Time.Text = Algo_Time;

                Timer = File.Folder_Filling_Algo(Input.Text, Output_Folder.Text, int.Parse(Time.Text), File_Type.Text, Text_Only);
                File.Split_Timer(Timer, ref Algo_Time);
                Folder_Fill_Time.Text = Algo_Time;
            }
            else if (Strategies.Text == "All (Priority Queue)")
            {
                File.Worst_Fit_Algo_PriorityQueue(Input.Text, Output_Folder.Text, int.Parse(Time.Text), File_Type.Text, Text_Only);
                File.Worst_Fit_Dec_Algo_PriorityQueue(Input.Text, Output_Folder.Text, int.Parse(Time.Text), File_Type.Text, Text_Only);
            }
            else if (Strategies.Text == "Worst Fit (Liner Search)")
            {
                Timer = File.Worst_Fit_Algo_LinerSearch(Input.Text, Output_Folder.Text, int.Parse(Time.Text), File_Type.Text, Text_Only);
                File.Split_Timer(Timer, ref Algo_Time);
                Worst_Liner_Time.Text = Algo_Time;
            }
            else if (Strategies.Text == "Worst Fit (Priority Queue)")
            {
                Timer = File.Worst_Fit_Algo_PriorityQueue(Input.Text, Output_Folder.Text, int.Parse(Time.Text), File_Type.Text, Text_Only);
                File.Split_Timer(Timer, ref Algo_Time);
                Worst_Priority_Time.Text = Algo_Time;
            }
            else if (Strategies.Text == "Worst Fit Decreasing (Liner Search)")
            {
                Timer = File.Worst_Fit_Dec_Algo_LinerSearch(Input.Text, Output_Folder.Text, int.Parse(Time.Text), File_Type.Text, Text_Only);
                File.Split_Timer(Timer, ref Algo_Time);
                Worst_Decreasing_Liner_Time.Text = Algo_Time;
            }
            else if (Strategies.Text == "Worst Fit Decreasing (Priority Queue)")
            {
                Timer = File.Worst_Fit_Dec_Algo_PriorityQueue(Input.Text, Output_Folder.Text, int.Parse(Time.Text), File_Type.Text, Text_Only);
                File.Split_Timer(Timer, ref Algo_Time);
                Worst_Decreasing_Priority_Time.Text = Algo_Time;
            }
            else if (Strategies.Text == "First Fit Decreasing (Liner Search)")
            {
                Timer = File.First_Fit_Dec_Algo_LinerSearch(Input.Text, Output_Folder.Text, int.Parse(Time.Text), File_Type.Text, Text_Only);
                File.Split_Timer(Timer, ref Algo_Time);
                First_Decreasing_Liner_Time.Text = Algo_Time;
            }
            else if (Strategies.Text == "Best Fit (Liner Search)")
            {
                Timer = File.Best_Fit_Algo_LinerSearch(Input.Text, Output_Folder.Text, int.Parse(Time.Text), File_Type.Text, Text_Only);
                File.Split_Timer(Timer, ref Algo_Time);
                Best_Liner_Time.Text = Algo_Time;
            }
            else if (Strategies.Text == "Best Fit Decreasing (Liner Search)")
            {
                Timer = File.Best_Fit_Dec_Algo_LinerSearch(Input.Text, Output_Folder.Text, int.Parse(Time.Text), File_Type.Text, Text_Only);
                File.Split_Timer(Timer, ref Algo_Time);
                Best_Decreasing_Liner_Time.Text = Algo_Time;
            }
            else if (Strategies.Text == "Folder Fill Algorithm")
            {
                Timer = File.Folder_Filling_Algo(Input.Text, Output_Folder.Text, int.Parse(Time.Text), File_Type.Text, Text_Only);
                File.Split_Timer(Timer, ref Algo_Time);
                Folder_Fill_Time.Text = Algo_Time;
            }
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




    }
}
