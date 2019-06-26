using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOUNDS_PACKING_PROJECT.Class
{
    class Folder
    {
        string Name;
        string Path;
        int Duration;
        int FreeSpace;
        List<Sound_File> Sounds;


        public Folder() 
        {
            Name = "";
            Path = "";
            Duration = 0;
            FreeSpace = 0;
            Sounds = new List<Sound_File>();
        }

        // Folder Class Constractor
        //
        public Folder(string Folder_Name, int Folder_Duration, int Folder_FreeSpace, List<Sound_File> Folder_Sounds)
        {
            Name = Folder_Name;
            Duration = Folder_Duration;
            FreeSpace = Folder_FreeSpace;
            Sounds = Folder_Sounds;
        }

        // Get Folder Name
        //
        public string Get_Name()
        {
            return Name;
        }

        // Get Folder Path
        //
        public string Get_Path()
        {
            return Path;
        }

        // Gte Folder Duration
        //
        public int Get_Duration()
        {
            return Duration;
        }

        // Get Folder Free Space
        //
        public int Get_FreeSpace()
        {
            return FreeSpace;
        }

        // Get List Sound
        //
        public List<Sound_File> Get_Sound_List()
        {
            return Sounds;
        }

        // Set Folder Name
        //
        public void Set_Name(string Folder_Name)
        {
            Name = Folder_Name;
        }

        // Set Folder Duration
        //
        public void Set_Duration(int Folder_Duration)
        {
            Duration = Folder_Duration;
        }

        // Set Folder Free Space
        //
        public void Set_FreeSpace(int Folder_FreeSpace)
        {
            FreeSpace = Folder_FreeSpace;
        }

        // Set Folder List Of Sounds
        //
        public void Set_Sound(Sound_File Sound)
        {
            Sounds.Add(Sound);
        }
    }
}
