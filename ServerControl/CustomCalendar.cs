using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
namespace ServerControl
{
    [ToolboxBitmap(typeof(Calendar))]
    [ToolboxData("<{0}:CustomCalendar runat=server></{0}:CustomCalendar>")]
    public class CustomCalendar :CompositeControl
    {
        TextBox textBox;
        ImageButton imageButton;
        Calendar calendar;
       
        
        [Category("Appearance")]
        [Description("Sets and Gets an ImageUrl")]
        public string ImageButtonImageUrl
        {
            get
            {
                EnsureChildControls();
                return imageButton.ImageUrl !=null ? imageButton.ImageUrl: string.Empty;
            }

            set
            {
                EnsureChildControls();
                imageButton.ImageUrl = value;
            }
        }
        protected override void CreateChildControls()
        {
            Controls.Clear();
            textBox = new TextBox();
            textBox.ID = "textbox1";
            textBox.Width = Unit.Pixel(80);

            imageButton = new ImageButton();
            imageButton.ID = "imagebutton1";
            imageButton.Click += ImageButton_Click;

            calendar = new Calendar();
            calendar.ID = "calender1";
            calendar.Visible = false;
            calendar.SelectionChanged += Calendar_SelectionChanged;

            Controls.Add(textBox);
            Controls.Add(imageButton);
            Controls.Add(calendar);
        }

        private void Calendar_SelectionChanged(object sender, EventArgs e)
        {
            textBox.Text = calendar.SelectedDate.ToShortDateString();
            calendar.Visible = false;
            DateSelectedEventArgs dateSelectedEventArgs = new DateSelectedEventArgs(Convert.ToDateTime(textBox.Text));
            OnDateSelected(dateSelectedEventArgs);
            
        }

        private void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
           if(calendar.Visible)
            {
                calendar.Visible = false;

            }
            else
            {
                calendar.Visible = true;
               if(string.IsNullOrEmpty(textBox.Text))
                {
                    calendar.VisibleDate = DateTime.Today;
                }
                else
                {
                    DateTime output = DateTime.Now;
                    DateTime.TryParse(textBox.Text, out output);
                    calendar.VisibleDate = output;
                }
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.RenderBeginTag("Table");
            writer.RenderBeginTag("tr");
            writer.RenderBeginTag("td");
            textBox.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderBeginTag("td");
            imageButton.RenderControl(writer);
            writer.RenderEndTag();

            writer.RenderEndTag();
            writer.RenderEndTag();

            calendar.RenderControl(writer);
        }

        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }

       public DateTime SelectedDateOfControl
        {

            get {
                EnsureChildControls();
                return string.IsNullOrEmpty(textBox.Text) ? DateTime.MaxValue : Convert.ToDateTime(textBox.Text);
            }
            set
            {
                
                if(value !=null)
                {
                    EnsureChildControls();
                    textBox.Text = value.ToShortDateString();
                    calendar.VisibleDate = value;
                }
                else
                {
                    EnsureChildControls();
                    textBox.Text = string.Empty;
                    calendar.VisibleDate = DateTime.Now;
                }
            }
           
        }

        public event DateSelectedEventHandler DateSelected;

        protected virtual void OnDateSelected (DateSelectedEventArgs e)
        {
            if(DateSelected !=null)
            {
                DateSelected(this, e);
            }
        }
    }

    public class DateSelectedEventArgs:EventArgs
    {
       private DateTime _selectedDate;

       public DateTime SelectedDate
        {
            get { return _selectedDate; }
        }

       public DateSelectedEventArgs(DateTime selectedDate)
        {
            _selectedDate = selectedDate;
        }
    }

    public delegate void DateSelectedEventHandler(object sender, DateSelectedEventArgs e);
}
