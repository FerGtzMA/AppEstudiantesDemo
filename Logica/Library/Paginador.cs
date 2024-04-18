using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logica.Library
{
    // Clase generica
    public class Paginador<T>
    {
        private List<T> _dataList;
        private Label _label;
        private static int maxReg, _reg_por_pagina, pageCount, numPage = 1;

        public Paginador(List<T> dataList, Label label, int reg_por_pagina) 
        {
            _dataList = dataList;
            _label = label;
            _reg_por_pagina = reg_por_pagina;
            cargarDatos();
        }

        private void cargarDatos()
        {
            numPage = 1;
            maxReg = _dataList.Count;
            pageCount = (maxReg / _reg_por_pagina);

            // Ajuste el numero de la pagina si la ultima pagina contiene una parte de la pagina
            if ((maxReg % _reg_por_pagina) > 0)
            {
                pageCount += 1;
            }
            _label.Text = $"Pagina 1/{pageCount}";
        }

        public int primero()
        {
            numPage = 1;
            _label.Text = $"Paginas {numPage}/{pageCount}";
            return numPage;
        }

        public int anteriro()
        {
            if (numPage > 1)
            {
                numPage -= 1;
                _label.Text = $"Paginas {numPage}/{pageCount}";
            }
            return numPage;
        }

        public int siguiente()
        {
            if (numPage == pageCount)
                numPage -= 1;
            if (numPage < pageCount)
            {
                numPage += 1;
                _label.Text = $"Paginas {numPage}/{pageCount}";
            }
            return numPage;
        }

        public int ultimo()
        {
            numPage = pageCount;
            _label.Text = $"Paginas {numPage}/{pageCount}";
            return numPage;
        }
    }
}
