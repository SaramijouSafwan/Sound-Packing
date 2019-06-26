using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SOUNDS_PACKING_PROJECT.Class;
using System.Diagnostics;
using System.Windows.Forms;
namespace SOUNDS_PACKING_PROJECT.Class
{
    class Sound_Allocation
    {
        // Calculate Time Duration
        //
        public string Cal_Duration(int Secound)
        {
            int hour = Secound / 3600;

            int rem = Secound - (3600 * hour);

            int min = rem / 60;

            int rem1 = rem - (min * 60);

            int sec = rem1;

            string Duration = hour + ":" + min + ":" + sec;

            return Duration;
        }

        // Read Sound Text File
        //
        public List<Sound_File> Read_Text_File(string Text_Path)
        {  
            FileStream File = new FileStream(Text_Path, FileMode.Open);
            StreamReader Read_File = new StreamReader(File);

            List<Sound_File> Sounds = new List<Sound_File>();

            int Number_Sound_File = int.Parse(Read_File.ReadLine());

            while (Read_File.Peek() != -1)
            {
                string Line = Read_File.ReadLine();

                string[] Split1 = Line.Split('.');

                string Sound_Name = Split1[0];

                string[] Split2 = Line.Split(' ');

                string[] Duration = Split2[1].Split(':');

                int Sound_Duration = int.Parse(Duration[0]) * 60 * 60 + int.Parse(Duration[1]) * 60 + int.Parse(Duration[2]);

                Sound_File Sound = new Sound_File(Sound_Name, Sound_Duration);

                Sounds.Add(Sound);
            }

            Read_File.Close();
            File.Close();

            return Sounds;
        }

