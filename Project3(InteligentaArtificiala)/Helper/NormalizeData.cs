using Project3_InteligentaArtificiala_.Controllers;
using Project3_InteligentaArtificiala_.Models;
using System.Data.Entity;

namespace Project3_InteligentaArtificiala_.Helper
{
    public class NormalizeData
    {
        private readonly GlassContext _context;
        private readonly CalculatingMINorMaxForEachColumn _calculator;
        private readonly NormalizeContext _normalizedTable;
        private readonly TestingContext _testingContext;
        private readonly TrainingContext _trainingContext;
        private readonly SplittingTheTableInTwoParts _splittingTheTableInTwoParts;
        public NormalizeData(GlassContext context, CalculatingMINorMaxForEachColumn calculator, NormalizeContext normalizedTable, TestingContext testingContext, TrainingContext trainingContext, SplittingTheTableInTwoParts splittingTheTableInTwoParts)
        {
            _context = context;
            _calculator = calculator;
            _normalizedTable = normalizedTable;
            _trainingContext = trainingContext;
            _testingContext = testingContext;
            _splittingTheTableInTwoParts = splittingTheTableInTwoParts;
        }
        public void NormalizingData()
        {

            var existingList = _normalizedTable.NormalizeTable.ToList();
            var normalTable = _context.GlassTable.ToList();
            var testingList = _testingContext.TestingTable.ToList();
            var trainingList = _trainingContext.TrainingTable2.ToList();

            foreach (var existingRecord in existingList)
            {
                var matchingNormalRecord = normalTable.FirstOrDefault(n => n.Id == existingRecord.Id);

                if (matchingNormalRecord != null)
                {
                    existingRecord.RI = matchingNormalRecord.RI;
                    existingRecord.Na = matchingNormalRecord.Na;
                    existingRecord.Mg = matchingNormalRecord.Mg;
                    existingRecord.Al = matchingNormalRecord.Al;
                    existingRecord.Si = matchingNormalRecord.Si;
                    existingRecord.K = matchingNormalRecord.K;
                    existingRecord.Ca = matchingNormalRecord.Ca;
                    existingRecord.Ba = matchingNormalRecord.Ba;
                    existingRecord.Fe = matchingNormalRecord.Fe;
                    existingRecord.Type = matchingNormalRecord.Type;
                }
            }
            _normalizedTable.SaveChanges();
            _splittingTheTableInTwoParts.SplitTheTable();

            float leftComponentRI = _calculator.CalculateMinForRI();
            float rightComponentRI = _calculator.CalculateMaxForRI();
            
            float leftComponentNa = _calculator.CalculateMinForNa();
            float rightComponentNa = _calculator.CalculateMaxForNa();

            float leftComponentMg = _calculator.CalculateMinForMg();
            float rightComponentMg = _calculator.CalculateMaxForMg();
             
            float leftComponentAl = _calculator.CalculateMinForAl();
            float rightComponentAl = _calculator.CalculateMaxForAl();

            float leftComponentSi = _calculator.CalculateMinForSi();
            float rightComponentSi = _calculator.CalculateMaxForSi();

            float leftComponentK = _calculator.CalculateMinForK();
            float rightComponentK = _calculator.CalculateMaxForK();

            float leftComponentCa = _calculator.CalculateMinForCa();
            float rightComponentCa = _calculator.CalculateMaxForCa();

            float leftComponentBa = _calculator.CalculateMinForBa();
            float rightComponentBa = _calculator.CalculateMaxForBa();

            float leftComponentFe = _calculator.CalculateMinForFe();
            float rightComponentFe  = _calculator.CalculateMaxForFe();

            float leftComponentType = 1;
            float rightComponentType = 7;

            float c = 0;
            float d = 1;


            foreach (var glass in existingList)
            {
                glass.RI = Math.Round((((c - d) / (leftComponentRI - rightComponentRI)) * glass.RI) + (leftComponentRI * d - rightComponentRI * c) / (leftComponentRI - rightComponentRI), 6);

                glass.Na = Math.Round((((c - d) / (leftComponentNa - rightComponentNa)) * glass.Na) + (leftComponentNa * d - rightComponentNa * c) / (leftComponentNa - rightComponentNa), 6);

                glass.Mg = Math.Round((((c - d) / (leftComponentMg - rightComponentMg)) * glass.Mg) + (leftComponentMg * d - rightComponentMg * c) / (leftComponentMg - rightComponentMg), 6);

                glass.Al = Math.Round((((c - d) / (leftComponentAl - rightComponentAl)) * glass.Al) + (leftComponentAl * d - rightComponentAl * c) / (leftComponentAl - rightComponentAl), 6);

                glass.Si = Math.Round((((c - d) / (leftComponentSi - rightComponentSi)) * glass.Si) + (leftComponentSi * d - rightComponentSi * c) / (leftComponentSi - rightComponentSi), 6);

                glass.K = Math.Round((((c - d) / (leftComponentK - rightComponentK)) * glass.K) + (leftComponentK * d - rightComponentK * c) / (leftComponentK - rightComponentK), 6);

                glass.Ca = Math.Round((((c - d) / (leftComponentCa - rightComponentCa)) * glass.Ca) + (leftComponentCa * d - rightComponentCa * c) / (leftComponentCa - rightComponentCa), 6);

                glass.Ba = Math.Round((((c - d) / (leftComponentBa - rightComponentBa)) * glass.Ba) + (leftComponentBa * d - rightComponentBa * c) / (leftComponentBa - rightComponentBa), 6);

                glass.Fe = Math.Round((((c - d) / (leftComponentFe - rightComponentFe)) * glass.Fe) + (leftComponentFe * d - rightComponentFe * c) / (leftComponentFe - rightComponentFe), 6);

                glass.Type = Math.Round((((c - d) / (-6)) * glass.Type) + ((1 * d - 7 * c) / (-6)), 6);
            }

            _normalizedTable.SaveChanges();

            foreach (var glassTesting in _testingContext.TestingTable)
            {
                glassTesting.RI = Math.Round((((c - d) / (leftComponentRI - rightComponentRI)) * glassTesting.RI) + (leftComponentRI * d - rightComponentRI * c) / (leftComponentRI - rightComponentRI), 6);

                glassTesting.Na = Math.Round((((c - d) / (leftComponentNa - rightComponentNa)) * glassTesting.Na) + (leftComponentNa * d - rightComponentNa * c) / (leftComponentNa - rightComponentNa), 6);

                glassTesting.Mg = Math.Round((((c - d) / (leftComponentMg - rightComponentMg)) * glassTesting.Mg) + (leftComponentMg * d - rightComponentMg * c) / (leftComponentMg - rightComponentMg), 6);

                glassTesting.Al = Math.Round((((c - d) / (leftComponentAl - rightComponentAl)) * glassTesting.Al) + (leftComponentAl * d - rightComponentAl * c) / (leftComponentAl - rightComponentAl), 6);

                glassTesting.Si = Math.Round((((c - d) / (leftComponentSi - rightComponentSi)) * glassTesting.Si) + (leftComponentSi * d - rightComponentSi * c) / (leftComponentSi - rightComponentSi), 6);

                glassTesting.K = Math.Round((((c - d) / (leftComponentK - rightComponentK)) * glassTesting.K) + (leftComponentK * d - rightComponentK * c) / (leftComponentK - rightComponentK), 6);

                glassTesting.Ca = Math.Round((((c - d) / (leftComponentCa - rightComponentCa)) * glassTesting.Ca) + (leftComponentCa * d - rightComponentCa * c) / (leftComponentCa - rightComponentCa), 6);

                glassTesting.Ba = Math.Round((((c - d) / (leftComponentBa - rightComponentBa)) * glassTesting.Ba) + (leftComponentBa * d - rightComponentBa * c) / (leftComponentBa - rightComponentBa), 6);

                glassTesting.Fe = Math.Round((((c - d) / (leftComponentFe - rightComponentFe)) * glassTesting.Fe) + (leftComponentFe * d - rightComponentFe * c) / (leftComponentFe - rightComponentFe), 6);

                glassTesting.Type = Math.Round((((c - d) / (-6)) * glassTesting.Type) + ((1 * d - 7 * c) / (-6)), 6);


            }
            _testingContext.SaveChanges();
            foreach (var glassTTraining in _trainingContext.TrainingTable2)
            {
                glassTTraining.RI = Math.Round((((c - d) / (leftComponentRI - rightComponentRI)) * glassTTraining.RI) + (leftComponentRI * d - rightComponentRI * c) / (leftComponentRI - rightComponentRI), 6);

                glassTTraining.Na = Math.Round((((c - d) / (leftComponentNa - rightComponentNa)) * glassTTraining.Na) + (leftComponentNa * d - rightComponentNa * c) / (leftComponentNa - rightComponentNa), 6);

                glassTTraining.Mg = Math.Round((((c - d) / (leftComponentMg - rightComponentMg)) * glassTTraining.Mg) + (leftComponentMg * d - rightComponentMg * c) / (leftComponentMg - rightComponentMg), 6);

                glassTTraining.Al = Math.Round((((c - d) / (leftComponentAl - rightComponentAl)) * glassTTraining.Al) + (leftComponentAl * d - rightComponentAl * c) / (leftComponentAl - rightComponentAl), 6);

                glassTTraining.Si = Math.Round((((c - d) / (leftComponentSi - rightComponentSi)) * glassTTraining.Si) + (leftComponentSi * d - rightComponentSi * c) / (leftComponentSi - rightComponentSi), 6);

                glassTTraining.K = Math.Round((((c - d) / (leftComponentK - rightComponentK)) * glassTTraining.K) + (leftComponentK * d - rightComponentK * c) / (leftComponentK - rightComponentK), 6);

                glassTTraining.Ca = Math.Round((((c - d) / (leftComponentCa - rightComponentCa)) * glassTTraining.Ca) + (leftComponentCa * d - rightComponentCa * c) / (leftComponentCa - rightComponentCa), 6);

                glassTTraining.Ba = Math.Round((((c - d) / (leftComponentBa - rightComponentBa)) * glassTTraining.Ba) + (leftComponentBa * d - rightComponentBa * c) / (leftComponentBa - rightComponentBa), 6);

                glassTTraining.Fe = Math.Round((((c - d) / (leftComponentFe - rightComponentFe)) * glassTTraining.Fe) + (leftComponentFe * d - rightComponentFe * c) / (leftComponentFe - rightComponentFe), 6);

                glassTTraining.Type = Math.Round((((c - d) / (-6)) * glassTTraining.Type) + ((1 * d - 7 * c) / (-6)), 6);

            }
            _trainingContext.SaveChanges();
        }


    }
}
