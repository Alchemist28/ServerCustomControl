using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServerControl
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CustomCalendar1.DateSelected += CustomCalendar1_DateSelected;

        }

        private void CustomCalendar1_DateSelected(object sender, DateSelectedEventArgs e)
        {
            Response.Write(e.SelectedDate.ToShortDateString());
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
          
            
        }
    }
}