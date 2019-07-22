using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BruteForceTask.BuyingBirds
{
    class Program
    {
        static void Main(string[] args)
        {
            BruteForceMethod b = new BruteForceMethod();
        }


    }
    class BruteForceMethod
    {
        struct X
        {
            public int startValue;
            public int endValue;
        }
        //C* = max(250x1+150x2+50x3)
        //при 20x1+33x2+x3 <= 50
        //Следовательно x1=0..20 x2=0..33 x3=0..50
        int V = 50;//Backpack's volume
        int N = 3;//Backpack's count subject
        int R = 5000;
        int[] w = { 1, 1, 1 };//Subject's volume
        int[] p = { 250, 150, 50 };//Subject's weight
        List<int> _X = new List<int>();//permutation options
        X[] Xconstraints = {
        new X() { startValue = 0, endValue = 20 },//3
        new X(){ startValue = 0, endValue = 33 }, //2
        new X(){ startValue = 0, endValue = 50 }};//1
        //-----------------------------------------------------
        void BruteForce2()
        {
            int column = Xconstraints.Length - 1;
            bool finish = false;
            int Count = 0;
            int C = 0;
            while (true)
            {
                int sum = 0;
                Print(ref _X);
                C = f(ref _X);
                for (int i = 0; i < _X.Count; i++) sum += _X[i];
                if ((C <= 5000) && sum==V)
                    Count++;
                //if (C > Cmax) Cmax = C;
                if (_X[column] == Xconstraints[column].endValue)
                {
                    for (int i = Xconstraints.Length - 1; i >= 0; i--)
                    {
                        if (_X[i] != Xconstraints[i].endValue)
                        {
                            finish = false;
                            break;
                        }
                        finish = true;
                    }
                    if (finish)
                        break;
                    _X[column] = Xconstraints[column].startValue;
                    for (int i = N - 2; i >= 0; i--)
                    {
                        if (_X[i] == Xconstraints[i].endValue)
                        {
                            _X[i] = Xconstraints[i].startValue;
                            if (_X[i - 1] < Xconstraints[i - 1].endValue)
                            {
                                _X[i - 1]++;
                                break;
                            }
                        }
                        else
                        {
                            _X[i]++;
                            break;
                        }
                    }
                    Print(ref _X);
                }
                _X[column]++;
            }
            Console.WriteLine("Cmax = {0}", Count);

        }

        void Print(ref List<int> X)
        {
            for (int i = 0; i < _X.Count; i++)
                Console.Write(_X[i] + " ");
            Console.WriteLine();
        }

        int f(ref List<int> X)
        {
            int _temp = 0;
            for (int i = 0; i < X.Count; i++) _temp += p[i] * X[i];
            return _temp;
        }
        public BruteForceMethod()
        {
            for (int i = 0; i < N; i++) _X.Add(Xconstraints[i].startValue);

            BruteForce2();

        }
    }
}
