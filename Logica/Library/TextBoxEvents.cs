using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logica.Library
{
    public class TextBoxEvents
    {
        public void textKeyPress(KeyPressEventArgs e)
        {
            // COndicion que solo permite ingresar datos de tipo text
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            // Condicion que no permite dar salto de linea cuando se oprime enter
            else if(e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
            }
            // condicion que nos permite utulizar la tecla de backspace
            else if(char.IsControl(e.KeyChar)) 
            {
                e.Handled = false;
            }
            // Condicions que nos permite utilizar la tecla space
            else if(char.IsSeparator(e.KeyChar)) 
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public void numberKeyPress(KeyPressEventArgs e)
        {
            // COndicion que solo permite ingresar datos de ese tipo
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            // Condicion que no permite dar salto de linea cuando se oprime enter
            else if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
            }
            // Condicion que no permite ingresar datos de tipo texto
            else if(char.IsLetter(e.KeyChar))
            {
                e.Handled= true;
            }
            // condicion que nos permite utulizar la tecla de backspace
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            // Condicions que nos permite utilizar la tecla space
            else if (char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public bool comprobarFormatEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }
    }
}
