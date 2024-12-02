using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FazendaUrbanaApp
{
    public partial class ControlePlantio : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-T25RSRN;Initial Catalog=PIM;User ID=sa;Password=Goias@123;Encrypt=False");
        public ControlePlantio()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Plantios Values('" + textBox1.Text + "','" + dateTimePicker1.Text + "'," + int.Parse(textBox4.Text) + "," + comboBox1.ValueMember + ")", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Inseriu com Sucesso!");
            conn.Close();
            recuperaPlantioTable();
        }

        private void recuperaPlantioTable() {
            SqlCommand cmd = new SqlCommand("select a.tipoCultivo 'Tipo de Cultivo', a.dataPlantio 'Data do Plantio', a.quantidadePlantada 'Quantidade em KG', b.nome 'Produtor' from Plantios a inner join Produtores b on a.produtor_id = b.id ", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();    
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void recuperaProdutorCB() {
            String scom = "select * from Produtores";
            SqlDataAdapter da = new SqlDataAdapter(scom, conn);
            DataTable dtResultado = new DataTable();
            dtResultado.Clear();//o ponto mais importante (limpa a table antes de preenche-la)
            comboBox1.DataSource = null;
            da.Fill(dtResultado);
            comboBox1.DataSource = dtResultado;
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "nome";
            comboBox1.SelectedItem = "";
            comboBox1.Refresh(); //faz uma nova busca no BD para preencher os valores da cb de departamentos.
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            recuperaPlantioTable();
            recuperaProdutorCB();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
