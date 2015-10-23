using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test
{
    class Compiler
    {
        static void Main(string[] args)
        {
            String filename = args[0];
            File.OpenText(filename);
            String fileText = File.ReadAllText(filename);

        }
    }
}
