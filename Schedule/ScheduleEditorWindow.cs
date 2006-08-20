using System;
using System.Globalization;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.Schedule;

namespace EVEMon.Schedule
{
    public partial class ScheduleEditorWindow : EVEMonForm
    {
        public ScheduleEditorWindow()
        {
            InitializeComponent();
        }

        public ScheduleEditorWindow(Settings s)
            : this()
        {
            m_settings = s;
            foreach(ScheduleEntry temp in m_settings.Schedule)
            {
                lbEntries.Items.Add(temp.Title);
            }
        }

        private Settings m_settings;

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (EditScheduleEntryWindow f = new EditScheduleEntryWindow())
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                m_settings.Schedule.Add(f.ScheduleEntry);
                lbEntries.Items.Add(f.ScheduleEntry.Title);
                m_settings.Save();
            }
        }

        private DateTime currentdate = DateTime.Now;

        private void ScheduleEditorWindow_Load(object sender, EventArgs e)
        {
            nudMonth.Items.Clear();
            string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            nudMonth.Items.Add(monthNames[0]);
            for (int i = 1; i <= CultureInfo.CurrentCulture.Calendar.GetMonthsInYear(currentdate.Year); i++)
            {
                nudMonth.Items.Add(
                    monthNames[((CultureInfo.CurrentCulture.Calendar.GetMonthsInYear(currentdate.Year)) - i)]);
            }
            nudMonth.Items.Add(monthNames[CultureInfo.CurrentCulture.Calendar.GetMonthsInYear(currentdate.Year) - 1]);
            nudYear.Value = currentdate.Year;
            nudMonth.SelectedIndex = ((nudMonth.Items.Count - 1) - currentdate.Month);
            nudDay.Maximum = CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(currentdate.Year, currentdate.Month) + 1;
            nudDay.Value = currentdate.Day;
            calControl.Date = currentdate;
        }

        private void changedYear(object sender, EventArgs e)
        {
            int oldyearnum = currentdate.Year;
            currentdate = currentdate.AddYears((int) nudYear.Value - currentdate.Year);
            if (currentdate.Month == 2 &&
                (CultureInfo.CurrentCulture.Calendar.IsLeapYear(currentdate.Year) ||
                 CultureInfo.CurrentCulture.Calendar.IsLeapYear(oldyearnum)))
            {
                nudDay.Maximum =
                    CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(currentdate.Year, currentdate.Month) + 1;
                if (CultureInfo.CurrentCulture.Calendar.IsLeapYear(oldyearnum) && nudDay.Value > nudDay.Maximum)
                {
                    nudDay.Value = nudDay.Maximum;
                }
            }
            calControl.Date = currentdate;
        }

        private void changedDay(object sender, EventArgs e)
        {
            bool donex = false;
            bool doney = false;
            if (nudDay.Value == 0)
            {
                if (nudMonth.SelectedIndex == (nudMonth.Items.Count - 2) && nudYear.Value == nudYear.Minimum)
                {
                    nudDay.Value = 1;
                }
                currentdate = currentdate.AddDays((int) nudDay.Value - currentdate.Day);
                nudDay.Maximum =
                    CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(currentdate.Year, currentdate.Month) + 1;
                if (nudDay.Value == 0)
                {
                    nudDay.Value =
                        CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(currentdate.Year, currentdate.Month);
                }
                donex = true;
            }
            else if (!donex && nudDay.Value == nudDay.Maximum)
            {
                if (nudMonth.SelectedIndex == 1 && nudYear.Value == nudYear.Maximum)
                {
                    nudDay.Value =
                        CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(currentdate.Year, currentdate.Month);
                    doney = true;
                }
                else if (!doney)
                {
                    currentdate = currentdate.AddDays((int) nudDay.Value - currentdate.Day);
                    nudDay.Value = 1;
                    nudDay.Maximum =
                        CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(currentdate.Year, currentdate.Month) + 1;
                }
                donex = true;
            }
            else if (!donex)
            {
                currentdate = currentdate.AddDays((int) nudDay.Value - currentdate.Day);
            }
            calControl.Date = currentdate;
            nudYear.Value = currentdate.Year;
            nudMonth.SelectedIndex = (CultureInfo.CurrentCulture.Calendar.GetMonthsInYear(currentdate.Year) -
                                      currentdate.Month) + 1;
        }

        private void changedMonth(object sender, EventArgs e)
        {
            if (nudMonth.SelectedIndex == nudMonth.Items.Count - 1 && nudYear.Value == nudYear.Minimum)
            {
                nudMonth.SelectedIndex = nudMonth.Items.Count - 2;
            }
            if (nudMonth.SelectedIndex == 0 && nudYear.Value == nudYear.Maximum)
            {
                nudMonth.SelectedIndex = 1;
            }
            currentdate =
                currentdate.AddMonths((((nudMonth.Items.Count - 1) - nudMonth.SelectedIndex) - currentdate.Month));
            nudMonth.SelectedIndex = ((nudMonth.SelectedIndex + (nudMonth.Items.Count - 3))%(nudMonth.Items.Count - 2)) +
                                     1;
            if (nudDay.Value > CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(currentdate.Year, currentdate.Month))
            {
                nudDay.Value = CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(currentdate.Year, currentdate.Month);
            }
            nudDay.Maximum = CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(currentdate.Year, currentdate.Month) + 1;
            calControl.Date = currentdate;
            nudYear.Value = currentdate.Year;
        }

        private void tsbDeleteEntry_Click(object sender, EventArgs e)
        {
            if (lbEntries.SelectedIndex != -1)
            {
                int i = -1;
                for (int x = 0; x < m_settings.Schedule.Count && i == -1; x++)
                {
                    if (m_settings.Schedule[x].Title == lbEntries.Items[lbEntries.SelectedIndex].ToString())
                    {
                        i = x;
                    }
                }
                m_settings.Schedule.RemoveAt(i);
                lbEntries.Items.RemoveAt(lbEntries.SelectedIndex);
                m_settings.Save();
            }
        }
    }
}
