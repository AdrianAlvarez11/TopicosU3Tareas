using MealOpciones.Models;
using MealOpciones.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MealOpciones.Viewmodels
{

    public enum Filtros { Nombre, Porciones, Tiempo }
    public class MealViewmodel : INotifyPropertyChanged
    {
        private Tipos? tipoSeleccionado = null;
        private Filtros filtroSeleccionado;

        private List<ComidaModel> ListaComidas { get; set; } = new();
        public ObservableCollection<ComidaModel> ListaFiltrada { get; set; } = new();

        public ComidaModel? Comida { get; set; } //quitar el anulable
        public string Error { get; set; } = "";

        public Tipos? TipoSeleccionado
        {
            get => tipoSeleccionado;
            set
            {
                tipoSeleccionado = value;
                Filtrar();
            }
        }

        public Filtros FiltroSeleccionado
        {
            get { return filtroSeleccionado; }
            set
            {
                filtroSeleccionado = value;
                Filtrar();

            }
        }

       
        public IEnumerable<Tipos> ListaTipos => Enum.GetValues(typeof(Tipos)).Cast<Tipos>();
        public IEnumerable<Filtros> ListaFiltros => Enum.GetValues(typeof(Filtros)).Cast<Filtros>();

        public ICommand IniciarCommand { get; set; }
        public ICommand VerAgregarCommand { get; set; }
        public ICommand AgregarCommand { get; set; }

        public ICommand VerDetallesCommand { get; set; }

        public ICommand VerEditarCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand VerEliminarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }

        public MealViewmodel()
        {

            IniciarCommand = new Command(Iniciar);
            VerAgregarCommand = new Command(VerAgregar);
            AgregarCommand = new Command(Agregar);
            VerDetallesCommand = new Command<ComidaModel>(VerDetalles);
            VerEditarCommand = new Command(VerEditar);
            EditarCommand = new Command(Editar);
            VerEliminarCommand = new Command(VerEliminar);
            EliminarCommand = new Command(Eliminar);

            Cargar();
            Filtrar();
        }

        private void Eliminar()
        {
            if(Comida!= null)
            {
                ListaComidas.Remove(Comida);
                Filtrar();
                Guardar();
                Shell.Current.GoToAsync("//Lista");
            }
        }

        private void VerEliminar()
        {
            ActualizarProp(nameof(Comida));
            Shell.Current.GoToAsync("//Eliminar");
        }

        private void Editar()
        {
            if (Comida != null)
            {
                Error = "";
                if (string.IsNullOrWhiteSpace(Comida.Nombre))
                {
                    Error += "Introduzca el nombre de la comida\n";
                }
                if (string.IsNullOrWhiteSpace(Comida.Descripción))
                {
                    Error += "Introduzca el descripcion de la comida\n";
                }
                if (string.IsNullOrWhiteSpace(Comida.Ingredientes))
                {
                    Error += "Introduzca los ingredientes de la comida\n";
                }
                if (string.IsNullOrWhiteSpace(Comida.Pasos))
                {
                    Error += "Introduzca el procedimiento de la comida\n";
                }
                if (string.IsNullOrWhiteSpace(Comida.URLImagen) || !Comida.URLImagen.StartsWith("http") || !Comida.URLImagen.EndsWith("jpg"))
                {
                    Error += "Introduzca una dirección para la comida en formato JPEG\n";
                }

                if (Comida.Porciones < 0 || Comida.Porciones > 250)
                {
                    Error += "Introduzca un número de porciones adecuado\n";
                }

                if (Comida.TiempoPreparacion < 0 || Comida.TiempoPreparacion > 999)
                {
                    Error += "Introduzca un tiempo de preparación adecuado\n";
                }

                ActualizarProp(nameof(Error));

                if (Error == "")
                {
                    ListaComidas[indiceEditar]=Comida;
                    Filtrar();
                    Guardar();
                    Shell.Current.GoToAsync("//Lista");
                }
            }
        }

        int indiceEditar { get; set; }

        private void VerEditar()
        {
            var clon = new ComidaModel
            {
                Nombre= Comida.Nombre,
                Descripción= Comida.Descripción,
                Ingredientes= Comida.Ingredientes,
                Pasos= Comida.Pasos,
                Porciones= Comida.Porciones,
                TiempoPreparacion=Comida.TiempoPreparacion,
                URLImagen= Comida.URLImagen,
                Tipo= Comida.Tipo
            };

            indiceEditar = ListaComidas.IndexOf(Comida);
            Comida = clon;
            Error = "";
            ActualizarProp(nameof(Error));
            ActualizarProp(nameof(Comida));
            Shell.Current.GoToAsync("//Editar");


        }

        private void VerDetalles(ComidaModel c)
        {

            Comida = c;
            ActualizarProp(nameof(Comida));
            Shell.Current.GoToAsync("//Detalles");
            
            
        }

        private void Agregar()
        {
            Error = "";
            if (string.IsNullOrWhiteSpace(Comida.Nombre))
            {
                Error += "Introduzca el nombre de la comida\n";
            }
            if (string.IsNullOrWhiteSpace(Comida.Descripción))
            {
                Error += "Introduzca el descripcion de la comida\n";
            }
            if (string.IsNullOrWhiteSpace(Comida.Ingredientes))
            {
                Error += "Introduzca los ingredientes de la comida\n";
            }
            if (string.IsNullOrWhiteSpace(Comida.Pasos))
            {
                Error += "Introduzca el procedimiento de la comida\n";
            }
            if (string.IsNullOrWhiteSpace(Comida.URLImagen) || !Comida.URLImagen.StartsWith("http") || !Comida.URLImagen.EndsWith("jpg"))
            {
                Error += "Introduzca una dirección para la comida en formato JPEG\n";
            }

            if (Comida.Porciones < 0 || Comida.Porciones > 250)
            {
                Error += "Introduzca un número de porciones adecuado\n";
            }

            if (Comida.TiempoPreparacion < 0 || Comida.TiempoPreparacion > 999)
            {
                Error += "Introduzca un tiempo de preparación adecuado\n";
            }

            ActualizarProp(nameof(Error));

            if (Error == "")
            {
                ListaComidas.Add(Comida);
                Filtrar();
                Guardar();
                Shell.Current.GoToAsync("//Lista");
            }


        }

        private void VerAgregar()
        {
            Comida = new();
            ActualizarProp(nameof(Comida));
            Shell.Current.GoToAsync("//Agregar");
        }

        private void Filtrar()
        {

            ListaFiltrada.Clear();
            List<ComidaModel> listafiltro = new();

            if (tipoSeleccionado != null)
            {

                if (filtroSeleccionado == Filtros.Nombre)
                {
                    listafiltro = ListaComidas.Where(x => x.Tipo == tipoSeleccionado).OrderBy(x => x.Nombre).ToList();
                }

                if (filtroSeleccionado == Filtros.Porciones)
                {
                    listafiltro = ListaComidas.Where(x => x.Tipo == tipoSeleccionado).OrderBy(x => x.Porciones).ToList();
                }

                if (filtroSeleccionado == Filtros.Tiempo)
                {
                    listafiltro = ListaComidas.Where(x => x.Tipo == tipoSeleccionado).OrderBy(x => x.TiempoPreparacion).ToList();
                }

            }
            else
            {
                if (filtroSeleccionado == Filtros.Nombre)
                {
                    listafiltro = ListaComidas.OrderBy(x => x.Nombre).ToList();
                }

                if (filtroSeleccionado == Filtros.Porciones)
                {
                    listafiltro = ListaComidas.OrderBy(x => x.Porciones).ToList();
                }

                if (filtroSeleccionado == Filtros.Tiempo)
                {
                    listafiltro = ListaComidas.OrderBy(x => x.TiempoPreparacion).ToList();
                }
            }

            if (listafiltro.Count > 0)
            {
                foreach (var comida in listafiltro)
                {
                    ListaFiltrada.Add(comida);
                }

                ActualizarProp(nameof(ListaFiltrada));
            }
            

            
            
        }


        private void Iniciar()
        {
            Error = "";
            Comida=null;
            ActualizarProp(nameof(Comida));
            Shell.Current.GoToAsync("//Lista");
        }

        private void Guardar()
        {
            var ruta = Path.Combine(FileSystem.AppDataDirectory,"comidas.json");
            File.WriteAllText(ruta, JsonSerializer.Serialize(ListaComidas));
        }

        private void Cargar()
        {
            var ruta = FileSystem.AppDataDirectory;
            if(File.Exists(ruta+"/comidas.json"))
            {
                var lista = JsonSerializer.Deserialize<List<ComidaModel>>(File.ReadAllText(ruta+"/comidas.json"));
                if(lista != null)
                {
                    ListaComidas.AddRange(lista);
                }
            }
        }

        private void ActualizarProp(string nombre)
        {
            PropertyChanged?.Invoke(this, new(nameof(nombre)));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
