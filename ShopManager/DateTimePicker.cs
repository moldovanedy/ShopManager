using System;
using System.Windows.Forms;
using ShopManager.Resources.Locale;

namespace ShopManager
{
    public partial class DateTimePicker : Form, IUserForm
    {
        internal DateTime DateTimeValue { get; set; } = DateTime.MinValue;

        public DateTimePicker()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            Translate();
        }

        public void Translate()
        {
            this.Text = Strings.Select_date_and_time;
            this.DateLabel.Text = Strings.Date;
            this.TimeLabel.Text = Strings.Time;
            this.SetButton.Text = Strings.Set;
        }

        private void SetButton_Click(object sender, EventArgs e)
        {
            DateTimeValue = new DateTime(
                year: this.DatePicker.Value.Year,
                month: this.DatePicker.Value.Month,
                day: this.DatePicker.Value.Day,

                hour: (int)this.HourSetter.Value,
                minute: (int)this.MinuteSetter.Value,
                second: (int)this.SecondSetter.Value);
            DialogResult = DialogResult.OK;
        }

        private void DateTimePicker_Shown(object sender, EventArgs e)
        {
            if (DateTimeValue == DateTime.MinValue)
            {
                DateTimeValue = DateTime.Now;
            }

            this.DatePicker.Value = DateTimeValue;
            this.HourSetter.Value = DateTimeValue.Hour;
            this.MinuteSetter.Value = DateTimeValue.Minute;
            this.SecondSetter.Value = DateTimeValue.Second;
        }
    }
}
