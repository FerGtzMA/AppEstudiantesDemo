using Data;
using LinqToDB;
using Logica.Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logica
{
    public class Estudiantes : Libreria
    {
        private List<TextBox> listTextBox;
        private List<Label> listLabels;
        private PictureBox image;
        private Bitmap _imageBitmap;
        private DataGridView _dataGridView;
        private NumericUpDown _numericUpDown;
        private Paginador<Estudiante> _paginador;

        private string _accion = "insert";

        public Estudiantes(List<TextBox> listTextBox, List<Label> listLabels, object[] objetos)
        {
            this.listTextBox = listTextBox;
            this.listLabels = listLabels;
            this.image = (PictureBox)objetos[0];
            this._imageBitmap = (Bitmap)objetos[1];
            this._dataGridView = (DataGridView)objetos[2];
            this._numericUpDown = (NumericUpDown)objetos[3];

            Restablecer();
        }

        public void Registrar()
        {
            if (listTextBox[0].Text.Equals(""))
            {
                listLabels[0].Text = "Este campo es requerido";
                listLabels[0].ForeColor = Color.Red;
                listTextBox[0].Focus();
            }
            else
            {
                if (listTextBox[1].Text.Equals(""))
                {
                    listLabels[1].Text = "Este campo es requerido";
                    listLabels[1].ForeColor = Color.Red;
                    listTextBox[1].Focus();
                }
                else
                {
                    if (listTextBox[2].Text.Equals(""))
                    {
                        listLabels[2].Text = "Este campo es requerido";
                        listLabels[2].ForeColor = Color.Red;
                        listTextBox[2].Focus();
                    }
                    else
                    {
                        if (listTextBox[3].Text.Equals(""))
                        {
                            listLabels[3].Text = "Este campo es requerido";
                            listLabels[3].ForeColor = Color.Red;
                            listTextBox[3].Focus();
                        }
                        else
                        {
                            if (textBoxEvents.comprobarFormatEmail(listTextBox[3].Text))
                            {
                                // Siempre que utilicemos where, tenemos que poner toList para hacerlo lista
                                var user = _Estudiante.Where(que => que.email.Equals(listTextBox[3].Text)).ToList();
                                if (user.Count.Equals(0))
                                {
                                    Save();
                                }
                                else
                                {
                                    if (user[0].id.Equals(_idEstudiante))
                                    {
                                        Save();
                                    }else
                                    {
                                        listLabels[3].Text = "Email ya registrado";
                                        listLabels[3].ForeColor = Color.Red;
                                        listTextBox[3].Focus();
                                    }
                                }
                            }
                            else
                            {
                                listLabels[3].Text = "Email no valido";
                                listLabels[3].ForeColor = Color.Red;
                                listTextBox[3].Focus();
                            }
                        }
                    }
                }
            }
        }

        private void Save()
        {
            // Lo va a adminsitrar las transacciones
            BeginTransactionAsync(); // decir que haremos una excepcion
            try
            {
                var imagenArray = uploadImage.ImageToByte(image.Image);

                switch (_accion)
                {
                    case "insert":
                        _Estudiante.Value(e => e.nid, listTextBox[0].Text)
                    .Value(e => e.nombre, listTextBox[1].Text)
                    .Value(e => e.apellido, listTextBox[2].Text)
                    .Value(e => e.email, listTextBox[3].Text)
                    .Value(e => e.imagen, imagenArray)
                    .Insert();
                        break;
                    case "update":
                        _Estudiante.Where(u => u.id.Equals(_idEstudiante))
                            .Set(u => u.nid, listTextBox[0].Text)
                            .Set(u => u.nombre, listTextBox[1].Text)
                            .Set(u => u.apellido, listTextBox[2].Text)
                            .Set(u => u.email, listTextBox[3].Text)
                            .Set(u => u.imagen, imagenArray)
                            .Update();
                        break;
                }

                

                // Se inserta la transarccion satisfactoriamente
                CommitTransaction();

                Restablecer();

                /*
                 * var db = new Conexion();
                db.Insert(new Estudiante()
                {
                    nid = listTextBox[0].Text,
                    nombre = listTextBox[1].Text,
                    apellido = listTextBox[2].Text,
                    email = listTextBox[3].Text,
                });
                */
            }
            catch (Exception)
            {
                // Si ocurre una Exception, la transaccion guardada se borrara
                RollbackTransaction();
            }
        }

        private int _num_pagina = 1, _reg_por_pagina = 2;
        public void SearchEstudiantes(string campo)
        {
            List<Estudiante> query = new List<Estudiante>();
            int inicio = (_num_pagina - 1) * _reg_por_pagina;
            if (campo.Equals(""))
            {
                query = _Estudiante.ToList();
            }
            else
            {
                query = _Estudiante.Where(
                    buscar => buscar.nid.StartsWith(campo) 
                    || buscar.nombre.StartsWith(campo) 
                    || buscar.apellido.StartsWith(campo)
                    ).ToList();
            }
            // verificamos si la consulta contiene registros
            if (0 < query.Count)
            {
                _dataGridView.DataSource = query.Select(c => new
                {
                    c.id,
                    c.nid,
                    c.nombre,
                    c.apellido,
                    c.email,
                    c.imagen,
                }).Skip(inicio).Take(_reg_por_pagina).ToList(); // creando paginador
                // con skip cuantos registros va a omitir de n registros encontrados en el query
                // con take guarda cuantos registros veremos de los n - skin registros que tenemos
                _dataGridView.Columns[0].Visible = false;
                _dataGridView.Columns[5].Visible = false;

                _dataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                _dataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            }
            else
            {
                _dataGridView.DataSource = query.Select(c => new
                {
                    c.nid,
                    c.nombre,
                    c.apellido,
                    c.email,
                }).ToList();
            }
        }

        private List<Estudiante> listEstudiante;
        public void Paginador(string metodo)
        {
            switch (metodo)
            {
                case "Primero":
                    _num_pagina = _paginador.primero();
                    break;
                case "Anterior":
                    _num_pagina = _paginador.anteriro();
                    break;
                case "Siguiente":
                    _num_pagina = _paginador.siguiente();
                    break;
                case "Ultimo":
                    _num_pagina = _paginador.ultimo();
                    break;
            }

            SearchEstudiantes("");
        }

        public void RegistroPaginas()
        {
            _num_pagina = 1;
            _reg_por_pagina = (int)_numericUpDown.Value;
            var list = _Estudiante.ToList();
            if (0 < list.Count)
            {
                _paginador = new Paginador<Estudiante>(listEstudiante, listLabels[4], _reg_por_pagina);
                SearchEstudiantes("");
            }
        }

        public void Eliminar()
        {
            if (_idEstudiante.Equals(0))
            {
                MessageBox.Show("Seleccione un estudiante");
            }
            else
            {
                if(MessageBox.Show("Estas seguro de eliminar el estudiante?", "Eliminar estudiante", MessageBoxButtons.YesNo ) == DialogResult.Yes)
                {
                    _Estudiante.Where(c => c.id.Equals(_idEstudiante)).Delete();
                    Restablecer();
                }
            }
        }
        public void Restablecer()
        {
            // Para el update
            _accion = "insert";
            _num_pagina = 1;
            _idEstudiante = 0;
            image.Image = _imageBitmap;

            image.Image = null;
            listLabels[0].Text = "Nid";
            listLabels[1].Text = "Nombre";
            listLabels[2].Text = "Apellido";
            listLabels[3].Text = "Email";
            listLabels[0].ForeColor = Color.LightSlateGray;
            listLabels[1].ForeColor = Color.LightSlateGray;
            listLabels[2].ForeColor = Color.LightSlateGray;
            listLabels[3].ForeColor = Color.LightSlateGray;

            listTextBox[0].Text = "";
            listTextBox[1].Text = "";
            listTextBox[2].Text = "";
            listTextBox[3].Text = "";

            listEstudiante = _Estudiante.ToList();
            if(0 < listEstudiante.Count)
            {
                _paginador = new Paginador<Estudiante>(listEstudiante, listLabels[4], _reg_por_pagina);
            }

            SearchEstudiantes("");
        }

        private int _idEstudiante = 0;
        public void getEstudiante()
        {
            _accion = "update";
            _idEstudiante = Convert.ToInt16(_dataGridView.CurrentRow.Cells[0].Value);
            listTextBox[0].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[1].Value);
            listTextBox[1].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[2].Value);
            listTextBox[2].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[3].Value);
            listTextBox[3].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[4].Value);

            try
            {
                byte[] arraImage = (byte[])_dataGridView.CurrentRow.Cells[5].Value;
                image.Image = uploadImage.byteArrayToImage(arraImage);
            }
            catch (Exception)
            {
                image.Image = _imageBitmap;
            }
        }
    }
}
