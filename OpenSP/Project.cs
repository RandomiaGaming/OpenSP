using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSP
{
    //Project Specifications:
    //
    //NameLength - 2
    //Name - ProjectNameLength
    //DescriptionLength - 2
    //Description - ProjectDescriptionLength
    //Sounds - Unknown (Terminated by end of file.)

    //Sound Specifications:
    //
    //NameLength - 2
    //Name - ProjectNameLength
    //DescriptionLength - 2
    //Description - ProjectDescriptionLength
    //Volume - 2
    //ChannelCount - 1
    //BitsPerSampl - 1
    //SampleRate - 4
    //DataLength - 8
    //Data - DataLength


    public sealed class Project
    {
        public ushort NameLength;
        public byte[] Name;
        
        public ushort DescriptionLength;
        public byte[] Description;
        
        public byte[] Sounds;
    }
    public sealed class Sound
    {
        public ushort NameLength;
        public byte[] Name;

        public ushort DescriptionLength;
        public byte[] Description;
        
        public ushort Volume;
        
        public byte ChannelCount;
        
        public byte BitsPerSample;
        
        public int SampleRate;
        
        public ulong DataLength;
        public byte[] Data;
    }
}
