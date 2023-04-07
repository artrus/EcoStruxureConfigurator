﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStruxureConfigurator
{
    public class TagModbus : TagBase
    {
        public enum ST_SP { ST, SP }
        public ST_SP DIR;
        public readonly int Addr;
        public readonly TagInfoModbus TagInfo;
        public List<string> Path = new List<string>();
        public string PsevdoName;

        public TagModbus(string name, string description, string system, int addr, ST_SP dir, TagInfoModbus tagInfo) : base(name, description, system)
        {
            Addr = addr;
            TagInfo = tagInfo;
            DIR = dir;
            PsevdoName = "";
        }

        public void AddPath(string path)
        {
            Path.Add(path);
        }

        public void AddPsevdoName (string name)
        {
            PsevdoName = name;
        }
    }
}
