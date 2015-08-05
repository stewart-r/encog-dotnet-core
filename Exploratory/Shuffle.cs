using Encog.App.Analyst.CSV.Shuffle;
using Encog.Util.CSV;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exploratory
{
    public class Utility
    {
        public static void ShuffleCsv(FileInfo source, FileInfo destination)
        {
            var shuffle = new ShuffleCSV();
            shuffle.Analyze(source, true, CSVFormat.English);
            shuffle.ProduceOutputHeaders = true;
            shuffle.Process(destination);
        }


    }
}
