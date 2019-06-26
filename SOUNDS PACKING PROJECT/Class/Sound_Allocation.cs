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
        Stopwatch Timer = new Stopwatch();

        public void Split_Timer(Stopwatch Timer, ref string Time)
        {
            int  t = Timer.Elapsed.Milliseconds;

            int Min, Sec, Mill;

            Min = t / (60 * 1000);

            t = t - (Min * 60000);

            Sec = t / 1000;

            t = t - (Sec * 1000);

            Mill = t;

            if (Min <= 9)
            {
                Time = "0" + Min;
            }
            else
            {
                Time = Min.ToString();
            }

            Time = Time + " : ";

            if (Sec <= 9)
            {
                Time = Time +  "0" + Sec;
            }
            else
            {
                Time = Time + Sec.ToString();
            }

            Time = Time + " : ";

            if (Mill <= 9)
            {
                Time = Time + "0" + Mill;
            }
            else
            {
                Time = Time + Mill.ToString();
            }
        }

        //Get Max Folder
        //
        public int Get_Max(int X, int Y)
        {
            if (X > Y)
                return X;
            else
                return Y;
        }

        // Sounds Merge Sort Algorithm From Max To Min
        //
        void Merge(List<Sound_File> Sounds, int Left, int Mid, int Right)
        {
            int i, j, k;
            int n1 = Mid - Left + 1;
            int n2 = Right - Mid;

            List<Sound_File> L = new List<Sound_File>(n1);
            List<Sound_File> R = new List<Sound_File>(n2);
            

            for (i = 0; i < n1; i++)
                L.Add(Sounds[Left + i]);
            for (j = 0; j < n2; j++)
                R.Add(Sounds[Mid + 1 + j]);

            i = 0;
            j = 0;
            k = Left;
            while (i < n1 && j < n2)
            {
                if (L[i].Get_Duration() > R[j].Get_Duration())
                {
                    Sounds[k] = L[i];
                    i++;
                }
                else
                {
                    Sounds[k] = R[j];
                    j++;
                }
                k++;
            }

            while (i < n1)
            {
                Sounds[k] = L[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                Sounds[k] = R[j];
                j++;
                k++;
            }
        }
        void Merge_Sort(List<Sound_File> Sounds, int Left, int Right)
        {
            if (Left < Right)
            {
                int Mid = (Left + Right) / 2;

                Merge_Sort(Sounds, Left, Mid);
                Merge_Sort(Sounds, Mid + 1, Right);

                Merge(Sounds, Left, Mid, Right);
            }
        }

        // Sound Merge Sort Algorithm From Min To Max
        //
        void Merge_Up(List<Sound_File> Sounds, int Left, int Mid, int Right)
        {
            int i, j, k;
            int n1 = Mid - Left + 1;
            int n2 = Right - Mid;

            List<Sound_File> L = new List<Sound_File>(n1);
            List<Sound_File> R = new List<Sound_File>(n2);


            for (i = 0; i < n1; i++)
                L.Add(Sounds[Left + i]);
            for (j = 0; j < n2; j++)
                R.Add(Sounds[Mid + 1 + j]);

            i = 0;
            j = 0;
            k = Left;
            while (i < n1 && j < n2)
            {
                if (L[i].Get_Duration() <= R[j].Get_Duration())
                {
                    Sounds[k] = L[i];
                    i++;
                }
                else
                {
                    Sounds[k] = R[j];
                    j++;
                }
                k++;
            }

            while (i < n1)
            {
                Sounds[k] = L[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                Sounds[k] = R[j];
                j++;
                k++;
            }
        }
        void Merge_Sort_Up(List<Sound_File> Sounds, int Left, int Right)
        {
            if (Left < Right)
            {
                int Mid = (Left + Right) / 2;

                Merge_Sort_Up(Sounds, Left, Mid);
                Merge_Sort_Up(Sounds, Mid + 1, Right);

                Merge_Up(Sounds, Left, Mid, Right);
            }
        }

        // Folders Merge Sort Algorithm Min To Max
        //
        void Merge_Up(List<Folder> Folders, int Left, int Mid, int Right)
        {
            int i, j, k;
            int n1 = Mid - Left + 1;
            int n2 = Right - Mid;

            List<Folder> L = new List<Folder>(n1);
            List<Folder> R = new List<Folder>(n2);


            for (i = 0; i < n1; i++)
                L.Add(Folders[Left + i]);
            for (j = 0; j < n2; j++)
                R.Add(Folders[Mid + 1 + j]);

            i = 0;
            j = 0;
            k = Left;
            while (i < n1 && j < n2)
            {
                if (L[i].Get_FreeSpace() <= R[j].Get_FreeSpace())
                {
                    Folders[k] = L[i];
                    i++;
                }
                else
                {
                    Folders[k] = R[j];
                    j++;
                }
                k++;
            }

            while (i < n1)
            {
                Folders[k] = L[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                Folders[k] = R[j];
                j++;
                k++;
            }
        }
        void Merge_Sort_Up(List<Folder> Folders, int Left, int Right)
        {
            if (Left < Right)
            {
                int Mid = (Left + Right) / 2;

                Merge_Sort_Up(Folders, Left, Mid);
                Merge_Sort_Up(Folders, Mid + 1, Right);

                Merge_Up(Folders, Left, Mid, Right);
            }
        }

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

                //Merge_Sort_Up(Sounds, 0, Sounds.Count() - 1);

                for (int j = 0; j <Sounds.Count(); j++)
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
        public void Copy_Sound_File(List<Folder> Folder, string Input_Path, string Output_Path, string file_type)
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
                        File.Copy(Input_Path + @"\" + Sounds[j].Get_Name() + file_type, Folder_Path + @"\" + Sounds[j].Get_Name() + "");
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
        public Stopwatch Worst_Fit_Algo_LinerSearch(string Input, string Output, int Folder_Duration, string file_type, bool Text_Only)
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

            Timer.Start();

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

            Timer.Stop();

            string Output_TextFile = StrategiesName_File;

            //Write_TextFile(Folders, Output_TextFile);

            string Output_SoundFile = StrategiesName_File;

            string Input_SoundFile  = Input + @"\Audios";

            if (Text_Only == true)
                Copy_Sound_File(Folders, Input_SoundFile, Output_SoundFile, file_type);

            return Timer;
        }
        
        // Worst Fit Algorthm Using Priority Queue
        //
        public Stopwatch Worst_Fit_Algo_PriorityQueue(string Input, string Output, int Folder_Duration, string file_type, bool Text_Only)
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

            Timer = new Stopwatch();

            Timer.Start();

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

            Timer.Stop();

            string Output_TextFile = StrategiesName_File;

            //Write_TextFile(List_Folders, Output_TextFile);

            string Output_SoundFile = StrategiesName_File;

            string Input_SoundFile = Input + @"\Audios";

            if (Text_Only == true)
            Copy_Sound_File(List_Folders, Input_SoundFile, Output_SoundFile, file_type);

            return Timer;
        }

        

        // Worst Fit Decreasing Algorithm Using Liner Search
        //
        public Stopwatch Worst_Fit_Dec_Algo_LinerSearch(string Input, string Output, int Folder_Duration, string file_type, bool Text_Only)
        {
            string Output_File = Output + @"\OUTPUT";

            if (!Directory.Exists(Output_File))
            {
                Directory.CreateDirectory(Output_File);
            }

            string StrategiesName_File = Output_File + @"\[2]WorstFit Decreasing";

            if (!Directory.Exists(StrategiesName_File))
            {
                Directory.CreateDirectory(StrategiesName_File);
            }

            string Input_TextFile_Path = Input + @"\AudiosInfo.txt";

            List<Sound_File> Sounds = Read_Text_File(Input_TextFile_Path);

            Timer = new Stopwatch();

            Timer.Start();

            Merge_Sort(Sounds, 0, Sounds.Count() - 1);

            List<Folder> Folders = new List<Folder>();

            Folder Folder = new Folder();

            if (Folders.Count() == 0)
            {
                Folder.Set_Name("F1");
                Folder.Set_Duration(0);
                Folder.Set_FreeSpace(Folder_Duration);
                Folders.Add(Folder);
            }

            for (int i = 0; i < Sounds.Count(); i++)
            {
                int Folder_Index = 0;

                Folder Max_Folder = Most_Remaining_Duration_LinerSearch(Folders, ref Folder_Index);

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
                    New_Folder.Set_Name("F" + (Folders.Count() + 1) + "");
                    New_Folder.Set_Duration(Sounds[i].Get_Duration());
                    New_Folder.Set_FreeSpace(Folder_Duration - Sounds[i].Get_Duration());
                    New_Folder.Set_Sound(Sounds[i]);
                    Folders.Add(New_Folder);
                }

            }

            Timer.Stop();

            string Output_TextFile = StrategiesName_File;

            //Write_TextFile(Folders, Output_TextFile);

            string Output_SoundFile = StrategiesName_File;

            string Input_SoundFile = Input + @"\Audios";

            if (Text_Only == true)
                Copy_Sound_File(Folders, Input_SoundFile, Output_SoundFile, file_type);

            return Timer;
        }

        // Worst Fit Decreasing Algorithm Using Priority Queue
        //
        public Stopwatch Worst_Fit_Dec_Algo_PriorityQueue(string Input, string Output, int Folder_Duration, string file_type, bool Text_Only)
        {
            string Output_File = Output + @"\OUTPUT";

            if (!Directory.Exists(Output_File))
            {
                Directory.CreateDirectory(Output_File);
            }

            string StrategiesName_File = Output_File + @"\[2]WorstFit Decreasing";

            if (!Directory.Exists(StrategiesName_File))
            {
                Directory.CreateDirectory(StrategiesName_File);
            }

            string Input_TextFile_Path = Input + @"\AudiosInfo.txt";

            List<Sound_File> Sounds = Read_Text_File(Input_TextFile_Path);

            Timer = new Stopwatch();

            Timer.Start();

            Merge_Sort(Sounds, 0, Sounds.Count() - 1);

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

                if ((Max_Folder.Get_FreeSpace()) * (-1) >= Sounds[i].Get_Duration())
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

            Timer.Stop();

            string Output_TextFile = StrategiesName_File;

           // Write_TextFile(List_Folders, Output_TextFile);

            string Output_SoundFile = StrategiesName_File;

            string Input_SoundFile = Input + @"\Audios";

            if (Text_Only == true)
            Copy_Sound_File(List_Folders, Input_SoundFile, Output_SoundFile, file_type);

            return Timer;
        }

        

        // Get First Folder Can Fit Sound
        //
        public void Get_First_Folder_Fit(List<Folder> Folders, int Sound_Duration ,ref int Folder_Index)
        {
            for (int i = 0; i < Folders.Count(); i++)
            {
                if (Folders[i].Get_FreeSpace() >= Sound_Duration)
                {
                    Folder_Index = i;
                    break;
                }
            }
        }

        // First Fit Decreasing Algorithm Using Liner Search
        //
        public Stopwatch First_Fit_Dec_Algo_LinerSearch(string Input, string Output, int Folder_Duration, string file_type,bool Text_Only)
        {
            string Output_File = Output + @"\OUTPUT";

            if (!Directory.Exists(Output_File))
            {
                Directory.CreateDirectory(Output_File);
            }

            string StrategiesName_File = Output_File + @"\[3] Firstfit Decreasing";

            if (!Directory.Exists(StrategiesName_File))
            {
                Directory.CreateDirectory(StrategiesName_File);
            }

            string Input_TextFile_Path = Input + @"\AudiosInfo.txt";

            List<Sound_File> Sounds = Read_Text_File(Input_TextFile_Path);

            Timer = new Stopwatch();

            Timer.Start();

            Merge_Sort(Sounds, 0, Sounds.Count() - 1);

            List<Folder> Folders = new List<Folder>();

            Folder Folder = new Folder();

            if (Folders.Count() == 0)
            {
                Folder.Set_Name("F1");
                Folder.Set_Duration(0);
                Folder.Set_FreeSpace(Folder_Duration);
                Folders.Add(Folder);
            }

            for (int i = 0; i < Sounds.Count(); i++)
            {
                int Folder_Index = -1;

                Get_First_Folder_Fit(Folders, Sounds[i].Get_Duration(), ref Folder_Index);

                if (Folder_Index != -1)
                {
                    Folder = Folders[Folder_Index];
                    int New_Folder_Duration = Folder.Get_Duration() + Sounds[i].Get_Duration();
                    int New_Folder_FreeSpace = Folder.Get_FreeSpace() - Sounds[i].Get_Duration();
                    Folder.Set_Sound(Sounds[i]);
                    Folder.Set_Duration(New_Folder_Duration);
                    Folder.Set_FreeSpace(New_Folder_FreeSpace);
                    Folders.RemoveAt(Folder_Index);
                    Folders.Add(Folder);
                }
                else
                {
                    Folder New_Folder = new Folder();
                    New_Folder.Set_Name("F" + (Folders.Count() + 1) + "");
                    New_Folder.Set_Duration(Sounds[i].Get_Duration());
                    New_Folder.Set_FreeSpace(Folder_Duration - Sounds[i].Get_Duration());
                    New_Folder.Set_Sound(Sounds[i]);
                    Folders.Add(New_Folder);
                }

            }

            Timer.Stop();

            string Output_TextFile = StrategiesName_File;

            //Write_TextFile(Folders, Output_TextFile);

            string Output_SoundFile = StrategiesName_File;

            string Input_SoundFile = Input + @"\Audios";

            if (Text_Only == true)
                Copy_Sound_File(Folders, Input_SoundFile, Output_SoundFile, file_type);

            return Timer;
        }

        

        // Get Min Folder Can Fitt Liner Search
        //
        public void Min_Remaining_Duration_LinerSearch(List<Folder> Folders, int Sound_Duration, ref int Folder_Index)
        {
            List<Folder> List_Folder = Folders;

            Merge_Sort_Up(List_Folder, 0, List_Folder.Count() - 1);

            bool Is_Found = false;

            for (int i = 0; i < List_Folder.Count(); i++)
            {
                
                if (List_Folder[i].Get_FreeSpace() >= Sound_Duration)
                {
                    Folder_Index = i;
                    Is_Found = true;
                    break;
                }
            }

            if (Is_Found == true)
            {
                for (int i = 0; i < Folders.Count(); i++)
                {
                    if (Folders[i] == List_Folder[Folder_Index])
                    {
                        Folder_Index = i;
                        break;
                    }
                }
            }

        }

        // Best Fit Algorithm Using Liner Search
        //
        public Stopwatch Best_Fit_Algo_LinerSearch(string Input, string Output, int Folder_Duration, string file_type, bool Text_Only)
        {
            string Output_File = Output + @"\OUTPUT";

            if (!Directory.Exists(Output_File))
            {
                Directory.CreateDirectory(Output_File);
            }

            string StrategiesName_File = Output_File + @"\[4]BestFit";

            if (!Directory.Exists(StrategiesName_File))
            {
                Directory.CreateDirectory(StrategiesName_File);
            }

            string Input_TextFile_Path = Input + @"\AudiosInfo.txt";

            List<Sound_File> Sounds = Read_Text_File(Input_TextFile_Path);

            Timer = new Stopwatch();

            Timer.Start();

            List<Folder> Folders = new List<Folder>();

            Folder Folder = new Folder();

            if (Folders.Count() == 0)
            {
                Folder.Set_Name("F1");
                Folder.Set_Duration(0);
                Folder.Set_FreeSpace(Folder_Duration);
                Folders.Add(Folder);
            }

            for (int i = 0; i < Sounds.Count(); i++)
            {
                int Folder_Index = -1;

                Min_Remaining_Duration_LinerSearch(Folders, Sounds[i].Get_Duration(), ref Folder_Index);

                if (Folder_Index != -1)
                {
                    Folder = Folders[Folder_Index];
                    int New_Folder_Duration = Folder.Get_Duration() + Sounds[i].Get_Duration();
                    int New_Folder_FreeSpace = Folder.Get_FreeSpace() - Sounds[i].Get_Duration();
                    Folder.Set_Sound(Sounds[i]);
                    Folder.Set_Duration(New_Folder_Duration);
                    Folder.Set_FreeSpace(New_Folder_FreeSpace);
                    Folders.RemoveAt(Folder_Index);
                    Folders.Add(Folder);
                }
                else
                {
                    Folder New_Folder = new Folder();
                    New_Folder.Set_Name("F" + (Folders.Count() + 1) + "");
                    New_Folder.Set_Duration(Sounds[i].Get_Duration());
                    New_Folder.Set_FreeSpace(Folder_Duration - Sounds[i].Get_Duration());
                    New_Folder.Set_Sound(Sounds[i]);
                    Folders.Add(New_Folder);
                }

            }

            Timer.Stop();

            string Output_TextFile = StrategiesName_File;

            Write_TextFile(Folders, Output_TextFile);

            string Output_SoundFile = StrategiesName_File;

            string Input_SoundFile = Input + @"\Audios";

            if (Text_Only == true)
                Copy_Sound_File(Folders, Input_SoundFile, Output_SoundFile, file_type);

            return Timer;
        }

        // Best Fit Algorithm Decreasing Using Liner Search
        //
        public Stopwatch Best_Fit_Dec_Algo_LinerSearch(string Input, string Output, int Folder_Duration, string file_type, bool Text_Only)
        {
            string Output_File = Output + @"\OUTPUT";

            if (!Directory.Exists(Output_File))
            {
                Directory.CreateDirectory(Output_File);
            }

            string StrategiesName_File = Output_File + @"\[5]BestFit Decreasing";

            if (!Directory.Exists(StrategiesName_File))
            {
                Directory.CreateDirectory(StrategiesName_File);
            }

            string Input_TextFile_Path = Input + @"\AudiosInfo.txt";

            List<Sound_File> Sounds = Read_Text_File(Input_TextFile_Path);

            Timer = new Stopwatch();

            Timer.Start();

            Merge_Sort(Sounds, 0, Sounds.Count() - 1);

            List<Folder> Folders = new List<Folder>();

            Folder Folder = new Folder();

            if (Folders.Count() == 0)
            {
                Folder.Set_Name("F1");
                Folder.Set_Duration(0);
                Folder.Set_FreeSpace(Folder_Duration);
                Folders.Add(Folder);
            }

            for (int i = 0; i < Sounds.Count(); i++)
            {
                int Folder_Index = -1;

                Min_Remaining_Duration_LinerSearch(Folders, Sounds[i].Get_Duration(), ref Folder_Index);

                if (Folder_Index != -1)
                {
                    Folder = Folders[Folder_Index];
                    int New_Folder_Duration = Folder.Get_Duration() + Sounds[i].Get_Duration();
                    int New_Folder_FreeSpace = Folder.Get_FreeSpace() - Sounds[i].Get_Duration();
                    Folder.Set_Sound(Sounds[i]);
                    Folder.Set_Duration(New_Folder_Duration);
                    Folder.Set_FreeSpace(New_Folder_FreeSpace);
                    Folders.RemoveAt(Folder_Index);
                    Folders.Add(Folder);
                }
                else
                {
                    Folder New_Folder = new Folder();
                    New_Folder.Set_Name("F" + (Folders.Count() + 1) + "");
                    New_Folder.Set_Duration(Sounds[i].Get_Duration());
                    New_Folder.Set_FreeSpace(Folder_Duration - Sounds[i].Get_Duration());
                    New_Folder.Set_Sound(Sounds[i]);
                    Folders.Add(New_Folder);
                }

            }

            Timer.Stop();

            string Output_TextFile = StrategiesName_File;

            Write_TextFile(Folders, Output_TextFile);

            string Output_SoundFile = StrategiesName_File;

            string Input_SoundFile = Input + @"\Audios";

            if (Text_Only == true)
                Copy_Sound_File(Folders, Input_SoundFile, Output_SoundFile, file_type);

            return Timer;
        }



        // Folder Filling Algorithm
        //
        public Stopwatch Folder_Filling_Algo(string Input, string Output, int Folder_Duration, string file_type, bool Text_Only)
        {
            string Output_File = Output + @"\OUTPUT";

            if (!Directory.Exists(Output_File))
            {
                Directory.CreateDirectory(Output_File);
            }

            string StrategiesName_File = Output_File + @"\[6] FolderFilling";

            if (!Directory.Exists(StrategiesName_File))
            {
                Directory.CreateDirectory(StrategiesName_File);
            }

            string Input_TextFile_Path = Input + @"\AudiosInfo.txt";

            List<Sound_File> Sounds = Read_Text_File(Input_TextFile_Path);

            Timer = new Stopwatch();

            Timer.Start();

            List<Folder> Folders = new List<Folder>();

            int File_Name = 0;

            while(Sounds.Count() > 0)
            {
                List<int> Sound_INT = new List<int>();

                for (int j = 0; j < Sounds.Count(); j++)
                {
                    Sound_INT.Add(Sounds[j].Get_Duration());
                }

                int[,] DB = new int[Sounds.Count() + 1, Folder_Duration + 1];

                int i, w;

                int Sound_Wight = Folder_Duration;

                int Number_Sound = Sounds.Count();

                for (i = 0; i <= Number_Sound; i++)
                {
                    for (w = 0; w <= Sound_Wight; w++)
                    {
                        if (i == 0 || w == 0)
                            DB[i, w] = 0;
                        else if (Sound_INT[i - 1] <= w)
                            DB[i, w] = Get_Max(Sound_INT[i - 1] + DB[i - 1, w - Sound_INT[i - 1]], DB[i - 1, w]);
                        else
                            DB[i, w] = DB[i - 1, w];
                    }
                }

                i = Number_Sound;
                w = Sound_Wight;

                Folder Folder = new Folder();
                Folder.Set_FreeSpace(Folder_Duration);
                Folder.Set_Duration(0);
                Folder.Set_Name("F" + (File_Name + 1) + "");

                List<int> Remove = new List<int>();

                while (w != 0 && i != 0)
                {
                    if (Sound_INT[i - 1] > w || (DB[i - 1, w] > Sound_INT[i - 1] + DB[i - 1, w - Sound_INT[i - 1]]))
                        i--;
                    else
                    {
                        Folder.Set_Duration(Folder.Get_Duration() + Sounds[i - 1].Get_Duration());
                        Folder.Set_FreeSpace(Folder.Get_FreeSpace() - Sounds[i - 1].Get_Duration());
                        Folder.Set_Sound(Sounds[i - 1]);

                        Remove.Add(i - 1);
                        w = w - Sound_INT[i - 1];
                        i--;
                    }
                }

                Folders.Add(Folder);

                for (int y = 0; y < Remove.Count(); y++)
                {
                     Sounds.RemoveAt(Remove[y]);
                     Sound_INT.RemoveAt(Remove[y]);
                }

                File_Name++;
            }

            Timer.Stop();

            string Output_TextFile = StrategiesName_File;

            //Write_TextFile(Folders, Output_TextFile);

            string Output_SoundFile = StrategiesName_File;

            string Input_SoundFile = Input + @"\Audios";

            if(Text_Only == true)
                Copy_Sound_File(Folders, Input_SoundFile, Output_SoundFile, file_type);

            return Timer;
        }

    }
}
