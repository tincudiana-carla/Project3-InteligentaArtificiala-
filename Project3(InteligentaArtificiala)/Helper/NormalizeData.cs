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
            

            foreach(var glass in existingList)
            {
                glass.RI = ((c - d) / (leftComponentRI - rightComponentRI)) * glass.RI + (leftComponentRI * d - rightComponentRI * c) / (leftComponentRI - rightComponentRI);
                glass.Na = ((c - d) / (leftComponentNa - rightComponentNa)) * glass.Na + (leftComponentNa * d - rightComponentNa * c) / (leftComponentNa - rightComponentNa);
                glass.Mg = ((c - d) / (leftComponentMg - rightComponentMg)) * glass.Mg + (leftComponentMg * d - rightComponentMg * c) / (leftComponentMg - rightComponentMg);
                glass.Al = ((c - d) / (leftComponentAl - rightComponentAl)) * glass.Al + (leftComponentAl * d - rightComponentAl * c) / (leftComponentAl - rightComponentAl);
                glass.Si = ((c - d) / (leftComponentSi - rightComponentSi)) * glass.Si + (leftComponentSi * d - rightComponentSi * c) / (leftComponentSi - rightComponentSi);
                glass.K = ((c - d) / (leftComponentK - rightComponentK)) * glass.K + (leftComponentK * d - rightComponentK * c) / (leftComponentK - rightComponentK);
                glass.Ca = ((c - d) / (leftComponentCa - rightComponentCa)) * glass.Ca + (leftComponentCa * d - rightComponentCa * c) / (leftComponentCa - rightComponentCa);
                glass.Ba = ((c - d) / (leftComponentBa - rightComponentBa)) * glass.Ba + (leftComponentBa * d - rightComponentBa * c) / (leftComponentBa - rightComponentBa);
                glass.Fe = ((c - d) / (leftComponentFe - rightComponentFe)) * glass.Fe + (leftComponentFe * d - rightComponentFe * c) / (leftComponentFe - rightComponentFe);
                glass.Type = (((c - d) / (leftComponentType - rightComponentType)) * glass.Type) + ((leftComponentType * d - rightComponentType * c) / (leftComponentType - rightComponentType));    
            }
            _normalizedTable.SaveChanges();

            foreach (var glassTesting in _testingContext.TestingTable)
            {
                glassTesting.RI = ((c - d) / (leftComponentRI - rightComponentRI)) * glassTesting.RI + (leftComponentRI * d - rightComponentRI * c) / (leftComponentRI - rightComponentRI);
                glassTesting.Na = ((c - d) / (leftComponentNa - rightComponentNa)) * glassTesting.Na + (leftComponentNa * d - rightComponentNa * c) / (leftComponentNa - rightComponentNa);
                glassTesting.Mg = ((c - d) / (leftComponentMg - rightComponentMg)) * glassTesting.Mg + (leftComponentMg * d - rightComponentMg * c) / (leftComponentMg - rightComponentMg);
                glassTesting.Al = ((c - d) / (leftComponentAl - rightComponentAl)) * glassTesting.Al + (leftComponentAl * d - rightComponentAl * c) / (leftComponentAl - rightComponentAl);
                glassTesting.Si = ((c - d) / (leftComponentSi - rightComponentSi)) * glassTesting.Si + (leftComponentSi * d - rightComponentSi * c) / (leftComponentSi - rightComponentSi);
                glassTesting.K = ((c - d) / (leftComponentK - rightComponentK)) * glassTesting.K + (leftComponentK * d - rightComponentK * c) / (leftComponentK - rightComponentK);
                glassTesting.Ca = ((c - d) / (leftComponentCa - rightComponentCa)) * glassTesting.Ca + (leftComponentCa * d - rightComponentCa * c) / (leftComponentCa - rightComponentCa);
                glassTesting.Ba = ((c - d) / (leftComponentBa - rightComponentBa)) * glassTesting.Ba + (leftComponentBa * d - rightComponentBa * c) / (leftComponentBa - rightComponentBa);
                glassTesting.Fe = ((c - d) / (leftComponentFe - rightComponentFe)) * glassTesting.Fe + (leftComponentFe * d - rightComponentFe * c) / (leftComponentFe - rightComponentFe);
                glassTesting.Type = (((c - d) / (leftComponentType - rightComponentType)) * glassTesting.Type) + ((leftComponentType * d - rightComponentType * c) / (leftComponentType - rightComponentType));


            }
            _testingContext.SaveChanges();
            foreach (var glassTTraining in _trainingContext.TrainingTable2)
            {
                glassTTraining.RI = ((c - d) / (leftComponentRI - rightComponentRI)) * glassTTraining.RI + (leftComponentRI * d - rightComponentRI * c) / (leftComponentRI - rightComponentRI);
                glassTTraining.Na = ((c - d) / (leftComponentNa - rightComponentNa)) * glassTTraining.Na + (leftComponentNa * d - rightComponentNa * c) / (leftComponentNa - rightComponentNa);
                glassTTraining.Mg = ((c - d) / (leftComponentMg - rightComponentMg)) * glassTTraining.Mg + (leftComponentMg * d - rightComponentMg * c) / (leftComponentMg - rightComponentMg);
                glassTTraining.Al = ((c - d) / (leftComponentAl - rightComponentAl)) * glassTTraining.Al + (leftComponentAl * d - rightComponentAl * c) / (leftComponentAl - rightComponentAl);
                glassTTraining.Si = ((c - d) / (leftComponentSi - rightComponentSi)) * glassTTraining.Si + (leftComponentSi * d - rightComponentSi * c) / (leftComponentSi - rightComponentSi);
                glassTTraining.K = ((c - d) / (leftComponentK - rightComponentK)) * glassTTraining.K + (leftComponentK * d - rightComponentK * c) / (leftComponentK - rightComponentK);
                glassTTraining.Ca = ((c - d) / (leftComponentCa - rightComponentCa)) * glassTTraining.Ca + (leftComponentCa * d - rightComponentCa * c) / (leftComponentCa - rightComponentCa);
                glassTTraining.Ba = ((c - d) / (leftComponentBa - rightComponentBa)) * glassTTraining.Ba + (leftComponentBa * d - rightComponentBa * c) / (leftComponentBa - rightComponentBa);
                glassTTraining.Fe = ((c - d) / (leftComponentFe - rightComponentFe)) * glassTTraining.Fe + (leftComponentFe * d - rightComponentFe * c) / (leftComponentFe - rightComponentFe);
                glassTTraining.Type = (((c - d) / (leftComponentType - rightComponentType)) * glassTTraining.Type) + ((leftComponentType * d - rightComponentType * c) / (leftComponentType - rightComponentType));

            }
            _trainingContext.SaveChanges();
        }


    }
}
