using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealOpciones.Models
{
    public enum Tipos { Desayuno, Comida, Cena, Snack }
    public class ComidaModel
    {
        public string Nombre { get; set; } = null!;
        public string Descripción { get; set; } = null!;
        public string Ingredientes { get; set; } = null!;
        public int TiempoPreparacion { get; set; }
        public int Porciones { get; set; }
        public string Pasos { get; set; }=null!;
        public Tipos Tipo { get; set; }
    }
}
