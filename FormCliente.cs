﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TP_Plataformas_de_Desarrollo
{
    public partial class FormCliente : Form
    {
        private Mercado merc;
        private int ID;
        public delegate void TransfDelegado2(); // Metodo
        public TransfDelegado2 TrasfEvento;

        public FormCliente(int ID, string nombre, Object m)
        {

            InitializeComponent();
            this.ID = ID;
            label2.Text = nombre;
            merc = (Mercado)m;
            refreshData(merc);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            tabControl1.SelectedIndexChanged += new EventHandler(ocultarMostrar);
            button15.Hide();
            button14.Hide();
            button2.Hide();

            //CREAMOS EVENTOS EN LAS TABLAS PARA DAR MAS ACCIONES
            dataGridView1.CellClick += dataGridView1_CellClick; //EVENTO TABLA PRODUCTOS
            dataGridView5.CellClick += dataGridView5_CellClick; //EVENTO TABLA CARRO DEL USUARIO

            //EVENTO PARA LOS BOTONES DE LA LISTA DE CATEGORIAS EN LA PESTAÑA DE PRODUCTOS
            dataGridView6.CellClick += dataGridView6_CellClick;
        }
        //######################################################
        //           ACTUALIZAR DATOS DE LAS TABLAS
        //######################################################
        private void refreshData(Mercado data)
        {
            //borro los datos
            dataGridView1.Rows.Clear(); //LIMPIAMOS TABLA PRODUCTOS
            dataGridView5.Rows.Clear(); //LIMPIAMOS TABLA MI CARRO
            dataGridView6.Rows.Clear(); //LIMPIAMOS TABLA CATEGORIAS DE PRODUCTOS
            
            // LLENAMOS LISTADO DE CATEGORIAS DE LA PESTAÑA DE PRODUCTOS
            foreach (Categoria c in data.nContexto.categorias)
            {
                if (c != null)
                {
                    dataGridView6.Rows.Add(c.nombre);
                }
            }

            // LLENAMOS LISTADO DE PRODUCTOS
            foreach (Producto p in data.nContexto.productos)
            {
                if (p != null)
                {
                    string[] pro = { p.idProducto.ToString(),
                                     p.nombre,
                                     p.precio.ToString(),
                                     p.cantidad.ToString(),
                                     p.cat.nombre };
                    dataGridView1.Rows.Add(pro);
                }
            }

            // LLENAMOS LISTADO DEL CARRO DEL USUARIO
            foreach (Carro c in data.nContexto.carros)
            {
                if (c.usuario.idUsuario == ID) 
                {
                   
                    if (c.carroProducto != null)
                    {
                        foreach (CarroProducto cp in c.carroProducto)
                        {
                            double total = 0;
                            total += cp.producto.precio * cp.cantidad;
                            string[] carroproducto = { cp.producto.idProducto.ToString(),
                                                       cp.producto.nombre,
                                                       cp.producto.precio.ToString(),
                                                       cp.cantidad.ToString(),
                                                       total.ToString()
                                                     };
                             dataGridView5.Rows.Add(carroproducto);
                        }
                    }
                }
            }
            if (dataGridView5.Columns["botonBorrarDelCarro"] == null)
            {
                DataGridViewButtonColumn borrarDelCarro = new DataGridViewButtonColumn();
                borrarDelCarro.HeaderText = "Borrar";
                borrarDelCarro.Text = "Borrar";
                borrarDelCarro.Name = "botonBorrarDelCarro";
                borrarDelCarro.UseColumnTextForButtonValue = true;
                dataGridView5.Columns.Add(borrarDelCarro);
            }
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //                                       PESTAÑA PRODUCTOS
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@


        //######################################################
        //                      MOSTRAR 
        //######################################################
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = int.Parse(dataGridView1[0, e.RowIndex].Value.ToString());
            Producto p = merc.nContexto.productos.Where(p => p.idProducto == indice ).First();
            

            label20.Text = p.idProducto.ToString();
            label7.Text = p.nombre;
            label8.Text = p.precio.ToString();
            label9.Text = p.cantidad.ToString();
            label10.Text = p.idCategoria.ToString();
            numericUpDown1.Maximum = p.cantidad;
            button2.Show();
            tabControl2.SelectedTab = MostrarProducto;

        }

        //######################################################
        //             AGREGAR PRODUCTO AL CARRO
        //######################################################
        private void button5_Click(object sender, EventArgs e)
        {
            if (merc.AgregarAlCarro(int.Parse(label20.Text), int.Parse(numericUpDown1.Value.ToString()), ID))
            {
                label20.Text = "";
                label7.Text = "";
                label8.Text = "";
                label9.Text = "";
                label10.Text = "";
                numericUpDown1.Value = 1;
                refreshData(merc);
                tabControl2.SelectedTab = ListaProductos;
            }
            else
            {
                MessageBox.Show("ERROR: el Producto no se pudo agregar al Carro.");
            }
        }

        //######################################################
        //              SELECCIONAR CATEGORIA 
        //######################################################

        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button1.Text = "Restablecer datos";
            dataGridView1.Rows.Clear(); //LIMPIAMOS TABLA PRODUCTOS
            foreach (Producto p in merc.BuscarProductoPorCategoria(dataGridView6[0, e.RowIndex].Value.ToString()))
            {
                if (p != null)
                {
                    string[] prods = { p.idProducto.ToString(),
                                       p.nombre,
                                       p.precio.ToString(),
                                       p.cantidad.ToString(),
                                       p.idCategoria.ToString() };
                    dataGridView1.Rows.Add(prods);
                }
            }
        }

        //######################################################
        //             OCULTAR MOSTRAR
        //######################################################
        private void ocultarMostrar(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                comboBox1.Show();
                comboBox2.Show();
                label46.Show();
                button13.Show();
                textBox34.Show();
                button15.Hide();
                button14.Hide();
            }
            else
            {
                comboBox1.Hide();
                comboBox2.Hide();
                label46.Hide();
                button13.Hide();
                textBox34.Hide();
                button15.Show();
                button14.Show();
            }
        }

        //######################################################
        //             BOTON BUSCAR PRODUCTO
        //######################################################
        private void button13_Click(object sender, EventArgs e)
        {
            if (textBox34.Text != "")
            {
                //Se intenta parsear el texto, si lo logra, busca Producto por precio.
                if (int.TryParse(textBox34.Text, out int result))
                {
                    button1.Text = "Restablecer datos";
                    dataGridView1.Rows.Clear(); //LIMPIAMOS TABLA PRODUCTOS

                    if (merc.BuscarProductoPorPrecio(textBox34.Text).Count == 0)
                    {
                        MessageBox.Show("No existe el producto: " + textBox34.Text);
                        refreshData(merc);
                        button1.Text = "Actualizar Datos";
                    }
                    else
                    {
                        foreach (Producto p in merc.BuscarProductoPorPrecio(textBox34.Text))
                        {
                            if (p != null)
                            {
                                string[] prods = { p.idProducto.ToString(),
                                                   p.nombre,
                                                   p.precio.ToString(),
                                                   p.cantidad.ToString(),
                                                   p.idCategoria.ToString() };
                                dataGridView1.Rows.Add(prods);
                            }
                        }
                    }
                }
                //Busca producto por Nombre
                else
                {
                    button1.Text = "Restablecer datos";
                    dataGridView1.Rows.Clear(); //LIMPIAMOS TABLA PRODUCTOS

                    if (merc.BuscarProducto(textBox34.Text, 1).Count == 0)
                    {
                        MessageBox.Show("No existe el producto: " + textBox34.Text);
                        refreshData(merc);
                        button1.Text = "Actualizar Datos";
                    }
                    else
                    {
                        foreach (Producto p in merc.BuscarProducto(textBox34.Text,1))
                        {
                            if (p != null)
                            {
                                string[] prods = { p.idProducto.ToString(),
                                                   p.nombre,
                                                   p.precio.ToString(),
                                                   p.cantidad.ToString(),
                                                   p.idCategoria.ToString() };
                                dataGridView1.Rows.Add(prods);
                            }
                        }
                    }
                }
            }
        }
        //######################################################
        //                COMBO BOX DE ORDEN
        //   COMBO BOX 1 -> ORDEN POR NOMBRE, CATEGORIA O PRECIO
        //   COMBO BOX 2 -> ORDEN ASCENDENTE O DESCENDENTE
        //######################################################
        private void OrdenNPC(int cambio) //Se repite en ambos eventos COMBOBOX entonces hago una sola funcion
        {
            if (comboBox1.Text == "Nombre")
            {

                dataGridView1.Rows.Clear(); //LIMPIAMOS TABLA PRODUCTOS
                foreach (Producto p in merc.BuscarProducto("", cambio))
                {
                    if (p != null)
                    {
                        string[] prods = { p.idProducto.ToString(),
                                           p.nombre,
                                           p.precio.ToString(),
                                           p.cantidad.ToString(),
                                           p.idCategoria.ToString() };
                        dataGridView1.Rows.Add(prods);
                    }
                }
            }
            else if (comboBox1.Text == "Precio")
            {

                dataGridView1.Rows.Clear(); //LIMPIAMOS TABLA PRODUCTOS
                foreach (Producto p in merc.MostrarTodosProductosPorPrecio(cambio))
                {
                    if (p != null)
                    {
                        string[] prods = { p.idProducto.ToString(),
                                           p.nombre,
                                           p.precio.ToString(),
                                           p.cantidad.ToString(),
                                           p.idCategoria.ToString() };
                        dataGridView1.Rows.Add(prods);
                    }
                }
            }
            else if (comboBox1.Text == "Categoria")
            {

                dataGridView1.Rows.Clear(); //LIMPIAMOS TABLA PRODUCTOS
                foreach (Producto p in merc.MostrarTodosProductosPorCategoria(cambio))
                {
                    if (p != null)
                    {
                        string[] prods = { p.idProducto.ToString(),
                                           p.nombre,
                                           p.precio.ToString(),
                                           p.cantidad.ToString(),
                                           p.idCategoria.ToString() };
                        dataGridView1.Rows.Add(prods);
                    }
                }
            }
        }

        int cambio = 0; // Variable que arregla error del REVERSE, si COMBOBOX es DESC (1), no vuelve a ejecutar el REVERSE 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 1)
            {
                cambio = 0;
                OrdenNPC(cambio);
            }
            else
            {
                cambio = 1;
                OrdenNPC(cambio);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 1)
            {
                cambio = 0;
                OrdenNPC(cambio);
            }
            else
            {
                cambio = 1;
                OrdenNPC(cambio);
            }
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //                                       PESTAÑA MI CARRO
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == dataGridView5.Columns["botonBorrarDelCarro"].Index)
            {
                // ELIMINAR PRODUCTO DEL CARRO
                DialogResult resutl = MessageBox.Show("¿Seguro que desea eliminar el producto de tu Carro?", "", MessageBoxButtons.YesNo);
                if (resutl == DialogResult.Yes)
                {
                    int idprod = int.Parse(dataGridView5[0, e.RowIndex].Value.ToString());

                    merc.ModificarCarro(idprod, 0, ID);
                    dataGridView5.Rows.RemoveAt(e.RowIndex);
                }
            }
            else
            {
                //MODIFICAR CANTIDAD DE PRODUCTO DEL CARRO
                textBox31.Text = dataGridView5[0, e.RowIndex].Value.ToString();
                textBox32.Text = dataGridView5[2, e.RowIndex].Value.ToString();
                textBox33.Text = dataGridView5[3, e.RowIndex].Value.ToString();
                button2.Show();
                tabControl6.SelectedTab = ModificarCarro;
            }
        }
        //######################################################
        //             MODIFICAR CARRO
        //######################################################
        private void button12_Click(object sender, EventArgs e)
        {

            try
            {
                if (merc.nContexto.carros.Where(c => c.idUsuario == ID).FirstOrDefault() != null)
                {
                    if (merc.ModificarCarro(int.Parse(textBox31.Text), int.Parse(textBox33.Text), ID))
                    {
                        textBox31.Text = "";
                        textBox32.Text = "";
                        textBox33.Text = "";
                        refreshData(merc);
                        tabControl6.SelectedTab = ListaCarro;
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("ERROR al ingresar los datos");
            }

        }
        //######################################################
        //             VACIAR CARRO
        //######################################################
        private void button15_Click(object sender, EventArgs e)
        {
            if (dataGridView5.Rows.Count == 0)
            {
                MessageBox.Show("No tienes productos que vaciar");
            }
            else if (merc.VaciarCarro(ID))
            {
                refreshData(merc);
            }

        }
        //######################################################
        //             COMPRAR
        //######################################################
        private void button14_Click(object sender, EventArgs e)
        {
            if (dataGridView5.Rows.Count == 0)
            {
                MessageBox.Show("Debes agregar productos al carro");
            }
            else if (merc.Comprar(ID))
            {
                refreshData(merc);
            }

        }

        //######################################################
        //             BOTON ACTUALIZAR DATOS
        //######################################################
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "Actualizar Datos";
            textBox34.Text = "";
            refreshData(merc); //RECARGA LAS LISTAS
        }



        //######################################################
        //             BOTON EXIT
        //######################################################
        private void iconButton2_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Seguro que deseas salir?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                
                this.TrasfEvento();
                this.Close();
            }
        }

        //######################################################
        //             BOTON VOLVER
        //######################################################

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Hide();
            if (tabControl1.SelectedTab.Text == "Productos" && tabControl2.SelectedTab == MostrarProducto)
            {
                tabControl2.SelectedTab = ListaProductos;
            }
            else if (tabControl1.SelectedTab.Text == "Mi Carro" && tabControl6.SelectedTab == ModificarCarro)
            {
                tabControl6.SelectedTab = ListaCarro;
            }

        }
    }
}
