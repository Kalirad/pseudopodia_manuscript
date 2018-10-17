using Accord.Statistics.Distributions.Univariate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StochasticalChemicalLevel
{
   public static class GodOfReactions
    {
        public static double GodWatch { get; private set; }
        public static void ExecuteReactionsInBoard()
        {
            while (ReactionsForEvaluation.Count > 0)
            {
                //switch (a)
                //{
                //    case 1: Reaction.R1(SubVolumes[i, j]); break;
                //    case 2: Reaction.R2(SubVolumes[i, j]); break;
                //    case 3: Reaction.R3(SubVolumes[i, j]); break;
                //    case 4: Reaction.R4(SubVolumes[i, j]); break;
                //    case 5: Reaction.R5(SubVolumes[i, j]); break;
                //    case 6: Reaction.R6(SubVolumes[i, j]); break;
                //    case 7: Reaction.R7(SubVolumes[i, j]); break;
                //    case 8: Reaction.R8(SubVolumes[i, j]); break;
                //    case 9: Reaction.R9(SubVolumes[i, j]); break;
                //    case 10: Reaction.R10(SubVolumes[i, j]); break;
                //    case 11: Reaction.R11(SubVolumes[i, j]); break;
                //    case 12: Reaction.R12(SubVolumes[i, j]); break;
                //    case 13: Reaction.R13(SubVolumes[i, j]); break;
                //    case 14: Reaction.R14(SubVolumes[i, j]); break;
                //    case 15: Reaction.R15(SubVolumes[i, j]); break;

                //    case 16: Reaction.DiffuseUp(SubVolumes[i, j], SubVolumes[i - 1, j]); break;
                //    case 17: Reaction.DiffuseDown(SubVolumes[i, j], SubVolumes[i + 1, j]); break;
                //    case 18: Reaction.DiffuseRight(SubVolumes[i, j], SubVolumes[i, j + 1]); break;
                //    case 19: Reaction.DiffuseLeft(SubVolumes[i, j], SubVolumes[i, j - 1]); break;
                //  //  case 20: Reaction.R21(SubVolumes[i, j]); break;
                //}
            }
        }

        internal static void ExecuteThisReactionOnThisVoxel(DrTirandazVoxel vox, int a, DrTirandazVoxel[,] SubVolumes)
        {
            switch (a)
            {
                case 1: DrTirandazReaction.R1(vox); break;
                case 2: DrTirandazReaction.R2(vox); break;
                case 3: DrTirandazReaction.R3(vox); break;
                case 4: DrTirandazReaction.R4(vox); break;
                case 5: DrTirandazReaction.R5(vox); break;
                case 6: DrTirandazReaction.R6(vox); break;
                case 7: DrTirandazReaction.R7(vox); break;
                case 8: DrTirandazReaction.R8(vox); break;
                case 9: DrTirandazReaction.R9(vox); break;
                case 10: DrTirandazReaction.R10(vox); break;
                case 11: DrTirandazReaction.R11(vox); break;
                case 12: DrTirandazReaction.R12(vox); break;
                case 13: DrTirandazReaction.R13(vox); break;
                case 14: DrTirandazReaction.R14(vox); break;
                case 15: DrTirandazReaction.R15(vox); break;

                case 16: DrTirandazReaction.DiffuseUp(vox, SubVolumes[vox.Row - 1, vox.Col]); break;
                case 17: DrTirandazReaction.DiffuseDown(vox, SubVolumes[vox.Row + 1, vox.Col]); break;
                case 18: DrTirandazReaction.DiffuseRight(vox, SubVolumes[vox.Row, vox.Col + 1]); break;
                case 19: DrTirandazReaction.DiffuseLeft(vox, SubVolumes[vox.Row, vox.Col - 1]); break;
                    //  case 20: Reaction.R21(vox); break;
            }
        }
        static Random rnd = new Random();
        internal static void ExecuteThisReactionOnThisVoxel(DrKaliradVoxel vox, int a, DrKaliradVoxel[,] SubVolumes)
        {
           
            switch (a)
            {
                case 1: DrKaliradReaction.R1(vox); break;
                case 2: DrKaliradReaction.R2(vox); break;
                case 3: DrKaliradReaction.R3(vox); break;
                case 4: DrKaliradReaction.R4(vox); break;
            }
            //Decay
           
            int r = rnd.Next(1, 10);
            if (r % 2 == 0 && vox.A>1) vox.A--;
            else
              if (r % 4 == 0) vox.A++;

        }
        
        static  List<QuantomicTime> ReactionsForEvaluation = new List<QuantomicTime>();
      //  static List<Voxel> DestinationOf
        public static void AddReactionToEvaluationBoard(DrTirandazVoxel voxel, int a)
        {
            // this.LogToFile(i, j, a, Reaction.LastStepTime, Reaction.TotalTime, SubVolumes);
            ReactionsForEvaluation.Add(new QuantomicTime() { Voxel=voxel, ReactionNumber_MustBeExecute=a, Quantoms=GetNeededQuantomsForThisReaction(a) });
        }

       const double qT = 0.001;//s
        public static double GetNeededQuantomsForThisReaction(int a)
        {
            //double time=0;
            //switch (a)
            //{
            //    case 1:  time= 1 / Voxel.K_a; break;
            //    case 2:  time= 1 / Voxel.K_I; break;
            //    case 3:  time= 1 / Voxel.K_1; break;
            //    case 4:  time= 1 / Voxel.K_n1; break;
            //    case 5:  time= 1 / Voxel.K_11; break; 
            //    case 6:  time= 1 / Voxel.K_2; break;
            //    case 7:  time= 1 / Voxel.K_n2; break;
            //    case 8:  time= 1 / Voxel.K_22; break;
            //    case 9:  time= 1 / Voxel.K_3; break;
            //    case 10: time= 1 / Voxel.K_4; break;
            //    case 11: time= 1 / Voxel.K_n4; break;
            //    case 12: time= 1 / Voxel.K_5; break;
            //    case 13: time= 1 / Voxel.K_6; break;
            //    case 14: time= 1 / Voxel.K_222; break;
            //    case 15: time= 1 / Voxel.K_333; break;
            //    case 16: time= 1 / ((Propensity.D_PTEN / Voxel.Area)); break;
            //    case 17: time= 1 / ((Propensity.D_PTEN / Voxel.Area)); break;
            //    case 18: time= 1 / ((Propensity.D_PTEN / Voxel.Area)); break;
            //    case 19: time= 1 / ((Propensity.D_PTEN / Voxel.Area)); break;
            //  case 20: time= 1 / ((Propensity.D_PTEN / Voxel.Area)); break;
            /* 
1 : time =1
2:  time = 10
3:  time = 2000
4:  time = 1
5:  time = 0.5
6:  time = 2000
7:  time = 1
8:  time = 1.25
9:  time = 1.25
10: time = 1
11: time = 2
12: time = 0.05
13: time = 0.05
14: time = 50
15: time = 50
16: time = 0.2
17: time = 0.2
18: time = 0.2
19: time = 0.2*/
            //   }
            // return (int)Math.Ceiling(time/qT);

            return GetExponentialTime();
        }

      public static double sumPropensity=0;
        private static double GetExponentialTime()
        {
            ExponentialDistribution ed = new ExponentialDistribution();

            //double d= ed.ProbabilityDensityFunction(sumPropensity);
            var d = ExponentialDistribution.Random(sumPropensity);
            return d;
        }
    }
}
