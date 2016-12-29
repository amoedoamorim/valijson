using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NJsonSchema;
using NJsonSchema.Validation;

namespace Valijson
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var schemaTask = NJsonSchema.JsonSchema4.FromJsonAsync(schemaTextBox.Text);
            schemaTask.ContinueWith(t => {
                try
                {
                    var schema = t.Result;
                    var errors = schema.Validate(jsonTextBox.Text);

                    showResult(errors);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show(ex.Message, "Error");
                }
            });
        }

        private void showResult(ICollection<ValidationError> errors)
        {
            if (errors.Count > 0)
            {
                string message = string.Empty;

                foreach (var error in errors)
                {
                    message += "Line " + error.LineNumber + " : " + error.ToString() + "\n";
                }

                MessageBox.Show(message, "Validation Errors");
            }
            else
            {
                MessageBox.Show("JSON fits into Schema", "SUCCESS");
            }
        }
    }
}
