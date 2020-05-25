using System;
using System.Collections.Generic;

namespace InteractiveDataDisplay.WPF
{
    /// <summary>
    /// Provides mechanisms to generate ticks displayed on an axis. Uses algorithm for time (e.g. days, hours)
    /// </summary>
    public class TicksProviderForDays
    {
        private int m_delta = 1;
        private int m_beta  = 0;
                
        public TicksProviderForDays()
        {
            minorProvider = new MinorTicksProvider();
        }

        private readonly MinorTicksProvider minorProvider;
        public MinorTicksProvider MinorProvider
        {
            get { return minorProvider; }
        }

        private Range range = new Range(0, 1);
        public Range Range
        {
            get { return range; }
            set
            {
                range = value;
                m_delta = 1;
                m_beta = (int)Math.Round( Math.Log10(range.Max - range.Min) ) - 1;
            }
        }
        
        public double[] GetTicks()
        {
            double start  = Range.Min;
            double finish = Range.Max;
            {
                double d = finish - start;
                if (d == 0) return new double[] { start, finish };
            }

            double temp = m_delta * Math.Pow( 12, m_beta );

            double min = Math.Floor( start  / temp );
            double max = Math.Floor( finish / temp );

            int count = (int)(max - min + 1);
            List<double> res = new List<double>();
            double x0 = min * temp;
            for (int i = 0; i < count + 1; i++) {
                double v = x0 + i * temp;
                if (v >= start && v <= finish) res.Add(v);
            }
            return res.ToArray();
        }
        
        public void DecreaseTickCount()
        {
            if      (m_delta == 1) { m_delta = 2;            }
            else if (m_delta == 2) { m_delta = 4;            }    // 5 --> 4
            else if (m_delta == 4) { m_delta = 1;  m_beta++; }    // 5 --> 4
        }

        public void IncreaseTickCount()
        {
            if      (m_delta == 1) { m_delta = 4; m_beta--; }   // 5 --> 4
            else if (m_delta == 2) { m_delta = 1;           }
            else if (m_delta == 4) { m_delta = 2;           }   // 5 --> 4
        }

        public double[] GetMinorTicks(Range range)
        {
            var ticks = new List<double>( GetTicks() );
            double temp = m_delta * Math.Pow( 10, m_beta );
            ticks.Insert( 0, RoundHelper.Round( ticks[0]               - temp, m_beta ));
            ticks.Add   (    RoundHelper.Round( ticks[ticks.Count - 1] + temp, m_beta ));
            return minorProvider.CreateTicks(range, ticks.ToArray());
        }
    }
}

