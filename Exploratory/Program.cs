using Encog.App.Analyst;
using Encog.App.Analyst.CSV.Segregate;
using Encog.App.Analyst.Wizard;
using Encog.Util.CSV;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exploratory
{
    class Program
    {
        static void Main(string[] args)
        {
            Utility.ShuffleCsv(sourceFile, shuffledFile);
            Segregate();
            Normalise();
        }

        private static void Normalise()
        {
            var analyst = new EncogAnalyst();
            var wizard = new AnalystWizard(analyst);

            wizard.Wizard(sourceFile, true, AnalystFileFormat.DecpntComma);

            //for numerical vals:
            analyst.Script.Normalize.NormalizedFields[0].Action = Encog.Util.Arrayutil.NormalizationAction.Normalize;

            //for enumerations:
            analyst.Script.Normalize.NormalizedFields[0].Action = Encog.Util.Arrayutil.NormalizationAction.Equilateral;
        }

        private static void Segregate()
        {
            var seg = new SegregateCSV();
            seg.Targets.Add(new SegregateTargetPercent(trainingFile, 50));
            seg.Targets.Add(new SegregateTargetPercent(crossValidationFile, 30));
            seg.Targets.Add(new SegregateTargetPercent(evaluationFile, 20));

            seg.ProduceOutputHeaders = true;
            seg.Analyze(shuffledFile, true, CSVFormat.English);
            seg.Process();
        }

        static FileInfo sourceFile = new FileInfo(@"C:\Temp\Encog\source.csv");
        static FileInfo shuffledFile = new FileInfo(@"C:\Temp\Encog\shuffled.csv");
        static FileInfo trainingFile = new FileInfo(@"C:\Temp\Encog\training.csv");
        static FileInfo crossValidationFile = new FileInfo(@"C:\Temp\Encog\crossValidation.csv");
        static FileInfo evaluationFile = new FileInfo(@"C:\Temp\Encog\evaluation.csv");

    }
}
