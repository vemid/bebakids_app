using ClosedXML.Excel;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace BebaKids.Prijava.Reports
{
    public partial class ReportByRadnik : Form
    {
        public ReportByRadnik()
        {
            InitializeComponent();
            Save save = new Save();
            Classes.Application radnici = new Classes.Application();

            cbRadnici.DataSource = radnici.radnici();
            cbRadnici.DisplayMember = "ImePrezime";
            cbRadnici.ValueMember = "SifraRadnika";
            cbRadnici.SelectedIndex = -1;
            cbRadnici.SelectedIndex = -1;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            int tsifra = Convert.ToInt32(cbRadnici.SelectedValue.ToString());
            Classes.Application radnici = new Classes.Application();
            DataTable tableUsera = radnici.radnici();

            //DataRow checkUser = tableUsera.Rows.Find(tsifra);

            Save tabela = new Save();

            string dFrom = dateFrom.Value.ToString("yyyy-MM-dd");
            string dTo = dateTo.Value.ToString("yyyy-MM-dd");
            string month = dateFrom.Value.ToString("MMMM");

            XLWorkbook wb = new XLWorkbook();

            //smestanja podataka na sat u excel
            DataTable excel = new DataTable();
            excel = tabela.tableIzvestajRadnika(tsifra, dFrom, dTo, "REGULAR");
            string TotalVreme = tabela.getTime(tsifra, dFrom, dTo, "REGULAR");

            var wRegular = wb.Worksheets.Add(excel, "Regular");
            wRegular.Columns().AdjustToContents();
            wRegular.Row(1).InsertRowsAbove(3);
            wRegular.Range("A1:K1").Style.Fill.BackgroundColor = XLColor.FromHtml("#20b2aa");
            wRegular.Range("A2:K3").Style.Fill.BackgroundColor = XLColor.FromHtml("#c0d6e4");
            wRegular.Range("A1:K1").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            wRegular.Range("A2:K3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            wRegular.Cell("A1").Value = "Izvestaj prijave radnika";
            wRegular.Range("A1:J1").Merge();
            wRegular.Cell("A2").Value = "Radnik: ";
            wRegular.Cell("B2").Value = cbRadnici.Text.ToString();
            wRegular.Cell("C2").Value = "Maticni objekat: ";
            wRegular.Cell("D2").Value = (from DataRow dr in tableUsera.Rows where (int)dr["SifraRadnika"] == tsifra select (string)dr["Objekat"]).FirstOrDefault();
            wRegular.Cell("C3").Value = "Status: ";
            wRegular.Cell("D3").Value = (from DataRow dr in tableUsera.Rows where (int)dr["SifraRadnika"] == tsifra select (string)dr["Status"]).FirstOrDefault();

            int lastRow = wRegular.LastRowUsed().RowNumber();
            string totalKolicina = "Total";
            wRegular.Cell(lastRow + 1, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            wRegular.Cell(lastRow + 1, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            wRegular.Cell(lastRow + 1, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            wRegular.Cell(lastRow + 1, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            wRegular.Cell(lastRow + 1, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            wRegular.Cell(lastRow + 1, 5).Value = totalKolicina;
            wRegular.Cell(lastRow + 1, 6).Value = TotalVreme.ToString();
            wRegular.Cell(lastRow + 1, 7).Value = excel.Compute("Sum(RedovanDan)", "").ToString();
            wRegular.Cell(lastRow + 1, 8).Value = excel.Compute("Sum(DuplaSmena)", "").ToString();
            wRegular.Cell(lastRow + 1, 9).Value = excel.Compute("Sum(Bolovanje)", "").ToString();
            wRegular.Cell(lastRow + 1, 10).Value = excel.Compute("Sum(PlacenDan)", "").ToString();

            //smestanja podataka na sat u excel
            DataTable excelHour = new DataTable();
            excelHour = tabela.tableIzvestajRadnika(tsifra, dFrom, dTo, "HOUR");
            string TotalVremeHour = tabela.getTime(tsifra, dFrom, dTo, "HOUR");

            var wHour = wb.Worksheets.Add(excelHour, "Hour");
            wHour.Columns().AdjustToContents();
            wHour.Row(1).InsertRowsAbove(3);
            wHour.Range("A1:K1").Style.Fill.BackgroundColor = XLColor.FromHtml("#20b2aa");
            wHour.Range("A2:K3").Style.Fill.BackgroundColor = XLColor.FromHtml("#c0d6e4");
            wHour.Range("A1:K1").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            wHour.Range("A2:K3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            wHour.Cell("A1").Value = "Izvestaj prijave radnika";
            wHour.Range("A1:J1").Merge();
            wHour.Cell("A2").Value = "Radnik: ";
            wHour.Cell("B2").Value = cbRadnici.Text.ToString();
            wHour.Cell("C2").Value = "Maticni objekat: ";
            wHour.Cell("D2").Value = (from DataRow dr in tableUsera.Rows where (int)dr["SifraRadnika"] == tsifra select (string)dr["Objekat"]).FirstOrDefault();
            wHour.Cell("C3").Value = "Status: ";
            wHour.Cell("D3").Value = (from DataRow dr in tableUsera.Rows where (int)dr["SifraRadnika"] == tsifra select (string)dr["Status"]).FirstOrDefault();

            int lastRowHour = wHour.LastRowUsed().RowNumber();
            //string totalKolicinaHour = "Total";
            wHour.Cell(lastRowHour + 1, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            wHour.Cell(lastRowHour + 1, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            wHour.Cell(lastRowHour + 1, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            wHour.Cell(lastRowHour + 1, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            wHour.Cell(lastRowHour + 1, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            wHour.Cell(lastRowHour + 1, 5).Value = totalKolicina;
            wHour.Cell(lastRowHour + 1, 6).Value = TotalVremeHour.ToString();
            wHour.Cell(lastRowHour + 1, 7).Value = excelHour.Compute("Sum(RedovanDan)", "").ToString();
            wHour.Cell(lastRowHour + 1, 8).Value = excelHour.Compute("Sum(DuplaSmena)", "").ToString();
            wHour.Cell(lastRowHour + 1, 9).Value = excelHour.Compute("Sum(Bolovanje)", "").ToString();
            wHour.Cell(lastRowHour + 1, 10).Value = excelHour.Compute("Sum(PlacenDan)", "").ToString();

            //smestanja podataka popisa u excel
            DataTable excelPopis = new DataTable();
            excelPopis = tabela.tableIzvestajRadnika(tsifra, dFrom, dTo, "POPIS");
            string TotalVremePopis = tabela.getTime(tsifra, dFrom, dTo, "POPIS");

            var bPopis = wb.Worksheets.Add(excelPopis, "Popis");
            bPopis.Columns().AdjustToContents();
            bPopis.Row(1).InsertRowsAbove(3);
            bPopis.Range("A1:K1").Style.Fill.BackgroundColor = XLColor.FromHtml("#20b2aa");
            bPopis.Range("A2:K3").Style.Fill.BackgroundColor = XLColor.FromHtml("#c0d6e4");
            bPopis.Range("A1:K1").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            bPopis.Range("A2:K3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            bPopis.Cell("A1").Value = "Izvestaj prijave radnika";
            bPopis.Range("A1:J1").Merge();
            bPopis.Cell("A2").Value = "Radnik: ";
            bPopis.Cell("B2").Value = cbRadnici.Text.ToString();
            bPopis.Cell("C2").Value = "Maticni objekat: ";
            bPopis.Cell("D2").Value = (from DataRow dr in tableUsera.Rows where (int)dr["SifraRadnika"] == tsifra select (string)dr["Objekat"]).FirstOrDefault();
            bPopis.Cell("C3").Value = "Status: ";
            bPopis.Cell("D3").Value = (from DataRow dr in tableUsera.Rows where (int)dr["SifraRadnika"] == tsifra select (string)dr["Status"]).FirstOrDefault();

            int lastRobPopis = bPopis.LastRowUsed().RowNumber();
            //string totalKolicinaHour = "Total";
            bPopis.Cell(lastRobPopis + 1, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            bPopis.Cell(lastRobPopis + 1, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            bPopis.Cell(lastRobPopis + 1, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            bPopis.Cell(lastRobPopis + 1, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            bPopis.Cell(lastRobPopis + 1, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            bPopis.Cell(lastRobPopis + 1, 5).Value = totalKolicina;
            bPopis.Cell(lastRobPopis + 1, 6).Value = TotalVremePopis.ToString();
            bPopis.Cell(lastRobPopis + 1, 7).Value = excelPopis.Compute("Sum(RedovanDan)", "").ToString();
            bPopis.Cell(lastRobPopis + 1, 8).Value = excelPopis.Compute("Sum(DuplaSmena)", "").ToString();
            bPopis.Cell(lastRobPopis + 1, 9).Value = excelPopis.Compute("Sum(Bolovanje)", "").ToString();
            bPopis.Cell(lastRobPopis + 1, 10).Value = excelPopis.Compute("Sum(PlacenDan)", "").ToString();



            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files|*.xlsx",
                Title = "Sacuvajte prijemnice"
            };
            saveFileDialog.InitialDirectory = "c:\\";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = month + "_" + cbRadnici.Text.ToString();
            //saveFileDialog.ShowDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK && !String.IsNullOrWhiteSpace(saveFileDialog.FileName))
            {

                wb.SaveAs(saveFileDialog.FileName);

                MessageBox.Show("Uspesno exportovani podaci", "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //excel.Clear();
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Save tabela = new Save();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Save tabela = new Save();
            int tsifra = Convert.ToInt32(cbRadnici.SelectedValue.ToString());
            string dFrom = dateFrom.Value.ToString("yyyy-MM-dd");
            string dTo = dateTo.Value.ToString("yyyy-MM-dd");

            DataTable excel = new DataTable();
            excel = tabela.tableIzvestajRadnika(tsifra, dFrom, dTo, "REGULAR");

            DataTable excelHour = new DataTable();
            excelHour = tabela.tableIzvestajRadnika(tsifra, dFrom, dTo, "HOUR");

            DataTable excelPopis = new DataTable();
            excelPopis = tabela.tableIzvestajRadnika(tsifra, dFrom, dTo, "POPIS");

            dataGridView1.DataSource = excel;
            dataGridView2.DataSource = excelHour;
            dataGridView3.DataSource = excelPopis;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Prijava.Aktivnost aktivnost = new Aktivnost();
            aktivnost.Show();
        }
    }
}
