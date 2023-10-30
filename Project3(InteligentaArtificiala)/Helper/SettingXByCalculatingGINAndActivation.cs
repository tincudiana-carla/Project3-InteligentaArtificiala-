using Project3_InteligentaArtificiala_.Models;

namespace Project3_InteligentaArtificiala_.Helper
{
    public class SettingXByCalculatingGINAndActivation
    {
        private readonly GlassContext _context;
        private readonly CalculatingMINorMaxForEachColumn _calculator;
        private readonly NormalizeContext _normalizeContext;
        private readonly NormalizeData _normalizeData;
        private readonly SplittingTheTableInTwoParts _splittingTheTableInTwoParts;
        private readonly TestingContext _testingContext;
        private readonly TrainingContext _trainingContext;
        private static List<LayerModel> _tempLayers = new List<LayerModel>();

        public SettingXByCalculatingGINAndActivation(GlassContext context, CalculatingMINorMaxForEachColumn calculator, NormalizeContext normalizeContext, NormalizeData normalizeData, TrainingContext trainingContext, TestingContext testingContext, SplittingTheTableInTwoParts splittingTheTableInTwoParts)
        {
            _context = context;
            _calculator = calculator;
            _normalizeContext = normalizeContext;
            _normalizeData = normalizeData;
            _trainingContext = trainingContext;
            _testingContext = testingContext;
            _splittingTheTableInTwoParts = splittingTheTableInTwoParts;
        }
    }
}
