using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class TimelineViewModel
    {
        public UsuarioViewModel Usuario { get; set; }
        public virtual ICollection<FotoViewModel> FotosAmigos { get; set; }

        public virtual ICollection<UsuarioViewModel> Seguidores { get; set; }
        public virtual ICollection<UsuarioViewModel> Seguindo { get; set; }
    }
}