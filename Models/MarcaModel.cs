﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class Marca
    {
        #region Attributes

        private int idMarca;
        private string nome;

        #endregion

        #region Methods

        #region Constructor

        /// <summary>
        /// Construtor de uma Marca ao receber parâmetros
        /// </summary>
        /// <param name="n"></param>
        public Marca(int id, string n)
        {
            idMarca = id;
            nome = n;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Propriedade para manipular o id da marca
        /// </summary>
        public int IdMarca
        {
            get { return idMarca; }
            set { idMarca = value; }
        }

        /// <summary>
        /// Propriedade para manipular o nome da marca
        /// </summary>
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        #endregion

        #region Other Methods
        #endregion

        #region Destructor

        /// <summary>
        /// Destrutor por defeito
        /// </summary>
        ~Marca()
        {
        }
        #endregion

        #endregion
    }
}
