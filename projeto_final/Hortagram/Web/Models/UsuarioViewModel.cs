using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class UsuarioViewModel
    {
        public UsuarioViewModel()
        {
            this.FOTO = new HashSet<FotoViewModel>();
            this.FOTO1 = new HashSet<FotoViewModel>();
            this.USUARIO1 = new HashSet<UsuarioViewModel>();
            this.USUARIO2 = new HashSet<UsuarioViewModel>();
        }

        public int USUARIO_ID { get; set; }
        public string NOME { get; set; }
        public string SOBRENOME { get; set; }
        public string FOTO_URL { get; set; }
        public string EMAIL { get; set; }
        public string TELEFONE { get; set; }
        public string ANIVERSARIO { get; set; }


        public virtual ICollection<FotoViewModel> FOTO { get; set; }

        public virtual ICollection<FotoViewModel> FOTO1 { get; set; }

        public virtual ICollection<UsuarioViewModel> USUARIO1 { get; set; }

        public virtual ICollection<UsuarioViewModel> USUARIO2 { get; set; }
    }
}