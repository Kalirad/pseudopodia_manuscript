using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StochasticalChemicalLevel
{
  public class DrTirandazVoxel
    {
        public int M1_Ras { get; set; } 
        public int M2_PI3K { get; set; }
        public int M3_PTEN { get; set; }
        //public int M4_SGP { get; set; }
        //public int M5_PIP { get; set; }
        public int M6_P2 { get; set; } 
        public int M26_PI3K_P2 { get; set; }
        public int M7_P3 { get; set; }
        public int M37_PTEN_P3 { get; internal set; }
        public int M8_Xactin { get; set; } 
        public int M9_Xmyosin { get; set; }
        public int PIP { get; internal set; }
        /*
        static public double R0 { get; private set; } = 2.2/100000;// 2.2*10^-5 M
        static public double L0 { get; private set; } = 0.00001;//10^-6 M
        static public double LR { get; private set; } = 0.01;//10^-5 1/M.s
        static public double K_betaGama { get; private set; } = 0.00001;//10^-5 1/M.s
        static public double K_a_betaGama { get; private set; } = 0.01;//10^-5 1/M.s
        static public double G_betaGama { get; private set; } = 0.01;
        static public double K_a_Ras { get; private set; } = 0.01;
        static public double K_n_PI3K { get; private set; } = 1;//negative
        static public double K_p_PI3K { get; private set; } = 1;//positive
        static public double K_M { get; private set; } = 0.000005;//0.5*10^-1 1/M.s
        static public double K_n_pten { get; private set; } = 0.1;
        static public double K_p_pten { get; private set; } = 0.0000001;
        static public double K_A { get; private set; } = 0.000005;//0.5*10^-1 1/M.s
        static public double K_B { get; private set; } = 0.8;
        static public double K_sgb { get; private set; } = 0.2;
        static public double K_n_pip { get; private set; } = 0.14;
        static public double K_nn_pip { get; private set; } = 0.00000045;//0.45*1-^-6
        static public double K_p_pip { get; private set; } = 0.0001;//10^-4
        static public double K_pp_pip { get; private set; } = 0.14;
         
        static public double K_n_p2 { get; private set; } = 0.01;
        static public double K_p_p2 { get; private set; } = 0.14;
        static public double K_f { get; private set; } = 0.0000008;//0.8 *  10^-8
        static public double K_p { get; private set; } = 0.8;//10^8 K_p3
        static public double K_n { get; private set; } = 1;//1 1/s 
        static public double K_i_M { get; private set; } = 0.1;
        static public double K_i_A { get; private set; } = 0.1;
        static public double TotalReactionsK = K_betaGama+ K_a_betaGama+ K_n_PI3K+ K_n_pten+ K_pp_pip+ K_n_pip+ K_n_p2+ K_p_pten+ K_p+ K_B;
        static public double[] AllReactionsK=new double[] { K_betaGama, K_a_betaGama, K_n_PI3K,K_n_pten,K_pp_pip, K_n_pip ,K_n_p2,K_p_pten,K_p, K_B};
        */
        static public double N_Avogadro = 6.0221415E+23; //6.0221415 * Math.Pow(10,23);//1/mol
        static public double K_a  = 1;// 1/s
        static public double K_I  = 0.1;
        //static public double K_1  = 8.302694315635062E-10;// 0.5 * Math.Pow(10,-6) / (N_Avogadro* Volume);
        //static public double K_2  = 8.302694315635062E-10;//0.5 * Math.Pow(10, -6) / (N_Avogadro * Volume);
        //static public double K_111 = 20 * Math.Pow(10, -6) / (N_Avogadro * Volume);
        //static public double K_222 = 20 * Math.Pow(10, -6) / (N_Avogadro * Volume);
        //static public double K_333 = 20 * Math.Pow(10, -6) / (N_Avogadro * Volume);
        static public double K_1 ;
        static public double K_2 ;
        static public double K_111;
        static public double K_222;
        static public double K_333;


        static public double K_n1  = 1;
        static public double K_n2 = 1;
       
        static public double K_11 = 2;
        static public double K_22 = 0.8;
        static public double K_3  = 0.8;
        static public double K_4  = 1;
        static public double K_5  = 20;
        static public double K_6  = 20;
        static public double K_n4  = 0.5;
        static public double deltaT = 0.05;//second
        public static double Area { get;  set; }
        public static double Volume { get; internal set; }
        public static double Size { get; internal set; }
  
        public bool IsLeftBoundry { get; internal set; } = false;
        public bool IsRightBoundry { get; internal set; } = false;
        public bool IsTopBoundry { get; internal set; } = false;
        public bool IsBottonBoundry { get; internal set; } = false;

        public int Row { get; set; }
        public int Col { get; set; }
        public double QuantomixClock { get; set; }

        public List<long> EntranceOfPten { get; set; }//میگه توی این لحظه یدونه پی-تن اضافه بشه بهت، همسایه داده

    }
}
