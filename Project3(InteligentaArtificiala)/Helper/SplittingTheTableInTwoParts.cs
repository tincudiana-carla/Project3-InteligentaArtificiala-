using Project3_InteligentaArtificiala_.Models;
using System;

namespace Project3_InteligentaArtificiala_.Helper
{
    public class SplittingTheTableInTwoParts
    {
        private readonly GlassContext _glassContext;
        private readonly TestingContext _testingContext;
        private readonly TrainingContext _trainingContext;
        private readonly Random _random;

        public SplittingTheTableInTwoParts(TestingContext testingContext, TrainingContext trainingContext, GlassContext glassContext)
        {
            _testingContext = testingContext;
            _trainingContext = trainingContext;
            _random = new Random();
            _glassContext = glassContext;
        }
        private int GenerateRandomIndex(double type)
        { 
            int intType = (int)type;
            switch (intType)
            {
                case 1:
                    return _random.Next(1, 70);
                case 2:
                    return _random.Next(71, 146);
                case 3:
                    return _random.Next(147, 163);
                case 5:
                    return _random.Next(164, 176);
                case 6:
                    return _random.Next(177, 185);
                case 7:
                    return _random.Next(186, 214);
                default:
                    return 0; 
            }
        }
        public void SplitTheTable()
        {
            _testingContext.TestingTable.RemoveRange(_testingContext.TestingTable);
            _trainingContext.TrainingTable2.RemoveRange(_trainingContext.TrainingTable2);
            var list = _glassContext.GlassTable.ToList();
            var random = new Random();

            foreach (var type in new[] { 1, 2, 3, 5, 6, 7 })
            {
                var itemsOfType = list.Where(item => item.Type == type).ToList();
                Shuffle(itemsOfType, random);

                int count = 0;
                foreach (var item in itemsOfType)
                {
                    if (count < GetMaxCountForType(type))
                    {
                        var newItem = new TestingGlassModel
                        {
                            Id = item.Id,
                            RI = item.RI,
                            Na = item.Na,
                            Mg = item.Mg,
                            Al = item.Al,
                            Si = item.Si,
                            K = item.K,
                            Ca = item.Ca,
                            Ba = item.Ba,
                            Fe = item.Fe,
                            Type = item.Type
                        };

                        _testingContext.TestingTable.Add(newItem);
                        count++;
                    }
                    else
                    {
                        var newItem2 = new TrainingGlassModel
                        {
                            Id = item.Id,
                            RI = item.RI,
                            Na = item.Na,
                            Mg = item.Mg,
                            Al = item.Al,
                            Si = item.Si,
                            K = item.K,
                            Ca = item.Ca,
                            Ba = item.Ba,
                            Fe = item.Fe,
                            Type = item.Type
                        };

                        _trainingContext.TrainingTable2.Add(newItem2);
                    }
                }
            }

           _testingContext.SaveChanges();
           _trainingContext.SaveChanges();
        }

        private void Shuffle<T>(IList<T> list, Random random)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private int GetMaxCountForType(int type)
        {
            switch (type)
            {
                case 1:
                    return 13;
                case 2:
                    return 15;
                case 3:
                    return 3;
                case 5:
                    return 5;
                case 6:
                    return 2;
                case 7:
                    return 5;
                default:
                    return 0;
            }
        }
    }
}

