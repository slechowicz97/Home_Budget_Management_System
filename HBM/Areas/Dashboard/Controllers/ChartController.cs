using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using HBM.ViewModels;
using HBM.Data;
using System.Collections;
using HBM.Entities;

namespace HBM.Areas.Dashboard.Controllers
{
    public class ChartController : Controller
    {
        // GET: Dashboard/Chart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CharterColumn()
        {
            var context = new HBMContext();
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            var results = (from c in context.Transactions select c);
           
            results.ToList().ForEach(rs => xValue.Add(rs.Budgets.Name));
            results.ToList().ForEach(rs => yValue.Add(rs.Price));

            new Chart(width: 600, height: 400, theme: ChartTheme.Blue)
                .AddTitle("Twoje wydatki")
                .AddSeries("Default", chartType: "Column", xValue: xValue, yValues: yValue)
                .Write("bmp");

            return null;
        }

        public ActionResult DoughnutGraph()
        {
            var context = new HBMContext();
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            var results = (from c in context.Transactions select c);

            results.ToList().ForEach(rs => xValue.Add(rs.Budgets.Name));
            results.ToList().ForEach(rs => yValue.Add(rs.Price));


    string myTheme = @"<Chart>
                <ChartAreas>
                    <ChartArea Name=""Default"">
                    </ChartArea>
                </ChartAreas>
                <Series>
                    <Series Name=""Default"" CustomProperties=""PieLabelStyle = Disabled""></Series>
                </Series>
                <Titles>
                    <Title Name=""Default""></Title>
                </Titles>
            </Chart>";

            new Chart(width: 600, height: 400, theme: myTheme)
                .AddSeries("Default", chartType: "Doughnut", xValue:xValue, yValues: yValue)
                .AddLegend("Kategorie")
                
                .Write("bmp");

            return null;
        }
    }
}