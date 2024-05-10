using MealOpciones.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealOpciones.Viewmodels
{
    public class MealViewmodel : INotifyPropertyChanged
    {
        public ObservableCollection<ComidaModel> ComidasPrueba { get; set; } = new();

        ComidaModel comida1 = new ComidaModel
        {
            Nombre = "Huevos revueltos",
            Descripción = "Una deliciosa opción para el desayuno",
            Ingredientes = "Huevos, sal, pimienta",
            TiempoPreparacion = 10,
            Porciones = 2,
            Pasos = "Batir los huevos con sal y pimienta, cocinar a fuego medio, revolviendo constantemente hasta que estén cocidos",
            Tipo = Tipos.Desayuno
        };

        ComidaModel comida2 = new ComidaModel
        {
            Nombre = "Ensalada César",
            Descripción = "Una ensalada fresca y saludable para el almuerzo",
            Ingredientes = "Lechuga, pollo a la parrilla, aderezo César",
            TiempoPreparacion = 15,
            Porciones = 4,
            Pasos = "Cortar la lechuga, añadir el pollo a la parrilla cortado en trozos, mezclar con el aderezo César",
            Tipo = Tipos.Comida
        };

        ComidaModel comida3 = new ComidaModel
        {
            Nombre = "Salmón a la parrilla",
            Descripción = "Un plato principal saludable y delicioso para la cena",
            Ingredientes = "Filete de salmón, limón, sal, pimienta",
            TiempoPreparacion = 20,
            Porciones = 2,
            Pasos = "Sazonar el salmón con sal y pimienta, exprimir limón, asar a la parrilla hasta que esté cocido",
            Tipo = Tipos.Cena
        };

        public MealViewmodel()
        {
            ComidasPrueba.Add(comida1);
            ComidasPrueba.Add(comida2);
            ComidasPrueba.Add(comida2);
            ComidasPrueba.Add(comida2);
            ComidasPrueba.Add(comida2); 
            ComidasPrueba.Add(comida3);

        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