        // Write Sound Text File
        //
        public void Write_TextFile(List<Folder> Folder, string Output_Path)
        {
            for (int i = 0; i < Folder.Count(); i++)
            {
                FileStream File = new FileStream(Output_Path + @"\" + Folder[i].Get_Name() + "_METADATA.Txt", FileMode.Append);
                StreamWriter Write_File = new StreamWriter(File);

                string FileName = Folder[i].Get_Name();
                Write_File.WriteLine(FileName);

                List<Sound_File> Sounds = Folder[i].Get_Sound_List();

                for (int j = 0; j < Sounds.Count(); j++)
                {
                    string Sound_Name = Sounds[j].Get_Name() + ".mp3 ";

                    int Secound = Sounds[j].Get_Duration();

                    string Duration = Cal_Duration(Secound);

                    string Line = Sound_Name + Duration;

                    Write_File.WriteLine(Line);
                }

                string Folder_Duration = Cal_Duration(Folder[i].Get_Duration());

                Write_File.WriteLine(Folder_Duration);

                Write_File.Close();
                File.Close();
            }


        }

        // Copy Sound From Source To Destination
        //
        public void Copy_Sound_File(List<Folder> Folder, string Input_Path, string Output_Path)
        {
            for (int i = 0; i < Folder.Count(); i++)
            {
                if (!Directory.Exists(Output_Path + @"\" + Folder[i].Get_Name()))
                {
                    Directory.CreateDirectory(Output_Path + @"\" + Folder[i].Get_Name());
                }

                List<Sound_File> Sounds = Folder[i].Get_Sound_List();

                string Folder_Path = Output_Path + @"\" + Folder[i].Get_Name();

                for (int j = 0; j < Sounds.Count(); j++)
                {
                    if (!File.Exists(Folder_Path + @"\" + Sounds[j].Get_Name() + ""))
                    {
                        File.Copy(Input_Path + @"\" + Sounds[j].Get_Name() + ".mp3", Folder_Path + @"\" + Sounds[j].Get_Name() + "");
                    }
                }
            }
        }




        // Get Most Remaining Duration Liner Search
        //
        public Folder Most_Remaining_Duration_LinerSearch(List<Folder> Folders, ref int Folder_Index)
        {
            int Max_Free_Space = 0;

            for (int i = 0; i < Folders.Count(); i++)
            {
                if (Folders[i].Get_FreeSpace() > Folders[Max_Free_Space].Get_FreeSpace())
                {
                    Max_Free_Space = i;
                }
            }

            Folder_Index = Max_Free_Space;
            return Folders[Max_Free_Space];
        }

        // Worst Fit Algorithm Using Liner Search
        //
        public void Worst_Fit_Algo_LinerSearch(string Input, string Output, int Folder_Duration)
        {
            string Output_File = Output + @"\OUTPUT";

            if (!Directory.Exists(Output_File))
            {
                Directory.CreateDirectory(Output_File);
            }

            string StrategiesName_File = Output_File + @"\[1]WorstFit";

            if (!Directory.Exists(StrategiesName_File))
            {
                Directory.CreateDirectory(StrategiesName_File);
            }

            string Input_TextFile_Path = Input + @"\AudiosInfo.txt";

            List<Sound_File> Sounds = Read_Text_File(Input_TextFile_Path);

            List<Folder> Folders = new List<Folder>();

            Folder Folder = new Folder();

            if (Folders.Count() == 0)
            {
                Folder.Set_Name("F1");
                Folder.Set_Duration(0);
                Folder.Set_FreeSpace(Folder_Duration);
                Folders.Add(Folder);
            }

            for(int i = 0; i < Sounds.Count();i++)
            {
                int Folder_Index = 0;

                Folder Max_Folder = Most_Remaining_Duration_LinerSearch(Folders,ref Folder_Index);

                if (Max_Folder.Get_FreeSpace() >= Sounds[i].Get_Duration())
                {
                    int New_Folder_Duration = Max_Folder.Get_Duration() + Sounds[i].Get_Duration();
                    int New_Folder_FreeSpace = Max_Folder.Get_FreeSpace() - Sounds[i].Get_Duration();
                    Max_Folder.Set_Sound(Sounds[i]);
                    Max_Folder.Set_Duration(New_Folder_Duration);
                    Max_Folder.Set_FreeSpace(New_Folder_FreeSpace);
                    Folders.RemoveAt(Folder_Index);
                    Folders.Add(Max_Folder);
                }
                else
                {
                    Folder New_Folder = new Folder();
                    New_Folder.Set_Name("F" + (Folders.Count() + 1 )+ "");
                    New_Folder.Set_Duration(Sounds[i].Get_Duration());
                    New_Folder.Set_FreeSpace(Folder_Duration - Sounds[i].Get_Duration());
                    New_Folder.Set_Sound(Sounds[i]);
                    Folders.Add(New_Folder);
                }

            }

            string Output_TextFile = StrategiesName_File;

            Write_TextFile(Folders, Output_TextFile);

            string Output_SoundFile = StrategiesName_File;

            string Input_SoundFile  = Input + @"\Audios";

            Copy_Sound_File(Folders, Input_SoundFile, Output_SoundFile);
        }
        
        // Worst Fit Algorthm Using Priority Queue
        //
        public void Worst_Fit_Algo_PriorityQueue(string Input, string Output, int Folder_Duration)
        {
            string Output_File = Output + @"\OUTPUT";

            if (!Directory.Exists(Output_File))
            {
                Directory.CreateDirectory(Output_File);
            }

            string StrategiesName_File = Output_File + @"\[1]WorstFit";

            if (!Directory.Exists(StrategiesName_File))
            {
                Directory.CreateDirectory(StrategiesName_File);
            }

            string Input_TextFile_Path = Input + @"\AudiosInfo.txt";

            List<Sound_File> Sounds = Read_Text_File(Input_TextFile_Path);

            PriorityQueue Folders = new PriorityQueue();

            Folder Folder = new Folder();

            if (Folders.Count() == 0)
            {
                Folder.Set_Name("F1");
                Folder.Set_Duration(0);
                int New_Free_Space = (-1) * Folder_Duration;
                Folder.Set_FreeSpace(New_Free_Space);
                Folders.Enqueue(Folder);
            }

            for (int i = 0; i < Sounds.Count(); i++)
            {


                Folder Max_Folder = Folders.Peek();
                
                if ((Max_Folder.Get_FreeSpace())*(-1) >= Sounds[i].Get_Duration())
                {
                    int New_Folder_Duration = Max_Folder.Get_Duration() + Sounds[i].Get_Duration();
                    int New_Folder_FreeSpace = (Max_Folder.Get_FreeSpace() * (-1)) - Sounds[i].Get_Duration();
                    Max_Folder.Set_Sound(Sounds[i]);
                    Max_Folder.Set_Duration(New_Folder_Duration);
                    int New_Free_Space = ((-1) * New_Folder_FreeSpace);
                    Max_Folder.Set_FreeSpace(New_Free_Space);
                    Folders.Dequeue();
                    Folders.Enqueue(Max_Folder);
                }
                else
                {
                    Folder New_Folder = new Folder();
                    New_Folder.Set_Name("F" + (Folders.Count() + 1) + "");
                    New_Folder.Set_Duration(Sounds[i].Get_Duration());
                    int New_Free_Space = (Folder_Duration - Sounds[i].Get_Duration()) * (-1);
                    New_Folder.Set_FreeSpace(New_Free_Space);
                    New_Folder.Set_Sound(Sounds[i]);
                    Folders.Enqueue(New_Folder);
                }

            }

            int Size = Folders.Count();

            List<Folder> List_Folders = new List<Folder>();

            for (int i = 0; i < Size; i++)
            {
                Folder folder = Folders.Dequeue();

                int FreeSpace = (folder.Get_FreeSpace() * (-1));

                folder.Set_FreeSpace(FreeSpace);

                List_Folders.Add(folder);
            }
            string Output_TextFile = StrategiesName_File;

            Write_TextFile(List_Folders, Output_TextFile);

            string Output_SoundFile = StrategiesName_File;

            string Input_SoundFile = Input + @"\Audios";

            Copy_Sound_File(List_Folders, Input_SoundFile, Output_SoundFile);
        }
    }
}
