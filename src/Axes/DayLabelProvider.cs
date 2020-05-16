//RML
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace InteractiveDataDisplay.WPF
{
    /// <summary>
    /// Provides mechanisms to generate labels displayed on an axis by DateTime ticks. 
    /// Aligns on day basis.
    /// </summary>
    public class DayLabelProvider : ILabelProvider
    {
        public string Format { get; set; } = "dd MMMM yyyy, HH:mm";
        private readonly double m_dTicksPerDay = TimeSpan.FromDays( 1 ).Ticks;
        private readonly double m_dDayTickStart;

        public DayLabelProvider()
        {
            bool isOkS = DateTime.TryParse( "1/1/2020 12:00:00 AM", out DateTime dtS );
            m_dDayTickStart = dtS.Ticks / m_dTicksPerDay;
        }

        /// <summary>
        /// Generates an array of labels from an array of double.
        /// </summary>
        /// <param name="ticks">An array of double ticks.</param>
        /// <returns>An array of <see cref="FrameworkElement"/>.</returns>
        public FrameworkElement[] GetLabels(double[] ticks)
        {
            if (ticks == null)
                throw new ArgumentNullException(nameof(ticks));

            List<TextBlock> Labels = new List<TextBlock>();
            foreach (double tick in ticks)
            {
                double dDayReal = tick + m_dDayTickStart;
                long lTimeTicks = (long)(dDayReal * m_dTicksPerDay);

                DateTime dtX = new DateTime( lTimeTicks );

                TextBlock text = new TextBlock
                {
                    Text = dtX.ToString(Format) //t + " (" + tick.ToString( "F3" ) + ")"
                };
                Labels.Add(text);
            }
            return Labels.ToArray();
        }
    }
}

