using Project3_InteligentaArtificiala_.Controllers;
using Project3_InteligentaArtificiala_.Models;

namespace Project3_InteligentaArtificiala_.Helper
{
    public class NormalizeData
    {
        private readonly GlassContext _context;
        private readonly CalculatingMINorMaxForEachColumn _calculator;
        private readonly NormalizeContext _normalizedTable;
        public NormalizeData(GlassContext context, CalculatingMINorMaxForEachColumn calculator, NormalizeContext normalizedTable)
        {
            _context = context;
            _calculator = calculator;
            _normalizedTable = normalizedTable;
        }
        public void NormalizingData()
        {
            var existingList = _normalizedTable.NormalizeTable.ToList();

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
        }


    }
}
